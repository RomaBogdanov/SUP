using AndoverLib;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace AndoverImportTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoWork();
                Console.WriteLine("SUCCESS");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        static void DoWork()
        {
            var binding = new WSDualHttpBinding()
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = 2147483647,
                    MaxStringContentLength = 2147483647
                }
            };
            var myChannelFactory = new ChannelFactory<IAndoverService>(
                binding,
                new EndpointAddress("http://localhost:7001/AndoverHost"));
            IAndoverService wcfClient = myChannelFactory.CreateChannel();

            var areas = wcfClient.GetAreas();

            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from Area";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var area in areas)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into Area " +
                            "(ObjectIdHi, ObjectIdLo, uiName, OwnerIdHi, OwnerIdLo, " +
                            "DeviceIdHi, DeviceIdLo, TemplateFlag, TemplateIdHi, TemplateIdLo, " +
                            "ControllerName, AlarmGraphicPage, DeletePending, " +
                            "Description, KnownOccupCount, State, ForceLock) " +
                            "values (" +
                            area.ObjectIdHi + ", " +
                            area.ObjectIdLo + ", " +
                            Convert(area.UiName) + ", " +
                            Convert(area.OwnerIdHi) + ", " +
                            Convert(area.OwnerIdLo) + ", " +
                            Convert(area.DeviceIdHi) + ", " +
                            Convert(area.DeviceIdLo) + ", " +
                            Convert(area.TemplateFlag) + ", " +
                            Convert(area.TemplateIdHi) + ", " +
                            Convert(area.TemplateIdLo) + ", " +
                            Convert(area.ControllerName) + ", " +
                            Convert(area.AlarmGraphicPage) + ", " +
                            Convert(area.DeletePending) + ", " +
                            Convert(area.Description) + ", " +
                            Convert(area.KnownOccupCount) + ", " +
                            Convert(area.State) + ", " +
                            Convert(area.ForceLock) + ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            var persons = wcfClient.GetPersons();
            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from Personnel";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var person in persons)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into Personnel " +
                            "(ObjectIdHi, ObjectIdLo, uiName, OwnerIdHi, OwnerIdLo, " +
                            "DeviceIdHi, DeviceIdLo, TemplateFlag, TemplateIdHi, TemplateIdLo, " +
                            "ControllerName, AlarmGraphicPage, ActivationDate, " +
                            "SavedActivationDate, ADA, Address, AllowEntEntEgr, " +
                            "Blood, CardType, CardType2, SiteCode, SiteCode2, " +
                            "CardNumber, CardNumber2, SavedCardType, City, Country, " +
                            "CustomControl1, CustomControl2, CustomControl3, " +
                            "DateOfBirth, DeletePending, Department, DepartmentCode, " +
                            "DistFailed, Duress, EmergencyContact, EmergencyPhone, " +
                            "EmpNumber, EntryEgress, ExpirationDate, SavedExpirationDate, " +
                            "EyeColor, FirstName, HairColor, Height, HomePhone, " +
                            "InactiveDisableDays, Info1, Info2, Info3, Info4, Info5, Info6, " +
                            "JobTitle, LastName, LicenseNumber, LostCard, MiddleName, " +
                            "OfficeLocation, ParkingSticker, PhotoFile, PIN, SavedPIN, " +
                            "SavedCardNumber, SavedSiteCode, Sex, Signature, SocSecNo, " +
                            "StartDate, State, SavedState, StateOfResidence, Supervisor, " +
                            "TimeEntered, ValueHi, ValueLo, VehicalInfo, Visitor, " +
                            "Weight, WorkPhone, Zip, Zone, ZonePointHi, ZonePointLo, " +
                            "NonABACardNumber, NonABACardNumber2, BLOB_Template, ExecutivePrivilege, " +
                            "DefaultClearanceLevel, FipsAgencyCode, FipsOrgId, FipsHmac, " +
                            "FipsSystemCode, FipsCredentialNumber, FipsPersonId, FipsCredentialSeries, " +
                            "FipsCredentialIssue, FipsOrgCategory, FipsPersonOrgAssociation, " +
                            "FipsExpirationDate, FipsPivControlled, FipsPivState, " +
                            "SavedCardType2, SavedCardNumber2, SavedSiteCode2, " +
                            "CardField1, CardField2, CardField3, " +
                            "CardField4, CardField5, CardField6" +
                            ") values (" +
                            person.ObjectIdHi + ", " +
                            person.ObjectIdLo + ", " +
                            Convert(person.UiName) + ", " +
                            Convert(person.OwnerIdHi) + ", " +
                            Convert(person.OwnerIdLo) + ", " +
                            Convert(person.DeviceIdHi) + ", " +
                            Convert(person.DeviceIdLo) + ", " +
                            Convert(person.TemplateFlag) + ", " +
                            Convert(person.TemplateIdHi) + ", " +
                            Convert(person.TemplateIdLo) + ", " +
                            Convert(person.ControllerName) + ", " +
                            Convert(person.AlarmGraphicPage) + ", " +
                            Convert(person.ActivationDate) + ", " +
                            Convert(person.SavedActivationDate) + ", " +
                            Convert(person.ADA) + ", " +
                            Convert(person.Address) + ", " +
                            Convert(person.AllowEntEntEgr) + ", " +
                            Convert(person.Blood) + ", " +
                            Convert(person.CardType) + ", " +
                            Convert(person.CardType2) + ", " +
                            Convert(person.SiteCode) + ", " +
                            Convert(person.SiteCode2) + ", " +
                            Convert(person.CardNumber) + ", " +
                            Convert(person.CardNumber2) + ", " +
                            Convert(person.SavedCardType) + ", " +
                            Convert(person.City) + ", " +
                            Convert(person.Country) + ", " +
                            Convert(person.CustomControl1) + ", " +
                            Convert(person.CustomControl2) + ", " +
                            Convert(person.CustomControl3) + ", " +
                            Convert(person.DateOfBirth) + ", " +
                            Convert(person.DeletePending) + ", " +
                            Convert(person.Department) + ", " +
                            Convert(person.DepartmentCode) + ", " +
                            Convert(person.DistFailed) + ", " +
                            Convert(person.Duress) + ", " +
                            Convert(person.EmergencyContact) + ", " +
                            Convert(person.EmergencyPhone) + ", " +
                            Convert(person.EmpNumber) + ", " +
                            Convert(person.EntryEgress) + ", " +
                            Convert(person.ExpirationDate) + ", " +
                            Convert(person.SavedExpirationDate) + ", " +
                            Convert(person.EyeColor) + ", " +
                            Convert(person.FirstName) + ", " +
                            Convert(person.HairColor) + ", " +
                            Convert(person.Height) + ", " +
                            Convert(person.HomePhone) + ", " +
                            Convert(person.InactiveDisableDays) + ", " +
                            Convert(person.Info1) + ", " +
                            Convert(person.Info2) + ", " +
                            Convert(person.Info3) + ", " +
                            Convert(person.Info4) + ", " +
                            Convert(person.Info5) + ", " +
                            Convert(person.Info6) + ", " +
                            Convert(person.JobTitle) + ", " +
                            Convert(person.LastName) + ", " +
                            Convert(person.LicenseNumber) + ", " +
                            Convert(person.LostCard) + ", " +
                            Convert(person.MiddleName) + ", " +
                            Convert(person.OfficeLocation) + ", " +
                            Convert(person.ParkingSticker) + ", " +
                            Convert(person.PhotoFile) + ", " +
                            Convert(person.PIN) + ", " +
                            Convert(person.SavedPIN) + ", " +
                            Convert(person.SavedCardNumber) + ", " +
                            Convert(person.SavedSiteCode) + ", " +
                            Convert(person.Sex) + ", " +
                            Convert(person.Signature) + ", " +
                            Convert(person.SocSecNo) + ", " +
                            Convert(person.StartDate) + ", " +
                            Convert(person.State) + ", " +
                            Convert(person.SavedState) + ", " +
                            Convert(person.StateOfResidence) + ", " +
                            Convert(person.Supervisor) + ", " +
                            Convert(person.TimeEntered) + ", " +
                            Convert(person.ValueHi) + ", " +
                            Convert(person.ValueLo) + ", " +
                            Convert(person.VehicalInfo) + ", " +
                            Convert(person.Visitor) + ", " +
                            Convert(person.Weight) + ", " +
                            Convert(person.WorkPhone) + ", " +
                            Convert(person.Zip) + ", " +
                            Convert(person.Zone) + ", " +
                            Convert(person.ZonePointHi) + ", " +
                            Convert(person.ZonePointLo) + ", " +
                            Convert(person.NonABACardNumber) + ", " +
                            Convert(person.NonABACardNumber2) + ", " +
                            Convert(person.BLOB_Template) + ", " +
                            Convert(person.ExecutivePrivilege) + ", " +
                            Convert(person.DefaultClearanceLevel) + ", " +
                            Convert(person.FipsAgencyCode) + ", " +
                            Convert(person.FipsOrgId) + ", " +
                            Convert(person.FipsHmac) + ", " +
                            Convert(person.FipsSystemCode) + ", " +
                            Convert(person.FipsCredentialNumber) + ", " +
                            Convert(person.FipsPersonId) + ", " +
                            Convert(person.FipsCredentialSeries) + ", " +
                            Convert(person.FipsCredentialIssue) + ", " +
                            Convert(person.FipsOrgCategory) + ", " +
                            Convert(person.FipsPersonOrgAssociation) + ", " +
                            Convert(person.FipsExpirationDate) + ", " +
                            Convert(person.FipsPivControlled) + ", " +
                            Convert(person.FipsPivState) + ", " +
                            Convert(person.SavedCardType2) + ", " +
                            Convert(person.SavedCardNumber2) + ", " +
                            Convert(person.SavedSiteCode2) + ", " +
                            Convert(person.CardField1) + ", " +
                            Convert(person.CardField2) + ", " +
                            Convert(person.CardField3) + ", " +
                            Convert(person.CardField4) + ", " +
                            Convert(person.CardField5) + ", " +
                            Convert(person.CardField6) +
                            ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            var schedules = wcfClient.GetSchedules();

            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from Schedule";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var schedule in schedules)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into Schedule " +
                            "(ObjectIdHi, ObjectIdLo, uiName, OwnerIdHi, OwnerIdLo, " +
                            "DeviceIdHi, DeviceIdLo, TemplateFlag, TemplateIdHi, TemplateIdLo, " +
                            "ControllerName, AlarmGraphicPage, Value, Note, " +
                            "Description, EffectivePeriod, WeeklySchedule, ExceptionSched, " +
                            "PropertyRef, Priority, OccupancyTime, UnoccupancyTime, ActiveText, " +
                            "InactiveText, State, CalenderRefList, WeeklySchNotes, ExcSchedNotes, " +
                            "ActiveValue, InactiveValue, PackagedDays, DownloadFlag, LastDownloadTime, " +
                            "ScheduleType, OccTimePointHi, OccTimePointLo, UnOccTimePointHi, UnOccTimePointLo, " +
                            "AutosendFlag, AutosendTime, UnavailableAttributes, SpecialEventName, " +
                            "TimeScale, ScheduleDefault, DefaultDataType, OutOfService, " +
                            "ApplyMidnightValue, DefaultMidnightValue, ClearPastEvents" +
                            ") values (" +
                            schedule.ObjectIdHi + ", " +
                            schedule.ObjectIdLo + ", " +
                            Convert(schedule.UiName) + ", " +
                            Convert(schedule.OwnerIdHi) + ", " +
                            Convert(schedule.OwnerIdLo) + ", " +
                            Convert(schedule.DeviceIdHi) + ", " +
                            Convert(schedule.DeviceIdLo) + ", " +
                            Convert(schedule.TemplateFlag) + ", " +
                            Convert(schedule.TemplateIdHi) + ", " +
                            Convert(schedule.TemplateIdLo) + ", " +
                            Convert(schedule.ControllerName) + ", " +
                            Convert(schedule.AlarmGraphicPage) + ", " +
                            Convert(schedule.Value) + ", " +
                            Convert(schedule.Note) + ", " +
                            Convert(schedule.Description) + ", " +
                            Convert(schedule.EffectivePeriod) + ", " +
                            Convert(schedule.WeeklySchedule) + ", " +
                            Convert(schedule.ExceptionSched) + ", " +
                            Convert(schedule.PropertyRef) + ", " +
                            Convert(schedule.Priority) + ", " +
                            Convert(schedule.OccupancyTime) + ", " +
                            Convert(schedule.UnoccupancyTime) + ", " +
                            Convert(schedule.ActiveText) + ", " +
                            Convert(schedule.InactiveText) + ", " +
                            Convert(schedule.State) + ", " +
                            Convert(schedule.CalenderRefList) + ", " +
                            Convert(schedule.WeeklySchNotes) + ", " +
                            Convert(schedule.ExcSchedNotes) + ", " +
                            Convert(schedule.ActiveValue) + ", " +
                            Convert(schedule.InactiveValue) + ", " +
                            Convert(schedule.PackagedDays) + ", " +
                            Convert(schedule.DownloadFlag) + ", " +
                            Convert(schedule.LastDownloadTime) + ", " +
                            Convert(schedule.ScheduleType) + ", " +
                            Convert(schedule.OccTimePointHi) + ", " +
                            Convert(schedule.OccTimePointLo) + ", " +
                            Convert(schedule.UnOccTimePointHi) + ", " +
                            Convert(schedule.UnOccTimePointLo) + ", " +
                            Convert(schedule.AutosendFlag) + ", " +
                            Convert(schedule.AutosendTime) + ", " +
                            Convert(schedule.UnavailableAttributes) + ", " +
                            Convert(schedule.SpecialEventName) + ", " +
                            Convert(schedule.TimeScale) + ", " +
                            Convert(schedule.ScheduleDefault) + ", " +
                            Convert(schedule.DefaultDataType) + ", " +
                            Convert(schedule.OutOfService) + ", " +
                            Convert(schedule.ApplyMidnightValue) + ", " +
                            Convert(schedule.DefaultMidnightValue) + ", " +
                            Convert(schedule.ClearPastEvents) +
                            ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            var areaLinks = wcfClient.GetAreaLinks();

            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from AreaLink";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var areaLink in areaLinks)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into AreaLink " +
                            "(ObjectIdHi, ObjectIdLo, AreaIdHi, AreaIdLo, PersonIdHi, PersonIdLo, " +
                            "Preload, SchedIdHi, SchedIdLo, State, TimeEntered, DistPending, " +
                            "DeletePending, DistTime, TemplateFlag, ClearanceLevel" +
                            ") values (" +
                            areaLink.ObjectIdHi + ", " +
                            areaLink.ObjectIdLo + ", " +
                            areaLink.AreaIdHi + ", " +
                            areaLink.AreaIdLo + ", " +
                            areaLink.PersonIdHi + ", " +
                            areaLink.PersonIdLo + ", " +
                            Convert(areaLink.Preload) + ", " +
                            Convert(areaLink.SchedIdHi) + ", " +
                            Convert(areaLink.SchedIdLo) + ", " +
                            Convert(areaLink.State) + ", " +
                            Convert(areaLink.TimeEntered) + ", " +
                            Convert(areaLink.DistPending) + ", " +
                            Convert(areaLink.DeletePending) + ", " +
                            Convert(areaLink.DistTime) + ", " +
                            Convert(areaLink.TemplateFlag) + ", " +
                            Convert(areaLink.ClearanceLevel) +
                            ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            var doors = wcfClient.GetDoors();

            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from Door";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var door in doors)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into Door " +
                            "(ObjectIdHi, ObjectIdLo, uiName, OwnerIdHi, OwnerIdLo, " +
                            "DeviceIdHi, DeviceIdLo, TemplateFlag, TemplateIdHi, TemplateIdLo, " +
                            "ControllerName, AlarmGraphicPage, ADADoorAjarTime, ADAOutputTime, " +
                            "ADAChannel, AlarmChannel, AlarmRelayTime, ArmCode, " +
                            "BondChannel, BondType, BondSensor, CardFormats, CardFormats2, " +
                            "Description, DoorAjarTime, DoorChannel, DoorScheduleHi, DoorScheduleLo, " +
                            "DoorStrikeTime, DoorSwitchChan, DoorSwitchType, " +
                            "EntryAntiPassTim, EntryAreaHi, EntryAreaLo, EntryIOU, EntryChannel, " +
                            "EntryEntEgr, EntryEntrAntiPas,  EntryEntrEntEgr, EntryEntrRvrsCrd, " +
                            "EntryKyPdChan, EntryNoCommMode, EntryNoDataMode, EntryNoReEntry, " +
                            "EntryNormMode, EntryPinDuress, EntryRvrsCrdDur, EntryScheduleHi, " +
                            "EntryScheduleLo, EntryZone, SvEntryZone, " +
                            "ExitAntiPassTim, ExitAreaHi, ExitAreaLo, ExitIOU, ExitChannel, " +
                            "ExitEntEgr, ExitEntrAntiPas,  ExitEntrEntEgr, ExitEntrRvrsCrd, " +
                            "ExitKyPdChan, ExitNoCommMode, ExitNoDataMode, ExitNoReEntry, " +
                            "ExitNormMode, ExitPinDuress, ExitRequestChan, " +
                            "ExitRequestType, ExitRvrsCrdDur, ExitScheduleHi, " +
                            "ExitScheduleLo, ExitZone, SvExitZone, " +
                            "Export, GeneralCode, Invert, LastDepEntrdPntHi, LastDepEntrdPntLo, LastDepExitdPntHi, LastDepExitdPntLo, " +
                            "OpenOnExitReqst, Port, RecordValNoEntryHist, RecordDrAjarHist, RecordExitRqHist, " +
                            "RecordForcedHist, RecordInValHist, RecordValHist, RelockOnClose, " +
                            "Site1, Site2, Site3, Site4, State, " +
                            "UnlockScheduleHi, UnlockScheduleLo, OperatingMode, AlarmValue, " +
                            "ADAExitRqstChan, ADAExitRqstType, ADAInputChan, ADAInputType, " +
                            "InfRefPoint1, InfRefPoint2, InfRefPoint3, InfRefPoint4, " +
                            "DbEntryZone, DbExitZone, ForcedEntryDelay, VideoLayoutIdHi, VideoLayoutIdLo, " +
                            "InfCameraPoint1, InfCameraPoint2, InfCameraPoint3, InfCameraPoint4, " +
                            "InfCameraPoint5, InfCameraPoint6, InfCameraPoint7, InfCameraPoint8, " +
                            "InfCameraPoint9, InfCameraPoint10, InfCameraPoint11, InfCameraPoint12, " +
                            "InfCameraPoint13, InfCameraPoint14, InfCameraPoint15, InfCameraPoint16, " +
                            "ForceLock" +
                            ") values (" +
                            door.ObjectIdHi + ", " +
                            door.ObjectIdLo + ", " +
                            Convert(door.UiName) + ", " +
                            Convert(door.OwnerIdHi) + ", " +
                            Convert(door.OwnerIdLo) + ", " +
                            Convert(door.DeviceIdHi) + ", " +
                            Convert(door.DeviceIdLo) + ", " +
                            Convert(door.TemplateFlag) + ", " +
                            Convert(door.TemplateIdHi) + ", " +
                            Convert(door.TemplateIdLo) + ", " +
                            Convert(door.ControllerName) + ", " +
                            Convert(door.AlarmGraphicPage) + ", " +
                            Convert(door.ADADoorAjarTime) + ", " +
                            Convert(door.ADAOutputTime) + ", " +
                            Convert(door.ADAChannel) + ", " +
                            Convert(door.AlarmChannel) + ", " +
                            Convert(door.AlarmRelayTime) + ", " +
                            Convert(door.ArmCode) + ", " +
                            Convert(door.BondChannel) + ", " +
                            Convert(door.BondType) + ", " +
                            Convert(door.BondSensor) + ", " +
                            Convert(door.CardFormats) + ", " +
                            Convert(door.CardFormats2) + ", " +
                            Convert(door.Description) + ", " +
                            Convert(door.DoorAjarTime) + ", " +
                            Convert(door.DoorChannel) + ", " +
                            Convert(door.DoorScheduleHi) + ", " +
                            Convert(door.DoorScheduleLo) + ", " +
                            Convert(door.DoorStrikeTime) + ", " +
                            Convert(door.DoorSwitchChan) + ", " +
                            Convert(door.DoorSwitchType) + ", " +
                            Convert(door.EntryAntiPassTim) + ", " +
                            Convert(door.EntryAreaHi) + ", " +
                            Convert(door.EntryAreaLo) + ", " +
                            Convert(door.EntryIOU) + ", " +
                            Convert(door.EntryChannel) + ", " +
                            Convert(door.EntryEntEgr) + ", " +
                            Convert(door.EntryEntrAntiPas) + ", " +
                            Convert(door.EntryEntrEntEgr) + ", " +
                            Convert(door.EntryEntrRvrsCrd) + ", " +
                            Convert(door.EntryKyPdChan) + ", " +
                            Convert(door.EntryNoCommMode) + ", " +
                            Convert(door.EntryNoDataMode) + ", " +
                            Convert(door.EntryNoReEntry) + ", " +
                            Convert(door.EntryNormMode) + ", " +
                            Convert(door.EntryPinDuress) + ", " +
                            Convert(door.EntryRvrsCrdDur) + ", " +
                            Convert(door.EntryScheduleHi) + ", " +
                            Convert(door.EntryScheduleLo) + ", " +
                            Convert(door.EntryZone) + ", " +
                            Convert(door.SvEntryZone) + ", " +
                            Convert(door.ExitAntiPassTim) + ", " +
                            Convert(door.ExitAreaHi) + ", " +
                            Convert(door.ExitAreaLo) + ", " +
                            Convert(door.ExitIOU) + ", " +
                            Convert(door.ExitChannel) + ", " +
                            Convert(door.ExitEntEgr) + ", " +
                            Convert(door.ExitEntrAntiPas) + ", " +
                            Convert(door.ExitEntrEntEgr) + ", " +
                            Convert(door.ExitEntrRvrsCrd) + ", " +
                            Convert(door.ExitKyPdChan) + ", " +
                            Convert(door.ExitNoCommMode) + ", " +
                            Convert(door.ExitNoDataMode) + ", " +
                            Convert(door.ExitNoReEntry) + ", " +
                            Convert(door.ExitNormMode) + ", " +
                            Convert(door.ExitPinDuress) + ", " +
                            Convert(door.ExitRequestChan) + ", " +
                            Convert(door.ExitRequestType) + ", " +
                            Convert(door.ExitRvrsCrdDur) + ", " +
                            Convert(door.ExitScheduleHi) + ", " +
                            Convert(door.ExitScheduleLo) + ", " +
                            Convert(door.ExitZone) + ", " +
                            Convert(door.SvExitZone) + ", " +
                            Convert(door.Export) + ", " +
                            Convert(door.GeneralCode) + ", " +
                            Convert(door.Invert) + ", " +
                            Convert(door.LastDepEntrdPntHi) + ", " +
                            Convert(door.LastDepEntrdPntLo) + ", " +
                            Convert(door.LastDepExitdPntHi) + ", " +
                            Convert(door.LastDepExitdPntLo) + ", " +
                            Convert(door.OpenOnExitReqst) + ", " +
                            Convert(door.Port) + ", " +
                            Convert(door.RecordValNoEntryHist) + ", " +
                            Convert(door.RecordDrAjarHist) + ", " +
                            Convert(door.RecordExitRqHist) + ", " +
                            Convert(door.RecordForcedHist) + ", " +
                            Convert(door.RecordInValHist) + ", " +
                            Convert(door.RecordValHist) + ", " +
                            Convert(door.RelockOnClose) + ", " +
                            Convert(door.Site1) + ", " +
                            Convert(door.Site2) + ", " +
                            Convert(door.Site3) + ", " +
                            Convert(door.Site4) + ", " +
                            Convert(door.State) + ", " +
                            Convert(door.UnlockScheduleHi) + ", " +
                            Convert(door.UnlockScheduleLo) + ", " +
                            Convert(door.OperatingMode) + ", " +
                            Convert(door.AlarmValue) + ", " +
                            Convert(door.ADAExitRqstChan) + ", " +
                            Convert(door.ADAExitRqstType) + ", " +
                            Convert(door.ADAInputChan) + ", " +
                            Convert(door.ADAInputType) + ", " +
                            Convert(door.InfRefPoint1) + ", " +
                            Convert(door.InfRefPoint2) + ", " +
                            Convert(door.InfRefPoint3) + ", " +
                            Convert(door.InfRefPoint4) + ", " +
                            Convert(door.DbEntryZone) + ", " +
                            Convert(door.DbExitZone) + ", " +
                            Convert(door.ForcedEntryDelay) + ", " +
                            Convert(door.VideoLayoutIdHi) + ", " +
                            Convert(door.VideoLayoutIdLo) + ", " +
                            Convert(door.InfCameraPoint1) + ", " +
                            Convert(door.InfCameraPoint2) + ", " +
                            Convert(door.InfCameraPoint3) + ", " +
                            Convert(door.InfCameraPoint4) + ", " +
                            Convert(door.InfCameraPoint5) + ", " +
                            Convert(door.InfCameraPoint6) + ", " +
                            Convert(door.InfCameraPoint7) + ", " +
                            Convert(door.InfCameraPoint8) + ", " +
                            Convert(door.InfCameraPoint9) + ", " +
                            Convert(door.InfCameraPoint10) + ", " +
                            Convert(door.InfCameraPoint11) + ", " +
                            Convert(door.InfCameraPoint12) + ", " +
                            Convert(door.InfCameraPoint13) + ", " +
                            Convert(door.InfCameraPoint14) + ", " +
                            Convert(door.InfCameraPoint15) + ", " +
                            Convert(door.InfCameraPoint16) + ", " +
                            Convert(door.ForceLock) +
                            ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            var doorLists = wcfClient.GetDoorLists();

            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "ContinuumCopy"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from DoorList";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                foreach (var doorList in doorLists)
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into DoorList " +
                            "(ObjectIdHi, ObjectIdLo, DoorIdHi, DoorIdLo, AreaIdHi, AreaIdLo, " +
                            "DeviceIdHi, DeviceIdLo, NetworkIdHi, NetworkIdLo" +
                            ") values (" +
                            doorList.ObjectIdHi + ", " +
                            doorList.ObjectIdLo + ", " +
                            doorList.DoorIdHi + ", " +
                            doorList.DoorIdLo + ", " +
                            doorList.AreaIdHi + ", " +
                            doorList.AreaIdLo + ", " +
                            Convert(doorList.DeviceIdHi) + ", " +
                            Convert(doorList.DeviceIdLo) + ", " +
                            Convert(doorList.NetworkIdHi) + ", " +
                            Convert(doorList.NetworkIdLo) +
                            ")";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(cmd.CommandText);
                    }
                }
            }

            Console.WriteLine("END");
        }

        private static string Convert(string value)
        {
            return value == null ? "NULL" : "'" + value + "'";
        }

        private static string Convert(int? value)
        {
            return value.HasValue ? value.Value.ToString() : "NULL";
        }

        private static string Convert(short? value)
        {
            return value.HasValue ? value.Value.ToString() : "NULL";
        }

        private static string Convert(byte? value)
        {
            return value.HasValue ? value.Value.ToString() : "NULL";
        }

        private static string Convert(float? value)
        {
            return value.HasValue ? value.Value.ToString() : "NULL";
        }

        private static string Convert(bool? value)
        {
            return value.HasValue ? (value.Value ? "1" : "0") : "NULL";
        }

        private static string Convert(DateTime? value)
        {
            if (!value.HasValue)
            {
                return "NULL";
            }
            return "'" + value.Value.Day.ToString("G2") + "-" +
                value.Value.Month.ToString("G2") + "-" +
                value.Value.Year.ToString("G4") + " " +
                value.Value.Hour.ToString("G2") + ":" +
                value.Value.Minute.ToString("G2") + ":" +
                value.Value.Second.ToString("G2") + "'";
        }

        private static string Convert(byte[] value)
        {
            if (value == null)
            {
                return "NULL";
            }
            var sb = new StringBuilder();
            sb.Append("0x");
            foreach (var bt in value)
            {
                sb.Append(bt.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
