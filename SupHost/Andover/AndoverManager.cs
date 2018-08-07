using AndoverLib;
using SupContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SupHost.Andover
{
    public class AndoverManager
    {
        private Logger logger = Logger.CurrentLogger;
        private readonly OperationInfo info;

        public AndoverManager(OperationInfo info)
        {
            this.info = info;
        }

        public bool Ping()
        {
            try
            {
                AndoverConnector connector = AndoverConnector.CurrentConnector;
                return connector.AndoverService.Ping() == "OK";
            }
            catch (Exception ex)
            {
                try
                {
                    AndoverConnector.CurrentConnector.ResetConnection();
                }
                catch (Exception)
                {
                }
                logger.ErrorMessage(ex.Message + ex.StackTrace);
                return false;
            }
        }

        public bool Import()
        {
            try
            {
                return ImportData();
            }
            catch (Exception ex)
            {
                try
                {
                    AndoverConnector.CurrentConnector.ResetConnection();
                }
                catch (Exception)
                {
                }
                logger.ErrorMessage(ex.Message + ex.StackTrace);
                return false;
            }
        }

        public bool Export(AndoverExportData data)
        {
            try
            {
                return ExportData(data);
            }
            catch (Exception ex)
            {
                try
                {
                    AndoverConnector.CurrentConnector.ResetConnection();
                }
                catch (Exception)
                {
                }
                logger.ErrorMessage(ex.Message + ex.StackTrace);
                return false;
            }
        }

        private bool ImportData()
        {
            AndoverConnector connector = AndoverConnector.CurrentConnector;

            var containers = connector.AndoverService.GetContainers();
            //var devices = connector.AndoverService.GetDevices();
            var schedules = connector.AndoverService.GetSchedules();
            var doors = connector.AndoverService.GetDoors();
            var areas = connector.AndoverService.GetAreas();
            var doorLists = connector.AndoverService.GetDoorLists();
            var personnels = connector.AndoverService.GetPersons();
            //var areaLinks = connector.AndoverService.GetAreaLinks();

            UpdateAreas(areas, containers);
            UpdateDoors(doors, areas, containers);
            UpdateSchedules(schedules, containers);
            UpdateCards(personnels, containers);

            return true;
        }

        private int DeleteOldData(AbstractTableWrapper wrapper, string idField)
        {
            int maxId = 0;
            foreach (DataRow row in wrapper.GetTable().Rows)
            {
                if ((int)row[idField] == 0)
                {
                    continue;
                }
                if ((int)row[idField] > maxId)
                {
                    maxId = (int)row[idField];
                }
                if ((string)row["f_deleted"] == "Y")
                {
                    continue;
                }
                row["f_deleted"] = "Y";

                int numRow = wrapper.GetTable().Rows.IndexOf(row);
                wrapper.UpdateRow(row.ItemArray, numRow, info);
            }
            return maxId;
        }

        private string GetPath(List<Container> containers, int? idHi, int? idLo)
        {
            var result = new List<string>();
            var container = containers.FirstOrDefault(c =>
                c.ObjectIdHi == (idHi.HasValue ? idHi.Value : 0) &&
                c.ObjectIdLo == (idLo.HasValue ? idLo.Value : 0));
            while (container != null)
            {
                result.Insert(0, container.UiName);
                container = containers.FirstOrDefault(c =>
                    c.ObjectIdHi == (container.OwnerIdHi.HasValue ?
                        container.OwnerIdHi.Value : 0) &&
                    c.ObjectIdLo == (container.OwnerIdLo.HasValue ?
                        container.OwnerIdLo.Value : 0));

            }
            var sb = new StringBuilder();
            foreach (var cont in result)
            {
                sb.Append(cont);
                sb.Append("\\");
            }
            return sb.ToString();
        }

        private bool UpdateAreas(List<Area> areas, List<Container> containers)
        {
            VisAreasTableWrapper areasTableWrapper =
                (VisAreasTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                    TableName.VisAreas);

            int maxId = DeleteOldData(areasTableWrapper, "f_area_id");

            DateTime date = DateTime.Now;
            foreach (var area in areas)
            {
                bool update = false;
                DataRow row = null;
                foreach (DataRow row2 in areasTableWrapper.GetTable().Rows)
                {
                    if (row2["f_object_id_hi"].Equals(area.ObjectIdHi) &&
                        row2["f_object_id_lo"].Equals(area.ObjectIdLo))
                    {
                        update = true;
                        row = row2;
                        break;
                    }
                }
                row = row ?? areasTableWrapper.GetTable().NewRow();
                if (!update)
                {
                    //// TODO - пока что генерим id сами
                    row["f_area_id"] = ++maxId;
                }
                else
                {
                    row.BeginEdit();
                }
                row["f_area_name"] = area.UiName;
                row["f_area_descript"] = "";
                row["f_deleted"] = "N";
                row["f_rec_date"] = date;
                row["f_rec_operator"] = info.Id;
                row["f_object_id_hi"] = area.ObjectIdHi;
                row["f_object_id_lo"] = area.ObjectIdLo;
                row["f_area_controller"] = area.ControllerName;
                row["f_area_path"] = GetPath(containers, area.OwnerIdHi, area.OwnerIdLo);
                row["f_area_data"] = ""; // TODO
                if (!update)
                {
                    areasTableWrapper.InsertRow(row.ItemArray, null);
                }
                else
                {
                    row.EndEdit();
                    int numRow = areasTableWrapper.GetTable().Rows.IndexOf(row);
                    areasTableWrapper.UpdateRow(row.ItemArray, numRow, info);
                }
            }

            return true;
        }

        private bool UpdateDoors(List<Door> doors, List<Area> areas, List<Container> containers)
        {
            VisAccessPointsTableWrapper doorsTableWrapper =
                (VisAccessPointsTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                    TableName.VisAccessPoints);

            int maxId = DeleteOldData(doorsTableWrapper, "f_access_point_id");

            DateTime date = DateTime.Now;
            foreach (var door in doors)
            {
                int spaceInIdHi = door.EntryAreaHi.HasValue ?
                    door.EntryAreaHi.Value : 0;
                int spaceInIdLo = door.EntryAreaLo.HasValue ?
                    door.EntryAreaLo.Value : 0;

                int spaceOutIdHi = door.ExitAreaHi.HasValue ?
                    door.ExitAreaHi.Value : 0;
                int spaceOutIdLo = door.ExitAreaLo.HasValue ?
                    door.ExitAreaLo.Value : 0;

                if (spaceInIdHi == 0 && spaceInIdLo == 0 &&
                    spaceOutIdHi == 0 && spaceOutIdLo == 0)
                {
                    continue;
                }

                Area areaIn = areas.FirstOrDefault(a =>
                    a.ObjectIdHi == spaceInIdHi && a.ObjectIdLo == spaceInIdLo);
                string spaceIn = areaIn != null ? areaIn.UiName : "";

                Area areaOut = areas.FirstOrDefault(a =>
                    a.ObjectIdHi == spaceOutIdHi && a.ObjectIdLo == spaceOutIdLo);
                string spaceOut = areaOut != null ? areaOut.UiName : "";

                bool update = false;
                DataRow row = null;
                foreach (DataRow row2 in doorsTableWrapper.GetTable().Rows)
                {
                    if (row2["f_object_id_hi"].Equals(door.ObjectIdHi) &&
                        row2["f_object_id_lo"].Equals(door.ObjectIdLo))
                    {
                        update = true;
                        row = row2;
                        break;
                    }
                }
                row = row ?? doorsTableWrapper.GetTable().NewRow();
                if (!update)
                {
                    //// TODO - пока что генерим id сами
                    row["f_access_point_id"] = ++maxId;
                }
                else
                {
                    row.BeginEdit();
                }
                row["f_access_point_name"] = door.UiName;
                row["f_access_point_description"] = "";
                row["f_access_point_space_in_id_hi"] = spaceInIdHi;
                row["f_access_point_space_in_id_lo"] = spaceInIdLo;
                row["f_access_point_space_out_id_hi"] = spaceOutIdHi;
                row["f_access_point_space_out_id_lo"] = spaceOutIdLo;
                row["f_access_point_space_in"] = spaceIn;
                row["f_access_point_space_out"] = spaceOut;
                row["f_deleted"] = "N";
                row["f_rec_date"] = date;
                row["f_rec_operator"] = info.Id;
                row["f_object_id_hi"] = door.ObjectIdHi;
                row["f_object_id_lo"] = door.ObjectIdLo;
                row["f_access_point_controller"] = door.ControllerName;
                row["f_access_point_path"] = GetPath(containers, door.OwnerIdHi, door.OwnerIdLo);
                row["f_access_point_data"] = ""; // TODO
                if (!update)
                {
                    doorsTableWrapper.InsertRow(row.ItemArray, null);
                }
                else
                {
                    row.EndEdit();
                    int numRow = doorsTableWrapper.GetTable().Rows.IndexOf(row);
                    doorsTableWrapper.UpdateRow(row.ItemArray, numRow, info);
                }
            }

            return true;
        }

        private bool UpdateSchedules(List<Schedule> schedules, List<Container> containers)
        {
            VisSchedulesTableWrapper schedulesTableWrapper =
                (VisSchedulesTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                    TableName.VisSchedules);

            int maxId = DeleteOldData(schedulesTableWrapper, "f_schedule_id");

            DateTime date = DateTime.Now;
            foreach (var schedule in schedules)
            {
                bool update = false;
                DataRow row = null;
                foreach (DataRow row2 in schedulesTableWrapper.GetTable().Rows)
                {
                    if (row2["f_object_id_hi"].Equals(schedule.ObjectIdHi) &&
                        row2["f_object_id_lo"].Equals(schedule.ObjectIdLo))
                    {
                        update = true;
                        row = row2;
                        break;
                    }
                }
                row = row ?? schedulesTableWrapper.GetTable().NewRow();
                if (!update)
                {
                    //// TODO - пока что генерим id сами
                    row["f_schedule_id"] = ++maxId;
                }
                else
                {
                    row.BeginEdit();
                }
                row["f_schedule_name"] = schedule.UiName;
                row["f_schedule_description"] = "";
                row["f_deleted"] = "N";
                row["f_rec_date"] = date;
                row["f_rec_operator"] = info.Id;
                row["f_object_id_hi"] = schedule.ObjectIdHi;
                row["f_object_id_lo"] = schedule.ObjectIdLo;
                row["f_schedule_controller"] = schedule.ControllerName;
                row["f_schedule_path"] = GetPath(containers, schedule.OwnerIdHi, schedule.OwnerIdLo);
                row["f_schedule_data"] = ""; // TODO
                if (!update)
                {
                    schedulesTableWrapper.InsertRow(row.ItemArray, null);
                }
                else
                {
                    row.EndEdit();
                    int numRow = schedulesTableWrapper.GetTable().Rows.IndexOf(row);
                    schedulesTableWrapper.UpdateRow(row.ItemArray, numRow, info);
                }
            }

            return true;
        }

        private bool UpdateCards(List<Personnel> personnels, List<Container> containers)
        {
            VisCardsTableWrapper cardsTableWrapper =
                (VisCardsTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                    TableName.VisCards);

            int maxId = DeleteOldData(cardsTableWrapper, "f_card_id");

            DateTime date = DateTime.Now;
            foreach (var person in personnels)
            {
                bool update = false;
                DataRow row = null;
                foreach (DataRow row2 in cardsTableWrapper.GetTable().Rows)
                {
                    if (row2["f_object_id_hi"].Equals(person.ObjectIdHi) &&
                        row2["f_object_id_lo"].Equals(person.ObjectIdLo))
                    {
                        update = true;
                        row = row2;
                        break;
                    }
                }
                row = row ?? cardsTableWrapper.GetTable().NewRow();
                if (!update)
                {
                    //// TODO - пока что генерим id сами
                    row["f_card_id"] = ++maxId;
                }
                else
                {
                    row.BeginEdit();
                }
                row["f_state_id"] = 1;
                row["f_card_name"] = person.UiName;
                row["f_card_text"] = "";
                row["f_last_visit_id"] = 0;
                row["f_deleted"] = "N";
                row["f_rec_date"] = date;
                row["f_rec_operator"] = info.Id;
                row["f_create_date"] = person.ActivationDate;
                row["f_rec_date"] = date;
                row["f_lost_date"] = DateTime.MinValue;
                row["f_comment"] = "";
                row["f_card_num"] = person.NonABACardNumber;
                row["f_object_id_hi"] = person.ObjectIdHi;
                row["f_object_id_lo"] = person.ObjectIdLo;
                row["f_card_controller"] = person.ControllerName;
                row["f_card_path"] = GetPath(containers, person.OwnerIdHi, person.OwnerIdLo);
                row["f_card_data"] = ""; // TODO
                if (!update)
                {
                    cardsTableWrapper.InsertRow(row.ItemArray, null);
                }
                else
                {
                    row.EndEdit();
                    int numRow = cardsTableWrapper.GetTable().Rows.IndexOf(row);
                    cardsTableWrapper.UpdateRow(row.ItemArray, numRow, info);
                }
            }

            return true;
        }

        private bool ExportData(AndoverExportData data)
        {
            AndoverConnector connector = AndoverConnector.CurrentConnector;

            VisCardsTableWrapper cardsTableWrapper =
               (VisCardsTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                   TableName.VisCards);

            DataRow card = null;
            foreach (DataRow row in cardsTableWrapper.GetTable().Rows)
            {
                if ((string)row["f_card_name"] == data.Card)
                {
                    card = row;
                    break;
                }
            }
            if (card == null)
            {
                return false;
            }

            var areaList = new List<DataRow>();
            if (data.Doors.Any())
            {
                VisAccessPointsTableWrapper doorsTableWrapper =
                    (VisAccessPointsTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                        TableName.VisAccessPoints);

                var doorList = new List<DataRow>();
                foreach (DataRow row in doorsTableWrapper.GetTable().Rows)
                {
                    if (data.Doors.Contains((string)row["f_access_point_name"]))
                    {
                        doorList.Add(row);
                    }
                }
                if (doorList.Any())
                {
                    VisAreasTableWrapper areasTableWrapper =
                        (VisAreasTableWrapper)VisAreasTableWrapper.GetTableWrapper(
                            TableName.VisAreas);
                    foreach (DataRow row in areasTableWrapper.GetTable().Rows)
                    {
                        if ((int)row["f_area_id"] == 0)
                        {
                            continue;
                        }
                        DataRow r = doorList.Find(d =>
                            (int)d["f_access_point_space_in_id_hi"] ==
                            (int)row["f_object_id_hi"] &&
                            (int)d["f_access_point_space_in_id_lo"] ==
                            (int)row["f_object_id_lo"] ||
                            (int)d["f_access_point_space_out_id_hi"] ==
                            (int)row["f_object_id_hi"] &&
                            (int)d["f_access_point_space_out_id_lo"] ==
                            (int)row["f_object_id_lo"]);
                        if (r != null)
                        {
                            areaList.Add(row);
                        }
                    }
                }
            }

            var schedulesList = new List<DataRow>(); // TODO

            var info = new PersonInfo
            {
                UiName = (string)card["f_card_name"],
                Path = (string)card["f_card_path"],
                Alias = (string)card["f_card_controller"],
                Areas = areaList.Select(a =>
                    (string)a["f_area_path"] + (string)a["f_area_name"]).ToList(),
                Schedules = schedulesList.Select(a =>
                    (string)a["f_schedule_name"]).ToList(),
            };
            bool result = connector.AndoverService.ExportPersonDmp(info);

            return result;
        }
    }
}
