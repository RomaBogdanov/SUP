﻿-- Создание БД ContinuumCopy на T-SQL
create database ContinuumCopy;
go

use ContinuumCopy;
go

CREATE TABLE [dbo].[Container](
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[uiName] [varchar](128) NULL,
	[OwnerIdHi] [int] NULL,
	[OwnerIdLo] [int] NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[TemplateFlag] [bit] NULL,
	[TemplateIdHi] [int] NULL,
	[TemplateIdLo] [int] NULL,
	[ControllerName] [varchar](16) NULL,
	[AlarmGraphicPage] [smallint] NULL,
	[ContainerType] [varchar](255) NULL,
	[DefaultControllerIdHi] [int] NULL,
	[DefaultControllerIdLo] [int] NULL,
	[ContainerCreateRuleIdHi] [int] NULL,
	[ContainerCreateRuleIdLo] [int] NULL,
	[VideoLayoutIdHi] [int] NULL,
	[VideoLayoutIdLo] [int] NULL,
	[InfCameraPoint1] [text] NULL,
	[InfCameraPoint2] [text] NULL,
	[InfCameraPoint3] [text] NULL,
	[InfCameraPoint4] [text] NULL,
	[InfCameraPoint5] [text] NULL,
	[InfCameraPoint6] [text] NULL,
	[InfCameraPoint7] [text] NULL,
	[InfCameraPoint8] [text] NULL,
	[InfCameraPoint9] [text] NULL,
	[InfCameraPoint10] [text] NULL,
	[InfCameraPoint11] [text] NULL,
	[InfCameraPoint12] [text] NULL,
	[InfCameraPoint13] [text] NULL,
	[InfCameraPoint14] [text] NULL,
	[InfCameraPoint15] [text] NULL,
	[InfCameraPoint16] [text] NULL
)

