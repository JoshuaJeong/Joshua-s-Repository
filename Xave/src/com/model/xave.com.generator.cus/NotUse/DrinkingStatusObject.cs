using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace xave.com.generator.cus
{
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class DrinkingStatusObject : ModelBase
    {
        #region :: Private Member
        private string frequency;
        private string alcoholConsumption;
        private string overdrinking;
        private string frequencyCode;
        private string alcoholConsumptionCode;
        private string overdrinkingCode;
        #endregion

        #region :: Public Property
        /// <summary>
        /// 음주상태코드 명칭(음주빈도)
        /// </summary>
        [DataMember]
        public string Frequency
        {
            get { return frequency; }
            set { frequency = value; OnPropertyChanged("Frequency"); }
        }

        /// <summary>
        /// 음주상태코드 명칭(음주량)
        /// </summary>
        [DataMember]
        public string AlcoholConsumption
        {
            get { return alcoholConsumption; }
            set { alcoholConsumption = value; OnPropertyChanged("AlcoholConsumption"); }
        }

        /// <summary>
        /// 음주상태코드 명칭(과음빈도)
        /// </summary>
        [DataMember]
        public string Overdrinking
        {
            get { return overdrinking; }
            set { overdrinking = value; OnPropertyChanged("Overdrinking"); }
        }

        /// <summary>
        /// 음주상태코드 (음주빈도)
        /// </summary>
        [DataMember]
        public string FrequencyCode
        {
            get { return frequencyCode; }
            set { frequencyCode = value; OnPropertyChanged("FrequencyCode"); }
        }

        /// <summary>
        /// 음주상태코드 (음주량)
        /// </summary>
        [DataMember]
        public string AlcoholConsumptionCode
        {
            get { return alcoholConsumptionCode; }
            set { alcoholConsumptionCode = value; OnPropertyChanged("AlcoholConsumptionCode"); }
        }

        /// <summary>
        /// 음주상태코드 (과음빈도)
        /// </summary>
        [DataMember]
        public string OverdrinkingCode
        {
            get { return overdrinkingCode; }
            set { overdrinkingCode = value; OnPropertyChanged("OverdrinkingCode"); }
        }
        #endregion 
    }
}

