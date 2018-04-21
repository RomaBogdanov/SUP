using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class UploadImageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string visitorId;
        private string type;
        private string imageSource;
        private ImagesWrapper imagesWrapper;

        private List<Img> images = new List<Img>();

        public ICommand Upload
        { get; set; }

        public ICommand Download
        { get; set; }

        public UploadImageViewModel()
        {
            VisitorId = "1";
            Type = "2";
            this.Upload = new RelayCommand(arg => OnUpload());
            this.Download = new RelayCommand(arg => OnDownload());
            imagesWrapper = ImagesWrapper.CurrentTable();
            imagesWrapper.OnChanged += Query;
        }

        public string VisitorId
        {
            get { return this.visitorId; }
            set
            {
                this.visitorId = value;
                OnPropertyChanged("VisitorId");
            }
        }

        public string Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                OnPropertyChanged("Type");
            }
        }

        public string ImageSource
        {
            get { return this.imageSource; }
            set
            {
                this.imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public void OnUpload()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataRow row = imagesWrapper.Table.NewRow();
                row["f_image_id"] = Guid.NewGuid();
                row["f_visitor_id"] = int.Parse(VisitorId);
                row["f_image_type"] = int.Parse(Type);
                row["f_data"] = File.ReadAllBytes(dlg.FileName);
                imagesWrapper.Table.Rows.Add(row);

                ImageSource = "";
                MessageBox.Show("Картинка загружена");
            }
        }

        public void OnDownload()
        {
            var image = images.FirstOrDefault(i => i.Visitor.ToString() == VisitorId);
            if (image != null)
            {
                string path = Path.GetTempPath() + "/" + image.Id.ToString() + ".jpg";
                File.WriteAllBytes(path, image.Data);
                ImageSource = path;
            }
            else
            {
                ImageSource = "";
            }
        }

        private void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void Query()
        {
            var table = imagesWrapper.Table;
            images = (from i in table.AsEnumerable()
                         select new Img
                          {
                              Id = i.Field<Guid>("f_image_id"),
                              Visitor = i.Field<int>("f_visitor_id"),
                              Type = i.Field<int>("f_image_type"),
                              Data = i.Field<byte[]>("f_data"),
                          }).ToList();
            /*var image = images.FirstOrDefault();
            if (image != null)
            {
                File.WriteAllBytes(Path.GetTempPath() + "/" + image.Id.ToString() + ".jpg", image.Data);
            }*/
        }

        private class Img
        {
            public Guid Id { get; set; }
            public int Visitor { get; set; }
            public int Type { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
