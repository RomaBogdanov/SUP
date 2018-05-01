using System.Collections.Generic;
using SupRealClient.Models;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

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
        public int FontSize => GlobalSettings.GetSettings();

        public string TypeToolType
        {
            get
            {
                var list = new List<string>();
                var organizations = OrganizationsWrapper.CurrentTable();

                foreach (DataRow row in organizations.Table.Rows)
                {
                    if (!list.Contains(row.ItemArray[2].ToString()) && row.ItemArray[2].ToString() != "нет данных")
                    {
                        list.Add(row.ItemArray[2].ToString());
                    }
                }

                return list.Aggregate("", (current, row) => current + (row + "\n"));
            }
        }

        public string NameToolType
        {
            get
            {
                var list = new List<string>();
                var organizations = OrganizationsWrapper.CurrentTable();

                foreach (DataRow row in organizations.Table.Rows)
                {
                    if (!list.Contains(row.ItemArray[3].ToString()))
                    {
                        list.Add(row.ItemArray[3].ToString());
                    }
                }

                return list.Aggregate("", (current, row) => current + (row + "\n"));
            }
        }

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

        public AddUpdateOrgsViewModel()
        {
            
        }

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
