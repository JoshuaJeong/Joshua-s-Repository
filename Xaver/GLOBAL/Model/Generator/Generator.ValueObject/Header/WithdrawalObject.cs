using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 철회정보
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class WithdrawalObject : ModelBase
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

        #region :: Private Member Member
        private string withdrawalDate;
        //private string policyTypeOID;
        private PrivacyPolicyType policyType;
        //private string policySystemOID;
        //private string policySystemName;
        //private string consentSubjectName;
        //private string consentSubjectContact;
        //private string consentSubjectDOB;
        private string additionalLocator;
        private string streetAddress;
        private string postalCode;
        private RelationshipType relationship;
        private ExceptOrganizationObject[] withdrawalOrganizations;
        //private List<string> withdrawalOrganizationOID;
        private string withdrawalOrganizationReason;
        //private List<string> withdrawalDepartmentName;
        private string[] withdrawalDepartmentCodes;
        private string withdrawalDepartmentReason;
        private string wholeWithdrawalReason;
        //private string withdrwalFormTitle;
        //private string policyText;
        #endregion

        #region :: Public Property

        /// <summary>
        /// 동의 철회일자
        /// </summary>
        [DataMember]
        public virtual string WithdrawalDate
        {
            get { return withdrawalDate; }
            set { withdrawalDate = value; OnPropertyChanged("WithdrawalDate"); }
        }

        public string GetWithdrawalDate() { return WithdrawalDate; }
        public void SetWithdrawalDate(string _WithdrawalDate) { WithdrawalDate = _WithdrawalDate; }

        ///// <summary>
        ///// 동의 철회정책 식별자
        ///// </summary>
        //[DataMember]
        //public virtual string PolicyTypeOID
        //{
        //    get { return policyTypeOID; }
        //    set { policyTypeOID = value; OnPropertyChanged("PolicyTypeOID"); }
        //}

        /// <summary>
        /// 동의 철회정책 유형
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
            get { return PolicyType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && PolicyType.ToString() != value)
                {
                    try
                    {
                        PolicyType = TryParse<PrivacyPolicyType>(value);
                        OnPropertyChanged("LabTypeString");
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
        ///// 동의주체 주소(기본주소)
        ///// </summary>
        //[DataMember]
        //public virtual string AdditionalLocator
        //{
        //    get { return additionalLocator; }
        //    set { additionalLocator = value; OnPropertyChanged("AdditionalLocator"); }
        //}

        ///// <summary>
        ///// 동의주체 주소(상세주소)
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

        /// <summary>
        /// 환자(동의주체) 와의 관계
        /// </summary>
        [DataMember]
        public virtual RelationshipType Relationship
        {
            get { return relationship; }
            set { relationship = value; OnPropertyChanged("Relationship"); }
        }

        public RelationshipType GetRelationship() { return Relationship; }
        public void SetRelationship(RelationshipType _Relationship) { Relationship = _Relationship; }

        /// <summary>
        /// 동의철회 의료기관 정보
        /// </summary>
        [DataMember]
        public virtual ExceptOrganizationObject[] WithdrawalOrganizations
        {
            get { return withdrawalOrganizations; }
            set { withdrawalOrganizations = value; OnPropertyChanged("WithdrawalOrganizations"); }
        }

        public ExceptOrganizationObject[] GetWithdrawalOrganizations() { return WithdrawalOrganizations; }
        public void SetWithdrawalOrganizations(ExceptOrganizationObject[] _WithdrawalOrganizations) { WithdrawalOrganizations = _WithdrawalOrganizations; }

        //public virtual List<ExceptOrganizationObject> WithdrawalOrganizationList
        //{
        //    get { return WithdrawalOrganizations != null ? WithdrawalOrganizations.ToList() : null; }
        //    set { WithdrawalOrganizations = value != null ? value.ToArray() : null; OnPropertyChanged("WithdrawalOrganizations"); }
        //}

        //public List<ExceptOrganizationObject> GetWithdrawalOrganizationList() { return WithdrawalOrganizationList; }
        //public void SetWithdrawalOrganizationList(List<ExceptOrganizationObject> _WithdrawalOrganizationList) { WithdrawalOrganizationList = _WithdrawalOrganizationList; }

        /// <summary>
        /// 의료기관 동의철회 사유
        /// </summary>
        [DataMember]
        public virtual string WithdrawalOrganizationReason
        {
            get { return withdrawalOrganizationReason; }
            set { withdrawalOrganizationReason = value; OnPropertyChanged("WithdrawalOrganizationReason"); }
        }


        public string GetWithdrawalOrganizationReason() { return WithdrawalOrganizationReason; }
        public void SetWithdrawalOrganizationReason(string _WithdrawalOrganizationReason) { WithdrawalOrganizationReason = _WithdrawalOrganizationReason; }

        /// <summary>
        /// 동의철회 진료과 코드
        /// </summary>
        [DataMember]
        public virtual string[] WithdrawalDepartmentCodes
        {
            get { return withdrawalDepartmentCodes; }
            set { withdrawalDepartmentCodes = value; OnPropertyChanged("WithdrawalDepartmentCode"); }
        }

        public string[] GetWithdrawalDepartmentCodes() { return WithdrawalDepartmentCodes; }
        public void SetWithdrawalDepartmentCodes(string[] _WithdrawalDepartmentCodes) { WithdrawalDepartmentCodes = _WithdrawalDepartmentCodes; }

        public virtual List<string> WithdrawalDepartmentCodeList
        {
            get { return WithdrawalDepartmentCodes != null ? WithdrawalDepartmentCodes.ToList() : null; }
            set { WithdrawalDepartmentCodes = value != null ? value.ToArray() : null; OnPropertyChanged("WithdrawalDepartmentCode"); }
        }

        public List<string> GetWithdrawalDepartmentCodeList() { return WithdrawalDepartmentCodeList; }
        public void SetWithdrawalDepartmentCodeList(List<string> _WithdrawalDepartmentCodeList) { WithdrawalDepartmentCodeList = _WithdrawalDepartmentCodeList; }

        /// <summary>
        /// 진료과 동의철회 사유
        /// </summary>
        [DataMember]
        public virtual string WithdrawalDepartmentReason
        {
            get { return withdrawalDepartmentReason; }
            set { withdrawalDepartmentReason = value; OnPropertyChanged("WithdrawalDepartmentReason"); }
        }
        public string GetWithdrawalDepartmentReason() { return WithdrawalDepartmentReason; }
        public void SetWithdrawalDepartmentReason(string _WithdrawalDepartmentReason) { WithdrawalDepartmentReason = _WithdrawalDepartmentReason; }


        /// <summary>
        /// 전체동의 철회사유
        /// </summary>
        [DataMember]
        public virtual string WholeWithdrawalReason
        {
            get { return wholeWithdrawalReason; }
            set { wholeWithdrawalReason = value; OnPropertyChanged("WholeWithdrawalReason"); }
        }

        public string GetWholeWithdrawalReason() { return WholeWithdrawalReason; }
        public void SetWholeWithdrawalReason(string _WholeWithdrawalReason) { WholeWithdrawalReason = _WholeWithdrawalReason; }

        #endregion

        /// <summary>
        /// 동의제외 의료기관 정보
        /// </summary>
        [DataContract]
        [System.SerializableAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        public class ExceptOrganizationObject
        {
            private string oid;

            [DataMember]
            public virtual string OID
            {
                get { return oid; }
                set { oid = value; }
            }
            public string GetOID() { return OID; }
            public void SetOID(string _OID) { OID = _OID; }


            private string name;

            [DataMember]
            public virtual string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string GetName() { return Name; }
            public void SetName(string _Name) { Name = _Name; }

        }

        #region : 삭제 고려
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
        ///// 철회서 양식 타이틀
        ///// </summary>
        //[DataMember]
        //public virtual string WithdrwalFormTitle
        //{
        //    get { return withdrwalFormTitle; }
        //    set { withdrwalFormTitle = value; OnPropertyChanged("WithdrwalFormTitle"); }
        //}

        ///// <summary>
        ///// 동의 철회정책 본문
        ///// </summary>
        //[DataMember]
        //public virtual string PolicyText
        //{
        //    get { return policyText; }
        //    set { policyText = value; OnPropertyChanged("PolicyText"); }
        //}

        ///// <summary>
        ///// 동의철회 의료기관 식별번호
        ///// </summary>
        //[DataMember]
        //public virtual List<string> WithdrawalOrganizationOID
        //{
        //    get { return withdrawalOrganizationOID; }
        //    set { withdrawalOrganizationOID = value; OnPropertyChanged("WithdrawalOrganizationOID"); }
        //}
        #endregion
    }
}

