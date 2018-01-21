using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 생체신호 정보 Model
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class VitalSignsObject : ModelBase
    {
        #region :: Private Member
        private string date;
        private string height;
        private string weight;
        private string bodyTemperature;
        private string bP_Diastolic;
        private string bP_Systolic;
        private string awarenessCondition;
        private string etc;
        private string heartRate;
        private DistinctionType distinction;

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
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
        /// 시행일자
        /// </summary>
        [DataMember]
        public virtual string Date
        {
            get { return date; }
            set { if (date != value) { date = value; OnPropertyChanged("Date"); } }
        }

        public string GetDate() { return Date; }
        public void SetDate(string _Date) { Date = _Date; }

        /// <summary>
        /// 신장
        /// </summary>
        [DataMember]
        public virtual string Height
        {
            get { return height; }
            set { if (height != value) { height = value; OnPropertyChanged("Height"); } }
        }

        public string GetHeight() { return Height; }
        public void SetHeight(string _Height) { Height = _Height; }

        /// <summary>
        /// 몸무게
        /// </summary>
        [DataMember]
        public virtual string Weight
        {
            get { return weight; }
            set { if (weight != value) { weight = value; OnPropertyChanged("Weight"); } }
        }

        public string GetWeight() { return Weight; }
        public void SetWeight(string _Weight) { Weight = _Weight; }

        /// <summary>
        /// 확장기 혈압(저)
        /// </summary>
        [DataMember]
        public virtual string BP_Diastolic
        {
            get { return bP_Diastolic; }
            set { if (bP_Diastolic != value) { bP_Diastolic = value; OnPropertyChanged("BP_Diastolic"); } }
        }

        public string GetBP_Diastolic() { return BP_Diastolic; }
        public void SetBP_Diastolic(string _BP_Diastolic) { BP_Diastolic = _BP_Diastolic; }

        /// <summary>
        /// 수축기 혈압(고)
        /// </summary>
        [DataMember]
        public virtual string BP_Systolic
        {
            get { return bP_Systolic; }
            set { if (bP_Systolic != value) { bP_Systolic = value; OnPropertyChanged("BP_Systolic"); } }
        }

        public string GetBP_Systolic() { return BP_Systolic; }
        public void SetBP_Systolic(string _BP_Systolic) { BP_Systolic = _BP_Systolic; }

        /// <summary>
        /// 체온
        /// </summary>
        [DataMember]
        public virtual string BodyTemperature
        {
            get { return bodyTemperature; }
            set { if (bodyTemperature != value) { bodyTemperature = value; OnPropertyChanged("BodyTemperature"); } }
        }

        public string GetBodyTemperature() { return BodyTemperature; }
        public void SetBodyTemperature(string _BodyTemperature) { BodyTemperature = _BodyTemperature; }

        /// <summary>
        /// 처치 전/후 구별
        /// </summary>
        [DataMember]
        public virtual DistinctionType Distinction
        {
            get { return distinction; }
            set { if (distinction != value) { distinction = value; OnPropertyChanged("Distinction"); } }
        }
        public DistinctionType GetDistinction() { return Distinction; }
        public void SetDistinction(DistinctionType _Distinction) { Distinction = _Distinction; }

        [DataMember]
        public virtual string DistinctionString
        {
            get { return Distinction.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && Distinction.ToString() != value)
                {
                    try
                    {
                        Distinction = TryParse<DistinctionType>(value);
                        OnPropertyChanged("DistinctionString");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 의식상태
        /// </summary>
        [DataMember]
        public virtual string AwarenessCondition
        {
            get { return awarenessCondition; ; }
            set { awarenessCondition = value; OnPropertyChanged("AwarenessCondition"); }
        }

        public string GetAwarenessCondition() { return AwarenessCondition; }
        public void SetAwarenessCondition(string _AwarenessCondition) { AwarenessCondition = _AwarenessCondition; }

        /// <summary>
        /// 기타
        /// </summary>
        [DataMember]
        public virtual string ETC
        {
            get { return etc; }
            set { if (etc != value) { etc = value; OnPropertyChanged("ETC"); } }
        }

        public string GetETC() { return ETC; }
        public void SetETC(string _ETC) { ETC = _ETC; }

        /// <summary>
        /// 심박수
        /// </summary>
        [DataMember]
        public virtual string HeartRate
        {
            get { return heartRate; }
            set { if (heartRate != value) { heartRate = value; OnPropertyChanged("HeartRate"); } }
        }

        public string GetHeartRate() { return HeartRate; }
        public void SetHeartRate(string _HeartRate) { HeartRate = _HeartRate; }
        #endregion

        #region :: Constructor
        public VitalSignsObject()
        {
            ClassCode = "CLUSTER";
            MoodCode = "EVN";

            EffectiveTimeType = "IVL_TS";
            CodeType = "CD";
            ClassCodeType = "x_ActClassDocumentEntryOrganizer";
            MoodCodeType = "System.String";
            StatusCodeType = "CS";

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;
        }
        #endregion
    }

    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.ComponentModel.DefaultValue(DistinctionType.None)]
    public enum DistinctionType
    {
        None = 0,
        PreProcedure = 1,
        PostProcedure = 2
    }
}
