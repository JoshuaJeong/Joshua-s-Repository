using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 흡연상태
    /// </summary>

    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    internal class SmokingStatusObject : ModelBase
    {
        private string smokingStatus;
        private string smokingStatusCode;

        /// <summary>
        /// 흡연 상태코드
        /// </summary>
        [DataMember]
        internal string SmokingStatusCode
        {
            get { return smokingStatusCode; }
            set { smokingStatusCode = value; OnPropertyChanged("StatusCode"); }
        }
        

        /// <summary>
        /// 흡연 상태명
        /// </summary>
        [DataMember]
        internal string SmokingStatus
        {
            get { return smokingStatus; }
            set { smokingStatus = value; OnPropertyChanged("Status"); }
        }        
    }
}
