using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 검사결과 Model    
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class LaboratoryTestObject : ModelBase
    {
        #region :: Private Member
        private LaboratoryType labType;
        private string testCode;
        private string testName;
        private string entryCode;
        private string entryName;
        private string resultValue;
        private string reference;
        private KostomObject[] kostomCodes;
        private string pacsURL;
        private string accessionNumber;        
        //private string accessionNumber;
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

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
        #endregion

        #region :: Public Property
        [DataMember]
        public virtual string LabTypeString
        {
            get { return labType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && labType.ToString() != value)
                {
                    try
                    {
                        labType = TryParse<LaboratoryType>(value);
                        OnPropertyChanged("LabTypeString");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public string GetLabTypeString() { return LabTypeString; }
        public void SetLabTypeString(string _LabTypeString) { LabTypeString = _LabTypeString; }

        [DataMember]
        public virtual string Extension
        {
            get { return labType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && labType.ToString() != value)
                {
                    try
                    {
                        labType = TryParse<LaboratoryType>(value);
                        OnPropertyChanged("Extension");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public string GetExtension() { return Extension; }
        public void SetExtension(string _Extension) { Extension = _Extension; }

        /// <summary>
        /// 검사유형 구별
        /// </summary>
        [DataMember]
        public virtual LaboratoryType LabType
        {
            get { return labType; }
            set { if (labType != value) { labType = value; OnPropertyChanged("LabType"); } }
        }

        public LaboratoryType GetLabType() { return LabType; }
        public void SetLabType(LaboratoryType _LabType) { LabType = _LabType; }

        private string date;
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
        /// 검사코드 (EDI)
        /// </summary>
        [DataMember]
        public virtual string TestCode
        {
            get { return testCode; }
            set { if (testCode != value) { testCode = value; OnPropertyChanged("TestCode"); } }
        }

        public string GetTestCode() { return TestCode; }
        public void SetTestCode(string _TestCode) { TestCode = _TestCode; }

        /// <summary>
        /// 검사명 (EDI)
        /// </summary>
        [DataMember]
        public virtual string TestName
        {
            get { return testName; }
            set { if (testName != value) { testName = value; OnPropertyChanged("TestName"); } }
        }

        public string GetTestName() { return TestName; }
        public void SetTestName(string _TestName) { TestName = _TestName; }

        /// <summary>
        /// 검사항목 코드(검체검사)
        /// </summary>
        [DataMember]
        public virtual string EntryCode
        {
            get { return entryCode; }
            set { if (entryCode != value) { entryCode = value; OnPropertyChanged("EntryCode"); } }
        }

        public string GetEntryCode() { return EntryCode; }
        public void SetEntryCode(string _EntryCode) { EntryCode = _EntryCode; }

        /// <summary>
        /// 검사항목명 (검체검사)
        /// </summary>
        [DataMember]
        public virtual string EntryName
        {
            get { return entryName; }
            set { if (entryName != value) { entryName = value; OnPropertyChanged("EntryName"); } }
        }

        public string GetEntryName() { return EntryName; }
        public void SetEntryName(string _EntryName) { EntryName = _EntryName; }

        /// <summary>
        /// 검사결과값
        /// </summary>
        [DataMember]
        public virtual string ResultValue
        {
            get { return resultValue; }
            set { if (resultValue != value) { resultValue = value; OnPropertyChanged("ResultValue"); } }
        }

        public string GetResultValue() { return ResultValue; }
        public void SetResultValue(string _ResultValue) { ResultValue = _ResultValue; }

        /// <summary>
        /// 참고치
        /// </summary>
        [DataMember]
        public virtual string Reference
        {
            get { return reference; }
            set { if (reference != value) { reference = value; OnPropertyChanged("Reference"); } }
        }

        public string GetReference() { return Reference; }
        public void SetReference(string _Reference) { Reference = _Reference; }

        /// <summary>
        /// 검사 kostom Code set
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
        /// 이미지 링크
        /// </summary>
        [DataMember]
        public virtual string PacsURL
        {
            get { return pacsURL; }
            set { if (pacsURL != value) { pacsURL = value; OnPropertyChanged("PacsURL"); } }
        }

        public string GetPacsURL() { return PacsURL; }
        public void SetPacsURL(string _PacsURL) { PacsURL = _PacsURL; }

        private bool offLineYN;

        /// <summary>
        /// 오프라인 첨부여부
        /// </summary>
        [DataMember]
        public virtual bool OffLineYN
        {
            get { return offLineYN; }
            set { if (offLineYN != value) { offLineYN = value; OnPropertyChanged("OffLineYN"); } }
        }
        public bool GetOffLineYN() { return OffLineYN; }
        public void SetOffLineYN(bool _OffLineYN) { OffLineYN = _OffLineYN; }


        private string kosUid;

        /// <summary>
        /// KOS UID
        /// </summary>
        [DataMember]
        public virtual string KosUid
        {
            get { return kosUid; }
            set { if (kosUid != value) { kosUid = value; OnPropertyChanged("KosUid"); } }
        }        

        //private bool radiologySendYN;

        ///// <summary>
        ///// 영상검사결과 전송여부
        ///// </summary>
        //[DataMember]
        //public virtual bool RadiologySendYN
        //{
        //    get { return radiologySendYN; }
        //    set { if (radiologySendYN != value) { radiologySendYN = value; OnPropertyChanged("RadiologySendYN"); } }
        //}

        private string webPacsBaseURL;

        /// <summary>
        /// WEB PACS 기본 URL 경로
        /// </summary>
        [DataMember]
        public virtual string WebPacsBaseURL
        {
            get { return webPacsBaseURL; }
            set { if (webPacsBaseURL != value) { webPacsBaseURL = value; OnPropertyChanged("WebPacsBaseURL"); } }
        }

        /// <summary>
        /// Dicom Tag (0008,0050)
        /// - Definition : A RIS generated number that identifies the order for the Study.
        /// </summary>
        [DataMember]
        public virtual string AccessionNumber
        {
            get { return accessionNumber; }
            set { if (accessionNumber != value) { accessionNumber = value; OnPropertyChanged("AccessionNumber"); } }
        }
        #endregion

        #region : Constructor
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public LaboratoryTestObject()
        {
            this.LabType = LaboratoryType.None;
            ClassCode = "BATTERY";
            MoodCode = "EVN";

            EffectiveTimeType = "IVL_TS";
            CodeType = "CD";
            ClassCodeType = "x_ActClassDocumentEntryOrganizer";
            MoodCodeType = "System.String";
            StatusCodeType = "CS";

            TypeCode = "DRIV";
            TypeCodeType = "x_ActRelationshipEntry";
        }
        #endregion        
    }

    #region :  Enum
    /// <summary>
    /// 검사 유형 구별
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum LaboratoryType
    {
        [System.ComponentModel.Description("검체검사")]
        Specimen = 0,
        [System.ComponentModel.Description("병리검사")]
        Pathology = 1,
        [System.ComponentModel.Description("영상검사")]
        Radiology = 2,
        [System.ComponentModel.Description("기능검사")]
        Functional = 3,
        //[System.ComponentModel.Description("요약")]
        //Summary,
        [System.ComponentModel.Description("미지정")]
        None = 4
    }
    #endregion
}

