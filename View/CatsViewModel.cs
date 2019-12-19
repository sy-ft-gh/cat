using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using cat.Model;

namespace cat.View {
    public class CatsViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = null;
        /// <summary>
        /// Cat Mst Edit Data Field
        /// </summary>
        private CatDisplay editData;
        /// <summary>
        /// Cat Mst List Data Field
        /// </summary>
        private List<CatDisplay> catList;

        /// <summary>
        /// Get Propaty's Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="propertyName">Property Name</param>
        /// <returns>Custom Attribute's</returns>
        public static T GetPropertyAttribute<T>(Type clsType, string propertyName) where T : Attribute {
            var attrType = typeof(T);
            var property = clsType.GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
        /// <summary>
        /// Entry Field CatId I/F
        /// </summary>
        public int? CatId { 
            get { return editData.CatId; }
            set {
                /// CatId <see cref="Model.Cat.CatId"/>
                editData.CatId = value == 0 ? null : value ;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatId));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string CatName {
            get { return editData.CatName; }
            set {
                editData.CatName = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(CatName));
            }
        }
        /// <summary>
        /// Entry Field CatName I/F
        /// </summary>
        public string HairPattern {
            get { return editData.HairPattern; }
            set {
                editData.HairPattern = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(HairPattern));
            }
        }
        /// <summary>
        /// Entry Field Gender I/F
        /// </summary>
        public CatGenderType Gender {
            get { return editData.Gender; }
            set {
                editData.Gender = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// Entry Field BodyType I/F
        /// </summary>
        public CatBodyType BodyType {
            get { return editData.BodyType; }
            set {
                editData.BodyType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(BodyType));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string FaceType {
            get { return editData.FaceType; }
            set {
                editData.FaceType = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(FaceType));
            }
        }
        /// <summary>
        /// Entry Field Age I/F
        /// </summary>
        public CatAge Age {
            get { return editData.Age; }
            set {
                editData.Age = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Age));
            }
        }
        /// <summary>
        /// Entry Field FaceType I/F
        /// </summary>
        public string Personality {
            get { return editData.Personality; }
            set {
                editData.Personality = value;
                //Post Change Event  
                this.OnPropertyChanged(nameof(Personality));
            }
        }
        /// <summary>
        /// Entry Field Age I/F
        /// </summary>
        public bool LostFlg {
            get { return string.IsNullOrEmpty(editData.LostFlag); }
            set {
                editData.LostFlag = value ? "1" : null;
                //Post Change Event  
                this.OnPropertyChanged(nameof(LostFlg));
            }
        }
        /// <summary>
        /// Editing Field Datas I/F
        /// </summary>
        public CatDisplay CatEdit { 
            get { return editData; }
            set {
                this.editData = value.Clone();
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
            this.CatEdit = new CatDisplay { Gender = CatGenderType.Unknown, BodyType = CatBodyType.Small, Age = CatAge.Unknown };
        }
        public void CopyEdit() {
            /// Clear ViewData's CatId <see cref="Model.Cat.CatId"/>
            this.CatId = null;
        }

        /// <summary>
        /// Validate And Get Entry Data
        /// </summary>
        /// <returns>Succeed-> Edited Data, Failure -> null </returns>
        public Cat ValidateEntry() {
            var CatNameProp = GetPropertyAttribute<MaxLengthAttribute>(typeof(Cat), nameof(Cat.CatName));
            if (!string.IsNullOrEmpty(this.editData.CatName) && Encoding.Unicode.GetByteCount(this.editData.CatName) > CatNameProp.Length) {
                MessageBox.Show(CatNameProp.ErrorMessage, "Cat Master",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var HairPatternProp = GetPropertyAttribute<MaxLengthAttribute>(typeof(Cat), nameof(Cat.HairPattern));
            if (!string.IsNullOrEmpty(this.editData.HairPattern) &&  Encoding.Unicode.GetByteCount(this.editData.HairPattern) > HairPatternProp.Length) {
                MessageBox.Show(HairPatternProp.ErrorMessage, "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var FaceTypeProp = GetPropertyAttribute<MaxLengthAttribute>(typeof(Cat), nameof(Cat.FaceType));
            if (!string.IsNullOrEmpty(this.editData.FaceType) && Encoding.Unicode.GetByteCount(this.editData.FaceType) > FaceTypeProp.Length) {
                MessageBox.Show(FaceTypeProp.ErrorMessage, "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var PersonalityProp = GetPropertyAttribute<MaxLengthAttribute>(typeof(Cat), nameof(Cat.FaceType));
            if (!string.IsNullOrEmpty(this.editData.Personality) && Encoding.Unicode.GetByteCount(this.editData.Personality) > PersonalityProp.Length) {
                MessageBox.Show(PersonalityProp.ErrorMessage, "Cat Master", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            

            return (Cat)this.editData;
        }

        public CatsViewModel() {
            // Data Init
            ClearEdit();
            this.catList = null;
        }
        protected void OnPropertyChanged(string info) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}
