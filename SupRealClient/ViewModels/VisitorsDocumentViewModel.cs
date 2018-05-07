﻿using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace SupRealClient.ViewModels
{
    public class VisitorsDocumentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected VisitorsDocumentModel model;
        private string name = "";
        private ObservableCollection<string> images =
            new ObservableCollection<string>();
        private List<Guid> imageCache = new List<Guid>();
        private int selectedImage = -1;

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
            }
        }

        public int SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

        public ICommand AddImageCommand { get; set; }
        public ICommand RemoveImageCommand { get; set; }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }

        public VisitorsDocumentViewModel()
        {
        }

        public void SetModel(VisitorsDocumentModel model)
        {
            this.model = model;
            Caption = model.Data.Id == 0 ? "Добавление документа" :
                "Редактирование документа";
            this.Name = model.Data.Name;
            SetModel();
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void SetModel()
        {
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

            this.Ok = new RelayCommand(arg => this.model.Ok(
                new VisitorsDocument
                {
                    Name = Name,
                    TypeId = 0,
                    Seria = "",
                    Num = "",
                    Org = "",
                    Code = "",
                    Date = DateTime.MinValue,
                    Images = imageCache,
                    IsChanged = true
                }));

            AddImageCommand = new RelayCommand(arg => AddImage());
            RemoveImageCommand = new RelayCommand(arg => RemoveImage());
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
    }
}