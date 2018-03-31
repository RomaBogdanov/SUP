using SupRealClient.Models;
using SupRealClient.Common.Data;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class AddItem1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAddItem1Model model;
        private string field = "";

        public string Field
        {
            get { return field; }
            set
            {
                if (value != null)
                {
                    field = value;
                    OnPropertyChanged("Field");
                }
            }
        }

        public ICommand Ok
        { get; set; }

        public ICommand Cancel
        { get; set; }

        public AddItem1ViewModel()
        {
        }

        public void SetModel(IAddItem1Model addItem1Model)
        {
            this.model = addItem1Model;
            this.Field = model.Data.Field;
            this.Ok = new RelayCommand(arg => this.model.Ok(new FieldData { Field = Field }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