CREATE TABLE [dbo].[Device](
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[uiName] [varchar](128) NULL,
	[OwnerIdHi] [int] NULL,
	[OwnerIdLo] [int] NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[TemplateFlag] [bit] NULL,
	[TemplateIdHi] [int] NULL,
	[TemplateIdLo] [int] NULL,
	[ControllerName] [varchar](16) NULL,
	[AlarmGraphicPage] [smallint] NULL,
	[Description] [varchar](255) NULL,
	[APDUSegTimeout] [int] NULL,
	[TimeSyncRecipients] [image] NULL,
	[APDUTimeout] [int] NULL,
	[ApplSoftVer] [varchar](32) NULL,
	[FirmRev] [varchar](128) NULL,
	[Location] [varchar](64) NULL,
	[UTCOffset] [smallint] NULL,
	[VendorId] [smallint] NULL,
	[VendorName] [varchar](64) NULL,
	[SystemStatus] [smallint] NULL,
	[ModelName] [varchar](32) NULL,
	[ProtoVer] [tinyint] NULL,
	[ProtoRevision] [tinyint] NULL,
	[DatabaseRevision] [int] NULL,
	[ProtoConfClass] [tinyint] NULL,
	[ProtServSup] [binary](8) NULL,
	[ProtObjSup] [int] NULL,
	[MaxAPDU] [smallint] NULL,
	[SegSupport] [smallint] NULL,
	[DaylightSav] [bit] NULL,
	[NumAPDURet] [tinyint] NULL,
	[ContainerType] [varchar](32) NULL,
	[CommStatus] [smallint] NULL,
	[IAmBCInterval] [int] NULL,
	[IAmBCScope] [smallint] NULL,
	[IAmRemNetwork] [smallint] NULL,
	[RemDeviceList] [image] NULL,
	[RemInfDevList] [image] NULL,
	[DefaultFolderHi] [int] NULL,
	[DefaultFolderLo] [int] NULL,
	[ProbeTime] [int] NULL,
	[Homepage] [text] NULL,
	[MaxResponseTime] [int] NULL,
	[NetworkId] [tinyint] NULL,
	[DefaultRouter] [int] NULL,
	[IPAddress] [int] NULL,
	[SubnetMask] [int] NULL,
	[PrimaryAccessServer] [tinyint] NULL,
	[SecondaryAccessServer] [tinyint] NULL,
	[ScheduleADL] [tinyint] NULL,
	[CommandlinePrompt] [text] NULL,
	[BadgeFormatFolder] [text] NULL,
	[DefaultBadgeFormat] [text] NULL,
	[DefaultReportViewer] [text] NULL,
	[IncrementReportFile] [bit] NULL,
	[AlarmViewerMaxEntries] [int] NULL,
	[AccessEventViewerMaxEntries] [int] NULL,
	[PEWizardFolder] [text] NULL,
	[AlarmPrinterPath] [text] NULL,
	[SuppressFormFeeds] [bit] NULL,
	[DefaultImageCropping] [bit] NULL,
	[AlarmEmailFormatFile] [text] NULL,
	[AlarmPagerFormatFile] [text] NULL,
	[AlarmPrinterFormatFile] [text] NULL,
	[AcknowledgeEmailFormatFile] [text] NULL,
	[AcknowledgePagerFormatFile] [text] NULL,
	[AcknowledgePrinterFormatFile] [text] NULL,
	[OperatorTextAlarmAck] [bit] NULL,
	[PPBackgroundsFolder] [text] NULL,
	[PPGraphicsFolder] [text] NULL,
	[PPGraphicsLibraryFolder] [text] NULL,
	[ViewAlwaysOnTop] [bit] NULL,
	[MainMenuFile] [text] NULL,
	[BACnetDevice] [bit] NULL,
	[BACnetNetworkNumber] [smallint] NULL,
	[BACnetMacAddress] [varbinary](6) NULL,
	[UrlGraphicsImgFiles] [varchar](255) NULL,
	[UrlGraphicsBckFiles] [varchar](255) NULL,
	[UrlGraphicsFiles] [varchar](255) NULL,
	[UrlWDNServicePort] [varchar](255) NULL,
	[UrlWDNServerName] [varchar](255) NULL,
	[BACnetWorkstation] [bit] NULL,
	[EnableLANDistribution] [bit] NULL,
	[EnableRASDistribution] [bit] NULL,
	[ExtLogLANEnable] [bit] NULL,
	[ExtLogRASEnable] [bit] NULL,
	[MaxSegmentsAccepted] [smallint] NULL,
	[IsBBMD] [bit] NULL,
	[BBMDIPAddress] [int] NULL,
	[BBMDPortNumber] [smallint] NULL,
	[BBMDTimeToLive] [smallint] NULL,
	[MaxInfoFrames] [int] NULL,
	[BacnetMaxMaster] [tinyint] NULL,
	[MaxAsyncRequests] [smallint] NULL,
	[RequestInterval] [int] NULL,
	[WPPContinuousPRate] [int] NULL,
	[SerialNum] [int] NULL,
	[VirtualDevice] [bit] NULL,
	[BackupFailureTimeout] [smallint] NULL,
	[LastRestoreTime] [varbinary](255) NULL,
	[LastBackupTime] [datetime] NULL,
	[LastRestoredFilePath] [varchar](255) NULL,
	[ProbeType] [smallint] NULL,
	[PPActivePollingRate] [smallint] NULL,
	[PPInactivePollingRate] [smallint] NULL,
	[UsePersonnelManager] [bit] NULL,
	[VideoMonitor] [varchar](100) NULL,
	[ProcessVideoEvents] [bit] NULL,
	[ProcessVideoAlarmPoints] [bit] NULL
)
GO

-- Создание Area

