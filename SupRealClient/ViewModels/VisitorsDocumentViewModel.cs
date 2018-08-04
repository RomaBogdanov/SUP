using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using SupRealClient.Handlers;
using SupRealClient.Views;

namespace SupRealClient.ViewModels
{
    public class VisitorsDocumentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private VisitorsDocumentModel model;
        private string name = "";
        private ObservableCollection<string> images =
            new ObservableCollection<string>();
        private List<Guid> imageCache = new List<Guid>();
        private int selectedImage = -1;
	    private bool _conditionButton_SaveChanges = false;
	    private bool _conditionButton_OpenDocument = false;
	    private object _selectedItem=null;

	    public event VisitorsDocumentTestingNameHandler _TestingNameVisitorsDocument;




		public string Caption { get; private set; }

        public ObservableCollection<string> Images
        {
            get { return images; }
            set
            {
                if (value != null)
                {
                    images = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }

	            Change_Condition_SaveChangesButton();

            }
        }

        public int SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
	            Change_Condition_OpenDocumentButton();

            }
        }

	    public object SelectedItem
	    {
		    get { return _selectedItem; }
		    set
		    {
			    _selectedItem = value;
			    OnPropertyChanged("SelectedItem");
		    }
	    }

		public bool ConditionButton_SaveChanges
	    {
		    get { return _conditionButton_SaveChanges; }
		    set
		    {
			    _conditionButton_SaveChanges = value;
				OnPropertyChanged(nameof(ConditionButton_SaveChanges));
		    }
		}

	    public bool ConditionButton_OpenDocument
	    {
		    get { return _conditionButton_OpenDocument; }
		    set
		    {
			    _conditionButton_OpenDocument = value;
			    OnPropertyChanged(nameof(ConditionButton_OpenDocument));
		    }
	    }

		public ICommand AddImageCommand { get; set; }
        public ICommand RemoveImageCommand { get; set; }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }

	    public ICommand OpenDocumentCommand { get; set; }

		public VisitorsDocumentViewModel()
        {
        }

        public void SetModel(VisitorsDocumentModel model)
        {
            this.model = model;
            Caption = model.Data.Id == -1 ? "Добавление документа" :
                "Редактирование документа";
            this.Name = model.Data.Name;
            if (model.Data.Images.Any())
            {
                imageCache = model.Data.Images;
            }
            else
            {
                imageCache = DocumentsHelper.CacheImages(model.Data.Id);
            }
            Images = new ObservableCollection<string>(imageCache.Select(i =>
                ImagesHelper.GetImagePath(i)));

            this.Ok = new RelayCommand(arg => { RealizationSaving(); });
            this.Cancel = new RelayCommand(arg =>
            {
		            this.model.Cancel();
            });

            AddImageCommand = new RelayCommand(arg => AddImage());
            RemoveImageCommand = new RelayCommand(arg => RemoveImage());

	        OpenDocumentCommand=new RelayCommand(arg => OpenDocument());

		}

	    public void RealizationSaving()
	    {
		    System.ComponentModel.CancelEventArgs cancelEventArgs = new System.ComponentModel.CancelEventArgs(true);
		    _TestingNameVisitorsDocument?.Invoke(this, cancelEventArgs);

		    if (cancelEventArgs.Cancel)
		    {
			    this.model.Ok(
				    new VisitorsDocument
				    {
					    Name = Name,
					    TypeId = 0,
					    Images = imageCache,
					    IsChanged = true
				    });
		    }
		}

	    protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void AddImage()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Guid id = ImagesHelper.LoadImage(dlg.FileName);
                Images.Add(ImagesHelper.GetImagePath(id));
                imageCache.Add(id);
            }
        }

        private void RemoveImage()
        {
            if (SelectedImage < 0)
            {
                return;
            }
            int selected = SelectedImage;
            Images.RemoveAt(selected);
            imageCache.RemoveAt(selected);
        }

	    private void OpenDocument()
	    {
		    if (SelectedImage >= 0 && SelectedImage< images.Count)
		    {
			    DocumentImageView documentImageView = new DocumentImageView(false);
			    documentImageView.ResizeMode = ResizeMode.NoResize;
			    documentImageView.DocumentImage = images[SelectedImage];
			    documentImageView.ShowDialog();
		    }
	    }

	    private void Change_Condition_SaveChangesButton()
	    {
		    ConditionButton_SaveChanges=(!string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name));
		}

	    private void Change_Condition_OpenDocumentButton()
	    {
			ConditionButton_OpenDocument = (SelectedImage >= 0 && images.Count>0);
	    }

	}
}
