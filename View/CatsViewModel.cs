using System;
using System.ComponentModel;
using System.Collections.Generic;

using cat.Model;

namespace cat.View {
    public class CatsViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = null;
        /// <summary>
        /// Cat Mst Edit Data Field
        /// </summary>
        private CatDisplay EditData;
        /// <summary>
        /// Cat Mst List Data Field
        /// </summary>
        private List<CatDisplay> catList;

        /// <summary>
        /// Entry Field CatId I/F
        /// </summary>
        public int? CatId { 
            get { return EditData.CatId; }
            set {
                /// CatId <see cref="Model.Cat.CatId"/>
                EditData.CatId = value == 0 ? null : value ;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatId));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string CatName {
            get { return EditData.CatName; }
            set {
                EditData.CatName = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatName));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string HairPattern {
            get { return EditData.HairPattern; }
            set {
                EditData.CatName = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(HairPattern));
            }
        }
        /// <summary>
        /// Entry Field Gender I/F
        /// </summary>
        public CatGenderType Gender {
            get { return EditData.Gender; }
            set {
                EditData.Gender = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// Entry Field BodyType I/F
        /// </summary>
        public CatBodyType BodyType {
            get { return EditData.BodyType; }
            set {
                EditData.BodyType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(BodyType));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string FaceType {
            get { return EditData.FaceType; }
            set {
                EditData.FaceType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(FaceType));
            }
        }
        /// <summary>
        /// Entry Field Age I/F
        /// </summary>
        public CatAge Age {
            get { return EditData.Age; }
            set {
                EditData.Age = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Age));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string Personality {
            get { return EditData.Personality; }
            set {
                EditData.Personality = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Personality));
            }
        }
        /// <summary>
        /// Entry Field Age I/F
        /// </summary>
        public bool LostFlg {
            get { return !string.IsNullOrEmpty(EditData.LostFlag); }
            set {
                EditData.LostFlag = value ? "1" : null;
                //Post Change Event  
                this.OnPropertyChanged(nameof(LostFlg));
            }
        }
        /// <summary>
        /// Editing Field Datas I/F
        /// </summary>
        public CatDisplay CatEdit { 
            get { return EditData; }
            set {
                this.EditData = value.Clone();
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatId));
                this.OnPropertyChanged(nameof(CatName));
                this.OnPropertyChanged(nameof(HairPattern));
                this.OnPropertyChanged(nameof(FaceType));
                this.OnPropertyChanged(nameof(Personality));
                this.OnPropertyChanged(nameof(LostFlg));
                this.OnPropertyChanged(nameof(Gender));
                this.OnPropertyChanged(nameof(BodyType));
                this.OnPropertyChanged(nameof(Age));
            }
        }
        /// <summary>
        /// Grid Data Items　I/F
        /// </summary>
        public List<CatDisplay> Cats {
            get {
                return catList;
            }
            set {
                this.catList = new List<CatDisplay>(value);

                //Post Change Event 
                this.OnPropertyChanged(nameof(Cats));
            }
        }
        /// <summary>
        /// All clear Editing Fields 
        /// </summary>
        public void ClearEdit() {
            // TODO: TBD Entry Default.
            this.CatEdit = new CatDisplay { Gender = CatGenderType.Unknown, BodyType = CatBodyType.Small, Age = CatAge.Unknown };
        }

        public CatsViewModel() {
            // TODO: Delete Sample Code

            // Sample Data for View 
            List<CatDisplay> data = new List<CatDisplay>();
            data.Add(new CatDisplay { CatId = 1, CatName = "Atari", HairPattern = "Ginger", Gender = CatGenderType.Male, BodyType = CatBodyType.Midium, FaceType = "Baby Face, Big ear", Age = CatAge.Young, Personality = "Talkative and sensitive", LostFlag = null });
            data.Add(new CatDisplay { CatId = 2, CatName = "Atari2", HairPattern = "Ginger2", Gender = CatGenderType.Female, BodyType = CatBodyType.Small, FaceType = "2Baby Face, Big ear", Age = CatAge.Baby, Personality = "1Talkative and sensitive", LostFlag = "1" });
            data.Add(new CatDisplay { CatId = 3, CatName = "Atari3", HairPattern = "Ginger3", Gender = CatGenderType.Unknown, BodyType = CatBodyType.Large, FaceType = "3Baby Face, Big ear", Age = CatAge.Adult, Personality = "2Talkative and sensitive", LostFlag = null });
            data.Add(new CatDisplay { CatId = 4, CatName = "Atari4", HairPattern = "Ginger4", Gender = CatGenderType.Male, BodyType = CatBodyType.Huge, FaceType = "4Baby Face, Big ear", Age = CatAge.Old, Personality = "3Talkative and sensitive", LostFlag = "1" });
            Cats = data;
            // End Sample Data for View 

            // Data Init
            ClearEdit();
            // TODO: List Init.
        }
        protected void OnPropertyChanged(string info) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}