CREATE TABLE Area
    (ObjectIdHi                    int not NULL,
     ObjectIdLo                    int not NULL,
     uiName                        VARCHAR(128),
     OwnerIdHi                     int,
     OwnerIdLo                     int,
     DeviceIdHi                    int,
     DeviceIdLo                    int,
     TemplateFlag                  bit,
     TemplateIdHi                  int,
     TemplateIdLo                  int,
     ControllerName                VARCHAR(16),
     AlarmGraphicPage              smallint,
     DeletePending                 tinyint,
     Description                   VARCHAR(32),
     KnownOccupCount               int,
     State                         smallint,
     ForceLock                     tinyint)
GO

-- Создание Personnel

CREATE TABLE Personnel(
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[uiName] [varchar](128) NULL,
	[OwnerIdHi] [int] NULL,
	[OwnerIdLo] [int] NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[TemplateFlag] [bit] NULL,
	[TemplateIdHi] [int] NULL,
	[TemplateIdLo] [int] NULL,
	[ControllerName] [varchar](16) NULL,
	[AlarmGraphicPage] [smallint] NULL,
	[ActivationDate] [datetime] NULL,
	[SavedActivationDate] [datetime] NULL,
	[ADA] [bit] NULL,
	[Address] [varchar](48) NULL,
	[AllowEntEntEgr] [bit] NULL,
	[Blood] [varchar](3) NULL,
	[CardType] [smallint] NULL,
	[CardType2] [smallint] NULL,
	[SiteCode] [smallint] NULL,
	[SiteCode2] [smallint] NULL,
	[CardNumber] [binary](9) NULL,
	[CardNumber2] [binary](9) NULL,
	[SavedCardType] [smallint] NULL,
	[City] [varchar](48) NULL,
	[Country] [varchar](48) NULL,
	[CustomControl1] [varchar](80) NULL,
	[CustomControl2] [varchar](80) NULL,
	[CustomControl3] [varchar](80) NULL,
	[DateOfBirth] [datetime] NULL,
	[DeletePending] [bit] NULL,
	[Department] [varchar](32) NULL,
	[DepartmentCode] [real] NULL,
	[DistFailed] [bit] NULL,
	[Duress] [bit] NULL,
	[EmergencyContact] [varchar](80) NULL,
	[EmergencyPhone] [varchar](40) NULL,
	[EmpNumber] [varchar](16) NULL,
	[EntryEgress] [bit] NULL,
	[ExpirationDate] [datetime] NULL,
	[SavedExpirationDate] [datetime] NULL,
	[EyeColor] [varchar](32) NULL,
	[FirstName] [varchar](32) NULL,
	[HairColor] [varchar](12) NULL,
	[Height] [varchar](16) NULL,
	[HomePhone] [varchar](40) NULL,
	[InactiveDisableDays] [smallint] NULL,
	[Info1] [varchar](40) NULL,
	[Info2] [varchar](40) NULL,
	[Info3] [varchar](40) NULL,
	[Info4] [varchar](40) NULL,
	[Info5] [varchar](40) NULL,
	[Info6] [varchar](40) NULL,
	[JobTitle] [varchar](32) NULL,
	[LastName] [varchar](32) NULL,
	[LicenseNumber] [varchar](12) NULL,
	[LostCard] [bit] NULL,
	[MiddleName] [varchar](40) NULL,
	[OfficeLocation] [varchar](16) NULL,
	[ParkingSticker] [varchar](8) NULL,
	[PhotoFile] [varchar](255) NULL,
	[PIN] [int] NULL,
	[SavedPIN] [int] NULL,
	[SavedCardNumber] [binary](9) NULL,
	[SavedSiteCode] [smallint] NULL,
	[Sex] [smallint] NULL,
	[Signature] [varchar](255) NULL,
	[SocSecNo] [varchar](11) NULL,
	[StartDate] [datetime] NULL,
	[State] [smallint] NULL,
	[SavedState] [smallint] NULL,
	[StateOfResidence] [varchar](2) NULL,
	[Supervisor] [varchar](40) NULL,
	[TimeEntered] [datetime] NULL,
	[ValueHi] [int] NULL,
	[ValueLo] [int] NULL,
	[VehicalInfo] [varchar](40) NULL,
	[Visitor] [bit] NULL,
	[Weight] [smallint] NULL,
	[WorkPhone] [varchar](40) NULL,
	[Zip] [varchar](10) NULL,
	[Zone] [smallint] NULL,
	[ZonePointHi] [int] NULL,
	[ZonePointLo] [int] NULL,
	[NonABACardNumber] [int] NULL,
	[NonABACardNumber2] [int] NULL,
	[BLOB_Template] [varchar](255) NULL,
	[ExecutivePrivilege] [bit] NULL,
	[DefaultClearanceLevel] [tinyint] NULL,
	[FipsAgencyCode] [smallint] NULL,
	[FipsOrgId] [smallint] NULL,
	[FipsHmac] [int] NULL,
	[FipsSystemCode] [smallint] NULL,
	[FipsCredentialNumber] [int] NULL,
	[FipsPersonId] [binary](10) NULL,
	[FipsCredentialSeries] [tinyint] NULL,
	[FipsCredentialIssue] [tinyint] NULL,
	[FipsOrgCategory] [smallint] NULL,
	[FipsPersonOrgAssociation] [smallint] NULL,
	[FipsExpirationDate] [datetime] NULL,
	[FipsPivControlled] [bit] NULL,
	[FipsPivState] [smallint] NULL,
	[SavedCardType2] [smallint] NULL,
	[SavedCardNumber2] [binary](9) NULL,
	[SavedSiteCode2] [smallint] NULL,
	[CardField1] [int] NULL,
	[CardField2] [int] NULL,
	[CardField3] [int] NULL,
	[CardField4] [int] NULL,
	[CardField5] [int] NULL,
	[CardField6] [int] NULL)
