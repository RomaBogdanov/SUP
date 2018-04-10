using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;
using System.Data;
using SupRealClient.TabsSingleton;
using SupClientConnectionLib;
using System.ComponentModel;

namespace SupRealClient.Models.OrganizationStructure
{
    class EditUnitModel : IModel
    {
        public string Description { get; set; }
        public bool Save
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        private int sectionId;

        public EditUnitModel(int sectionId)
        {
            this.sectionId = sectionId;
        }

        public void EditItem()
        {
            if (!(Description == "" | Description == null))
            {
                DataRow row = DepartmentSectionWrapper.CurrentTable()
                    .Table.Rows.Find(sectionId);
                row["f_section_name"] = Description;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            }
            OnClose?.Invoke();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
