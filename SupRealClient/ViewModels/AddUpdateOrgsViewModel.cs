using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.ViewModels
{
    public class AddUpdateOrgsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAddUpdateOrgsModel model;
        private string type = "";
        private string name = "";
        private string comment = "";
        private string fullName = "";

        public string Type
        {
            get { return type; }
            set
            {
                if (value != null)
                {
                    type = value;
                    OnPropertyChanged("Type");
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

        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != null)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value != null)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateOrgsViewModel() { }

        public void SetModel(IAddUpdateOrgsModel addItem1Model)
        {
            this.model = addItem1Model;
            this.Type = model.Data.Type;
            this.Name = model.Data.Name;
            this.Comment = model.Data.Comment;
            this.FullName = model.Data.FullName;

            this.Ok = new RelayCommand(arg => this.model.Ok(new Organization
            {
                Type = Type,
                Name = Name,
                Comment = Comment,
                FullName = FullName
            }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
