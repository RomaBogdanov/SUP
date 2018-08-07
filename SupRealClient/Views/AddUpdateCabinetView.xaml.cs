using System;
using System.Linq;
using System.Windows;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupClientConnectionLib;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateCabinetView.xaml
    /// </summary>
    public partial class AddUpdateCabinetView
    {
        bool isUpdate;
        Cabinet cabinet;

        public AddUpdateCabinetView()
        {
            InitializeComponent();

            AfterInitialize();
        }

        public AddUpdateCabinetView(Cabinet cabinet)
        {
            InitializeComponent();
            isUpdate = true;
            this.cabinet = cabinet;
            numCab.Text = cabinet.CabNum;
            descript.Text = cabinet.Descript;
            numDoor.Text = cabinet.DoorNum;

            AfterInitialize();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                CabinetsWrapper cabinetsWrapper = CabinetsWrapper.CurrentTable();
                DataRow row = cabinetsWrapper.Table.Rows.Find(this.cabinet.Id);
                if (numCab.Text != "" & numDoor.Text != "")
                {
                    row.BeginEdit();
                    row["f_cabinet_num"] = numCab.Text;
                    row["f_cabinet_desc"] = descript.Text;
                    row["f_door_num"] = numDoor.Text;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row.EndEdit();
                }
            }
            else
            {
                if (numCab.Text != "" & numDoor.Text != "")
                {
                    CabinetsWrapper cabinetsWrapper = CabinetsWrapper.CurrentTable();
                    int doorNumInt = cabinetsWrapper.Table.AsEnumerable()
                        .Max(arg => arg.Field<int>("f_door_num_int"));
                    DataRow row = cabinetsWrapper.Table.NewRow();
                    row["f_cabinet_num"] = numCab.Text;
                    row["f_cabinet_desc"] = descript.Text;
                    row["f_door_num"] = numDoor.Text;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row["f_door_num_int"] = doorNumInt++;
                    cabinetsWrapper.Table.Rows.Add(row);
                }
            }
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CabinetsWrapper cabinetsWrapper = CabinetsWrapper.CurrentTable();
            DataRow row = cabinetsWrapper.Table.Rows.Find(this.cabinet.Id);
            cabinetsWrapper.Table.Rows.Remove(row);
        }
    }
}
