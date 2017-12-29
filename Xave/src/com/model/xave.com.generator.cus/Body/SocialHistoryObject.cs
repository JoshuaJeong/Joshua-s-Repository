using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// Social History Model
    /// </summary>
    [Serializable]
    [DataContract]
    [System.ServiceModel.XmlSerializerFormat]
    public class SocialHistoryObject : ModelBase
    {
        #region :: Private Member
        private string smokingStatus;
        private string smokingStatusCode;
        private string frequency;
        private string alcoholConsumption;
        private string overdrinking;
        private string frequencyCode;
        private string alcoholConsumptionCode;
        private string overdrinkingCode;
        #endregion        

        #region :: Fixed Value
        private string classCode = string.Empty;
        private string moodCode = string.Empty;
        private string typeCode = string.Empty;
        private string typeCodeType = string.Empty;
        private string effectiveTimeType = string.Empty;
        private string codeType = string.Empty;
        private string classCodeType = string.Empty;
        private string moodCodeType = string.Empty;
        private string statusCodeType = string.Empty;
        private string valueClassType = string.Empty;

        private string codeSystem = string.Empty;
        public virtual string CodeSystem { get { return codeSystem; } set { codeSystem = value; OnPropertyChanged("CodeSystem"); } }

        public string GetCodeSystem() { return CodeSystem; }
        public void SetCodeSystem(string _CodeSystem) { CodeSystem = _CodeSystem; }


        private string codeSystemName = string.Empty;
        public virtual string CodeSystemName { get { return codeSystemName; } set { codeSystemName = value; OnPropertyChanged("CodeSystemName"); } }

        public string GetCodeSystemName() { return CodeSystemName; }
        public void SetCodeSystemName(string _CodeSystemName) { CodeSystemName = _CodeSystemName; }


        public virtual string Name { get { return smokingStatus; } set { smokingStatus = value; OnPropertyChanged("Name"); } }
        public string GetName() { return Name; }
        public void SetName(string _Name) { Name = _Name; }


        public virtual string Value { get { return smokingStatusCode; } set { smokingStatusCode = value; OnPropertyChanged("Value"); } }
        public string GetValue() { return Value; }
        public void SetValue(string _Value) { Value = _Value; }


        public virtual string ValueClassType { get { return valueClassType; } set { valueClassType = value; OnPropertyChanged("ValueClassType"); } }
        public string GetValueClassType() { return ValueClassType; }
        public void SetValueClassType(string _ValueClassType) { ValueClassType = _ValueClassType; }




        public virtual string ClassCode { get { return classCode; } set { classCode = value; OnPropertyChanged("ClassCode"); } }
        public string GetClassCode() { return ClassCode; }
        public void SetClassCode(string _ClassCode) { ClassCode = _ClassCode; }

        public virtual string MoodCode { get { return moodCode; } set { moodCode = value; OnPropertyChanged("MoodCode"); } }
        public string GetMoodCode() { return MoodCode; }
        public void SetMoodCode(string _MoodCode) { MoodCode = _MoodCode; }

        public virtual string TypeCode { get { return typeCode; } set { typeCode = value; OnPropertyChanged("TypeCode"); } }
        public string GetTypeCode() { return TypeCode; }
        public void SetTypeCode(string _TypeCode) { TypeCode = _TypeCode; }

        public virtual string TypeCodeType { get { return typeCodeType; } set { typeCodeType = value; OnPropertyChanged("TypeCodeType"); } }
        public string GetTypeCodeType() { return TypeCodeType; }
        public void SetTypeCodeType(string _TypeCodeType) { TypeCodeType = _TypeCodeType; }

        public virtual string EffectiveTimeType { get { return effectiveTimeType; } set { effectiveTimeType = value; OnPropertyChanged("EffectiveTimeType"); } }
        public string GetEffectiveTimeType() { return EffectiveTimeType; }
        public void SetEffectiveTimeType(string _EffectiveTimeType) { EffectiveTimeType = _EffectiveTimeType; }

        public virtual string CodeType { get { return codeType; } set { codeType = value; OnPropertyChanged("CodeType"); } }
        public string GetCodeType() { return CodeType; }
        public void SetCodeType(string _CodeType) { CodeType = _CodeType; }

        public virtual string ClassCodeType { get { return classCodeType; } set { classCodeType = value; OnPropertyChanged("ClassCodeType"); } }
        public string GetClassCodeType() { return ClassCodeType; }
        public void SetClassCodeType(string _ClassCodeType) { ClassCodeType = _ClassCodeType; }

        public virtual string MoodCodeType { get { return moodCodeType; } set { moodCodeType = value; OnPropertyChanged("MoodCodeType"); } }
        public string GetMoodCodeType() { return MoodCodeType; }
        public void SetMoodCodeType(string _MoodCodeType) { MoodCodeType = _MoodCodeType; }

        public virtual string StatusCodeType { get { return statusCodeType; } set { statusCodeType = value; OnPropertyChanged("StatusCodeType"); } }
        public string GetStatusCodeType() { return StatusCodeType; }
        public void SetStatusCodeType(string _StatusCodeType) { StatusCodeType = _StatusCodeType; }

        #endregion

        #region :: Public Property
        /// <summary>
        /// 흡연 상태명
        /// </summary>
        [DataMember]
        public virtual string SmokingStatus
        {
            get { return smokingStatus; }
            set { if (smokingStatus != value) { smokingStatus = value; OnPropertyChanged("SmokingStatus"); } }
        }

        public string GetSmokingStatus() { return SmokingStatus; }
        public void SetSmokingStatus(string _SmokingStatus) { SmokingStatus = _SmokingStatus; }

        /// <summary>
        /// 흡연 상태코드
        /// </summary>
        [DataMember]
        public virtual string SmokingStatusCode
        {
            get { return smokingStatusCode; }
            set { if (smokingStatus != value) { smokingStatusCode = value; OnPropertyChanged("SmokingStatusCode"); } }
        }

        public string GetSmokingStatusCode() { return SmokingStatusCode; }
        public void SetSmokingStatusCode(string _SmokingStatusCode) { SmokingStatusCode = _SmokingStatusCode; }        

        /// <summary>
        /// 음주상태코드 명칭(음주빈도)
        /// </summary>
        [DataMember]
        public virtual string Frequency
        {
            get { return frequency; }
            set { if (frequency != value) { frequency = value; OnPropertyChanged("Frequency"); } }
        }

        public string GetFrequency() { return Frequency; }
        public void SetFrequency(string _Frequency) { Frequency = _Frequency; }

        /// <summary>
        /// 음주상태코드 명칭(음주량)
        /// </summary>
        [DataMember]
        public virtual string AlcoholConsumption
        {
            get { return alcoholConsumption; }
            set { if (alcoholConsumption != value) { alcoholConsumption = value; OnPropertyChanged("AlcoholConsumption"); } }
        }

        public string GetAlcoholConsumption() { return AlcoholConsumption; }
        public void SetAlcoholConsumption(string _AlcoholConsumption) { AlcoholConsumption = _AlcoholConsumption; }

        /// <summary>
        /// 음주상태코드 명칭(과음빈도)
        /// </summary>
        [DataMember]
        public virtual string Overdrinking
        {
            get { return overdrinking; }
            set { if (overdrinking != value) { overdrinking = value; OnPropertyChanged("Overdrinking"); } }
        }

        public string GetOverdrinking() { return Overdrinking; }
        public void SetOverdrinking(string _Overdrinking) { Overdrinking = _Overdrinking; }

        /// <summary>
        /// 음주상태코드 (음주빈도)
        /// </summary>
        [DataMember]
        public virtual string FrequencyCode
        {
            get { return frequencyCode; }
            set { if (frequencyCode != value) { frequencyCode = value; OnPropertyChanged("FrequencyCode"); } }
        }

        public string GetFrequencyCode() { return FrequencyCode; }
        public void SetFrequencyCode(string _FrequencyCode) { FrequencyCode = _FrequencyCode; }

        /// <summary>
        /// 음주상태코드 (음주량)
        /// </summary>
        [DataMember]
        public virtual string AlcoholConsumptionCode
        {
            get { return alcoholConsumptionCode; }
            set { if (alcoholConsumptionCode != value) { alcoholConsumptionCode = value; OnPropertyChanged("AlcoholConsumptionCode"); } }
        }

        public string GetAlcoholConsumptionCode() { return AlcoholConsumptionCode; }
        public void SetAlcoholConsumptionCode(string _AlcoholConsumptionCode) { AlcoholConsumptionCode = _AlcoholConsumptionCode; }

        /// <summary>
        /// 음주상태코드 (과음빈도)
        /// </summary>
        [DataMember]
        public virtual string OverdrinkingCode
        {
            get { return overdrinkingCode; }
            set { if (overdrinkingCode != value) { overdrinkingCode = value; OnPropertyChanged("OverdrinkingCode"); } }
        }
        public string GetOverdrinkingCode() { return OverdrinkingCode; }
        public void SetOverdrinkingCode(string _OverdrinkingCode) { OverdrinkingCode = _OverdrinkingCode; }

        #endregion

        #region :: Constructor
        public SocialHistoryObject()
        {
            ClassCode = string.Empty;
            MoodCode = string.Empty;

            EffectiveTimeType = "IVL_TS";
            CodeType = string.Empty;
            ClassCodeType = string.Empty;
            MoodCodeType = string.Empty;
            StatusCodeType = "CS";

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;

            CodeSystem = "2.16.840.1.113883.6.1";
            CodeSystemName = "LOINC";

            ValueClassType = "CD";
        }
        #endregion
    }
}
