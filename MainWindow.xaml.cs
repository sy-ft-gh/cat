using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;

using cat.View;
using cat.DB;
using cat.Model;

namespace cat {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// View Model
        /// </summary>
        private CatsViewModel vm = new CatsViewModel();
 
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            this.DataContext = vm;
            this.GetCats();
        }

        /// <summary>
        /// Show Cat Master Data to Editing 
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void dgCatMaster_CurrentCellChanged(object sender, EventArgs e) {
            int RowCnt = dgCatMaster.Items.IndexOf(dgCatMaster.CurrentItem); 
            if (RowCnt >= 0 && vm.CatEdit.CatId != vm.Cats[RowCnt].CatId) {
                vm.CatEdit = vm.Cats[RowCnt];
            }
        }

        /// <summary>
        /// Clear Entry
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnErase_Click(object sender, RoutedEventArgs e) {
            vm.ClearEdit();
        }

        /// <summary>
        /// Setup Copy Entry
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnCopy_Click(object sender, RoutedEventArgs e) {
            vm.CopyEdit();
        }

        /// <summary>
        /// Regist or Update Cat Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnApply_Click(object sender, RoutedEventArgs e) {
            // 0.PreProcessing(Validattion)
            Cat cat = null;
            if ((cat = vm.ValidateEntry()) == null) return;

            /// <see cref="Model.Cat.CatId"/>
            // 1.Start Trans
            using (var context = new CatMasterContext())
            using (var dbTransaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {
                string ErrorMessage = null;
                try {
                    // 2.Reg or Upd Jdg (CatId > 0 -> Update else Reg)
                    // 3.Execute
                    if (!(cat.CatId > 0)) {
                        // 3.1 Regist
                        // Insert Record
                        Cat ins = new Cat();
                        ins.CopyFrom(cat);
                        ins.UpdateDate = context.GetDBDate();
                        ins.RegistDate = ins.UpdateDate;
                        context.Cats.Add(ins);
                        context.SaveChanges();
                    } else {
                        // 3.2 Update
                        // Select And Lock Registed Data
                        var RegistedData = context.Database.SqlQuery<Cat>("SELECT * FROM CATS WITH (UPDLOCK) WHERE CATID = " + cat.CatId.ToString()).ToList();
                        // 3.2.1 Is Not Data
                        if (RegistedData.Count != 1) {
                            // Exit for Show deleted error
                            ErrorMessage = "This Data is Not Exists";
                        } else {
                            // 3.2.2 Is Data
                            var upd = context.Cats.Single(x => x.CatId == cat.CatId);
                            // Check Update Time Is Modified
                            if (cat.UpdateDate > RegistedData.First().UpdateDate) {
                                // Is Modified -> Over Write Confirm
                                if (MessageBox.Show("Is Already UPDATED. Wanna Over Write??", "Cat Master", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;
                            }
                            // Is Not Modified or Over Write -> Update Data
                            upd.CopyFrom(cat);
                            upd.UpdateDate = context.GetDBDate();
                            context.SaveChanges();
                        }
                    }
                } catch (Exception ex) {
                    ErrorMessage = "Error Rized:" + ex.Message;
                } finally {
                    // 4-5.End Trans & Post Process
                    // Succeed -> Commit, Reload & Init UI
                    // Failue -> Rollback, Show Error Message
                    if (string.IsNullOrEmpty(ErrorMessage)) {
                        dbTransaction.Commit();
                        this.GetCats();
                    } else {
                        dbTransaction.Rollback();
                        MessageBox.Show(ErrorMessage, "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Delete Cat Mst Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            // TODO: Make Code(Delete Data)

            Cat cat = null;
            // 0.PreProcessing(Validattion)
            // Is there Selected ?
            if (!(vm.CatId > 0) || (cat = vm.ValidateEntry()) == null) {
                MessageBox.Show("Please Select A Record", "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 1.Start Trans
            using (var context = new CatMasterContext())
            using (var dbTransaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {
                string ErrorMessage = null;
                try {
                    // 2.Execute
                    // 2.1 Delete Check
                    // Select And Lock Registed Data
                    var RegistedData = context.Database.SqlQuery<Cat>("SELECT * FROM CATS WITH (UPDLOCK) WHERE CATID = " + vm.CatId.ToString()).ToList();

                    // 2.2.1 Is Not Data
                    // Exit for Show deleted error
                    if (RegistedData.Count == 0) {
                        ErrorMessage = "Selected Data Is Deleted.";
                    } else {
                        // 2.2.2 Is Data
                        var tgt = context.Cats.Single(x => x.CatId == vm.CatId);
                        // Check Update Time Is Modified
                        if (cat.UpdateDate < tgt.UpdateDate) {
                            // Is Modified -> Confirm Delete (Continue or stop)
                            if (MessageBox.Show("Is Already UPDATED. Wanna Force Delete??", "Cat Master", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;
                        }
                        // Is Not Modified or Force Delete
                        context.Cats.Remove(tgt);
                        context.SaveChanges();
                    }
                } catch (Exception ex) {
                    ErrorMessage = "Error Rized:" + ex.Message;
                } finally {
                    // 3-4.End Trans & Post Process
                    // Succeed -> Commit, Reload & Init UI
                    // Failue -> Rollback, Show Error Message
                    if (string.IsNullOrEmpty(ErrorMessage)) {
                        dbTransaction.Commit();
                        this.GetCats();
                    } else {
                        dbTransaction.Rollback();
                        MessageBox.Show(ErrorMessage, "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

        /// <summary>
        /// Get Cat Master List
        /// </summary>
        private void GetCats() {
            List<CatDisplay> dspList = new List<CatDisplay>();
            using (var context = new CatMasterContext()) {
                foreach (var cat in context.Cats.OrderBy(x => x.CatId)) {
                    CatDisplay dsp = new CatDisplay();
                    dsp.CopyFrom(cat);
                    dspList.Add(dsp);
                }
            }
            vm.Cats = dspList; 
        }
    }
}
