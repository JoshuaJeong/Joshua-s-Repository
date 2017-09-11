using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
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
        #endregion

        #region :: Key
        private int id;
        private int cdaObjectID;

        public virtual int Id
        {
            get { return id; }
            set { if (id != value) { id = value; OnPropertyChanged("Id"); } }
        }

        public int GetId() { return Id; }
        public void SetId(int _Id) { Id = _Id; }

        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { if (cdaObjectID != value) { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); } }
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

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
        #endregion

        #region :: Public Property
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
                        OnPropertyChanged("LabTypeString");
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

        public virtual List<KostomObject> KostomCodeList
        {
            get { return KostomCodes != null ? KostomCodes.ToList() : null; }
            set { KostomCodes = value != null ? value.ToArray() : null; OnPropertyChanged("KostomCodes"); }
        }

        public List<KostomObject> GetKostomCodeList() { return KostomCodeList; }
        public void SetKostomCodeList(List<KostomObject> _KostomCodeList) { KostomCodeList = _KostomCodeList; }

        /// <summary>
        /// 이미지 링크
        /// </summary>
        [DataMember]
        public virtual string PacsURL
        {
            get { return pacsURL; }
            set { if (pacsURL != value) { pacsURL = value; OnPropertyChanged("ImageLink"); } }
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

        #region : 삭제예정
        //private string entryName_KOSTOM;
        //private string entryCode_KOSTOM;        

        //private string labTypeName;

        ///// <summary>
        ///// 검사유형 명칭
        ///// </summary>
        //[DataMember]
        //public string LabTypeName
        //{
        //    get { return labTypeName; }
        //    set { this.labTypeName = value; OnPropertyChanged("LabTypeName"); }
        //}

        //private string orderDate;

        ///// <summary>
        ///// 검사처방일
        ///// </summary>
        //[DataMember]
        //public string OrderDate
        //{
        //    get { return orderDate; }
        //    set { this.orderDate = value; OnPropertyChanged("OrderDate"); }
        //}

        //private string resultDate;

        ///// <summary>
        ///// 최초 결과  보고일
        ///// </summary>
        //[DataMember]
        //public string ResultDate
        //{
        //    get { return resultDate; }
        //    set { this.resultDate = value; OnPropertyChanged("ResultDate"); }
        //}

        //private string resultUnit;

        ///// <summary>
        ///// 결과 단위
        ///// </summary>
        //[DataMember]
        //public string ResultUnit
        //{
        //    get { return resultUnit; }
        //    set { resultUnit = value; }
        //}

        //private string referenceUnit;

        ///// <summary>
        ///// 참고치 단위
        ///// </summary>
        //[DataMember]
        //public string ReferenceUnit
        //{
        //    get { return referenceUnit; }
        //    set { this.referenceUnit = value; OnPropertyChanged("ReferenceUnit"); }
        //}

        //private string referenceLow;

        ///// <summary>
        ///// 참고치(최소값)
        ///// </summary>
        //[DataMember]
        //public string ReferenceLowValue
        //{
        //    get { return referenceLow; }
        //    set { referenceLow = value; }
        //}
        //private string referenceHigh;

        ///// <summary>
        ///// 참고치(최대값)
        ///// </summary>
        //[DataMember]
        //public string ReferenceHighValue
        //{
        //    get { return referenceHigh; }
        //    set { referenceHigh = value; }
        //}

        //private string date;
        ///// <summary>
        ///// 검사일자
        ///// </summary>
        //[DataMember]
        //public string Date
        //{
        //    get { return date; }
        //    set { this.date = value; OnPropertyChanged("Date"); }
        //}

        ///// <summary>
        ///// KOSTOM 검사항목명
        ///// </summary>
        //[DataMember]
        //public virtual string EntryName_KOSTOM
        //{
        //    get { return entryName_KOSTOM; }
        //    set { if (entryName_KOSTOM != value) { entryName_KOSTOM = value; OnPropertyChanged("EntryName_KOSTOM"); } }
        //}

        ///// <summary>
        ///// KOSTOM 검사항목 코드
        ///// </summary>
        //[DataMember]
        //public virtual string EntryCode_KOSTOM
        //{
        //    get { return entryCode_KOSTOM; }
        //    set { if (entryCode_KOSTOM != value) { entryCode_KOSTOM = value; OnPropertyChanged("EntryCode_KOSTOM"); } }
        //}
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

