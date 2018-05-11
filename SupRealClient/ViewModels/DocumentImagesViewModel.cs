using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class DocumentImagesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private VisitorsDocumentBase document;
        private string image = "";
        private int selectedImage = -1;

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public void SetDocument(VisitorsDocumentBase document)
        {
            this.document = document;
            if (!document.Images.Any())
            {
                document.Images =
                    DocumentsHelper.CacheImages(document.Id);
            }
            if (document.Images.Any())
            {
                SetImage(0);
            }

            PrevCommand = new RelayCommand(arg => Prev());
            NextCommand = new RelayCommand(arg => Next());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void Prev()
        {
            if (selectedImage > 0)
            {
                SetImage(selectedImage - 1);
            }
        }

        private void Next()
        {
            if (selectedImage < document.Images.Count - 1)
            {
                SetImage(selectedImage + 1);
            }
        }

        private void SetImage(int index)
        {
            selectedImage = index;
            Image = selectedImage < 0 ? "" :
                ImagesHelper.GetImagePath(document.Images[selectedImage]);
        }
    }
}
