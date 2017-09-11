using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Generator.ValueObject;

namespace Generator.ValueObject
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

        #endregion

        #region :: 추가
        //private List<string> problemName_KOSTOM;
        private string problemName_KOR;
        //private string problemCode_KOSTOM;
        private KostomObject[] kostomCodes;

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
        public virtual List<KostomObject> KostomCodeList
        {
            get { return KostomCodes != null ? KostomCodes.ToList() : null; }
            set { KostomCodes = value != null ? value.ToArray() : null; OnPropertyChanged("KostomCodes"); }
        }
        public List<KostomObject> GetKostomCodeList() { return KostomCodeList; }
        public void SetKostomCodeList(List<KostomObject> _KostomCodeList) { KostomCodeList = _KostomCodeList; }


        ///// <summary>
        ///// 상병병(보건의료용어표준)
        ///// </summary>
        //[DataMember]
        //public virtual List<string> ProblemName_KOSTOM
        //{
        //    get { return problemName_KOSTOM; }
        //    set { if (problemName_KOSTOM != value) { problemName_KOSTOM = value; OnPropertyChanged("ProblemName_KOSTOM"); } }
        //}

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


        ///// <summary>
        ///// 상병코드(보건의료용어표준)
        ///// </summary>
        //[DataMember]
        //public virtual string ProblemCode_KOSTOM
        //{
        //    get { return problemCode_KOSTOM; }
        //    set { problemCode_KOSTOM = value; OnPropertyChanged("ProblemCode_KOSTOM"); }
        //}

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
            //KostomCodes = new List<KostomObject>() { }; KostomCodes.Add(new KostomObject() { Code = "test", CodeSystemName = "1" });
        }
        #endregion
    }
}