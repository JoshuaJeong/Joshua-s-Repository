using Generator.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Generator.ValueObject
{
    /// <summary>
    /// 정보수신처 
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class InformationRecipientObject : ModelBase
    {
        #region :: Key
        private int informationRecipientId;
        public virtual int InformationRecipientId
        {
            get { return informationRecipientId; }
            set { informationRecipientId = value; OnPropertyChanged("InformationRecipientId"); }
        }
        public int GetInformationRecipientId() { return InformationRecipientId; }
        public void SetInformationRecipientId(int _InformationRecipientId) { InformationRecipientId = _InformationRecipientId; }


        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }
        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #endregion

        #region ::  Private Member
        private string id;
        private string oID;
        private string doctorName;
        private string telecomNumber;
        //private string country;
        //private string state;
        private string additionalLocator;
        //private string city;
        private string streetAddress;
        private string postalCode;
        private string organizationName;
        private string departmentName;
        private string departmentCode;
        private string medicalLicenseID;
        #endregion

        #region ::  Public Property
        /// <summary>
        /// 의료기관 OID
        /// </summary>
        [DataMember]
        public virtual string OID
        {
            get { return oID; }
            set { oID = value; OnPropertyChanged("OID"); }
        }

        public string GetOID() { return OID; }
        public void SetOID(string _OID) { OID = _OID; }

        /// <summary>
        /// 요양기관 기호
        /// </summary>
        [DataMember]
        public virtual string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string GetId() { return Id; }
        public void SetId(string _Id) { Id = _Id; }

        /// <summary>
        /// 요양기관명
        /// </summary>
        [DataMember]
        public virtual string OrganizationName
        {
            get { return organizationName; }
            set { organizationName = value; OnPropertyChanged("OrganizationName"); }
        }

        public string GetOrganizationName() { return OrganizationName; }
        public void SetOrganizationName(string _OrganizationName) { OrganizationName = _OrganizationName; }

        /// <summary>
        /// 수신처 진료의 성명
        /// </summary>
        [DataMember]
        public virtual string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; OnPropertyChanged("DoctorName"); }
        }

        public string GetDoctorName() { return DoctorName; }
        public void SetDoctorName(string _DoctorName) { DoctorName = _DoctorName; }

        /// <summary>
        /// 연락처 정보
        /// </summary>
        [DataMember]
        public virtual string TelecomNumber
        {
            get { return telecomNumber; }
            set { telecomNumber = value; OnPropertyChanged("TelecomNumber"); }
        }

        public string GetTelecomNumber() { return TelecomNumber; }
        public void SetTelecomNumber(string _TelecomNumber) { TelecomNumber = _TelecomNumber; }

        /// <summary>
        /// 주소 ( 기본주소 )
        /// </summary>
        [DataMember]
        public virtual string AdditionalLocator
        {
            get { return additionalLocator; }
            set { additionalLocator = value; OnPropertyChanged("AdditionalLocator"); }
        }

        public string GetAdditionalLocator() { return AdditionalLocator; }
        public void SetAdditionalLocator(string _AdditionalLocator) { AdditionalLocator = _AdditionalLocator; }

        /// <summary>
        /// 주소 ( 상세주소 )
        /// </summary>
        [DataMember]
        public virtual string StreetAddress
        {
            get { return streetAddress; }
            set { streetAddress = value; OnPropertyChanged("StreetAddress"); }
        }

        public string GetStreetAddress() { return StreetAddress; }
        public void SetStreetAddress(string _StreetAddress) { StreetAddress = _StreetAddress; }

        /// <summary>
        /// 주소 (우편번호)
        /// </summary>
        [DataMember]
        public virtual string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; OnPropertyChanged("PostalCode"); }
        }

        public string GetPostalCode() { return PostalCode; }
        public void SetPostalCode(string _PostalCode) { PostalCode = _PostalCode; }



        /// <summary>
        /// 진료과명
        /// </summary>
        [DataMember]
        public virtual string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; OnPropertyChanged("DepartmentName"); }
        }

        public string GetDepartmentName() { return DepartmentName; }
        public void SetDepartmentName(string _DepartmentName) { DepartmentName = _DepartmentName; }

        /// <summary>
        /// 진료과 코드
        /// </summary>
        [DataMember]
        public virtual string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; OnPropertyChanged("DepartmentCode"); }
        }

        public string GetDepartmentCode() { return DepartmentCode; }
        public void SetDepartmentCode(string _DepartmentCode) { DepartmentCode = _DepartmentCode; }

        /// <summary>
        /// 진료의 면허번호
        /// </summary>
        [DataMember]
        public virtual string MedicalLicenseID
        {
            get { return medicalLicenseID; }
            set
            {
                medicalLicenseID = value; OnPropertyChanged("MedicalLicenseID");
            }
        }
        public string GetMedicalLicenseID() { return MedicalLicenseID; }
        public void SetMedicalLicenseID(string _MedicalLicenseID) { MedicalLicenseID = _MedicalLicenseID; }

        #endregion
    }
}
