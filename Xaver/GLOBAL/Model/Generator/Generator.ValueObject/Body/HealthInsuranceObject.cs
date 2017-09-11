using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 건강보험정보 정보 Model
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class HealthInsuranceObject : ModelBase
    {
        #region :: Private Member
        private string insuranceTypeCode;        
        private string insuranceType;        
        #endregion

        #region :: Public Property
        /// <summary>
        /// 보험유형 코드
        /// </summary>
        [DataMember]
        public virtual string InsuranceTypeCode
        {
            get { return insuranceTypeCode; }
            set { insuranceTypeCode = value; OnPropertyChanged("InsuranceTypeCode"); }
        }

        /// <summary>
        /// 보험유형
        /// </summary>
        [DataMember]
        public virtual string InsuranceType
        {
            get { return insuranceType; }
            set { insuranceType = value; OnPropertyChanged("InsuranceType"); }
        }
        #endregion
    }
}
