using System;
using System.Data;
using SupRealClient.Common.Interfaces;
using SupRealClient.Views;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupClientConnectionLib;

namespace SupRealClient.Models.AddUpdateModel
{

    /// <summary>
    /// Класс-претендент на то, чтобы стать основой для всех Model
    /// форм добавления-редактироания данных.
    /// </summary>
    public abstract class AddUpdateAbstrModel
    {
        public event ModelPropertyChanged OnModelPropertyChanged;
        public event Action<object> OnClose;
        public virtual object CurrentItem { get; set; }
        
        //public object Result { get; set; }

        public virtual void Ok()
        {
            SaveResult();
            OnClose?.Invoke(CurrentItem);
        }

        public virtual void Cancel()
        {
            OnClose?.Invoke(null);
        }

        public IWindow Parent { get; internal set; }

        protected virtual void SaveResult() { }
    }

    public class AddSpaceModel: AddUpdateAbstrModel
    {
        
        public AddSpaceModel()
        {
            CurrentItem = new Space();
        }

        protected override void SaveResult()
        {
            DataRow row = SpacesWrapper.CurrentTable().Table.NewRow();
            row["f_num_real"] = ((Space)CurrentItem).NumReal;
            row["f_num_build"] = ((Space)CurrentItem).NumBuild;
            row["f_descript"] = ((Space)CurrentItem).Descript;
            row["f_note"] = ((Space)CurrentItem).Note;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            SpacesWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddDoorModel : AddUpdateAbstrModel
    {
        public AddDoorModel()
        {
            CurrentItem = new Door();
        }

        protected override void SaveResult()
        {
            DataRow row = DoorsWrapper.CurrentTable().Table.NewRow();
            row["f_door_num"] = ((Door)CurrentItem).DoorNum;
            row["f_descript"] = ((Door)CurrentItem).Descript;
            row["f_space_in"] = ((Door)CurrentItem).SpaceIn;
            row["f_space_out"] = ((Door)CurrentItem).SpaceOut;
            row["f_access_point_id"] = ((Door)CurrentItem).AccessPointId;
            row["f_key_id"] = ((Door)CurrentItem).KeyId;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            DoorsWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddAreaModel : AddUpdateAbstrModel
    {
        public AddAreaModel()
        {
            CurrentItem = new Area();
        }

        protected override void SaveResult()
        {
            DataRow row = AreasWrapper.CurrentTable().Table.NewRow();
            row["f_area_name"] = ((Area)CurrentItem).Name;
            row["f_area_descript"] = ((Area)CurrentItem).Descript;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AreasWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddAreaSpaceModel : AddUpdateAbstrModel
    {
        public AddAreaSpaceModel()
        {
            CurrentItem = new AreaSpace();
        }

        protected override void SaveResult()
        {
            DataRow row = AreasSpacesWrapper.CurrentTable().Table.NewRow();
            row["f_area_id"] = ((AreaSpace)CurrentItem).AreaId;
            row["f_space_id"] = ((AreaSpace)CurrentItem).SpaceId;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AreasSpacesWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddAccessPointModel : AddUpdateAbstrModel
    {
        public AddAccessPointModel()
        {
            CurrentItem = new AccessPoint();
        }

        protected override void SaveResult()
        {
            DataRow row = AccessPointsWrapper.CurrentTable().Table.NewRow();
            row["f_access_point_name"] = ((AccessPoint)CurrentItem).Name;
            row["f_access_point_description"] = ((AccessPoint)CurrentItem).Descript;
            row["f_access_point_space_in"] = ((AccessPoint)CurrentItem).SpaceIn;
            row["f_access_point_space_out"] = ((AccessPoint)CurrentItem).SpaceOut;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AccessPointsWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddRealKeyModel : AddUpdateAbstrModel
    {
        public AddRealKeyModel()
        {
            CurrentItem = new RealKey();
        }

        protected override void SaveResult()
        {
            DataRow row = KeysWrapper.CurrentTable().Table.NewRow();
            row["f_key_name"] = ((RealKey)CurrentItem).Name;
            row["f_key_description"] = ((RealKey)CurrentItem).Descript;
            row["f_door_id"] = ((RealKey)CurrentItem).DoorId;
            row["f_key_holder_id"] = ((RealKey)CurrentItem).KeyHolderId;
            row["f_key_case_id"] = ((RealKey)CurrentItem).KeyCaseId;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            KeysWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddAccessLevelModel : AddUpdateAbstrModel
    {
        public AddAccessLevelModel()
        {
            CurrentItem = new AccessLevel();
        }

        protected override void SaveResult()
        {
            DataRow row = AccessLevelWrapper.CurrentTable().Table.NewRow();
            row["f_area_id"] = ((AccessLevel)CurrentItem).AreaId;
            row["f_level_name"] = ((AccessLevel)CurrentItem).Name;
            row["f_schedule_id"] = ((AccessLevel)CurrentItem).ScheduleId;
            row["f_access_level_note"] = ((AccessLevel)CurrentItem).AccessLevelNote;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AccessLevelWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddCarModel : AddUpdateAbstrModel
    {
        public AddCarModel()
        {
            CurrentItem = new Car();
        }

        protected override void SaveResult()
        {
            DataRow row = CarsWrapper.CurrentTable().Table.NewRow();
            row["f_car_mark"] = ((Car)CurrentItem).CarMark;
            row["f_car_number"] = ((Car)CurrentItem).CarNumber;
            row["f_org_id"] = ((Car)CurrentItem).OrgId;
            row["f_visitor_id"] = ((Car)CurrentItem).VisitorId;
            row["f_color"] = ((Car)CurrentItem).Color;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            CarsWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class AddEquipmentModel : AddUpdateAbstrModel
    {
        public AddEquipmentModel()
        {
            CurrentItem = new Equipment();
        }

        protected override void SaveResult()
        {
            DataRow row = EquipmentWrapper.CurrentTable().Table.NewRow();
            row["f_equip_name"] = ((Equipment)CurrentItem).Name;
            row["f_equip_count"] = ((Equipment)CurrentItem).Count;
            row["f_equip_num"] = ((Equipment)CurrentItem).EquipNum;
            row["f_direct"] = ((Equipment)CurrentItem).Direct;
            row["f_from"] = ((Equipment)CurrentItem).From;
            row["f_to"] = ((Equipment)CurrentItem).To;
            row["f_org_id"] = ((Equipment)CurrentItem).OrgId;
            row["f_visitor_id"] = ((Equipment)CurrentItem).VisId;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            EquipmentWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }
}