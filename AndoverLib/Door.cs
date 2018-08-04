using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Door
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
        public byte? ADADoorAjarTime { get; set; }

        [DataMember]
        public byte? ADAOutputTime { get; set; }

        [DataMember]
        public byte? ADAChannel { get; set; }

        [DataMember]
        public byte? AlarmChannel { get; set; }

        [DataMember]
        public float? AlarmRelayTime { get; set; }

        [DataMember]
        public short? ArmCode { get; set; }

        [DataMember]
        public byte? BondChannel { get; set; }

        [DataMember]
        public short? BondType { get; set; }

        [DataMember]
        public short? BondSensor { get; set; }

        [DataMember]
        public short? CardFormats { get; set; }

        [DataMember]
        public int? CardFormats2 { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte? DoorAjarTime { get; set; }

        [DataMember]
        public byte? DoorChannel { get; set; }

        [DataMember]
        public int? DoorScheduleHi { get; set; }

        [DataMember]
        public int? DoorScheduleLo { get; set; }

        [DataMember]
        public byte? DoorStrikeTime { get; set; }

        [DataMember]
        public byte? DoorSwitchChan { get; set; }

        [DataMember]
        public short? DoorSwitchType { get; set; }

        [DataMember]
        public short? EntryAntiPassTim { get; set; }

        [DataMember]
        public int? EntryAreaHi { get; set; }

        [DataMember]
        public int? EntryAreaLo { get; set; }

        [DataMember]
        public byte? EntryIOU { get; set; }

        [DataMember]
        public byte? EntryChannel { get; set; }

        [DataMember]
        public bool? EntryEntEgr { get; set; }

        [DataMember]
        public bool? EntryEntrAntiPas { get; set; }

        [DataMember]
        public bool? EntryEntrEntEgr { get; set; }

        [DataMember]
        public bool? EntryEntrRvrsCrd { get; set; }

        [DataMember]
        public byte? EntryKyPdChan { get; set; }

        [DataMember]
        public byte? EntryNoCommMode { get; set; }

        [DataMember]
        public byte? EntryNoDataMode { get; set; }

        [DataMember]
        public bool? EntryNoReEntry { get; set; }

        [DataMember]
        public byte? EntryNormMode { get; set; }

        [DataMember]
        public bool? EntryPinDuress { get; set; }

        [DataMember]
        public bool? EntryRvrsCrdDur { get; set; }

        [DataMember]
        public int? EntryScheduleHi { get; set; }

        [DataMember]
        public int? EntryScheduleLo { get; set; }

        [DataMember]
        public float? EntryZone { get; set; }

        [DataMember]
        public short? SvEntryZone { get; set; }

        [DataMember]
        public short? ExitAntiPassTim { get; set; }

        [DataMember]
        public int? ExitAreaHi { get; set; }

        [DataMember]
        public int? ExitAreaLo { get; set; }

        [DataMember]
        public int? ExitIOU { get; set; }

        [DataMember]
        public byte? ExitChannel { get; set; }

        [DataMember]
        public bool? ExitEntEgr { get; set; }

        [DataMember]
        public bool? ExitEntrAntiPas { get; set; }

        [DataMember]
        public bool? ExitEntrEntEgr { get; set; }

        [DataMember]
        public bool? ExitEntrRvrsCrd { get; set; }

        [DataMember]
        public byte? ExitKyPdChan { get; set; }

        [DataMember]
        public byte? ExitNoCommMode { get; set; }

        [DataMember]
        public byte? ExitNoDataMode { get; set; }

        [DataMember]
        public bool? ExitNoReEntry { get; set; }

        [DataMember]
        public byte? ExitNormMode { get; set; }

        [DataMember]
        public bool? ExitPinDuress { get; set; }

        [DataMember]
        public byte? ExitRequestChan { get; set; }

        [DataMember]
        public short? ExitRequestType { get; set; }

        [DataMember]
        public bool? ExitRvrsCrdDur { get; set; }

        [DataMember]
        public int? ExitScheduleHi { get; set; }

        [DataMember]
        public int? ExitScheduleLo { get; set; }

        [DataMember]
        public float? ExitZone { get; set; }

        [DataMember]
        public short? SvExitZone { get; set; }

        [DataMember]
        public bool? Export { get; set; }

        [DataMember]
        public short? GeneralCode { get; set; }

        [DataMember]
        public bool? Invert { get; set; }

        [DataMember]
        public int? LastDepEntrdPntHi { get; set; }

        [DataMember]
        public int? LastDepEntrdPntLo { get; set; }

        [DataMember]
        public int? LastDepExitdPntHi { get; set; }

        [DataMember]
        public int? LastDepExitdPntLo { get; set; }

        [DataMember]
        public bool? OpenOnExitReqst { get; set; }

        [DataMember]
        public short? Port { get; set; }

        [DataMember]
        public bool? RecordValNoEntryHist { get; set; }

        [DataMember]
        public bool? RecordDrAjarHist { get; set; }

        [DataMember]
        public bool? RecordExitRqHist { get; set; }

        [DataMember]
        public bool? RecordForcedHist { get; set; }

        [DataMember]
        public bool? RecordInValHist { get; set; }

        [DataMember]
        public bool? RecordValHist { get; set; }

        [DataMember]
        public bool? RelockOnClose { get; set; }

        [DataMember]
        public short? Site1 { get; set; }

        [DataMember]
        public short? Site2 { get; set; }

        [DataMember]
        public short? Site3 { get; set; }

        [DataMember]
        public short? Site4 { get; set; }

        [DataMember]
        public short? State { get; set; }

        [DataMember]
        public int? UnlockScheduleHi { get; set; }

        [DataMember]
        public int? UnlockScheduleLo { get; set; }

        [DataMember]
        public short? OperatingMode { get; set; }

        [DataMember]
        public byte? AlarmValue { get; set; }

        [DataMember]
        public byte? ADAExitRqstChan { get; set; }

        [DataMember]
        public short? ADAExitRqstType { get; set; }

        [DataMember]
        public byte? ADAInputChan { get; set; }

        [DataMember]
        public short? ADAInputType { get; set; }

        [DataMember]
        public byte[] InfRefPoint1 { get; set; }

        [DataMember]
        public byte[] InfRefPoint2 { get; set; }

        [DataMember]
        public byte[] InfRefPoint3 { get; set; }

        [DataMember]
        public byte[] InfRefPoint4 { get; set; }

        [DataMember]
        public short? DbEntryZone { get; set; }

        [DataMember]
        public short? DbExitZone { get; set; }

        [DataMember]
        public byte? ForcedEntryDelay { get; set; }

        [DataMember]
        public int? VideoLayoutIdHi { get; set; }

        [DataMember]
        public int? VideoLayoutIdLo { get; set; }

        [DataMember]
        public string InfCameraPoint1 { get; set; }

        [DataMember]
        public string InfCameraPoint2 { get; set; }

        [DataMember]
        public string InfCameraPoint3 { get; set; }

        [DataMember]
        public string InfCameraPoint4 { get; set; }

        [DataMember]
        public string InfCameraPoint5 { get; set; }

        [DataMember]
        public string InfCameraPoint6 { get; set; }

        [DataMember]
        public string InfCameraPoint7 { get; set; }

        [DataMember]
        public string InfCameraPoint8 { get; set; }

        [DataMember]
        public string InfCameraPoint9 { get; set; }

        [DataMember]
        public string InfCameraPoint10 { get; set; }

        [DataMember]
        public string InfCameraPoint11 { get; set; }

        [DataMember]
        public string InfCameraPoint12 { get; set; }

        [DataMember]
        public string InfCameraPoint13 { get; set; }

        [DataMember]
        public string InfCameraPoint14 { get; set; }

        [DataMember]
        public string InfCameraPoint15 { get; set; }

        [DataMember]
        public string InfCameraPoint16 { get; set; }

        [DataMember]
        public byte? ForceLock { get; set; }
    }
}
