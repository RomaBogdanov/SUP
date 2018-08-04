﻿using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
        public virtual event ModelPropertyChanged OnModelPropertyChanged;
        public virtual event Action<object> OnClose;
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

        /// <summary>
        /// Действия, происходящие при нажатии Ок
        /// </summary>
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

    public class UpdateSpaceModel : AddUpdateAbstrModel
    {
        public UpdateSpaceModel(Space space)
        {
            CurrentItem = space.Clone();
        }

        protected override void SaveResult()
        {
            DataRow row = SpacesWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);
            row.BeginEdit();
            row["f_num_real"] = ((Space)CurrentItem).NumReal;
            row["f_num_build"] = ((Space)CurrentItem).NumBuild;
            row["f_descript"] = ((Space)CurrentItem).Descript;
            row["f_note"] = ((Space)CurrentItem).Note;
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row.EndEdit();
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
            row["f_space_in"] = ((Door)CurrentItem).SpaceInId;
            row["f_space_out"] = ((Door)CurrentItem).SpaceOutId;
            row["f_access_point_id_hi"] = ((Door)CurrentItem).AccessPointIdHi;
            row["f_access_point_id_lo"] = ((Door)CurrentItem).AccessPointIdLo;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            DoorsWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class UpdateDoorModel : AddUpdateAbstrModel
    {
        public UpdateDoorModel(Door door)
        {
            CurrentItem = door.Clone();
        }

        protected override void SaveResult()
        {
            DataRow row = DoorsWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);
            row.BeginEdit();
            row["f_door_num"] = ((Door)CurrentItem).DoorNum;
            row["f_descript"] = ((Door)CurrentItem).Descript;
            row["f_space_in"] = ((Door)CurrentItem).SpaceInId;
            row["f_space_out"] = ((Door)CurrentItem).SpaceOutId;
            row["f_access_point_id_hi"] = ((Door)CurrentItem).AccessPointIdHi;
            row["f_access_point_id_lo"] = ((Door)CurrentItem).AccessPointIdLo;
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row.EndEdit();
        }
    }

    public class UpdateAreaModel : AddUpdateAbstrModel
    {
        public UpdateAreaModel(Area area)
        {
            CurrentItem = area.Clone();
        }

        protected override void SaveResult()
        {
            DataRow mainRow = AreasWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);

            DataRow row = null;
            foreach (DataRow r in AreasExtWrapper.CurrentTable().Table.Rows)
            {
                if (mainRow.Field<int>("f_object_id_hi") == r.Field<int>("f_object_id_hi") &&
                    mainRow.Field<int>("f_object_id_lo") == r.Field<int>("f_object_id_lo"))
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
            {
                row = AreasExtWrapper.CurrentTable().Table.NewRow();
                row["f_object_id_hi"] = ((Area)CurrentItem).ObjectIdHi;
                row["f_object_id_lo"] = ((Area)CurrentItem).ObjectIdLo;
                row["f_description"] = ((Area)CurrentItem).Descript;
                AreasExtWrapper.CurrentTable().Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_description"] = ((Area)CurrentItem).Descript;
                row.EndEdit();
            }

            mainRow.BeginEdit();
            mainRow["f_rec_date"] = DateTime.Now;
            mainRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            mainRow.EndEdit();
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
            row["f_area_id_hi"] = ((AreaSpace)CurrentItem).AreaIdHi;
            row["f_area_id_lo"] = ((AreaSpace)CurrentItem).AreaIdLo;
            row["f_space_id"] = ((AreaSpace)CurrentItem).SpaceId;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AreasSpacesWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class UpdateAreaSpaceModel : AddUpdateAbstrModel
    {
        public UpdateAreaSpaceModel(AreaSpace areaSpace)
        {
            CurrentItem = areaSpace.Clone();
        }

        protected override void SaveResult()
        {
            DataRow row = AreasSpacesWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);
            row.BeginEdit();
            row["f_area_id_hi"] = ((AreaSpace)CurrentItem).AreaIdHi;
            row["f_area_id_lo"] = ((AreaSpace)CurrentItem).AreaIdLo;
            row["f_space_id"] = ((AreaSpace)CurrentItem).SpaceId;
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row.EndEdit();
        }
    }

    public class UpdateAccessPointModel : AddUpdateAbstrModel
    {
        public UpdateAccessPointModel(AccessPoint accessPoint)
        {
            CurrentItem = accessPoint.Clone();
        }

        protected override void SaveResult()
        {
            DataRow mainRow = AccessPointsWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);

            DataRow row = null;
            foreach (DataRow r in AccessPointsExtWrapper.CurrentTable().Table.Rows)
            {
                if (mainRow.Field<int>("f_object_id_hi") == r.Field<int>("f_object_id_hi") &&
                    mainRow.Field<int>("f_object_id_lo") == r.Field<int>("f_object_id_lo"))
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
            {
                row = AccessPointsExtWrapper.CurrentTable().Table.NewRow();
                row["f_object_id_hi"] = ((AccessPoint)CurrentItem).ObjectIdHi;
                row["f_object_id_lo"] = ((AccessPoint)CurrentItem).ObjectIdLo;
                row["f_description"] = ((AccessPoint)CurrentItem).Descript;
                AccessPointsExtWrapper.CurrentTable().Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_description"] = ((AccessPoint)CurrentItem).Descript;
                row.EndEdit();
            }

            mainRow.BeginEdit();
            mainRow["f_rec_date"] = DateTime.Now;
            mainRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            mainRow.EndEdit();
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
            row["f_area_id_hi"] = ((AccessLevel)CurrentItem).AreaIdHi;
            row["f_area_id_lo"] = ((AccessLevel)CurrentItem).AreaIdLo;
            row["f_level_name"] = ((AccessLevel)CurrentItem).Name;
            row["f_schedule_id_hi"] = ((AccessLevel)CurrentItem).ScheduleIdHi;
            row["f_schedule_id_lo"] = ((AccessLevel)CurrentItem).ScheduleIdLo;
            row["f_access_level_note"] = ((AccessLevel)CurrentItem).AccessLevelNote;
            row["f_deleted"] = "N";
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            AccessLevelWrapper.CurrentTable().Table.Rows.Add(row);
        }
    }

    public class UpdateAccessLevelModel : AddUpdateAbstrModel
    {
        public UpdateAccessLevelModel(AccessLevel accessLevel)
        {
            CurrentItem = accessLevel.Clone();
        }

        protected override void SaveResult()
        {
            DataRow row = AccessLevelWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);
            row.BeginEdit();
            row["f_area_id_hi"] = ((AccessLevel)CurrentItem).AreaIdHi;
            row["f_area_id_lo"] = ((AccessLevel)CurrentItem).AreaIdLo;
            row["f_level_name"] = ((AccessLevel)CurrentItem).Name;
            row["f_schedule_id_hi"] = ((AccessLevel)CurrentItem).ScheduleIdHi;
            row["f_schedule_id_lo"] = ((AccessLevel)CurrentItem).ScheduleIdLo;
            row["f_access_level_note"] = ((AccessLevel)CurrentItem).AccessLevelNote;
            row["f_rec_date"] = DateTime.Now;
            row.EndEdit();
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

    public class UpdateScheduleModel : AddUpdateAbstrModel
    {
        public UpdateScheduleModel(Schedule schedule)
        {
            CurrentItem = schedule.Clone();
        }

        protected override void SaveResult()
        {
            DataRow mainRow = SchedulesWrapper.CurrentTable().Table.Rows.Find(((IdEntity)CurrentItem).Id);

            DataRow row = null;
            foreach (DataRow r in SchedulesExtWrapper.CurrentTable().Table.Rows)
            {
                if (mainRow.Field<int>("f_object_id_hi") == r.Field<int>("f_object_id_hi") &&
                    mainRow.Field<int>("f_object_id_lo") == r.Field<int>("f_object_id_lo"))
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
            {
                row = SchedulesExtWrapper.CurrentTable().Table.NewRow();
                row["f_object_id_hi"] = ((Schedule)CurrentItem).ObjectIdHi;
                row["f_object_id_lo"] = ((Schedule)CurrentItem).ObjectIdLo;
                row["f_description"] = ((Schedule)CurrentItem).Descript;
                SchedulesExtWrapper.CurrentTable().Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_description"] = ((Schedule)CurrentItem).Descript;
                row.EndEdit();
            }

            mainRow.BeginEdit();
            mainRow["f_rec_date"] = DateTime.Now;
            mainRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            mainRow.EndEdit();
        }
    }

    /// <summary>
    /// Обработчик формы добавления новой разовой заявки.
    /// </summary>
    public class AddSingleBidModel : AddUpdateAbstrModel
    {
        public AddSingleBidModel(bool isTimeEditable,DateTime dateFrom, DateTime dateTo)
        {
            CurrentItem = new OrderElement(isTimeEditable, dateFrom, dateTo);
        }
    }

    /// <summary>
    /// Обработчик формы по редактированию заявки на человека.
    /// </summary>
    public class UpdateBidModel : AddUpdateAbstrModel
    {
        public UpdateBidModel(OrderElement visitor)
        {
            CurrentItem = visitor.Clone();
        }
    }

    /// <summary>
    /// Обработчик формы для добавления редактирования зон в заявку.
    /// </summary>
    public class AddUpdateZonesToBidModel : AddUpdateAbstrModel
    {

        public override event Action<object> OnClose;

        private ObservableCollection<Area> setAllZones;
        private ObservableCollection<Area> setAppointZones;
        private Area currentAppointZone;

        public ObservableCollection<Area> SetAllZones
        {
            get { return setAllZones;}
            set { setAllZones = value; }
        }

        public ObservableCollection<Area> SetAppointZones
        {
            get { return setAppointZones; }
            set { setAppointZones = value; }
        }

        public Area CurrentAppointZone
        {
            get { return currentAppointZone; }
            set { currentAppointZone = value; }
        }

        public Area ToAppointZones()
        {
            if (CurrentItem != null)
            {
                int i = setAllZones.IndexOf((Area) CurrentItem);
                SetAppointZones.Add((Area) CurrentItem);
                SetAllZones.Remove((Area) CurrentItem);
                if (SetAllZones.Count > i)
                {
                    CurrentItem = SetAllZones[i];
                }
                else if (SetAllZones.Count == 0)
                {
                    CurrentItem = null;
                }
                else
                {
                    CurrentItem = SetAllZones[i - 1];
                }
            }

            return (Area)CurrentItem;
        }

        public Area ToAllZonesCommand()
        {

            if (CurrentAppointZone != null)
            {
                int i = SetAppointZones.IndexOf(CurrentAppointZone);
                SetAllZones.Add(CurrentAppointZone);
                SetAppointZones.Remove(CurrentAppointZone);
                if (SetAppointZones.Count > i)
                {
                    CurrentAppointZone = SetAppointZones[i];
                }
                else if (SetAppointZones.Count == 0)
                {
                    CurrentAppointZone = null;
                }
                else
                {
                    CurrentAppointZone = SetAppointZones[i - 1];
                }
            }

            return CurrentAppointZone;
        }

        public AddUpdateZonesToBidModel(ObservableCollection<Area> setAppointZones)
        {
            SetAppointZones = setAppointZones ?? new ObservableCollection<Area>();
            Query();
        }

        private void Query()
        {
            
            SetAllZones = new ObservableCollection<Area>(
                from areas in AreasWrapper.CurrentTable().Table.AsEnumerable()
                where areas.Field<int>("f_area_id") != 0 && 
                      SetAppointZones.Where(
                      arg => arg.Id == areas.Field<int>("f_area_id")).Count()==0
                select new Area
                {
                    Id = areas.Field<int>("f_area_id"),
                    Name = areas.Field<string>("f_area_name"),
                    Descript = areas.Field<string>("f_area_descript")
                });
            //SetAppointZones = new ObservableCollection<Area>();
            
            CurrentItem = SetAllZones.Count > 0 ? SetAllZones[0] : null;
            CurrentAppointZone = SetAppointZones.Count > 0 ? SetAppointZones[0] : null;
        }

        public override void Ok()
        {
            OnClose?.Invoke(SetAppointZones);
        }
    }
}