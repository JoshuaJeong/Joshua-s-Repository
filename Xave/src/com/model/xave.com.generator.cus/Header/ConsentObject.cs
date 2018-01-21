using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 동의정보 
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ConsentObject : ModelBase
    {
        #region :: Private Member
        private string consentTime;                     //동의일자     
        private PrivacyPolicyType policyType;           //동의정책 명칭        
        private RelationshipType relationship;          //환자(동의주체)와의 관계        
        private string[] exceptDepartmentCodes;         //동의제외 진료과 코드      
        //private string representiveName; // 대리인 성명
        //private string repersentiveContact; // 대리인 연락처

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
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

        [DataMember]
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

        [DataMember]
        public virtual string ConsentSubjectName { get; set; }

        [DataMember]
        public virtual string ConsentSubjectContact { get; set; }
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
