using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using cat.View;

namespace cat {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private CatsViewModel vm = new CatsViewModel();
        public MainWindow() {
            InitializeComponent();

            this.DataContext = vm;
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

        // TODO: Add Erase Editing Function

        /// <summary>
        /// Setup Copy Entry
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnCopy_Click(object sender, RoutedEventArgs e) {
            /// Clear ViewData's CatId <see cref="Model.Cat.CatId"/>
            vm.CatId = null;
        }

        /// <summary>
        /// Regist or Update Cat Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnApply_Click(object sender, RoutedEventArgs e) {
            // TODO: Make Code(Regist Update Data )

            // 0.PreProcessing(Validattion)

            // 1.Start Trans

            // 2.Reg or Upd Jdg (CatId > 0 -> Update else Reg)
            /// <see cref="Model.Cat.CatId"/>

            // 3.Execute

            // 3.1 Regist
            // Insert Record

            // 3.2 Update
            // Select And Lock Registed Data

            // 3.2.1 Is Not Data
            // Exit for Show deleted error

            // 3.2.2 Is Data
            // Check Update Time Is Modified
            // Is Modified -> Prc End

            // Is Not Modified -> Update Data

            // 4.End Trans
            // Succeed -> Commit, Failue -> Rollback

            // 6.Post Process
            // Succeed -> Reload & Init UI, Failue -> Show Error Message
        }

        /// <summary>
        /// Delete Cat Mst Data
        /// </summary>
        /// <param name="sender">Cause Object</param>
        /// <param name="e">Event Object</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            // TODO: Make Code(Delete Data)

            // 0.PreProcessing(Validattion)
            // Is there Selected ?

            // 1.Start Trans

            // 2.Execute

            // 2.1 Delete Check
            // Select And Lock Registed Data

            // 2.2.1 Is Not Data
            // Exit for Show deleted error

            // 3.2.2 Is Data
            // Check Update Time Is Modified
            // Is Modified -> Confirm Delete (Continue or stop)

            // Is Not Modified or Force Delete

            // 4.End Trans
            // Succeed -> Commit, Failue -> Rollback

            // 5.Post Process
            // Succeed -> Reload & Init UI, Failue -> Show Error Message

        }
        // TODO: Get Cats Master List Data Function

    }
}
