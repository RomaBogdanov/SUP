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
	    private bool _previousButtonEnable = true;
	    private bool _nextButtonEnable = true;

		public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

	    /// <summary>
	    /// Объект со списком свойств Enable для кнопок
	    /// </summary>
	    public VisitorsEnableOrVisible VisitorsEnable { get; set; } = new VisitorsEnableOrVisible();

		/// <summary>
		/// Объект со списком свойтсв Visible для кнопок
		/// </summary>
		public VisitorsEnableOrVisible VisitorsVisible { get; set; } = new VisitorsEnableOrVisible();

	    public bool PreviousButtonEnable
	    {
		    get { return _previousButtonEnable; }
		    set
		    {
			    _previousButtonEnable = value; 
				OnPropertyChanged(nameof(PreviousButtonEnable));
		    }
	    }

	    public bool NextButtonEnable
	    {
		    get { return _nextButtonEnable; }
		    set
		    {
			    _nextButtonEnable = value; 
				OnPropertyChanged(nameof(NextButtonEnable));
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

	        SetConditionsToButtons(0);

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
	        SetConditionsToButtons(selectedImage);
		}

	    private void SetConditionsToButtons(int index)
	    {
		    if (document != null && document.Images != null)
		    {
			    if (!document.Images.Any() || document.Images.Count <= 1)
			    {
				    NextButtonEnable = false;
				    PreviousButtonEnable = false;
				    return;
			    }

			    if (index==0)
			    {
				    NextButtonEnable = true ;
				    PreviousButtonEnable = false;
				    return;
			    }

			    if (index == document.Images.Count - 1)
			    {
				    NextButtonEnable = false ;
				    PreviousButtonEnable = true;
				    return;
			    }

			    NextButtonEnable = true;
			    PreviousButtonEnable = true;
			}
		    else
		    {
				NextButtonEnable = false;
			    PreviousButtonEnable = false;
			}
			
		}
    }
}
