using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 동의정보 
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ConsentObject : ModelBase
    {
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

        #region :: Private Member
        private string consentTime;                     //동의일자
        //private string policyTypeOID;                   //동의정책 식별자
        private PrivacyPolicyType policyType;           //동의정책 명칭
        //private string policySystemOID;                 //동의정책 관리체계 식별자
        //private string policySystemName;                //동의정책 관리체계 명칭
        //private string consentSubjectName;              //동의주체 성명
        //private string consentSubjectContact;           //동의주체 연락처
        //private string consentSubjectDOB;               //동의주체 생년월일
        //private string additionalLocator;               //동의주체 기본주소
        //private string streetAddress;                   //동의주체 상세주소
        //private string postalCode;                      //동의주체 우편번호
        private RelationshipType relationship;          //환자(동의주체)와의 관계
        //private List<string> exceptDepartmentNames;     //동의제외 진료과명
        private string[] exceptDepartmentCodes;     //동의제외 진료과 코드
        //private string policyText;                      //동의정책 본문
        //private string consentFormTitle;                //동의서 양식 타이틀
        #endregion

        #region :: Public Property
        /// <summary>
        /// 동의시간
        /// </summary>
        [DataMember]
        public virtual string ConsentTime
        {
            get { return consentTime; }
            set { consentTime = value; OnPropertyChanged("ConsentTime"); }
        }

        public string GetConsentTime() { return ConsentTime; }
        public void SetConsentTime(string _ConsentTime) { ConsentTime = _ConsentTime; }

        ///// <summary>
        ///// 동의정책 식별자
        ///// </summary>
        //[DataMember]
        //public virtual string PolicyTypeOID
        //{
        //    get { return policyTypeOID; }
        //    set { policyTypeOID = value; OnPropertyChanged("PolicyTypeOID"); }
        //}

        /// <summary>
        /// 동의정책 명칭
        /// </summary>
        [DataMember]
        public virtual PrivacyPolicyType PolicyType
        {
            get { return policyType; }
            set { policyType = value; OnPropertyChanged("PolicyType"); }
        }

        public PrivacyPolicyType GetPolicyType() { return PolicyType; }
        public void SetPolicyType(PrivacyPolicyType _PolicyType) { PolicyType = _PolicyType; }

        public virtual string PolicyTypeString
        {
            get { return policyType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && policyType.ToString() != value)
                {
                    try
                    {
                        policyType = TryParse<PrivacyPolicyType>(value);
                        OnPropertyChanged("PolicyTypeString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetPolicyTypeString() { return PolicyTypeString; }
        public void SetPolicyTypeString(string _PolicyTypeString) { PolicyTypeString = _PolicyTypeString; }

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

        ///// <summary>
        ///// 동의주체 연락처
        ///// </summary>
        //[DataMember]
        //public virtual string ConsentSubjectContact
        //{
        //    get { return consentSubjectContact; }
        //    set { consentSubjectContact = value; OnPropertyChanged("ConsentSubjectContact"); }
        //}

        ///// <summary>
        ///// 동의주체 주소(기본)
        ///// </summary>
        //[DataMember]
        //public virtual string AdditionalLocator
        //{
        //    get { return additionalLocator; }
        //    set { additionalLocator = value; OnPropertyChanged("AdditionalLocator"); }
        //}

        ///// <summary>
        ///// 동의주체 주소(상세)
        ///// </summary>
        //[DataMember]
        //public virtual string StreetAddress
        //{
        //    get { return streetAddress; }
        //    set { streetAddress = value; OnPropertyChanged("StreetAddress"); }
        //}

        ///// <summary>
        ///// 동의주체 주소(우편번호)
        ///// </summary>
        //[DataMember]
        //public virtual string PostalCode
        //{
        //    get { return postalCode; }
        //    set { postalCode = value; OnPropertyChanged("PostalCode"); }
        //}

        //private string address;
        //public virtual string Address
        //{
        //    get { return string.Format("{0} {1}", additionalLocator, streetAddress); }
        //    set { address = value; OnPropertyChanged("Address"); }
        //}

        /// <summary>
        /// 환자(동의주체)와의 관계
        /// </summary>
        [DataMember]
        public virtual RelationshipType Relationship
        {
            get { return relationship; }
            set { relationship = value; OnPropertyChanged("Relationship"); }
        }

        public RelationshipType GetRelationship() { return Relationship; }
        public void SetRelationship(RelationshipType _Relationship) { Relationship = _Relationship; }

        public virtual string RelationshipString
        {
            get { return relationship.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && relationship.ToString() != value)
                {
                    try
                    {
                        relationship = TryParse<RelationshipType>(value);
                        OnPropertyChanged("RelationshipString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetRelationshipString() { return RelationshipString; }
        public void SetRelationshipString(string _RelationshipString) { RelationshipString = _RelationshipString; }

        ///// <summary>
        ///// 동의제외 진료과명
        ///// </summary>
        //[DataMember]
        //public virtual List<string> ExceptDepartmentNames
        //{
        //    get { return exceptDepartmentNames; }
        //    set 
        //    {
        //        if (value != null)
        //        {
        //            value.ForEach(d => string.Format("d{0}", d));
        //        }
        //        exceptDepartmentNames = value; OnPropertyChanged("ExceptDepartmentNames"); 
        //    }
        //}

        /// <summary>
        /// 동의제외 진료과 코드
        /// </summary>
        [DataMember]
        public virtual string[] ExceptDepartmentCodes
        {
            get { return exceptDepartmentCodes; }
            set { exceptDepartmentCodes = value; OnPropertyChanged("ExceptDepartmentCodes"); }
        }

        public string[] GetExceptDepartmentCodes() { return ExceptDepartmentCodes; }
        public void SetExceptDepartmentCodes(string[] _ExceptDepartmentCodes) { ExceptDepartmentCodes = _ExceptDepartmentCodes; }
        public virtual List<string> ExceptDepartmentCodeList
        {
            get { return ExceptDepartmentCodes != null ? ExceptDepartmentCodes.ToList() : null; }
            set { ExceptDepartmentCodes = value != null ? value.ToArray() : null; OnPropertyChanged("ExceptDepartmentCodes"); }
        }
        public List<string> GetExceptDepartmentCodeList() { return ExceptDepartmentCodeList; }
        public void SetExceptDepartmentCodeList(List<string> _ExceptDepartmentCodeList) { ExceptDepartmentCodeList = _ExceptDepartmentCodeList; }

        #endregion

        #region : 삭제 검토속성
        ///// <summary>
        ///// 동의주체 성명
        ///// </summary>
        //[DataMember]
        //public virtual string ConsentSubjectName
        //{
        //    get { return consentSubjectName; }
        //    set { consentSubjectName = value; OnPropertyChanged("ConsentSubjectName"); }
        //}

        ///// <summary>
        ///// 동의주체 생년월일
        ///// </summary>
        //[DataMember]
        //public virtual string ConsentSubjectDOB
        //{
        //    get { return consentSubjectDOB; }
        //    set { consentSubjectDOB = value; OnPropertyChanged("ConsentSubjectDOB"); }
        //}

        ///// <summary>
        ///// 동의정책 관리체계 식별자
        ///// </summary>
        //[DataMember]
        //public virtual string PolicySystemOID
        //{
        //    get { return policySystemOID; }
        //    set { policySystemOID = value; OnPropertyChanged("PolicySystemOID"); }
        //}

        ///// <summary>
        ///// 동의정책 관리체계 명칭
        ///// </summary>
        //[DataMember]
        //public virtual string PolicySystemName
        //{
        //    get { return policySystemName; }
        //    set { policySystemName = value; OnPropertyChanged("PolicySystemName"); }
        //}

        ///// <summary>
        ///// 동의정책 본문
        ///// </summary>
        //[DataMember]
        //public virtual string PolicyText
        //{
        //    get { return policyText; }
        //    set { policyText = value; OnPropertyChanged("PolicyText"); }
        //}

        ///// <summary>
        ///// 동의서 양식 타이틀
        ///// </summary>
        //[DataMember]
        //public virtual string ConsentFormTitle
        //{
        //    get { return consentFormTitle; }
        //    set { consentFormTitle = value; OnPropertyChanged("ConsentFormTitle"); }
        //}
        #endregion

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public ConsentObject()
        {
            //this.TimeRange = 60;
            this.Relationship = RelationshipType.Myself;
            this.PolicyType = PrivacyPolicyType.NONE;
        }
    }
}
