using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class AddItemNationsModel : IAddItem1Model
    {
        private AddItem1ViewModel viewModel;

        public AddItem1ViewModel ViewModel
        {
            set { this.viewModel = value; }
        }

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok()
        {
            CountriesWrapper countries = CountriesWrapper.CurrentTable();
            if (this.viewModel.Field != "")
            {
                DataRow row = countries.Table.NewRow();
                row["f_cntr_name"] = this.viewModel.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
                countries.Table.Rows.Add(row);
            }
            Cancel();
        }
    }
}
