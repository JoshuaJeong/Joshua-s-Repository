using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 철회정보
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class WithdrawalObject : ModelBase
    {
        #region :: Private Member
        private string withdrawalDate;        
        private PrivacyPolicyType policyType;        
        private RelationshipType relationship;
        private ExceptOrganizationObject[] withdrawalOrganizations;        
        private string withdrawalOrganizationReason;        
        private string[] withdrawalDepartmentCodes;
        private string withdrawalDepartmentReason;
        private string wholeWithdrawalReason;

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
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

        [DataMember]
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

        [DataMember]
        public virtual string RelationshipString
        {
            get { return Relationship.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && Relationship.ToString() != value)
                {
                    try
                    {
                        Relationship = TryParse<RelationshipType>(value);
                        OnPropertyChanged("RelationshipString");
                    }
                    catch
                    {
                    }
                }
            }
        }

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
            set { withdrawalDepartmentCodes = value; OnPropertyChanged("WithdrawalDepartmentCodes"); }
        }

        public string[] GetWithdrawalDepartmentCodes() { return WithdrawalDepartmentCodes; }
        public void SetWithdrawalDepartmentCodes(string[] _WithdrawalDepartmentCodes) { WithdrawalDepartmentCodes = _WithdrawalDepartmentCodes; }        

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

        [DataMember]
        public virtual string WithdrawalSubjectName { get; set; }
        [DataMember]
        public virtual string WithdrawalSubjectContact { get; set; }

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


            private string organizationName;

            [DataMember]
            public virtual string OrganizationName
            {
                get { return organizationName; }
                set { organizationName = value; }
            }
            public string GetName() { return OrganizationName; }
            public void SetName(string _Name) { OrganizationName = _Name; }

        }        
    }
}

