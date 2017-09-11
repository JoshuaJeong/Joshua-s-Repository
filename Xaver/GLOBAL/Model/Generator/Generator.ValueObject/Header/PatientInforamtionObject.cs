using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Generator.ValueObject;

namespace Generator.ValueObject
{
    /// <summary>
    /// Patient Inforamtion
    /// 환자 정보 Model
    /// </summary>

    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class RecordTargetObject : ModelBase
    {
        #region :: Private Member
        private string localId;
        private string dateOfBirth;
        private string patientName;
        private GenderType gender;
        private string telecomNumber;
        private string postalCode;
        private string additionalLocator;
        private string streetAddress;
        private CareTypes _CareType;
        private string oID;
        //private string genderCode;
        //private string careTypeCode;
        private string encounterDepartmentName;
        private string encounterDepartmentCode;
        #endregion

        #region :: Key
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public int GetId() { return Id; }
        public void SetId(int _Id) { Id = _Id; }

        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }
        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #endregion

        #region :: Public Property
        /// <summary>
        /// 환자 Local ID
        /// </summary>
        [DataMember]
        public virtual string LocalId
        {
            get { return localId; }
            set { localId = value; OnPropertyChanged("LocalId"); }
        }

        public string GetLocalId() { return LocalId; }
        public void SetLocalId(string _LocalId) { LocalId = _LocalId; }

        /// <summary>
        /// 생년월일 
        /// </summary>
        [DataMember]
        public virtual string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; OnPropertyChanged("DateOfBirth"); }
        }

        public string GetDateOfBirth() { return DateOfBirth; }
        public void SetDateOfBirth(string _DateOfBirth) { DateOfBirth = _DateOfBirth; }

        /// <summary>
        /// 환자 이름
        /// </summary>
        [DataMember]
        public virtual string PatientName
        {
            get { return patientName; }
            set { patientName = value; OnPropertyChanged("PatientName"); }
        }

        public string GetPatientName() { return PatientName; }
        public void SetPatientName(string _PatientName) { PatientName = _PatientName; }

        /// <summary>
        /// 환자성별 구분
        /// </summary>
        [DataMember]
        public virtual GenderType Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }

        public GenderType GetGender() { return Gender; }
        public void SetGender(GenderType _Gender) { Gender = _Gender; }

        public virtual string GenderString
        {
            get { return Gender.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && Gender.ToString() != value)
                {
                    try
                    {
                        Gender = TryParse<GenderType>(value);
                        OnPropertyChanged("GenderString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetGenderString() { return GenderString; }
        public void SetGenderString(string _GenderString) { GenderString = _GenderString; }

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

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
        /// 주소 ( 우편번호 )
        /// </summary>
        [DataMember]
        public virtual string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; OnPropertyChanged("PostalCode"); }
        }

        public string GetPostalCode() { return PostalCode; }
        public void SetPostalCode(string _PostalCode) { PostalCode = _PostalCode; }

        [DataMember]
        public virtual string OID
        {
            get { return oID; }
            set { oID = value; OnPropertyChanged("OID"); }
        }

        public string GetOID() { return OID; }
        public void SetOID(string _OID) { OID = _OID; }

        /// <summary>
        /// 진료 구분
        /// </summary>
        [DataMember]
        public virtual CareTypes CareType
        {
            get { return _CareType; }
            set { _CareType = value; OnPropertyChanged("CareType"); }
        }

        public CareTypes GetCareType() { return CareType; }
        public void SetCareType(CareTypes _CareType) { CareType = _CareType; }

        public virtual string CareTypeString
        {
            get { return CareType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && CareType.ToString() != value)
                {
                    try
                    {
                        CareType = TryParse<CareTypes>(value);
                        OnPropertyChanged("CareTypeString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetCareTypeString() { return CareTypeString; }
        public void SetCareTypeString(string _CareTypeString) { CareTypeString = _CareTypeString; }

        ///// <summary>
        ///// 성별 코드
        ///// </summary>
        //[DataMember]
        //public virtual string GenderCode
        //{
        //    get { return genderCode; }
        //    set { genderCode = value; OnPropertyChanged("GenderCode"); }
        //}

        ///// <summary>
        ///// 진료구분 코드
        ///// </summary>
        //[DataMember]
        //public virtual string CareTypeCode
        //{
        //    get { return careTypeCode; }
        //    set { careTypeCode = value; OnPropertyChanged("GenderCode"); }
        //}

        /// <summary>
        /// 수진 진료과명(심평원 진료과 명칭)
        /// </summary>
        [DataMember]
        public virtual string EncounterDepartmentName
        {
            get { return encounterDepartmentName; }
            set { encounterDepartmentName = value; OnPropertyChanged("EncounterDepartmentName"); }
        }

        public string GetEncounterDepartmentName() { return EncounterDepartmentName; }
        public void SetEncounterDepartmentName(string _EncounterDepartmentNameString) { EncounterDepartmentName = _EncounterDepartmentNameString; }

        /// <summary>
        /// 수진 진료과 코드(심평원 진료과 코드)
        /// </summary>
        [DataMember]
        public virtual string EncounterDepartmentCode
        {
            get { return encounterDepartmentCode; }
            set { encounterDepartmentCode = value; OnPropertyChanged("EncounterDepartmentCode"); }
        }

        public string GetEncounterDepartmentCode() { return EncounterDepartmentCode; }
        public void SetEncounterDepartmentCode(string _EncounterDepartmentCodeString) { EncounterDepartmentCode = _EncounterDepartmentCodeString; }
        #endregion

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public RecordTargetObject()
        {
            this.CareType = CareTypes.NONE;
        }
    }
}





