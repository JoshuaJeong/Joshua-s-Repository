using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 법정 전염성 감염병
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class InfectionObject : ModelBase
    {
        #region :: Private Member
        private string onsetDate;
        private string diagnosisDate;
        private string infectionName;
        private string reportedDate;
        private string classification;
        private string testResult;
        private bool admissionYN;
        private string suspectedArea;
        #endregion

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
        /// 발병일자
        /// </summary>
        [DataMember]
        public virtual string OnsetDate
        {
            get { return onsetDate; }
            set { if (onsetDate != value) { onsetDate = value; OnPropertyChanged("OnsetDate"); } }
        }

        public string GetOnsetDate() { return OnsetDate; }
        public void SetOnsetDate(string _OnsetDate) { OnsetDate = _OnsetDate; }

        /// <summary>
        /// 진단일
        /// </summary>
        [DataMember]
        public virtual string DiagnosisDate
        {
            get { return diagnosisDate; }
            set { if (diagnosisDate != value) { diagnosisDate = value; OnPropertyChanged("DiagnosisDate"); } }
        }

        public string GetDiagnosisDate() { return DiagnosisDate; }
        public void SetDiagnosisDate(string _DiagnosisDate) { DiagnosisDate = _DiagnosisDate; }

        /// <summary>
        /// 감염병명
        /// </summary>
        [DataMember]
        public virtual string InfectionName
        {
            get { return infectionName; }
            set { if (infectionName != value) { infectionName = value; OnPropertyChanged("InfectionName"); } }
        }

        public string GetInfectionName() { return InfectionName; }
        public void SetInfectionName(string _InfectionName) { InfectionName = _InfectionName; }

        /// <summary>
        /// 신고일
        /// </summary>
        [DataMember]
        public virtual string ReportedDate
        {
            get { return reportedDate; }
            set { if (reportedDate != value) { reportedDate = value; OnPropertyChanged("ReportedDate"); } }
        }

        public string GetReportedDate() { return ReportedDate; }
        public void SetReportedDate(string _ReportedDate) { ReportedDate = _ReportedDate; }

        /// <summary>
        /// 환자분류
        /// </summary>
        [DataMember]
        public virtual string Classification
        {
            get { return classification; }
            set { if (classification != value) { classification = value; OnPropertyChanged("Classification"); } }
        }

        public string GetClassification() { return Classification; }
        public void SetClassification(string _Classification) { Classification = _Classification; }

        /// <summary>
        /// 확진검사 결과
        /// </summary>
        [DataMember]
        public virtual string TestResult
        {
            get { return testResult; }
            set { if (testResult != value) { testResult = value; OnPropertyChanged("TestResult"); } }
        }

        public string GetTestResult() { return TestResult; }
        public void SetTestResult(string _TestResult) { TestResult = _TestResult; }

        /// <summary>
        /// 입원여부
        /// </summary>
        [DataMember]
        public virtual bool AdmissionYN
        {
            get { return admissionYN; }
            set { if (admissionYN != value) { admissionYN = value; OnPropertyChanged("AdmissionYN"); } }
        }

        public bool GetAdmissionYN() { return AdmissionYN; }
        public void SetAdmissionYN(bool _AdmissionYN) { AdmissionYN = _AdmissionYN; }

        /// <summary>
        /// 추정감염 지역
        /// </summary>
        [DataMember]
        public virtual string SuspectedArea
        {
            get { return suspectedArea; }
            set { if (suspectedArea != value) { suspectedArea = value; OnPropertyChanged("SuspectedArea"); } }
        }
        public string GetSuspectedArea() { return SuspectedArea; }
        public void SetSuspectedArea(string _SuspectedArea) { SuspectedArea = _SuspectedArea; }

        #endregion

        #region :: Constructor
        public InfectionObject()
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