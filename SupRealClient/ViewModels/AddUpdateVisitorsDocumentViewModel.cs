using SupRealClient.Models;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.ViewModels
{
    public class AddUpdateVisitorsDocumentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private VisitorsDocumentModel model;
        private string name = "";

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

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateVisitorsDocumentViewModel() { }

        public void SetModel(VisitorsDocumentModel model)
        {
            this.model = model;
            this.Name = model.Data.Name;

            this.Ok = new RelayCommand(arg => this.model.Ok(
                new VisitorsDocument
                {
                    Name = Name
                }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
