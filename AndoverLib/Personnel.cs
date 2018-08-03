using System;
using System.Runtime.Serialization;

namespace AndoverLib
{
    [DataContract]
    public class Personnel
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
        public DateTime? ActivationDate { get; set; }

        [DataMember]
        public DateTime? SavedActivationDate { get; set; }

        [DataMember]
        public bool? ADA { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public bool? AllowEntEntEgr { get; set; }

        [DataMember]
        public string Blood { get; set; }

        [DataMember]
        public short? CardType { get; set; }

        [DataMember]
        public short? CardType2 { get; set; }

        [DataMember]
        public short? SiteCode { get; set; }

        [DataMember]
        public short? SiteCode2 { get; set; }

        [DataMember]
        public byte[] CardNumber { get; set; }

        [DataMember]
        public byte[] CardNumber2 { get; set; }

        [DataMember]
        public short? SavedCardType { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string CustomControl1 { get; set; }

        [DataMember]
        public string CustomControl2 { get; set; }

        [DataMember]
        public string CustomControl3 { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public bool? DeletePending { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public float? DepartmentCode { get; set; }

        [DataMember]
        public bool? DistFailed { get; set; }

        [DataMember]
        public bool? Duress { get; set; }

        [DataMember]
        public string EmergencyContact { get; set; }

        [DataMember]
        public string EmergencyPhone { get; set; }

        [DataMember]
        public string EmpNumber { get; set; }

        [DataMember]
        public bool? EntryEgress { get; set; }

        [DataMember]
        public DateTime? ExpirationDate { get; set; }

        [DataMember]
        public DateTime? SavedExpirationDate { get; set; }

        [DataMember]
        public string EyeColor { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string HairColor { get; set; }

        [DataMember]
        public string Height { get; set; }

        [DataMember]
        public string HomePhone { get; set; }

        [DataMember]
        public short? InactiveDisableDays { get; set; }

        [DataMember]
        public string Info1 { get; set; }

        [DataMember]
        public string Info2 { get; set; }

        [DataMember]
        public string Info3 { get; set; }

        [DataMember]
        public string Info4 { get; set; }

        [DataMember]
        public string Info5 { get; set; }

        [DataMember]
        public string Info6 { get; set; }

        [DataMember]
        public string JobTitle { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string LicenseNumber { get; set; }

        [DataMember]
        public bool? LostCard { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string OfficeLocation { get; set; }

        [DataMember]
        public string ParkingSticker { get; set; }

        [DataMember]
        public string PhotoFile { get; set; }

        [DataMember]
        public int? PIN { get; set; }

        [DataMember]
        public int? SavedPIN { get; set; }

        [DataMember]
        public byte[] SavedCardNumber { get; set; }

        [DataMember]
        public short? SavedSiteCode { get; set; }

        [DataMember]
        public short? Sex { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string SocSecNo { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public short? State { get; set; }

        [DataMember]
        public short? SavedState { get; set; }

        [DataMember]
        public string StateOfResidence { get; set; }

        [DataMember]
        public string Supervisor { get; set; }

        [DataMember]
        public DateTime? TimeEntered { get; set; }

        [DataMember]
        public int? ValueHi { get; set; }

        [DataMember]
        public int? ValueLo { get; set; }

        [DataMember]
        public string VehicalInfo { get; set; }

        [DataMember]
        public bool? Visitor { get; set; }

        [DataMember]
        public short? Weight { get; set; }

        [DataMember]
        public string WorkPhone { get; set; }

        [DataMember]
        public string Zip { get; set; }

        [DataMember]
        public short? Zone { get; set; }

        [DataMember]
        public int? ZonePointHi { get; set; }

        [DataMember]
        public int? ZonePointLo { get; set; }

        /// <summary>
        /// CardNumber
        /// </summary>
        [DataMember]
        public int? NonABACardNumber { get; set; }

        [DataMember]
        public int? NonABACardNumber2 { get; set; }

        [DataMember]
        public string BLOB_Template { get; set; }

        [DataMember]
        public bool? ExecutivePrivilege { get; set; }

        [DataMember]
        public byte? DefaultClearanceLevel { get; set; }

        [DataMember]
        public short? FipsAgencyCode { get; set; }

        [DataMember]
        public short? FipsOrgId { get; set; }

        [DataMember]
        public int? FipsHmac { get; set; }

        [DataMember]
        public short? FipsSystemCode { get; set; }

        [DataMember]
        public int? FipsCredentialNumber { get; set; }

        [DataMember]
        public byte[] FipsPersonId { get; set; }

        [DataMember]
        public byte? FipsCredentialSeries { get; set; }

        [DataMember]
        public byte? FipsCredentialIssue { get; set; }

        [DataMember]
        public short? FipsOrgCategory { get; set; }

        [DataMember]
        public short? FipsPersonOrgAssociation { get; set; }

        [DataMember]
        public DateTime? FipsExpirationDate { get; set; }

        [DataMember]
        public bool? FipsPivControlled { get; set; }

        [DataMember]
        public short? FipsPivState { get; set; }

        [DataMember]
        public short? SavedCardType2 { get; set; }

        [DataMember]
        public byte[] SavedCardNumber2 { get; set; }

        [DataMember]
        public short? SavedSiteCode2 { get; set; }

        [DataMember]
        public int? CardField1 { get; set; }

        [DataMember]
        public int? CardField2 { get; set; }

        [DataMember]
        public int? CardField3 { get; set; }

        [DataMember]
        public int? CardField4 { get; set; }

        [DataMember]
        public int? CardField5 { get; set; }

        [DataMember]
        public int? CardField6 { get; set; }
    }
}
