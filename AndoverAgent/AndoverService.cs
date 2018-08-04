using AndoverLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace AndoverAgent
{
    public class AndoverService : IAndoverService
    {
        public string Ping()
        {
            Console.WriteLine("Ping executed");
            return "OK";
        }

        public List<Container> GetContainers()
        {
            Console.WriteLine("GetContainers() executed");

            var result = new List<Container>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Container";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Container
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    ContainerType = reader["ContainerType"] is DBNull ?
                                        null : (string)reader["ContainerType"],
                                    DefaultControllerIdHi = reader["DefaultControllerIdHi"] is DBNull ?
                                        null : (int?)reader["DefaultControllerIdHi"],
                                    DefaultControllerIdLo = reader["DefaultControllerIdLo"] is DBNull ?
                                        null : (int?)reader["DefaultControllerIdLo"],
                                    ContainerCreateRuleIdHi = reader["ContainerCreateRuleIdHi"] is DBNull ?
                                        null : (int?)reader["ContainerCreateRuleIdHi"],
                                    ContainerCreateRuleIdLo = reader["ContainerCreateRuleIdLo"] is DBNull ?
                                        null : (int?)reader["ContainerCreateRuleIdLo"],
                                    VideoLayoutIdHi = reader["VideoLayoutIdHi"] is DBNull ?
                                        null : (int?)reader["VideoLayoutIdHi"],
                                    VideoLayoutIdLo = reader["VideoLayoutIdLo"] is DBNull ?
                                        null : (int?)reader["VideoLayoutIdLo"],
                                    InfCameraPoint1 = reader["InfCameraPoint1"] is DBNull ?
                                        null : (string)reader["InfCameraPoint1"],
                                    InfCameraPoint2 = reader["InfCameraPoint2"] is DBNull ?
                                        null : (string)reader["InfCameraPoint2"],
                                    InfCameraPoint3 = reader["InfCameraPoint3"] is DBNull ?
                                        null : (string)reader["InfCameraPoint3"],
                                    InfCameraPoint4 = reader["InfCameraPoint4"] is DBNull ?
                                        null : (string)reader["InfCameraPoint4"],
                                    InfCameraPoint5 = reader["InfCameraPoint5"] is DBNull ?
                                        null : (string)reader["InfCameraPoint5"],
                                    InfCameraPoint6 = reader["InfCameraPoint6"] is DBNull ?
                                        null : (string)reader["InfCameraPoint6"],
                                    InfCameraPoint7 = reader["InfCameraPoint7"] is DBNull ?
                                        null : (string)reader["InfCameraPoint7"],
                                    InfCameraPoint8 = reader["InfCameraPoint8"] is DBNull ?
                                        null : (string)reader["InfCameraPoint8"],
                                    InfCameraPoint9 = reader["InfCameraPoint9"] is DBNull ?
                                        null : (string)reader["InfCameraPoint9"],
                                    InfCameraPoint10 = reader["InfCameraPoint10"] is DBNull ?
                                        null : (string)reader["InfCameraPoint10"],
                                    InfCameraPoint11 = reader["InfCameraPoint11"] is DBNull ?
                                        null : (string)reader["InfCameraPoint11"],
                                    InfCameraPoint12 = reader["InfCameraPoint12"] is DBNull ?
                                        null : (string)reader["InfCameraPoint12"],
                                    InfCameraPoint13 = reader["InfCameraPoint13"] is DBNull ?
                                        null : (string)reader["InfCameraPoint13"],
                                    InfCameraPoint14 = reader["InfCameraPoint14"] is DBNull ?
                                        null : (string)reader["InfCameraPoint14"],
                                    InfCameraPoint15 = reader["InfCameraPoint15"] is DBNull ?
                                        null : (string)reader["InfCameraPoint15"],
                                    InfCameraPoint16 = reader["InfCameraPoint16"] is DBNull ?
                                        null : (string)reader["InfCameraPoint16"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Device> GetDevices()
        {
            Console.WriteLine("GetDevices() executed");

            var result = new List<Device>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Device";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Device
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    Description = reader["Description"] is DBNull ?
                                        null : (string)reader["Description"],
                                    APDUSegTimeout = reader["APDUSegTimeout"] is DBNull ?
                                        null : (int?)reader["APDUSegTimeout"],
                                    TimeSyncRecipients = reader["TimeSyncRecipients"] is DBNull ?
                                        null : (byte[])reader["TimeSyncRecipients"],
                                    APDUTimeout = reader["APDUTimeout"] is DBNull ?
                                        null : (int?)reader["APDUTimeout"],
                                    ApplSoftVer = reader["ApplSoftVer"] is DBNull ?
                                        null : (string)reader["ApplSoftVer"],
                                    FirmRev = reader["FirmRev"] is DBNull ?
                                        null : (string)reader["FirmRev"],
                                    Location = reader["Location"] is DBNull ?
                                        null : (string)reader["Location"],
                                    UTCOffset = reader["UTCOffset"] is DBNull ?
                                        null : (short?)reader["UTCOffset"],
                                    VendorId = reader["VendorId"] is DBNull ?
                                        null : (short?)reader["VendorId"],
                                    VendorName = reader["VendorName"] is DBNull ?
                                        null : (string)reader["VendorName"],
                                    SystemStatus = reader["SystemStatus"] is DBNull ?
                                        null : (short?)reader["SystemStatus"],
                                    ModelName = reader["ModelName"] is DBNull ?
                                        null : (string)reader["ModelName"],
                                    ProtoVer = reader["ProtoVer"] is DBNull ?
                                        null : (byte?)reader["ProtoVer"],
                                    ProtoRevision = reader["ProtoRevision"] is DBNull ?
                                        null : (byte?)reader["ProtoRevision"],
                                    DatabaseRevision = reader["DatabaseRevision"] is DBNull ?
                                        null : (int?)reader["DatabaseRevision"],
                                    ProtoConfClass = reader["ProtoConfClass"] is DBNull ?
                                        null : (byte?)reader["ProtoConfClass"],
                                    ProtServSup = reader["ProtServSup"] is DBNull ?
                                        null : (byte[])reader["ProtServSup"],
                                    ProtObjSup = reader["ProtObjSup"] is DBNull ?
                                        null : (int?)reader["ProtObjSup"],
                                    MaxAPDU = reader["MaxAPDU"] is DBNull ?
                                        null : (short?)reader["MaxAPDU"],
                                    SegSupport = reader["SegSupport"] is DBNull ?
                                        null : (short?)reader["SegSupport"],
                                    DaylightSav = reader["DaylightSav"] is DBNull ?
                                        null : (bool?)reader["DaylightSav"],
                                    NumAPDURet = reader["NumAPDURet"] is DBNull ?
                                        null : (byte?)reader["NumAPDURet"],
                                    ContainerType = reader["ContainerType"] is DBNull ?
                                        null : (string)reader["ContainerType"],
                                    CommStatus = reader["CommStatus"] is DBNull ?
                                        null : (short?)reader["CommStatus"],
                                    IAmBCInterval = reader["IAmBCInterval"] is DBNull ?
                                        null : (int?)reader["IAmBCInterval"],
                                    IAmBCScope = reader["IAmBCScope"] is DBNull ?
                                        null : (short?)reader["IAmBCScope"],
                                    IAmRemNetwork = reader["IAmRemNetwork"] is DBNull ?
                                        null : (short?)reader["IAmRemNetwork"],
                                    RemDeviceList = reader["RemDeviceList"] is DBNull ?
                                        null : (byte[])reader["RemDeviceList"],
                                    RemInfDevList = reader["RemInfDevList"] is DBNull ?
                                        null : (byte[])reader["RemInfDevList"],
                                    DefaultFolderHi = reader["DefaultFolderHi"] is DBNull ?
                                        null : (int?)reader["DefaultFolderHi"],
                                    DefaultFolderLo = reader["DefaultFolderLo"] is DBNull ?
                                        null : (int?)reader["DefaultFolderLo"],
                                    ProbeTime = reader["ProbeTime"] is DBNull ?
                                        null : (int?)reader["ProbeTime"],
                                    Homepage = reader["Homepage"] is DBNull ?
                                        null : (string)reader["Homepage"],
                                    MaxResponseTime = reader["MaxResponseTime"] is DBNull ?
                                        null : (int?)reader["MaxResponseTime"],
                                    NetworkId = reader["NetworkId"] is DBNull ?
                                        null : (byte?)reader["NetworkId"],
                                    DefaultRouter = reader["DefaultRouter"] is DBNull ?
                                        null : (int?)reader["DefaultRouter"],
                                    IPAddress = reader["IPAddress"] is DBNull ?
                                        null : (int?)reader["IPAddress"],
                                    SubnetMask = reader["SubnetMask"] is DBNull ?
                                        null : (int?)reader["SubnetMask"],
                                    PrimaryAccessServer = reader["PrimaryAccessServer"] is DBNull ?
                                        null : (byte?)reader["PrimaryAccessServer"],
                                    SecondaryAccessServer = reader["SecondaryAccessServer"] is DBNull ?
                                        null : (byte?)reader["SecondaryAccessServer"],
                                    ScheduleADL = reader["ScheduleADL"] is DBNull ?
                                        null : (byte?)reader["ScheduleADL"],
                                    CommandlinePrompt = reader["CommandlinePrompt"] is DBNull ?
                                        null : (string)reader["CommandlinePrompt"],
                                    BadgeFormatFolder = reader["BadgeFormatFolder"] is DBNull ?
                                        null : (string)reader["BadgeFormatFolder"],
                                    DefaultBadgeFormat = reader["DefaultBadgeFormat"] is DBNull ?
                                        null : (string)reader["DefaultBadgeFormat"],
                                    DefaultReportViewer = reader["DefaultReportViewer"] is DBNull ?
                                        null : (string)reader["DefaultReportViewer"],
                                    IncrementReportFile = reader["IncrementReportFile"] is DBNull ?
                                        null : (bool?)reader["IncrementReportFile"],
                                    AlarmViewerMaxEntries = reader["AlarmViewerMaxEntries"] is DBNull ?
                                        null : (int?)reader["AlarmViewerMaxEntries"],
                                    AccessEventViewerMaxEntries = reader["AccessEventViewerMaxEntries"] is DBNull ?
                                        null : (int?)reader["AccessEventViewerMaxEntries"],
                                    PEWizardFolder = reader["PEWizardFolder"] is DBNull ?
                                        null : (string)reader["PEWizardFolder"],
                                    AlarmPrinterPath = reader["AlarmPrinterPath"] is DBNull ?
                                        null : (string)reader["AlarmPrinterPath"],
                                    SuppressFormFeeds = reader["SuppressFormFeeds"] is DBNull ?
                                        null : (bool?)reader["SuppressFormFeeds"],
                                    DefaultImageCropping = reader["DefaultImageCropping"] is DBNull ?
                                        null : (bool?)reader["DefaultImageCropping"],
                                    AlarmEmailFormatFile = reader["AlarmEmailFormatFile"] is DBNull ?
                                        null : (string)reader["AlarmEmailFormatFile"],
                                    AlarmPagerFormatFile = reader["AlarmPagerFormatFile"] is DBNull ?
                                        null : (string)reader["AlarmPagerFormatFile"],
                                    AlarmPrinterFormatFile = reader["AlarmPrinterFormatFile"] is DBNull ?
                                        null : (string)reader["AlarmPrinterFormatFile"],
                                    AcknowledgeEmailFormatFile = reader["AcknowledgeEmailFormatFile"] is DBNull ?
                                        null : (string)reader["AcknowledgeEmailFormatFile"],
                                    AcknowledgePagerFormatFile = reader["AcknowledgePagerFormatFile"] is DBNull ?
                                        null : (string)reader["AcknowledgePagerFormatFile"],
                                    AcknowledgePrinterFormatFile = reader["AcknowledgePrinterFormatFile"] is DBNull ?
                                        null : (string)reader["AcknowledgePrinterFormatFile"],
                                    OperatorTextAlarmAck = reader["OperatorTextAlarmAck"] is DBNull ?
                                        null : (bool?)reader["OperatorTextAlarmAck"],
                                    PPBackgroundsFolder = reader["PPBackgroundsFolder"] is DBNull ?
                                        null : (string)reader["PPBackgroundsFolder"],
                                    PPGraphicsFolder = reader["PPGraphicsFolder"] is DBNull ?
                                        null : (string)reader["PPGraphicsFolder"],
                                    PPGraphicsLibraryFolder = reader["PPGraphicsLibraryFolder"] is DBNull ?
                                        null : (string)reader["PPGraphicsLibraryFolder"],
                                    ViewAlwaysOnTop = reader["ViewAlwaysOnTop"] is DBNull ?
                                        null : (bool?)reader["ViewAlwaysOnTop"],
                                    MainMenuFile = reader["MainMenuFile"] is DBNull ?
                                        null : (string)reader["MainMenuFile"],
                                    BACnetDevice = reader["BACnetDevice"] is DBNull ?
                                        null : (bool?)reader["BACnetDevice"],
                                    BACnetNetworkNumber = reader["BACnetNetworkNumber"] is DBNull ?
                                        null : (short?)reader["BACnetNetworkNumber"],
                                    BACnetMacAddress = reader["BACnetMacAddress"] is DBNull ?
                                        null : (byte[])reader["BACnetMacAddress"],
                                    UrlGraphicsImgFiles = reader["UrlGraphicsImgFiles"] is DBNull ?
                                        null : (string)reader["UrlGraphicsImgFiles"],
                                    UrlGraphicsBckFiles = reader["UrlGraphicsBckFiles"] is DBNull ?
                                        null : (string)reader["UrlGraphicsBckFiles"],
                                    UrlGraphicsFiles = reader["UrlGraphicsFiles"] is DBNull ?
                                        null : (string)reader["UrlGraphicsFiles"],
                                    UrlWDNServicePort = reader["UrlWDNServicePort"] is DBNull ?
                                        null : (string)reader["UrlWDNServicePort"],
                                    UrlWDNServerName = reader["UrlWDNServerName"] is DBNull ?
                                        null : (string)reader["UrlWDNServerName"],
                                    BACnetWorkstation = reader["BACnetWorkstation"] is DBNull ?
                                        null : (bool?)reader["BACnetWorkstation"],
                                    EnableLANDistribution = reader["EnableLANDistribution"] is DBNull ?
                                        null : (bool?)reader["EnableLANDistribution"],
                                    EnableRASDistribution = reader["EnableRASDistribution"] is DBNull ?
                                        null : (bool?)reader["EnableRASDistribution"],
                                    ExtLogLANEnable = reader["ExtLogLANEnable"] is DBNull ?
                                        null : (bool?)reader["ExtLogLANEnable"],
                                    ExtLogRASEnable = reader["ExtLogRASEnable"] is DBNull ?
                                        null : (bool?)reader["ExtLogRASEnable"],
                                    MaxSegmentsAccepted = reader["MaxSegmentsAccepted"] is DBNull ?
                                        null : (short?)reader["MaxSegmentsAccepted"],
                                    IsBBMD = reader["IsBBMD"] is DBNull ?
                                        null : (bool?)reader["IsBBMD"],
                                    BBMDIPAddress = reader["BBMDIPAddress"] is DBNull ?
                                        null : (int?)reader["BBMDIPAddress"],
                                    BBMDPortNumber = reader["BBMDPortNumber"] is DBNull ?
                                        null : (short?)reader["BBMDPortNumber"],
                                    BBMDTimeToLive = reader["BBMDTimeToLive"] is DBNull ?
                                        null : (short?)reader["BBMDTimeToLive"],
                                    MaxInfoFrames = reader["MaxInfoFrames"] is DBNull ?
                                        null : (int?)reader["MaxInfoFrames"],
                                    BacnetMaxMaster = reader["BacnetMaxMaster"] is DBNull ?
                                        null : (byte?)reader["BacnetMaxMaster"],
                                    MaxAsyncRequests = reader["MaxAsyncRequests"] is DBNull ?
                                        null : (short?)reader["MaxAsyncRequests"],
                                    RequestInterval = reader["RequestInterval"] is DBNull ?
                                        null : (int?)reader["RequestInterval"],
                                    WPPContinuousPRate = reader["WPPContinuousPRate"] is DBNull ?
                                        null : (int?)reader["WPPContinuousPRate"],
                                    SerialNum = reader["SerialNum"] is DBNull ?
                                        null : (int?)reader["SerialNum"],
                                    VirtualDevice = reader["VirtualDevice"] is DBNull ?
                                        null : (bool?)reader["VirtualDevice"],
                                    BackupFailureTimeout = reader["BackupFailureTimeout"] is DBNull ?
                                        null : (short?)reader["BackupFailureTimeout"],
                                    LastRestoreTime = reader["LastRestoreTime"] is DBNull ?
                                        null : (byte[])reader["LastRestoreTime"],
                                    LastBackupTime = reader["LastBackupTime"] is DBNull ?
                                        null : (DateTime?)reader["LastBackupTime"],
                                    LastRestoredFilePath = reader["LastRestoredFilePath"] is DBNull ?
                                        null : (string)reader["LastRestoredFilePath"],
                                    ProbeType = reader["ProbeType"] is DBNull ?
                                        null : (short?)reader["ProbeType"],
                                    PPActivePollingRate = reader["PPActivePollingRate"] is DBNull ?
                                        null : (short?)reader["PPActivePollingRate"],
                                    PPInactivePollingRate = reader["PPInactivePollingRate"] is DBNull ?
                                        null : (short?)reader["PPInactivePollingRate"],
                                    UsePersonnelManager = reader["UsePersonnelManager"] is DBNull ?
                                        null : (bool?)reader["UsePersonnelManager"],
                                    VideoMonitor = reader["VideoMonitor"] is DBNull ?
                                        null : (string)reader["VideoMonitor"],
                                    ProcessVideoEvents = reader["ProcessVideoEvents"] is DBNull ?
                                        null : (bool?)reader["ProcessVideoEvents"],
                                    ProcessVideoAlarmPoints = reader["ProcessVideoAlarmPoints"] is DBNull ?
                                        null : (bool?)reader["ProcessVideoAlarmPoints"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Area> GetAreas()
        {
            Console.WriteLine("GetAreas() executed");

            var result = new List<Area>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Area";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Area
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    DeletePending = reader["DeletePending"] is DBNull ?
                                        null : (byte?)reader["DeletePending"],
                                    Description = reader["Description"] is DBNull ?
                                        null : (string)reader["Description"],
                                    KnownOccupCount = reader["KnownOccupCount"] is DBNull ?
                                        null : (int?)reader["KnownOccupCount"],
                                    State = reader["State"] is DBNull ?
                                        null : (short?)reader["State"],
                                    ForceLock = reader["ForceLock"] is DBNull ?
                                        null : (byte?)reader["ForceLock"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Personnel> GetPersons()
        {
            Console.WriteLine("GetPersons() executed");

            var result = new List<Personnel>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Personnel";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Personnel
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    ActivationDate = reader["ActivationDate"] is DBNull ?
                                        null : (DateTime?)reader["ActivationDate"],
                                    SavedActivationDate = reader["SavedActivationDate"] is DBNull ?
                                        null : (DateTime?)reader["SavedActivationDate"],
                                    ADA = reader["ADA"] is DBNull ?
                                        null : (bool?)reader["ADA"],
                                    Address = reader["Address"] is DBNull ?
                                        null : (string)reader["Address"],
                                    AllowEntEntEgr = reader["AllowEntEntEgr"] is DBNull ?
                                        null : (bool?)reader["AllowEntEntEgr"],
                                    Blood = reader["Blood"] is DBNull ?
                                        null : (string)reader["Blood"],
                                    CardType = reader["CardType"] is DBNull ?
                                        null : (short?)reader["CardType"],
                                    CardType2 = reader["CardType2"] is DBNull ?
                                        null : (short?)reader["CardType2"],
                                    SiteCode = reader["SiteCode"] is DBNull ?
                                        null : (short?)reader["SiteCode"],
                                    SiteCode2 = reader["SiteCode2"] is DBNull ?
                                        null : (short?)reader["SiteCode2"],
                                    CardNumber = reader["CardNumber"] is DBNull ?
                                        null : (byte[])reader["CardNumber"],
                                    CardNumber2 = reader["CardNumber2"] is DBNull ?
                                        null : (byte[])reader["CardNumber2"],
                                    SavedCardType = reader["SavedCardType"] is DBNull ?
                                        null : (short?)reader["SavedCardType"],
                                    City = reader["City"] is DBNull ?
                                        null : (string)reader["City"],
                                    Country = reader["Country"] is DBNull ?
                                        null : (string)reader["Country"],
                                    CustomControl1 = reader["CustomControl1"] is DBNull ?
                                        null : (string)reader["CustomControl1"],
                                    CustomControl2 = reader["CustomControl2"] is DBNull ?
                                        null : (string)reader["CustomControl2"],
                                    CustomControl3 = reader["CustomControl3"] is DBNull ?
                                        null : (string)reader["CustomControl3"],
                                    DateOfBirth = reader["DateOfBirth"] is DBNull ?
                                        null : (DateTime?)reader["DateOfBirth"],
                                    DeletePending = reader["DeletePending"] is DBNull ?
                                        null : (bool?)reader["DeletePending"],
                                    Department = reader["Department"] is DBNull ?
                                        null : (string)reader["Department"],
                                    DepartmentCode = reader["DepartmentCode"] is DBNull ?
                                        null : (float?)reader["DepartmentCode"],
                                    DistFailed = reader["DistFailed"] is DBNull ?
                                        null : (bool?)reader["DistFailed"],
                                    Duress = reader["Duress"] is DBNull ?
                                        null : (bool?)reader["Duress"],
                                    EmergencyContact = reader["EmergencyContact"] is DBNull ?
                                        null : (string)reader["EmergencyContact"],
                                    EmergencyPhone = reader["EmergencyPhone"] is DBNull ?
                                        null : (string)reader["EmergencyPhone"],
                                    EmpNumber = reader["EmpNumber"] is DBNull ?
                                        null : (string)reader["EmpNumber"],
                                    EntryEgress = reader["EntryEgress"] is DBNull ?
                                        null : (bool?)reader["EntryEgress"],
                                    ExpirationDate = reader["ExpirationDate"] is DBNull ?
                                        null : (DateTime?)reader["ExpirationDate"],
                                    SavedExpirationDate = reader["SavedExpirationDate"] is DBNull ?
                                        null : (DateTime?)reader["SavedExpirationDate"],
                                    EyeColor = reader["EyeColor"] is DBNull ?
                                        null : (string)reader["EyeColor"],
                                    FirstName = reader["FirstName"] is DBNull ?
                                        null : (string)reader["FirstName"],
                                    HairColor = reader["HairColor"] is DBNull ?
                                        null : (string)reader["HairColor"],
                                    Height = reader["Height"] is DBNull ?
                                        null : (string)reader["Height"],
                                    HomePhone = reader["HomePhone"] is DBNull ?
                                        null : (string)reader["HomePhone"],
                                    InactiveDisableDays = reader["InactiveDisableDays"] is DBNull ?
                                        null : (short?)reader["InactiveDisableDays"],
                                    Info1 = reader["Info1"] is DBNull ?
                                        null : (string)reader["Info1"],
                                    Info2 = reader["Info2"] is DBNull ?
                                        null : (string)reader["Info2"],
                                    Info3 = reader["Info3"] is DBNull ?
                                        null : (string)reader["Info3"],
                                    Info4 = reader["Info4"] is DBNull ?
                                        null : (string)reader["Info4"],
                                    Info5 = reader["Info5"] is DBNull ?
                                        null : (string)reader["Info5"],
                                    Info6 = reader["Info6"] is DBNull ?
                                        null : (string)reader["Info6"],
                                    JobTitle = reader["JobTitle"] is DBNull ?
                                        null : (string)reader["JobTitle"],
                                    LastName = reader["LastName"] is DBNull ?
                                        null : (string)reader["LastName"],
                                    LicenseNumber = reader["LicenseNumber"] is DBNull ?
                                        null : (string)reader["LicenseNumber"],
                                    LostCard = reader["LostCard"] is DBNull ?
                                        null : (bool?)reader["LostCard"],
                                    MiddleName = reader["MiddleName"] is DBNull ?
                                        null : (string)reader["MiddleName"],
                                    OfficeLocation = reader["OfficeLocation"] is DBNull ?
                                        null : (string)reader["OfficeLocation"],
                                    ParkingSticker = reader["ParkingSticker"] is DBNull ?
                                        null : (string)reader["ParkingSticker"],
                                    PhotoFile = reader["PhotoFile"] is DBNull ?
                                        null : (string)reader["PhotoFile"],
                                    PIN = reader["PIN"] is DBNull ?
                                        null : (int?)reader["PIN"],
                                    SavedPIN = reader["SavedPIN"] is DBNull ?
                                        null : (int?)reader["SavedPIN"],
                                    SavedCardNumber = reader["SavedCardNumber"] is DBNull ?
                                        null : (byte[])reader["SavedCardNumber"],
                                    SavedSiteCode = reader["SavedSiteCode"] is DBNull ?
                                        null : (short?)reader["SavedSiteCode"],
                                    Sex = reader["Sex"] is DBNull ?
                                        null : (short?)reader["Sex"],
                                    Signature = reader["Signature"] is DBNull ?
                                        null : (string)reader["Signature"],
                                    SocSecNo = reader["SocSecNo"] is DBNull ?
                                        null : (string)reader["SocSecNo"],
                                    StartDate = reader["StartDate"] is DBNull ?
                                        null : (DateTime?)reader["StartDate"],
                                    State = reader["State"] is DBNull ?
                                        null : (short?)reader["State"],
                                    SavedState = reader["SavedState"] is DBNull ?
                                        null : (short?)reader["SavedState"],
                                    StateOfResidence = reader["StateOfResidence"] is DBNull ?
                                        null : (string)reader["StateOfResidence"],
                                    Supervisor = reader["Supervisor"] is DBNull ?
                                        null : (string)reader["Supervisor"],
                                    TimeEntered = reader["TimeEntered"] is DBNull ?
                                        null : (DateTime?)reader["TimeEntered"],
                                    ValueHi = reader["ValueHi"] is DBNull ?
                                        null : (int?)reader["ValueHi"],
                                    ValueLo = reader["ValueLo"] is DBNull ?
                                        null : (int?)reader["ValueLo"],
                                    VehicalInfo = reader["VehicalInfo"] is DBNull ?
                                        null : (string)reader["VehicalInfo"],
                                    Visitor = reader["Visitor"] is DBNull ?
                                        null : (bool?)reader["Visitor"],
                                    Weight = reader["Weight"] is DBNull ?
                                        null : (short?)reader["Weight"],
                                    WorkPhone = reader["WorkPhone"] is DBNull ?
                                        null : (string)reader["WorkPhone"],
                                    Zip = reader["Zip"] is DBNull ?
                                        null : (string)reader["Zip"],
                                    Zone = reader["Zone"] is DBNull ?
                                        null : (short?)reader["Zone"],
                                    ZonePointHi = reader["ZonePointHi"] is DBNull ?
                                        null : (int?)reader["ZonePointHi"],
                                    ZonePointLo = reader["ZonePointLo"] is DBNull ?
                                        null : (int?)reader["ZonePointLo"],
                                    NonABACardNumber = reader["NonABACardNumber"] is DBNull ?
                                        null : (int?)reader["NonABACardNumber"],
                                    NonABACardNumber2 = reader["NonABACardNumber2"] is DBNull ?
                                        null : (int?)reader["NonABACardNumber2"],
                                    BLOB_Template = reader["BLOB_Template"] is DBNull ?
                                        null : (string)reader["BLOB_Template"],
                                    ExecutivePrivilege = reader["ExecutivePrivilege"] is DBNull ?
                                        null : (bool?)reader["ExecutivePrivilege"],
                                    DefaultClearanceLevel = reader["DefaultClearanceLevel"] is DBNull ?
                                        null : (byte?)reader["DefaultClearanceLevel"],
                                    FipsAgencyCode = reader["FipsAgencyCode"] is DBNull ?
                                        null : (short?)reader["FipsAgencyCode"],
                                    FipsOrgId = reader["FipsOrgId"] is DBNull ?
                                        null : (short?)reader["FipsOrgId"],
                                    FipsHmac = reader["FipsHmac"] is DBNull ?
                                        null : (int?)reader["FipsHmac"],
                                    FipsSystemCode = reader["FipsSystemCode"] is DBNull ?
                                        null : (short?)reader["FipsSystemCode"],
                                    FipsCredentialNumber = reader["FipsCredentialNumber"] is DBNull ?
                                        null : (int?)reader["FipsCredentialNumber"],
                                    FipsPersonId = reader["FipsPersonId"] is DBNull ?
                                        null : (byte[])reader["FipsPersonId"],
                                    FipsCredentialSeries = reader["FipsCredentialSeries"] is DBNull ?
                                        null : (byte?)reader["FipsCredentialSeries"],
                                    FipsCredentialIssue = reader["FipsCredentialIssue"] is DBNull ?
                                        null : (byte?)reader["FipsCredentialIssue"],
                                    FipsOrgCategory = reader["FipsOrgCategory"] is DBNull ?
                                        null : (short?)reader["FipsOrgCategory"],
                                    FipsPersonOrgAssociation = reader["FipsPersonOrgAssociation"] is DBNull ?
                                        null : (short?)reader["FipsPersonOrgAssociation"],
                                    FipsExpirationDate = reader["FipsExpirationDate"] is DBNull ?
                                        null : (DateTime?)reader["FipsExpirationDate"],
                                    FipsPivControlled = reader["FipsPivControlled"] is DBNull ?
                                        null : (bool?)reader["FipsPivControlled"],
                                    FipsPivState = reader["FipsPivState"] is DBNull ?
                                        null : (short?)reader["FipsPivState"],
                                    SavedCardType2 = reader["SavedCardType2"] is DBNull ?
                                        null : (short?)reader["SavedCardType2"],
                                    SavedCardNumber2 = reader["SavedCardNumber2"] is DBNull ?
                                        null : (byte[])reader["SavedCardNumber2"],
                                    SavedSiteCode2 = reader["SavedSiteCode2"] is DBNull ?
                                        null : (short?)reader["SavedSiteCode2"],
                                    CardField1 = reader["CardField1"] is DBNull ?
                                        null : (int?)reader["CardField1"],
                                    CardField2 = reader["CardField2"] is DBNull ?
                                        null : (int?)reader["CardField2"],
                                    CardField3 = reader["CardField3"] is DBNull ?
                                        null : (int?)reader["CardField3"],
                                    CardField4 = reader["CardField4"] is DBNull ?
                                        null : (int?)reader["CardField4"],
                                    CardField5 = reader["CardField5"] is DBNull ?
                                        null : (int?)reader["CardField5"],
                                    CardField6 = reader["CardField6"] is DBNull ?
                                        null : (int?)reader["CardField6"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Schedule> GetSchedules()
        {
            Console.WriteLine("GetSchedules() executed");

            var result = new List<Schedule>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Schedule";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Schedule
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    Value = reader["Value"] is DBNull ?
                                        null : (byte[])reader["Value"],
                                    Note = reader["Note"] is DBNull ?
                                        null : (string)reader["Note"],
                                    Description = reader["Description"] is DBNull ?
                                        null : (string)reader["Description"],
                                    EffectivePeriod = reader["EffectivePeriod"] is DBNull ?
                                        null : (byte[])reader["EffectivePeriod"],
                                    WeeklySchedule = reader["WeeklySchedule"] is DBNull ?
                                        null : (byte[])reader["WeeklySchedule"],
                                    ExceptionSched = reader["ExceptionSched"] is DBNull ?
                                        null : (byte[])reader["ExceptionSched"],
                                    PropertyRef = reader["PropertyRef"] is DBNull ?
                                        null : (byte[])reader["PropertyRef"],
                                    Priority = reader["Priority"] is DBNull ?
                                        null : (byte?)reader["Priority"],
                                    OccupancyTime = reader["OccupancyTime"] is DBNull ?
                                        null : (DateTime?)reader["OccupancyTime"],
                                    UnoccupancyTime = reader["UnoccupancyTime"] is DBNull ?
                                        null : (DateTime?)reader["UnoccupancyTime"],
                                    ActiveText = reader["ActiveText"] is DBNull ?
                                        null : (string)reader["ActiveText"],
                                    InactiveText = reader["InactiveText"] is DBNull ?
                                        null : (string)reader["InactiveText"],
                                    State = reader["State"] is DBNull ?
                                        null : (short?)reader["State"],
                                    CalenderRefList = reader["CalenderRefList"] is DBNull ?
                                        null : (byte[])reader["CalenderRefList"],
                                    WeeklySchNotes = reader["WeeklySchNotes"] is DBNull ?
                                        null : (byte[])reader["WeeklySchNotes"],
                                    ExcSchedNotes = reader["ExcSchedNotes"] is DBNull ?
                                        null : (byte[])reader["ExcSchedNotes"],
                                    ActiveValue = reader["ActiveValue"] is DBNull ?
                                        null : (byte[])reader["ActiveValue"],
                                    InactiveValue = reader["InactiveValue"] is DBNull ?
                                        null : (byte[])reader["InactiveValue"],
                                    PackagedDays = reader["PackagedDays"] is DBNull ?
                                        null : (byte[])reader["PackagedDays"],
                                    DownloadFlag = reader["DownloadFlag"] is DBNull ?
                                        null : (byte?)reader["DownloadFlag"],
                                    LastDownloadTime = reader["LastDownloadTime"] is DBNull ?
                                        null : (DateTime?)reader["LastDownloadTime"],
                                    ScheduleType = reader["ScheduleType"] is DBNull ?
                                        null : (byte?)reader["ScheduleType"],
                                    OccTimePointHi = reader["OccTimePointHi"] is DBNull ?
                                        null : (int?)reader["OccTimePointHi"],
                                    OccTimePointLo = reader["OccTimePointLo"] is DBNull ?
                                        null : (int?)reader["OccTimePointLo"],
                                    UnOccTimePointHi = reader["UnOccTimePointHi"] is DBNull ?
                                        null : (int?)reader["UnOccTimePointHi"],
                                    UnOccTimePointLo = reader["UnOccTimePointLo"] is DBNull ?
                                        null : (int?)reader["UnOccTimePointLo"],
                                    AutosendFlag = reader["AutosendFlag"] is DBNull ?
                                        null : (byte?)reader["AutosendFlag"],
                                    AutosendTime = reader["AutosendTime"] is DBNull ?
                                        null : (DateTime?)reader["AutosendTime"],
                                    UnavailableAttributes = reader["UnavailableAttributes"] is DBNull ?
                                        null : (string)reader["UnavailableAttributes"],
                                    SpecialEventName = reader["SpecialEventName"] is DBNull ?
                                        null : (byte[])reader["SpecialEventName"],
                                    TimeScale = reader["TimeScale"] is DBNull ?
                                        null : (byte?)reader["TimeScale"],
                                    ScheduleDefault = reader["ScheduleDefault"] is DBNull ?
                                        null : (byte[])reader["ScheduleDefault"],
                                    DefaultDataType = reader["DefaultDataType"] is DBNull ?
                                        null : (short?)reader["DefaultDataType"],
                                    OutOfService = reader["OutOfService"] is DBNull ?
                                        null : (bool?)reader["OutOfService"],
                                    ApplyMidnightValue = reader["ApplyMidnightValue"] is DBNull ?
                                        null : (bool?)reader["ApplyMidnightValue"],
                                    DefaultMidnightValue = reader["DefaultMidnightValue"] is DBNull ?
                                        null : (byte[])reader["DefaultMidnightValue"],
                                    ClearPastEvents = reader["ClearPastEvents"] is DBNull ?
                                        null : (bool?)reader["ClearPastEvents"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<AreaLink> GetAreaLinks()
        {
            Console.WriteLine("GetAreaLinks() executed");

            var result = new List<AreaLink>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from AreaLink";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new AreaLink
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    AreaIdHi = (int)reader["AreaIdHi"],
                                    AreaIdLo = (int)reader["AreaIdLo"],
                                    PersonIdHi = (int)reader["PersonIdHi"],
                                    PersonIdLo = (int)reader["PersonIdLo"],
                                    Preload = reader["Preload"] is DBNull ?
                                        null : (bool?)reader["Preload"],
                                    SchedIdHi = reader["SchedIdHi"] is DBNull ?
                                        null : (int?)reader["SchedIdHi"],
                                    SchedIdLo = reader["SchedIdLo"] is DBNull ?
                                        null : (int?)reader["SchedIdLo"],
                                    State = reader["State"] is DBNull ?
                                        null : (short?)reader["State"],
                                    TimeEntered = reader["TimeEntered"] is DBNull ?
                                        null : (DateTime?)reader["TimeEntered"],
                                    DistPending = reader["DistPending"] is DBNull ?
                                        null : (bool?)reader["DistPending"],
                                    DeletePending = reader["DeletePending"] is DBNull ?
                                        null : (bool?)reader["DeletePending"],
                                    DistTime = reader["DistTime"] is DBNull ?
                                        null : (DateTime?)reader["DistTime"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (byte?)reader["TemplateFlag"],
                                    ClearanceLevel = reader["ClearanceLevel"] is DBNull ?
                                        null : (byte?)reader["ClearanceLevel"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Door> GetDoors()
        {
            Console.WriteLine("GetDoors() executed");

            var result = new List<Door>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from Door";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new Door
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    UiName = reader["uiName"] is DBNull ?
                                        null : (string)reader["uiName"],
                                    OwnerIdHi = reader["OwnerIdHi"] is DBNull ?
                                        null : (int?)reader["OwnerIdHi"],
                                    OwnerIdLo = reader["OwnerIdLo"] is DBNull ?
                                        null : (int?)reader["OwnerIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    TemplateFlag = reader["TemplateFlag"] is DBNull ?
                                        null : (bool?)reader["TemplateFlag"],
                                    TemplateIdHi = reader["TemplateIdHi"] is DBNull ?
                                        null : (int?)reader["TemplateIdHi"],
                                    TemplateIdLo = reader["TemplateIdLo"] is DBNull ?
                                        null : (int?)reader["TemplateIdLo"],
                                    ControllerName = reader["ControllerName"] is DBNull ?
                                        null : (string)reader["ControllerName"],
                                    AlarmGraphicPage = reader["AlarmGraphicPage"] is DBNull ?
                                        null : (short?)reader["AlarmGraphicPage"],
                                    ADADoorAjarTime = reader["ADADoorAjarTime"] is DBNull ?
                                        null : (byte?)reader["ADADoorAjarTime"],
                                    ADAOutputTime = reader["ADAOutputTime"] is DBNull ?
                                        null : (byte?)reader["ADAOutputTime"],
                                    ADAChannel = reader["ADAChannel"] is DBNull ?
                                        null : (byte?)reader["ADAChannel"],
                                    AlarmChannel = reader["AlarmChannel"] is DBNull ?
                                        null : (byte?)reader["AlarmChannel"],
                                    AlarmRelayTime = reader["AlarmRelayTime"] is DBNull ?
                                        null : (float?)reader["AlarmRelayTime"],
                                    ArmCode = reader["ArmCode"] is DBNull ?
                                        null : (short?)reader["ArmCode"],
                                    BondChannel = reader["BondChannel"] is DBNull ?
                                        null : (byte?)reader["BondChannel"],
                                    BondType = reader["BondType"] is DBNull ?
                                        null : (short?)reader["BondType"],
                                    BondSensor = reader["BondSensor"] is DBNull ?
                                        null : (short?)reader["BondSensor"],
                                    CardFormats = reader["CardFormats"] is DBNull ?
                                        null : (short?)reader["CardFormats"],
                                    CardFormats2 = reader["CardFormats2"] is DBNull ?
                                        null : (int?)reader["CardFormats2"],
                                    Description = reader["Description"] is DBNull ?
                                        null : (string)reader["Description"],
                                    DoorAjarTime = reader["DoorAjarTime"] is DBNull ?
                                        null : (byte?)reader["DoorAjarTime"],
                                    DoorChannel = reader["DoorChannel"] is DBNull ?
                                        null : (byte?)reader["DoorChannel"],
                                    DoorScheduleHi = reader["DoorScheduleHi"] is DBNull ?
                                        null : (int?)reader["DoorScheduleHi"],
                                    DoorScheduleLo = reader["DoorScheduleLo"] is DBNull ?
                                        null : (int?)reader["DoorScheduleHi"],
                                    DoorStrikeTime = reader["DoorStrikeTime"] is DBNull ?
                                        null : (byte?)reader["DoorStrikeTime"],
                                    DoorSwitchChan = reader["DoorSwitchChan"] is DBNull ?
                                        null : (byte?)reader["DoorSwitchChan"],
                                    DoorSwitchType = reader["DoorSwitchType"] is DBNull ?
                                        null : (short?)reader["DoorSwitchType"],
                                    EntryAntiPassTim = reader["EntryAntiPassTim"] is DBNull ?
                                        null : (short?)reader["EntryAntiPassTim"],
                                    EntryAreaHi = reader["EntryAreaHi"] is DBNull ?
                                        null : (int?)reader["EntryAreaHi"],
                                    EntryAreaLo = reader["EntryAreaLo"] is DBNull ?
                                        null : (int?)reader["EntryAreaLo"],
                                    EntryIOU = reader["EntryIOU"] is DBNull ?
                                        null : (byte?)reader["EntryIOU"],
                                    EntryChannel = reader["EntryChannel"] is DBNull ?
                                        null : (byte?)reader["EntryChannel"],
                                    EntryEntEgr = reader["EntryEntEgr"] is DBNull ?
                                        null : (bool?)reader["EntryEntEgr"],
                                    EntryEntrAntiPas = reader["EntryEntrAntiPas"] is DBNull ?
                                        null : (bool?)reader["EntryEntrAntiPas"],
                                    EntryEntrEntEgr = reader["EntryEntrEntEgr"] is DBNull ?
                                        null : (bool?)reader["EntryEntrEntEgr"],
                                    EntryEntrRvrsCrd = reader["EntryEntrRvrsCrd"] is DBNull ?
                                        null : (bool?)reader["EntryEntrRvrsCrd"],
                                    EntryKyPdChan = reader["EntryKyPdChan"] is DBNull ?
                                        null : (byte?)reader["EntryKyPdChan"],
                                    EntryNoCommMode = reader["EntryNoCommMode"] is DBNull ?
                                        null : (byte?)reader["EntryNoCommMode"],
                                    EntryNoDataMode = reader["EntryNoDataMode"] is DBNull ?
                                        null : (byte?)reader["EntryNoDataMode"],
                                    EntryNoReEntry = reader["EntryNoReEntry"] is DBNull ?
                                        null : (bool?)reader["EntryNoReEntry"],
                                    EntryNormMode = reader["EntryNormMode"] is DBNull ?
                                        null : (byte?)reader["EntryNormMode"],
                                    EntryPinDuress = reader["EntryPinDuress"] is DBNull ?
                                        null : (bool?)reader["EntryPinDuress"],
                                    EntryRvrsCrdDur = reader["EntryRvrsCrdDur"] is DBNull ?
                                        null : (bool?)reader["EntryRvrsCrdDur"],
                                    EntryScheduleHi = reader["EntryScheduleHi"] is DBNull ?
                                        null : (int?)reader["EntryScheduleHi"],
                                    EntryScheduleLo = reader["EntryScheduleLo"] is DBNull ?
                                        null : (int?)reader["EntryScheduleLo"],
                                    EntryZone = reader["EntryZone"] is DBNull ?
                                        null : (float?)reader["EntryZone"],
                                    SvEntryZone = reader["SvEntryZone"] is DBNull ?
                                        null : (short?)reader["SvEntryZone"],
                                    ExitAntiPassTim = reader["ExitAntiPassTim"] is DBNull ?
                                        null : (short?)reader["ExitAntiPassTim"],
                                    ExitAreaHi = reader["ExitAreaHi"] is DBNull ?
                                        null : (int?)reader["ExitAreaHi"],
                                    ExitAreaLo = reader["ExitAreaLo"] is DBNull ?
                                        null : (int?)reader["ExitAreaLo"],
                                    ExitIOU = reader["ExitIOU"] is DBNull ?
                                        null : (byte?)reader["ExitIOU"],
                                    ExitChannel = reader["ExitChannel"] is DBNull ?
                                        null : (byte?)reader["ExitChannel"],
                                    ExitEntEgr = reader["ExitEntEgr"] is DBNull ?
                                        null : (bool?)reader["ExitEntEgr"],
                                    ExitEntrAntiPas = reader["ExitEntrAntiPas"] is DBNull ?
                                        null : (bool?)reader["ExitEntrAntiPas"],
                                    ExitEntrEntEgr = reader["ExitEntrEntEgr"] is DBNull ?
                                        null : (bool?)reader["ExitEntrEntEgr"],
                                    ExitEntrRvrsCrd = reader["ExitEntrRvrsCrd"] is DBNull ?
                                        null : (bool?)reader["ExitEntrRvrsCrd"],
                                    ExitKyPdChan = reader["ExitKyPdChan"] is DBNull ?
                                        null : (byte?)reader["ExitKyPdChan"],
                                    ExitNoCommMode = reader["ExitNoCommMode"] is DBNull ?
                                        null : (byte?)reader["ExitNoCommMode"],
                                    ExitNoDataMode = reader["ExitNoDataMode"] is DBNull ?
                                        null : (byte?)reader["ExitNoDataMode"],
                                    ExitNoReEntry = reader["ExitNoReEntry"] is DBNull ?
                                        null : (bool?)reader["ExitNoReEntry"],
                                    ExitNormMode = reader["ExitNormMode"] is DBNull ?
                                        null : (byte?)reader["ExitNormMode"],
                                    ExitPinDuress = reader["ExitPinDuress"] is DBNull ?
                                        null : (bool?)reader["ExitPinDuress"],
                                    ExitRequestChan = reader["ExitRequestChan"] is DBNull ?
                                        null : (byte?)reader["ExitRequestChan"],
                                    ExitRequestType = reader["ExitRequestType"] is DBNull ?
                                        null : (short?)reader["ExitRequestType"],
                                    ExitRvrsCrdDur = reader["ExitRvrsCrdDur"] is DBNull ?
                                        null : (bool?)reader["ExitRvrsCrdDur"],
                                    ExitScheduleHi = reader["ExitScheduleHi"] is DBNull ?
                                        null : (int?)reader["ExitScheduleHi"],
                                    ExitScheduleLo = reader["ExitScheduleLo"] is DBNull ?
                                        null : (int?)reader["ExitScheduleLo"],
                                    ExitZone = reader["ExitZone"] is DBNull ?
                                        null : (float?)reader["ExitZone"],
                                    SvExitZone = reader["SvExitZone"] is DBNull ?
                                        null : (short?)reader["SvExitZone"],
                                    Export = reader["Export"] is DBNull ?
                                        null : (bool?)reader["Export"],
                                    GeneralCode = reader["GeneralCode"] is DBNull ?
                                        null : (short?)reader["GeneralCode"],
                                    Invert = reader["Invert"] is DBNull ?
                                        null : (bool?)reader["Invert"],
                                    LastDepEntrdPntHi = reader["LastDepEntrdPntHi"] is DBNull ?
                                        null : (int?)reader["LastDepEntrdPntHi"],
                                    LastDepEntrdPntLo = reader["LastDepEntrdPntLo"] is DBNull ?
                                        null : (int?)reader["LastDepEntrdPntLo"],
                                    LastDepExitdPntHi = reader["LastDepExitdPntHi"] is DBNull ?
                                        null : (int?)reader["LastDepExitdPntHi"],
                                    LastDepExitdPntLo = reader["LastDepExitdPntLo"] is DBNull ?
                                        null : (int?)reader["LastDepExitdPntLo"],
                                    OpenOnExitReqst = reader["OpenOnExitReqst"] is DBNull ?
                                        null : (bool?)reader["OpenOnExitReqst"],
                                    Port = reader["Port"] is DBNull ?
                                        null : (short?)reader["Port"],
                                    RecordValNoEntryHist = reader["RecordValNoEntryHist"] is DBNull ?
                                        null : (bool?)reader["RecordValNoEntryHist"],
                                    RecordDrAjarHist = reader["RecordDrAjarHist"] is DBNull ?
                                        null : (bool?)reader["RecordDrAjarHist"],
                                    RecordExitRqHist = reader["RecordExitRqHist"] is DBNull ?
                                        null : (bool?)reader["RecordExitRqHist"],
                                    RecordForcedHist = reader["RecordForcedHist"] is DBNull ?
                                        null : (bool?)reader["RecordForcedHist"],
                                    RecordInValHist = reader["RecordInValHist"] is DBNull ?
                                        null : (bool?)reader["RecordInValHist"],
                                    RecordValHist = reader["RecordValHist"] is DBNull ?
                                        null : (bool?)reader["RecordValHist"],
                                    RelockOnClose = reader["RelockOnClose"] is DBNull ?
                                        null : (bool?)reader["RelockOnClose"],
                                    Site1 = reader["Site1"] is DBNull ?
                                        null : (short?)reader["Site1"],
                                    Site2 = reader["Site2"] is DBNull ?
                                        null : (short?)reader["Site2"],
                                    Site3 = reader["Site3"] is DBNull ?
                                        null : (short?)reader["Site3"],
                                    Site4 = reader["Site4"] is DBNull ?
                                        null : (short?)reader["Site4"],
                                    State = reader["State"] is DBNull ?
                                        null : (short?)reader["State"],
                                    UnlockScheduleHi = reader["UnlockScheduleHi"] is DBNull ?
                                        null : (int?)reader["UnlockScheduleHi"],
                                    UnlockScheduleLo = reader["UnlockScheduleLo"] is DBNull ?
                                        null : (int?)reader["UnlockScheduleLo"],
                                    OperatingMode = reader["OperatingMode"] is DBNull ?
                                        null : (short?)reader["OperatingMode"],
                                    AlarmValue = reader["AlarmValue"] is DBNull ?
                                        null : (byte?)reader["AlarmValue"],
                                    ADAExitRqstChan = reader["ADAExitRqstChan"] is DBNull ?
                                        null : (byte?)reader["ADAExitRqstChan"],
                                    ADAExitRqstType = reader["ADAExitRqstType"] is DBNull ?
                                        null : (short?)reader["ADAExitRqstType"],
                                    ADAInputChan = reader["ADAInputChan"] is DBNull ?
                                        null : (byte?)reader["ADAInputChan"],
                                    ADAInputType = reader["ADAInputType"] is DBNull ?
                                        null : (short?)reader["ADAInputType"],
                                    InfRefPoint1 = reader["InfRefPoint1"] is DBNull ?
                                        null : (byte[])reader["InfRefPoint1"],
                                    InfRefPoint2 = reader["InfRefPoint2"] is DBNull ?
                                        null : (byte[])reader["InfRefPoint2"],
                                    InfRefPoint3 = reader["InfRefPoint3"] is DBNull ?
                                        null : (byte[])reader["InfRefPoint3"],
                                    InfRefPoint4 = reader["InfRefPoint4"] is DBNull ?
                                        null : (byte[])reader["InfRefPoint4"],
                                    DbEntryZone = reader["DbEntryZone"] is DBNull ?
                                        null : (short?)reader["DbEntryZone"],
                                    DbExitZone = reader["DbExitZone"] is DBNull ?
                                        null : (short?)reader["DbExitZone"],
                                    ForcedEntryDelay = reader["ForcedEntryDelay"] is DBNull ?
                                        null : (byte?)reader["ForcedEntryDelay"],
                                    VideoLayoutIdHi = reader["VideoLayoutIdHi"] is DBNull ?
                                        null : (int?)reader["VideoLayoutIdHi"],
                                    VideoLayoutIdLo = reader["VideoLayoutIdLo"] is DBNull ?
                                        null : (int?)reader["VideoLayoutIdLo"],
                                    InfCameraPoint1 = reader["InfCameraPoint1"] is DBNull ?
                                        null : (string)reader["InfCameraPoint1"],
                                    InfCameraPoint2 = reader["InfCameraPoint2"] is DBNull ?
                                        null : (string)reader["InfCameraPoint2"],
                                    InfCameraPoint3 = reader["InfCameraPoint3"] is DBNull ?
                                        null : (string)reader["InfCameraPoint3"],
                                    InfCameraPoint4 = reader["InfCameraPoint4"] is DBNull ?
                                        null : (string)reader["InfCameraPoint4"],
                                    InfCameraPoint5 = reader["InfCameraPoint5"] is DBNull ?
                                        null : (string)reader["InfCameraPoint5"],
                                    InfCameraPoint6 = reader["InfCameraPoint6"] is DBNull ?
                                        null : (string)reader["InfCameraPoint6"],
                                    InfCameraPoint7 = reader["InfCameraPoint7"] is DBNull ?
                                        null : (string)reader["InfCameraPoint7"],
                                    InfCameraPoint8 = reader["InfCameraPoint8"] is DBNull ?
                                        null : (string)reader["InfCameraPoint8"],
                                    InfCameraPoint9 = reader["InfCameraPoint9"] is DBNull ?
                                        null : (string)reader["InfCameraPoint9"],
                                    InfCameraPoint10 = reader["InfCameraPoint10"] is DBNull ?
                                        null : (string)reader["InfCameraPoint10"],
                                    InfCameraPoint11 = reader["InfCameraPoint11"] is DBNull ?
                                        null : (string)reader["InfCameraPoint11"],
                                    InfCameraPoint12 = reader["InfCameraPoint12"] is DBNull ?
                                        null : (string)reader["InfCameraPoint12"],
                                    InfCameraPoint13 = reader["InfCameraPoint13"] is DBNull ?
                                        null : (string)reader["InfCameraPoint13"],
                                    InfCameraPoint14 = reader["InfCameraPoint14"] is DBNull ?
                                        null : (string)reader["InfCameraPoint14"],
                                    InfCameraPoint15 = reader["InfCameraPoint15"] is DBNull ?
                                        null : (string)reader["InfCameraPoint15"],
                                    InfCameraPoint16 = reader["InfCameraPoint16"] is DBNull ?
                                        null : (string)reader["InfCameraPoint16"],
                                    ForceLock = reader["ForceLock"] is DBNull ?
                                        null : (byte?)reader["ForceLock"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<DoorList> GetDoorLists()
        {
            Console.WriteLine("GetDoorLists() executed");

            var result = new List<DoorList>();

            try
            {
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "Continuum"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from DoorList";
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                result.Add(new DoorList
                                {
                                    ObjectIdHi = (int)reader["ObjectIdHi"],
                                    ObjectIdLo = (int)reader["ObjectIdLo"],
                                    DoorIdHi = (int)reader["DoorIdHi"],
                                    DoorIdLo = (int)reader["DoorIdLo"],
                                    AreaIdHi = (int)reader["AreaIdHi"],
                                    AreaIdLo = (int)reader["AreaIdLo"],
                                    DeviceIdHi = reader["DeviceIdHi"] is DBNull ?
                                        null : (int?)reader["DeviceIdHi"],
                                    DeviceIdLo = reader["DeviceIdLo"] is DBNull ?
                                        null : (int?)reader["DeviceIdLo"],
                                    NetworkIdHi = reader["NetworkIdHi"] is DBNull ?
                                        null : (int?)reader["NetworkIdHi"],
                                    NetworkIdLo = reader["NetworkIdLo"] is DBNull ?
                                        null : (int?)reader["NetworkIdLo"]
                                });
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public void ExportPersons(List<Personnel> persons)
        {
            Console.WriteLine("ExportPersons() executed");

            try
            {
                //ExecuteImport();
                if (!SavePersons(persons))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
        }

        public bool ExportPersonsDmp(List<PersonInfo> persons)
        {
            Console.WriteLine("ExportPersonsDmp() executed");
            /*for (int i = 0; i < 5; i++)
            {
                try
                {
                    return SavePersonsDmp(persons);
                }
                catch (Exception)
                {
                    Thread.Sleep(200);
                }
            }*/
            return SavePersonsDmp(persons);
        }

        public bool ExportPersonDmp(PersonInfo person)
        {
            Console.WriteLine("ExportPersonsDmp() executed");
           
            return SavePerson(person);
        }

        private bool SavePerson(PersonInfo person)
        {
            int i = 0;
            for (i = 0; i < int.Parse(ConfigurationManager.AppSettings["TryCount"]); i++)
            {
                try
                {
                    SavePersonDmp2(person);
                    Thread.Sleep(200);
                    return true;
                }
                catch (IOException)
                {
                    Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["TryTimeout"]));
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private bool SavePersonsDmp(List<PersonInfo> persons)
        {
            foreach (var person in persons)
            {
                int i = 0;
                for (i = 0; i < 5; i++)
                {
                    try
                    {
                        if (person.CreateFolder)
                        {
                            SaveFolderDmp(person);
                            Thread.Sleep(1500);
                        }
                        SavePersonDmp(person);
                        Thread.Sleep(200);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(200);
                    }
                }
                if (i >= 5)
                {
                    return false;
                }
            }

            return true;
        }

        private bool SaveFolderDmp(PersonInfo person)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Continuum");
            sb.AppendLine("Logical");
            sb.AppendLine();
            string path = "";
            if (person.Containers.Count <= 1)
            {
                sb.AppendLine(string.Format("Path : Root"));
                sb.AppendLine();
                path = "Root";
            }
            else
            {
                sb.Append(string.Format("Path : "));
                for (int i = 0; i < person.Containers.Count - 1; i++)
                {
                    path += person.Containers[i];
                    sb.Append(person.Containers[i]);
                    if (i < person.Containers.Count - 2)
                    {
                        sb.Append("\\");
                        path += "\\";
                    }
                    else
                    {
                        sb.AppendLine();
                    }
                }
            }

            string folder = person.Containers[person.Containers.Count - 1];
            sb.AppendLine();
            sb.AppendLine("Dictionary : ");
            sb.AppendLine(string.Format(" Folder : : {0} : False : {0}", folder));
            sb.AppendLine("EndDictionary");
            sb.AppendLine();
            sb.AppendLine(string.Format("Object : {0}", folder));
            sb.AppendLine(" Type : Folder");
            sb.AppendLine(string.Format(" Owner : {0}", path));
            sb.AppendLine(string.Format(" Alias : {0}", folder));
            sb.AppendLine("EndObject");
            sb.AppendLine();

            File.WriteAllText(ConfigurationManager.AppSettings["DmpFile"], sb.ToString());

            return true;
        }

        private bool SavePersonDmp(PersonInfo person)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Continuum");
            sb.AppendLine("Logical");
            sb.AppendLine();
            string path = "";
            if (person.Containers.Count <= 1)
            {
                sb.AppendLine(string.Format("Path : Root"));
                sb.AppendLine();
                path = "Root";
            }
            else
            {
                sb.Append(string.Format("Path : "));
                for (int i = 0; i < person.Containers.Count; i++)
                {
                    path += person.Containers[i];
                    sb.Append(person.Containers[i]);
                    if (i < person.Containers.Count - 1)
                    {
                        sb.Append("\\");
                        path += "\\";
                    }
                    else
                    {
                        sb.AppendLine();
                    }
                }
            }

            string key = string.Format("{0}_{1}", person.LastName, person.FirstName);
            string alias = string.Format("{0}{1}", person.FirstName[0], person.LastName[0]);

            sb.AppendLine();
            sb.AppendLine("Dictionary : ");
            sb.AppendLine(string.Format(" Personnel : : {0} : False : {1}",
                key, alias));
            sb.AppendLine("EndDictionary");
            sb.AppendLine();
            sb.AppendLine(string.Format("Object : {0}", key));
            sb.AppendLine(" Type : Personnel");
            sb.AppendLine(string.Format(" Owner : {0}", path));
            //sb.AppendLine(string.Format(" Alias : {0}", alias));
            sb.AppendLine(string.Format(" CardNumber : {0}", person.CardNum));
            sb.AppendLine(string.Format(" FirstName : {0}", person.FirstName));
            sb.AppendLine(string.Format(" LastName : {0}", person.LastName));
            sb.AppendLine(" AreaLinks : ");
            foreach (var areaLink in person.Areas)
            {
                sb.AppendLine(string.Format(
                    "  {0} : Enabled : False : : 01.01.1989 ;0 ", areaLink));
            }
            sb.AppendLine(" EndAreaLinks");
            sb.AppendLine("EndObject");
            sb.AppendLine();

            File.WriteAllText(ConfigurationManager.AppSettings["DmpFile"], sb.ToString());

            return true;
        }

        private bool SavePersonDmp2(PersonInfo person)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Continuum");
            sb.AppendLine("Network");
            sb.AppendLine();
            sb.AppendLine(string.Format("Path : {0}", person.Path.TrimEnd(new[] { '\\' })));
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Dictionary : ");
            sb.AppendLine(string.Format(" Personnel : : {0} : False : {1}",
                person.UiName, person.Alias));
            sb.AppendLine("EndDictionary");
            sb.AppendLine();
            sb.AppendLine(string.Format("Object : {0}", person.UiName));
            sb.AppendLine(" Type : Personnel");
            sb.AppendLine(string.Format(" Owner : {0}", person.Path.TrimEnd(new[] { '\\' })));
            sb.AppendLine(string.Format(" Alias : {0}", person.Alias));
            //sb.AppendLine(string.Format(" CardNumber : {0}", person.CardNum));
            sb.AppendLine(" AreaLinks : ");
            foreach (var areaLink in person.Areas)
            {
                sb.AppendLine(string.Format(
                    "  {0} : Enabled : False : : 01.01.1989 ;0 ", areaLink));
            }
            sb.AppendLine(" EndAreaLinks");
            sb.AppendLine("EndObject");
            sb.AppendLine();

            File.WriteAllText(ConfigurationManager.AppSettings["DmpFile"], sb.ToString());

            return true;
        }

        private bool SavePersons(List<Personnel> persons)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("personnel");
                sb.AppendLine("Key,FirstName,LastName");
                sb.AppendLine("FirstName,LastName");
                foreach (var person in persons)
                {
                    sb.AppendLine(string.Format("{0},{1}",
                        person.FirstName, person.LastName));
                }
                sb.AppendLine("");
                File.WriteAllText(ConfigurationManager.AppSettings["ExportFile"],
                    sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR Save Persons in .csv file");
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void ExecuteImport()
        {
            try
            {
                string exportFile = ConfigurationManager.AppSettings["ExportFile"];
                string command = ConfigurationManager.AppSettings["ImportCommand"];
                string commandArgs = ConfigurationManager.AppSettings["ImportCommandArgs"];

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = commandArgs,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR Import Persons from .csv file");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
