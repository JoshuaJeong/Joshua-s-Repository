using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 소견 및 주의사항
    /// </summary>
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class AssessmentObject : ModelBase
    {
        #region :: Private Member
        private string physicalScienceLab;
        private string assessment;
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
        /// 이학적 검사결과
        /// </summary>
        [DataMember]
        public virtual string PhysicalScienceLab
        {
            get { return physicalScienceLab; }
            set { if (physicalScienceLab != value) { physicalScienceLab = value; OnPropertyChanged("PhysicalScienceLab"); } }
        }

        public string GetPhysicalScienceLab() { return PhysicalScienceLab; }
        public void SetPhysicalScienceLab(string _PhysicalScienceLab) { PhysicalScienceLab = _PhysicalScienceLab; }

        /// <summary>
        /// 기타 소견 및 주의사항
        /// </summary>
        [DataMember]
        public virtual string Assessment
        {
            get { return assessment; }
            set { if (assessment != value) { assessment = value; OnPropertyChanged("Assessment"); } }
        }
        public string GetAssessment() { return Assessment; }
        public void SetAssessment(string _Assessment) { Assessment = _Assessment; }

        #endregion

        #region :: Constructor
        public AssessmentObject()
        {
            ClassCode = string.Empty;
            MoodCode = string.Empty;

            EffectiveTimeType = string.Empty;
            CodeType = string.Empty;
            ClassCodeType = string.Empty;
            MoodCodeType = string.Empty;
            StatusCodeType = string.Empty;

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;
        }
        #endregion
    }
}