GO

-- Создание Schedule

CREATE TABLE Schedule
(
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[uiName] [varchar](128) NULL,
	[OwnerIdHi] [int] NULL,
	[OwnerIdLo] [int] NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[TemplateFlag] [bit] NULL,
	[TemplateIdHi] [int] NULL,
	[TemplateIdLo] [int] NULL,
	[ControllerName] [varchar](16) NULL,
	[AlarmGraphicPage] [smallint] NULL,
	[Value] [varbinary](255) NULL,
	[Note] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
	[EffectivePeriod] [varbinary](64) NULL,
	[WeeklySchedule] [image] NULL,
	[ExceptionSched] [image] NULL,
	[PropertyRef] [image] NULL,
	[Priority] [tinyint] NULL,
	[OccupancyTime] [datetime] NULL,
	[UnoccupancyTime] [datetime] NULL,
	[ActiveText] [varchar](80) NULL,
	[InactiveText] [varchar](80) NULL,
	[State] [smallint] NULL,
	[CalenderRefList] [image] NULL,
	[WeeklySchNotes] [image] NULL,
	[ExcSchedNotes] [image] NULL,
	[ActiveValue] [varbinary](255) NULL,
	[InactiveValue] [varbinary](255) NULL,
	[PackagedDays] [image] NULL,
	[DownloadFlag] [tinyint] NULL,
	[LastDownloadTime] [datetime] NULL,
	[ScheduleType] [tinyint] NULL,
	[OccTimePointHi] [int] NULL,
	[OccTimePointLo] [int] NULL,
	[UnOccTimePointHi] [int] NULL,
	[UnOccTimePointLo] [int] NULL,
	[AutosendFlag] [tinyint] NULL,
	[AutosendTime] [datetime] NULL,
	[UnavailableAttributes] [varchar](255) NULL,
	[SpecialEventName] [image] NULL,
	[TimeScale] [tinyint] NULL,
	[ScheduleDefault] [varbinary](255) NULL,
	[DefaultDataType] [smallint] NULL,
	[OutOfService] [bit] NULL,
	[ApplyMidnightValue] [bit] NULL,
	[DefaultMidnightValue] [varbinary](255) NULL,
	[ClearPastEvents] [bit] NULL
)
GO

