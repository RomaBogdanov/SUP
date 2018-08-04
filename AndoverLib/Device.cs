using System;
using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Device
    {
        [DataMember]
        public int ObjectIdHi { get; set; }

        [DataMember]
        public int ObjectIdLo { get; set; }

        [DataMember]
        public string UiName { get; set; }

        [DataMember]
        public int? OwnerIdHi { get; set; }

        [DataMember]
        public int? OwnerIdLo { get; set; }

        [DataMember]
        public int? DeviceIdHi { get; set; }

        [DataMember]
        public int? DeviceIdLo { get; set; }

        [DataMember]
        public bool? TemplateFlag { get; set; }

        [DataMember]
        public int? TemplateIdHi { get; set; }

        [DataMember]
        public int? TemplateIdLo { get; set; }

        [DataMember]
        public string ControllerName { get; set; }

        [DataMember]
        public short? AlarmGraphicPage { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? APDUSegTimeout { get; set; }

        [DataMember]
        public byte[] TimeSyncRecipients { get; set; }

        [DataMember]
        public int? APDUTimeout { get; set; }

        [DataMember]
        public string ApplSoftVer { get; set; }

        [DataMember]
        public string FirmRev { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public short? UTCOffset { get; set; }

        [DataMember]
        public short? VendorId { get; set; }

        [DataMember]
        public string VendorName { get; set; }

        [DataMember]
        public short? SystemStatus { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public byte? ProtoVer { get; set; }

        [DataMember]
        public byte? ProtoRevision { get; set; }

        [DataMember]
        public int? DatabaseRevision { get; set; }

        [DataMember]
        public byte? ProtoConfClass { get; set; }

        [DataMember]
        public byte[] ProtServSup { get; set; }

        [DataMember]
        public int? ProtObjSup { get; set; }

        [DataMember]
        public short? MaxAPDU { get; set; }

        [DataMember]
        public short? SegSupport { get; set; }

        [DataMember]
        public bool? DaylightSav { get; set; }

        [DataMember]
        public byte? NumAPDURet { get; set; }

        [DataMember]
        public string ContainerType { get; set; }

        [DataMember]
        public short? CommStatus { get; set; }

        [DataMember]
        public int? IAmBCInterval { get; set; }

        [DataMember]
        public short? IAmBCScope { get; set; }

        [DataMember]
        public short? IAmRemNetwork { get; set; }

        [DataMember]
        public byte[] RemDeviceList { get; set; }

        [DataMember]
        public byte[] RemInfDevList { get; set; }

        [DataMember]
        public int? DefaultFolderHi { get; set; }

        [DataMember]
        public int? DefaultFolderLo { get; set; }

        [DataMember]
        public int? ProbeTime { get; set; }

        [DataMember]
        public string Homepage { get; set; }

        [DataMember]
        public int? MaxResponseTime { get; set; }

        [DataMember]
        public byte? NetworkId { get; set; }

        [DataMember]
        public int? DefaultRouter { get; set; }

        [DataMember]
        public int? IPAddress { get; set; }

        [DataMember]
        public int? SubnetMask { get; set; }

        [DataMember]
        public byte? PrimaryAccessServer { get; set; }

        [DataMember]
        public byte? SecondaryAccessServer { get; set; }

        [DataMember]
        public byte? ScheduleADL { get; set; }

        [DataMember]
        public string CommandlinePrompt { get; set; }

        [DataMember]
        public string BadgeFormatFolder { get; set; }

        [DataMember]
        public string DefaultBadgeFormat { get; set; }

        [DataMember]
        public string DefaultReportViewer { get; set; }

        [DataMember]
        public bool? IncrementReportFile { get; set; }

        [DataMember]
        public int? AlarmViewerMaxEntries { get; set; }

        [DataMember]
        public int? AccessEventViewerMaxEntries { get; set; }

        [DataMember]
        public string PEWizardFolder { get; set; }

        [DataMember]
        public string AlarmPrinterPath { get; set; }

        [DataMember]
        public bool? SuppressFormFeeds { get; set; }

        [DataMember]
        public bool? DefaultImageCropping { get; set; }

        [DataMember]
        public string AlarmEmailFormatFile { get; set; }

        [DataMember]
        public string AlarmPagerFormatFile { get; set; }

        [DataMember]
        public string AlarmPrinterFormatFile { get; set; }

        [DataMember]
        public string AcknowledgeEmailFormatFile { get; set; }

        [DataMember]
        public string AcknowledgePagerFormatFile { get; set; }

        [DataMember]
        public string AcknowledgePrinterFormatFile { get; set; }

        [DataMember]
        public bool? OperatorTextAlarmAck { get; set; }

        [DataMember]
        public string PPBackgroundsFolder { get; set; }

        [DataMember]
        public string PPGraphicsFolder { get; set; }

        [DataMember]
        public string PPGraphicsLibraryFolder { get; set; }

        [DataMember]
        public bool? ViewAlwaysOnTop { get; set; }

        [DataMember]
        public string MainMenuFile { get; set; }

        [DataMember]
        public bool? BACnetDevice { get; set; }

        [DataMember]
        public short? BACnetNetworkNumber { get; set; }

        [DataMember]
        public byte[] BACnetMacAddress { get; set; }

        [DataMember]
        public string UrlGraphicsImgFiles { get; set; }

        [DataMember]
        public string UrlGraphicsBckFiles { get; set; }

        [DataMember]
        public string UrlGraphicsFiles { get; set; }

        [DataMember]
        public string UrlWDNServicePort { get; set; }

        [DataMember]
        public string UrlWDNServerName { get; set; }

        [DataMember]
        public bool? BACnetWorkstation { get; set; }

        [DataMember]
        public bool? EnableLANDistribution { get; set; }

        [DataMember]
        public bool? EnableRASDistribution { get; set; }

        [DataMember]
        public bool? ExtLogLANEnable { get; set; }

        [DataMember]
        public bool? ExtLogRASEnable { get; set; }

        [DataMember]
        public short? MaxSegmentsAccepted { get; set; }

        [DataMember]
        public bool? IsBBMD { get; set; }

        [DataMember]
        public int? BBMDIPAddress { get; set; }

        [DataMember]
        public short? BBMDPortNumber { get; set; }

        [DataMember]
        public short? BBMDTimeToLive { get; set; }

        [DataMember]
        public int? MaxInfoFrames { get; set; }

        [DataMember]
        public byte? BacnetMaxMaster { get; set; }

        [DataMember]
        public short? MaxAsyncRequests { get; set; }

        [DataMember]
        public int? RequestInterval { get; set; }

        [DataMember]
        public int? WPPContinuousPRate { get; set; }

        [DataMember]
        public int? SerialNum { get; set; }

        [DataMember]
        public bool? VirtualDevice { get; set; }

        [DataMember]
        public short? BackupFailureTimeout { get; set; }

        [DataMember]
        public byte[] LastRestoreTime { get; set; }

        [DataMember]
        public DateTime? LastBackupTime { get; set; }

        [DataMember]
        public string LastRestoredFilePath { get; set; }

        [DataMember]
        public short? ProbeType { get; set; }

        [DataMember]
        public short? PPActivePollingRate { get; set; }

        [DataMember]
        public short? PPInactivePollingRate { get; set; }

        [DataMember]
        public bool? UsePersonnelManager { get; set; }

        [DataMember]
        public string VideoMonitor { get; set; }

        [DataMember]
        public bool? ProcessVideoEvents { get; set; }

        [DataMember]
        public bool? ProcessVideoAlarmPoints { get; set; }
    }
}
