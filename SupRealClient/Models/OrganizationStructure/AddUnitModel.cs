﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;
using System.Data;
using SupRealClient.TabsSingleton;
using SupClientConnectionLib;

namespace SupRealClient.Models.OrganizationStructure
{
    class AddUnitModel : IModel
    {
        public string Description { get; set; }
        public bool Save
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        private int departmentId;

        public AddUnitModel(int departmentId)
        {
            this.departmentId = departmentId;
        }

        public void EditItem()
        {
            if (!(Description == "" | Description == null))
            {
                DataRow row = DepartmentSectionWrapper.CurrentTable().Table.NewRow();
                row["f_section_name"] = Description;
                row["f_dep_id"] = departmentId;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                DepartmentSectionWrapper.CurrentTable().Table.Rows.Add(row);
            }
            OnClose?.Invoke();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}