CREATE TABLE [dbo].[AreaLink]
(
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[AreaIdHi] [int] NOT NULL,
	[AreaIdLo] [int] NOT NULL,
	[PersonIdHi] [int] NOT NULL,
	[PersonIdLo] [int] NOT NULL,
	[Preload] [bit] NULL,
	[SchedIdHi] [int] NULL,
	[SchedIdLo] [int] NULL,
	[State] [smallint] NULL,
	[TimeEntered] [datetime] NULL,
	[DistPending] [bit] NULL,
	[DeletePending] [bit] NULL,
	[DistTime] [datetime] NULL,
	[TemplateFlag] [tinyint] NULL,
	[ClearanceLevel] [tinyint] NULL
)
GO

CREATE TABLE [dbo].[Door]
(
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[uiName] [varchar](128) NULL,
	[OwnerIdHi] [int] NULL,
	[OwnerIdLo] [int] NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[TemplateFlag] [bit] NULL,
	[TemplateIdHi] [int] NULL,
	[TemplateIdLo] [int] NULL,
	[ControllerName] [varchar](16) NULL,
	[AlarmGraphicPage] [smallint] NULL,
	[ADADoorAjarTime] [tinyint] NULL,
	[ADAOutputTime] [tinyint] NULL,
	[ADAChannel] [tinyint] NULL,
	[AlarmChannel] [tinyint] NULL,
	[AlarmRelayTime] [real] NULL,
	[ArmCode] [smallint] NULL,
	[BondChannel] [tinyint] NULL,
	[BondType] [smallint] NULL,
	[BondSensor] [smallint] NULL,
	[CardFormats] [smallint] NULL,
	[CardFormats2] [int] NULL,
	[Description] [varchar](255) NULL,
	[DoorAjarTime] [tinyint] NULL,
	[DoorChannel] [tinyint] NULL,
	[DoorScheduleHi] [int] NULL,
	[DoorScheduleLo] [int] NULL,
	[DoorStrikeTime] [tinyint] NULL,
	[DoorSwitchChan] [tinyint] NULL,
	[DoorSwitchType] [smallint] NULL,
	[EntryAntiPassTim] [smallint] NULL,
	[EntryAreaHi] [int] NULL,
	[EntryAreaLo] [int] NULL,
	[EntryIOU] [tinyint] NULL,
	[EntryChannel] [tinyint] NULL,
	[EntryEntEgr] [bit] NULL,
	[EntryEntrAntiPas] [bit] NULL,
	[EntryEntrEntEgr] [bit] NULL,
	[EntryEntrRvrsCrd] [bit] NULL,
	[EntryKyPdChan] [tinyint] NULL,
	[EntryNoCommMode] [tinyint] NULL,
	[EntryNoDataMode] [tinyint] NULL,
	[EntryNoReEntry] [bit] NULL,
	[EntryNormMode] [tinyint] NULL,
	[EntryPinDuress] [bit] NULL,
	[EntryRvrsCrdDur] [bit] NULL,
	[EntryScheduleHi] [int] NULL,
	[EntryScheduleLo] [int] NULL,
	[EntryZone] [real] NULL,
	[SvEntryZone] [smallint] NULL,
	[ExitAntiPassTim] [smallint] NULL,
	[ExitAreaHi] [int] NULL,
	[ExitAreaLo] [int] NULL,
	[ExitIOU] [tinyint] NULL,
	[ExitChannel] [tinyint] NULL,
	[ExitEntEgr] [bit] NULL,
	[ExitEntrAntiPas] [bit] NULL,
	[ExitEntrEntEgr] [bit] NULL,
	[ExitEntrRvrsCrd] [bit] NULL,
	[ExitKyPdChan] [tinyint] NULL,
	[ExitNoCommMode] [tinyint] NULL,
	[ExitNoDataMode] [tinyint] NULL,
	[ExitNoReEntry] [bit] NULL,
	[ExitNormMode] [tinyint] NULL,
	[ExitPinDuress] [bit] NULL,
	[ExitRequestChan] [tinyint] NULL,
	[ExitRequestType] [smallint] NULL,
	[ExitRvrsCrdDur] [bit] NULL,
	[ExitScheduleHi] [int] NULL,
	[ExitScheduleLo] [int] NULL,
	[ExitZone] [real] NULL,
	[SvExitZone] [smallint] NULL,
	[Export] [bit] NULL,
	[GeneralCode] [smallint] NULL,
	[Invert] [bit] NULL,
	[LastDepEntrdPntHi] [int] NULL,
	[LastDepEntrdPntLo] [int] NULL,
	[LastDepExitdPntHi] [int] NULL,
	[LastDepExitdPntLo] [int] NULL,
	[OpenOnExitReqst] [bit] NULL,
	[Port] [smallint] NULL,
	[RecordValNoEntryHist] [bit] NULL,
	[RecordDrAjarHist] [bit] NULL,
	[RecordExitRqHist] [bit] NULL,
	[RecordForcedHist] [bit] NULL,
	[RecordInValHist] [bit] NULL,
	[RecordValHist] [bit] NULL,
	[RelockOnClose] [bit] NULL,
	[Site1] [smallint] NULL,
	[Site2] [smallint] NULL,
	[Site3] [smallint] NULL,
	[Site4] [smallint] NULL,
	[State] [smallint] NULL,
	[UnlockScheduleHi] [int] NULL,
	[UnlockScheduleLo] [int] NULL,
	[OperatingMode] [smallint] NULL,
	[AlarmValue] [tinyint] NULL,
	[ADAExitRqstChan] [tinyint] NULL,
	[ADAExitRqstType] [smallint] NULL,
	[ADAInputChan] [tinyint] NULL,
	[ADAInputType] [smallint] NULL,
	[InfRefPoint1] [binary](6) NULL,
	[InfRefPoint2] [binary](6) NULL,
	[InfRefPoint3] [binary](6) NULL,
	[InfRefPoint4] [binary](6) NULL,
	[DbEntryZone] [smallint] NULL,
	[DbExitZone] [smallint] NULL,
	[ForcedEntryDelay] [tinyint] NULL,
	[VideoLayoutIdHi] [int] NULL,
	[VideoLayoutIdLo] [int] NULL,
	[InfCameraPoint1] [text] NULL,
	[InfCameraPoint2] [text] NULL,
	[InfCameraPoint3] [text] NULL,
	[InfCameraPoint4] [text] NULL,
	[InfCameraPoint5] [text] NULL,
	[InfCameraPoint6] [text] NULL,
	[InfCameraPoint7] [text] NULL,
	[InfCameraPoint8] [text] NULL,
	[InfCameraPoint9] [text] NULL,
	[InfCameraPoint10] [text] NULL,
	[InfCameraPoint11] [text] NULL,
	[InfCameraPoint12] [text] NULL,
	[InfCameraPoint13] [text] NULL,
	[InfCameraPoint14] [text] NULL,
	[InfCameraPoint15] [text] NULL,
	[InfCameraPoint16] [text] NULL,
	[ForceLock] [tinyint] NULL)
GO

CREATE TABLE [dbo].[DoorList]
(
	[ObjectIdHi] [int] NOT NULL,
	[ObjectIdLo] [int] NOT NULL,
	[DoorIdHi] [int] NOT NULL,
	[DoorIdLo] [int] NOT NULL,
	[AreaIdHi] [int] NOT NULL,
	[AreaIdLo] [int] NOT NULL,
	[DeviceIdHi] [int] NULL,
	[DeviceIdLo] [int] NULL,
	[NetworkIdHi] [int] NULL,
	[NetworkIdLo] [int] NULL
)
GO
