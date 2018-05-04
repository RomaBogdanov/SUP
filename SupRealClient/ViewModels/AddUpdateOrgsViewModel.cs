using System.Collections.Generic;
using SupRealClient.Models;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Common.Interfaces;
using SupRealClient.Views;
using System;

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
        private string region = "";
        private string country = "";
        private Organization organization = new Organization();
        private IWindow view;
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

        public string Region
        {
            get { return region; }
            set
            {
                if (value != null)
                {
                    region = value;
                    OnPropertyChanged("Region");
                }
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                if (value != null)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public ICommand CountryCommand { get; set; }
        public ICommand RegionCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public AddUpdateOrgsViewModel()
        {
            CountryCommand = new RelayCommand(arg => CountyList());
            RegionCommand = new RelayCommand(arg => RegionsList());
            ClearCommand = new RelayCommand(arg => Clear(arg as string));
        }

        public void SetModel(IAddUpdateOrgsModel addItem1Model)
        {
            this.model = addItem1Model;
            this.Type = model.Data.Type;
            this.Name = model.Data.Name;
            this.Comment = model.Data.Comment;
            this.FullName = model.Data.FullName;
            organization.Type = Type;
            organization.Name = Name;
            organization.Comment = Comment;
            organization.FullName = FullName;
            this.Ok = new RelayCommand(arg => this.model.Ok(organization));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));

        private void CountyList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4NationsWindView", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            organization.CountryId = result.Id;
            organization.Country = result.Name;
            Country = result.Name;

        }

        private void RegionsList()
        {
            var result = ViewManager.Instance.OpenWindowModal(
                "Base4RegionsWindView", view) as BaseModelResult;
            if (result == null)
            {
                return;
            }
            organization.RegionId = result.Id;
            organization.Region = result.Name;
            Region = result.Name;
        }

        private void Clear(string field)
        {
            switch (field)
            {
                case "Nation":
                    organization.CountryId = -1;
                    organization.Country = "";
                    Country = "";
                    break;
                case "Region":
                    organization.RegionId = -1;
                    organization.Region = "";
                    Region = "";
                    break;
                    /*
                case "Organization":
                    CurrentItem.OrganizationId = -1;
                    CurrentItem.Organization = "";
                    break;
                case "DocType":
                    CurrentItem.DocumentId = -1;
                    CurrentItem.DocType = "";
                    break;
                case "Department":
                    CurrentItem.DepartmentId = -1;
                    CurrentItem.Department = "";
                    break;
                case "Cabinet":
                    CurrentItem.CabinetId = -1;
                    CurrentItem.Cabinet = "";
                    break;
                case "Position":
                    CurrentItem.Position = "";
                    break;
                case "Comment":
                    CurrentItem.Comment = "";
                    break;*/
                default:
                    return;
            }

            OnPropertyChanged("CurrentItem");
        }
    }
}
