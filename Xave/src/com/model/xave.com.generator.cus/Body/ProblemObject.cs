using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using xave.com.generator.cus.Voca;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 진단내역 Model
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ProblemObject : ModelBase
    {
        #region :: Private Member
        private string startDate;
        private string endDate;
        private string problemCode;
        private string problemName;
        private string acuityScale;
        private string problemName_KOR;
        private KostomObject[] kostomCodes;
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

        public virtual string Value { get { return problemCode; } set { problemCode = value; OnPropertyChanged("Value"); } }
        public string GetValue() { return Value; }
        public void SetValue(string _Value) { Value = _Value; }
        public virtual string Name { get { return problemName; } set { problemName = value; OnPropertyChanged("Name"); } }
        public string GetName() { return Name; }
        public void SetName(string _Name) { Name = _Name; }
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
        /// 시작일자
        /// </summary>
        [DataMember]
        public virtual string StartDate
        {
            get { return startDate; }
            set { if (startDate != value) { startDate = value; OnPropertyChanged("StartDate"); } }
        }

        public string GetStartDate() { return StartDate; }
        public void SetStartDate(string _StartDate) { StartDate = _StartDate; }

        /// <summary>
        /// 종료일자
        /// </summary>
        [DataMember]
        public virtual string EndDate
        {
            get { return endDate; }
            set { if (endDate != value) { endDate = value; OnPropertyChanged("EndDate"); } }
        }

        public string GetEndDate() { return EndDate; }
        public void SetEndDate(string _EndDate) { EndDate = _EndDate; }

        /// <summary>
        /// 상병 코드(KCD)
        /// </summary>
        [DataMember]
        public virtual string ProblemCode
        {
            get { return problemCode; }
            set { if (problemCode != value) { problemCode = value; OnPropertyChanged("ProblemCode"); } }
        }

        public string GetProblemCode() { return ProblemCode; }
        public void SetProblemCode(string _ProblemCode) { ProblemCode = _ProblemCode; }

        /// <summary>
        /// 상병명(영문)        
        /// </summary>
        [DataMember]
        public virtual string ProblemName
        {
            get { return problemName; }
            set { if (problemName != value) { problemName = value; OnPropertyChanged("ProblemName"); } }
        }

        public string GetProblemName() { return ProblemName; }
        public void SetProblemName(string _ProblemName) { ProblemName = _ProblemName; }

        /// <summary>
        /// 중증도 분류
        /// </summary>
        [DataMember]
        public virtual string AcuityScale
        {
            get { return acuityScale; }
            set { if (acuityScale != value) { acuityScale = value; OnPropertyChanged("AcuityScale"); } }
        }
        public string GetAcuityScale() { return AcuityScale; }
        public void SetAcuityScale(string _AcuityScale) { AcuityScale = _AcuityScale; }

        /// <summary>
        /// 진단 KOSTOM 코드 set
        /// </summary>
        [DataMember]
        public virtual KostomObject[] KostomCodes
        {
            get { return kostomCodes; }
            set { if (kostomCodes != value) { kostomCodes = value; OnPropertyChanged("KostomCodes"); } }
        }

        public KostomObject[] GetKostomCodes() { return KostomCodes; }
        public void SetKostomCodes(KostomObject[] _KostomCodes) { KostomCodes = _KostomCodes; }        

        /// <summary>
        /// 상병병(KCD 한글)
        /// </summary>
        [DataMember]
        public virtual string ProblemName_KOR
        {
            get { return problemName_KOR; }
            set { problemName_KOR = value; OnPropertyChanged("ProblemName_KOR"); }
        }
        public string GetProblemName_KOR() { return ProblemName_KOR; }
        public void SetProblemName_KOR(string _ProblemName_KOR) { ProblemName_KOR = _ProblemName_KOR; }
      

        private string problem_EngKor;

        public virtual string Problem_EngKor
        {
            get
            {
                if (!string.IsNullOrEmpty(problemName_KOR))
                {
                    return string.Format("{0}( {1} )", problemName, problemName_KOR);
                }
                else
                {
                    return problemName;
                }
            }
            set { problem_EngKor = value; OnPropertyChanged("Problem_EngKor"); }
        }
        public string GetProblem_EngKor() { return Problem_EngKor; }
        public void SetProblem_EngKor(string _Problem_EngKor) { Problem_EngKor = _Problem_EngKor; }

        #endregion

        #region :: Constructor
        public ProblemObject()
        {
            ClassCode = "ACT";
            MoodCode = "EVN";
            TypeCode = string.Empty;
            TypeCodeType = string.Empty;
            EffectiveTimeType = "IVL_TS";
            CodeType = "CE";
            ClassCodeType = "x_ActClassDocumentEntryAct";
            MoodCodeType = "x_DocumentActMood";
            StatusCodeType = "CS";
            ValueClassType = "ANY[]";            
        }
        #endregion
    }
}