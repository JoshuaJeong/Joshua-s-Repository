using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 수술 정보 Model
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ProcedureObject : ModelBase
    {
        #region:: Private Member
        private string date;
        //private string procedureCode;
        private string procedureCode_ICD9CM;
        //private string procedureName;
        private string procedureName_ICD9CM;
        private string postDiagnosisName;
        private string anesthesia;
        private KostomObject[] kostomCodes;
        private string text;
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
        /// 수술일자
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
        /// 수술코드 (ICD-9-CM)
        /// </summary>
        [DataMember]
        public virtual string ProcedureCode_ICD9CM
        {
            get { return procedureCode_ICD9CM; }
            set { if (procedureCode_ICD9CM != value) { procedureCode_ICD9CM = value; OnPropertyChanged("ProcedureCode_ICD9CM"); } }
        }

        public string GetProcedureCode_ICD9CM() { return ProcedureCode_ICD9CM; }
        public void SetProcedureCode_ICD9CM(string _ProcedureCode_ICD9CM) { ProcedureCode_ICD9CM = _ProcedureCode_ICD9CM; }

        /// <summary>
        /// 수술명 (ICD-9-CM)
        /// </summary>
        [DataMember]
        public virtual string ProcedureName_ICD9CM
        {
            get { return procedureName_ICD9CM; }
            set { if (procedureName_ICD9CM != value) { procedureName_ICD9CM = value; OnPropertyChanged("ProcedureName_ICD9CM"); } }
        }

        public string GetProcedureName_ICD9CM() { return ProcedureName_ICD9CM; }
        public void SetProcedureName_ICD9CM(string _ProcedureName_ICD9CM) { ProcedureName_ICD9CM = _ProcedureName_ICD9CM; }

        /// <summary>
        /// 수술 후 진단명
        /// </summary>
        [DataMember]
        public virtual string PostDiagnosisName
        {
            get { return postDiagnosisName; }
            set { if (postDiagnosisName != value) { postDiagnosisName = value; OnPropertyChanged("PostDiagnosisName"); } }
        }

        public string GetPostDiagnosisName() { return PostDiagnosisName; }
        public void SetPostDiagnosisName(string _PostDiagnosisName) { PostDiagnosisName = _PostDiagnosisName; }

        /// <summary>
        /// 마취종류
        /// </summary>
        [DataMember]
        public virtual string Anesthesia
        {
            get { return anesthesia; }
            set { if (anesthesia != value) { anesthesia = value; OnPropertyChanged("Anesthesia"); } }
        }

        public string GetAnesthesia() { return Anesthesia; }
        public void SetAnesthesia(string _Anesthesia) { Anesthesia = _Anesthesia; }

        /// <summary>
        /// 수술 KOSTOM code set
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
        /// 응급처치사항
        /// </summary>
        [DataMember]
        public virtual string Text
        {
            get { return text; }
            set { if (text != value) { text = value; OnPropertyChanged("Text"); } }
        }
        public string GetText() { return Text; }
        public void SetText(string _Text) { Text = _Text; }        
        #endregion

        #region :: Constructor
        public ProcedureObject()
        {
            ClassCode = "PROC";
            MoodCode = "EVN";

            EffectiveTimeType = "IVL_TS";
            CodeType = "CD";
            ClassCodeType = "System.String";
            MoodCodeType = "x_DocumentProcedureMood";
            StatusCodeType = "CS";

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;
        }
        #endregion
    }
}