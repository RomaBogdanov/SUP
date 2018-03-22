using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class UpdateItemNationsModel : IAddItem1Model
    {
        private AddItem1ViewModel viewModel;
        private Nation nation;

        public AddItem1ViewModel ViewModel
        {
            set
            {
                this.viewModel = value;
                this.viewModel.Field = nation.CountryName;
            }
        }

        public event Action OnClose;

        public UpdateItemNationsModel(Nation nation)
        {
            this.nation = nation;
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok()
        {
            CountriesWrapper countries = CountriesWrapper.CurrentTable();
            if (this.viewModel.Field != "")
            {
                DataRow dataRow = countries.Table.Rows.Find(nation.Id);
                dataRow["f_cntr_name"] = this.viewModel.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            }
            Cancel();
        }
    }
}
