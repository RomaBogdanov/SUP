using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление организации - модель
    /// </summary>
    class AddOrgsModel : IAddUpdateOrgsModel
    {
        protected bool IsChild { get; set; } = false;
        protected bool IsMaster { get; set; } = false;
        
        public Organization Data => new Organization();

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }
        
        public void Ok(Organization data)
        {
            if (string.IsNullOrEmpty(data.Name))
            {
                MessageBox.Show("Заполните поле Название");
                return; 
            }

            data.Name = CheckName(data.Name);

            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();

            var rows = (from object row in organizations.Table.Rows select row as DataRow).ToList();

            var newOrganization =
                rows.SingleOrDefault(
                    r =>
                        r.ItemArray[3].ToString() == data.Name && r.ItemArray[13].ToString() == data.Country &&
                        r.ItemArray[14].ToString() == data.Region) == null;

            if (newOrganization)
            {
                if (!(data.Type == "" | data.Name == "" | data.FullName == ""))
                {
                    DataRow row = organizations.Table.NewRow();
                    row["f_org_type"] = data.Type;
                    row["f_org_name"] = data.Name;
                    row["f_comment"] = data.Comment;
                    row["f_full_org_name"] = data.FullName;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row["f_has_free_access"] = IsChild ? "Y" : "N";
                    row["f_is_basic"] = IsMaster ? "Y" : "N";
                    organizations.Table.Rows.Add(row);
                }
                Cancel();
            }
            else
            {
                MessageBox.Show("Такая организация уже записана!");
            }
        }

        public string CheckName(string name)
        {
            if (name.First() == '\"' && name.Last() == '\"')
            {
                return name;
            }

            if (name.First() == '\'')
            {
                name = name.Remove(0, 1);
            }

            if (name.Last() == '\'')
            {
                name = name.Remove(name.Length - 1, 1);
            }

            return $"\"{name}\"";
        }
    }

    class AddChildOrgsModel: AddOrgsModel
    {
        public AddChildOrgsModel()
        {
            IsChild = true;
        }
    }

    class AddMasterOrgsModel: AddOrgsModel
    {
        public AddMasterOrgsModel()
        {
            IsMaster = true;
        }
    }
}
