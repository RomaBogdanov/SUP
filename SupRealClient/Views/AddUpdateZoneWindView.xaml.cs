using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupClientConnectionLib;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateZoneWindView.xaml
    /// </summary>
    public partial class AddUpdateZoneWindView : Window
    {
        //IEnumerable<string> doors;
        ObservableCollection<CabDoor> relDoorsList = new ObservableCollection<CabDoor>();
        ObservableCollection<CabDoor> baseRelDoorsList = new ObservableCollection<CabDoor>();
        ObservableCollection<CabDoor> doorList;
        ObservableCollection<TypeZone> types;
        int zoneNum;
        int zoneId;
        bool isUpdate = false;

        public AddUpdateZoneWindView()
        {
            InitializeComponent();
            doorList = new ObservableCollection<CabDoor>(
                from t in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
                select new CabDoor { Id = t.Field<int>("f_cabinet_id"),
                    Door = t.Field<string>("f_door_num") 
                    + ", " + t.Field<string>("f_cabinet_desc")
                });
            zoneNum = ZonesWrapper.CurrentTable().Table.AsEnumerable()
                .Max(arg => arg.Field<int>("f_zone_num"));
            zoneNum++;
            doors.ItemsSource = doorList;
            types = new ObservableCollection<TypeZone>(
                from z in ZoneTypesWrapper.CurrentTable().Table.AsEnumerable()
                select new TypeZone { Id = z.Field<int>("f_zone_type_id"),
                    ZoneType = z.Field<string>("f_zone_type_name") });
            zoneType.ItemsSource = types;
            relDoors.ItemsSource = relDoorsList;
        }

        public AddUpdateZoneWindView(Zone zone)
        {
            zoneId = zone.Id;
            isUpdate = true;
            InitializeComponent();
            zoneName.Text = zone.Name;
            types = new ObservableCollection<TypeZone>(
                from z in ZoneTypesWrapper.CurrentTable().Table.AsEnumerable()
                select new TypeZone
                {
                    Id = z.Field<int>("f_zone_type_id"),
                    ZoneType = z.Field<string>("f_zone_type_name")
                });
            zoneType.ItemsSource = types;
            zoneType.SelectedItem = types.First(arg => arg.ZoneType == zone.Type);

            baseRelDoorsList = new ObservableCollection<CabDoor>(
                from t in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
                join c in CabinetsZonesWrapper.CurrentTable().Table.AsEnumerable()
                on t.Field<int>("f_cabinet_id") equals c.Field<int>("f_cabinet_id")
                where c.Field<int>("f_zone_id") == zone.Id
                select new CabDoor
                {
                    Id = t.Field<int>("f_cabinet_id"),
                    Door = t.Field<string>("f_door_num")
                    + ", " + t.Field<string>("f_cabinet_desc")
                });

            relDoorsList = new ObservableCollection<CabDoor>(
                from t in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
                join c in CabinetsZonesWrapper.CurrentTable().Table.AsEnumerable()
                on t.Field<int>("f_cabinet_id") equals c.Field<int>("f_cabinet_id")
                where c.Field<int>("f_zone_id") == zone.Id
                select new CabDoor
                {
                    Id = t.Field<int>("f_cabinet_id"),
                    Door = t.Field<string>("f_door_num")
                    + ", " + t.Field<string>("f_cabinet_desc")
                });
            relDoors.ItemsSource = relDoorsList;

            doorList = new ObservableCollection<CabDoor>(
                from t in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
                where !(from r in relDoorsList select r.Id)
                .Contains(t.Field<int>("f_cabinet_id"))
                select new CabDoor
                {
                    Id = t.Field<int>("f_cabinet_id"),
                    Door = t.Field<string>("f_door_num")
                    + ", " + t.Field<string>("f_cabinet_desc")
                });
            //doorList.Where(arg => !relDoorsList.Select(arg2 => arg2.Id).Contains(arg.Id));
            doors.ItemsSource = doorList;
        }
        
        private void set1_Click(object sender, RoutedEventArgs e)
        {
            if (doors.SelectedItem != null)
            {
                relDoorsList.Add((CabDoor)doors.SelectedItem);
                doorList.Remove((CabDoor)doors.SelectedItem);
            }
        }

        private void rem1_Click(object sender, RoutedEventArgs e)
        {
            if (relDoors.SelectedItem != null)
            {
                doorList.Add((CabDoor)relDoors.SelectedItem);
                relDoorsList.Remove((CabDoor)relDoors.SelectedItem);
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                Update();
            }
            else
            {
                Add();
            }
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add()
        {
            if (zoneName.Text == "" | zoneType.SelectedItem == null) return;
            // Создаём зону
            ZonesWrapper zonesWrapper = ZonesWrapper.CurrentTable();
            DataRow row = zonesWrapper.Table.NewRow();
            row["f_zone_type_id"] = ((TypeZone)zoneType.SelectedValue).Id;
            row["f_zone_name"] = zoneName.Text;
            row["f_zone_num"] = zoneNum;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            zonesWrapper.Table.Rows.Add(row);
            int a = (int)row["f_zone_id"];
            // Создаём связи между зоной и дверьми
            foreach (var item in relDoorsList)
            {
                row = CabinetsZonesWrapper.CurrentTable().Table.NewRow();
                row["f_cabinet_id"] = item.Id;
                row["f_zone_id"] = a;
                CabinetsZonesWrapper.CurrentTable().Table.Rows.Add(row);
            }
        }

        private void Update()
        {
            if (zoneName.Text == "" | zoneType.SelectedItem == null) return;
            // Проверяем на изменнения зону.
            ZonesWrapper zonesWrapper = ZonesWrapper.CurrentTable();
            DataRow row = zonesWrapper.Table.Rows.Find(zoneId);
            row["f_zone_type_id"] = ((TypeZone)zoneType.SelectedValue).Id;
            row["f_zone_name"] = zoneName.Text;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            // Проверяем на изменения список привязанных дверей.
            ObservableCollection<CabDoor> forDelet = new ObservableCollection<CabDoor>(
                from f in baseRelDoorsList
                where !(from r in relDoorsList select r.Id).Contains(f.Id)
                select f);
            foreach (var item in forDelet)
            {

                DataRow[] rows = CabinetsZonesWrapper.CurrentTable().Table
                    .Select("f_cabinet_id='" + item.Id + "' and f_zone_id='" + zoneId + "'");
                foreach (var item2 in rows)
                {
                    CabinetsZonesWrapper.CurrentTable().Table.Rows.Remove(item2);
                }
                //CabinetsZonesWrapper.CurrentTable().Table.Rows.Remove()
            }

            ObservableCollection<CabDoor> forAdd = new ObservableCollection<CabDoor>(
                from f in relDoorsList
                where !(from r in baseRelDoorsList select r.Id).Contains(f.Id)
                select f);
            foreach (var item in forAdd)
            {
                row = CabinetsZonesWrapper.CurrentTable().Table.NewRow();
                row["f_cabinet_id"] = item.Id;
                row["f_zone_id"] = zoneId;
                CabinetsZonesWrapper.CurrentTable().Table.Rows.Add(row);
            }
        }

        class CabDoor
        {
            public int Id { get; set; }

            public string Door { get; set; } = "";
            
            public override string ToString()
            {
                return Door;
            }
        }

        class TypeZone
        {
            public int Id { get; set; }

            public string ZoneType { get; set; } = "";

            public override string ToString()
            {
                return ZoneType;
            }
        }
    }
}
