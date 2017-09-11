using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Generator.ValueObject;

namespace Generator.ValueObject
{
    /// <summary>
    /// 투약내역 Model
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class MedicationObject : ModelBase
    {
        #region :: Private Member
        private string startDate;
        private string medicationCode;
        private string medicationName;
        private string doseQuantity;
        private string doseQuantityUnit;
        private string repeatNumber;
        private string period;
        private string majorComponentCode;
        private string majorComponent;
        private string usage;
        private string beginningDate;
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

        private string consumableCodeSystemName = string.Empty;
        public virtual string ConsumableCodeSystemName { get { return consumableCodeSystemName; } set { consumableCodeSystemName = value; OnPropertyChanged("ConsumableCodeSystemName"); } }

        public string GetConsumableCodeSystemName() { return ConsumableCodeSystemName; }
        public void SetConsumableCodeSystemName(string _ConsumableCodeSystemName) { ConsumableCodeSystemName = _ConsumableCodeSystemName; }

        private string consumableCodeSystem = string.Empty;
        public virtual string ConsumableCodeSystem { get { return consumableCodeSystem; } set { consumableCodeSystem = value; OnPropertyChanged("ConsumableCodeSystem"); } }

        public string GetConsumableCodeSystem() { return ConsumableCodeSystem; }
        public void SetConsumableCodeSystem(string _ConsumableCodeSystem) { ConsumableCodeSystem = _ConsumableCodeSystem; }

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
        /// 투여일자(최초) / 약처방일자
        /// </summary>
        [DataMember]
        public virtual string StartDate
        {
            get { return startDate; }
            set { if (startDate != value) { startDate = value; OnPropertyChanged("StartDate"); } }
        }

        public string GetStartDate() { return StartDate; }
        public void SetStartDate(string _StartDate) { StartDate = _StartDate; }

        private string endDate;
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
        /// 투약코드 (KD)
        /// </summary>
        [DataMember]
        public virtual string MedicationCode
        {
            get { return medicationCode; }
            set { if (medicationCode != value) { medicationCode = value; OnPropertyChanged("MedicationCode"); } }
        }

        public string GetMedicationCode() { return MedicationCode; }
        public void SetMedicationCode(string _MedicationCode) { MedicationCode = _MedicationCode; }

        /// <summary>
        /// 투약코드 명칭(EDI)
        /// </summary>
        [DataMember]
        public virtual string MedicationName
        {
            get { return medicationName; }
            set { if (medicationName != value) { medicationName = value; OnPropertyChanged("MedicationName"); } }
        }

        public string GetMedicationName() { return MedicationName; }
        public void SetMedicationName(string _MedicationName) { MedicationName = _MedicationName; }

        /// <summary>
        /// 용량
        /// </summary>
        [DataMember]
        public virtual string DoseQuantity
        {
            get { return doseQuantity; }
            set { if (doseQuantity != value) { doseQuantity = value; OnPropertyChanged("DoseQuantity"); } }
        }

        public string GetDoseQuantity() { return DoseQuantity; }
        public void SetDoseQuantity(string _DoseQuantity) { DoseQuantity = _DoseQuantity; }

        /// <summary>
        /// 복용단위
        /// </summary>
        [DataMember]
        public virtual string DoseQuantityUnit
        {
            get { return doseQuantityUnit; }
            set { if (doseQuantityUnit != value) { doseQuantityUnit = value; OnPropertyChanged("DoseQuantityUnit"); } }
        }

        public string GetDoseQuantityUnit() { return DoseQuantityUnit; }
        public void SetDoseQuantityUnit(string _DoseQuantityUnit) { DoseQuantityUnit = _DoseQuantityUnit; }

        /// <summary>
        /// 횟수
        /// </summary>        
        [DataMember]
        public virtual string RepeatNumber
        {
            get { return repeatNumber; }
            set { if (repeatNumber != value) { repeatNumber = value; OnPropertyChanged("RepeatNumber"); } }
        }

        public string GetRepeatNumber() { return RepeatNumber; }
        public void SetRepeatNumber(string _RepeatNumber) { RepeatNumber = _RepeatNumber; }

        /// <summary>
        /// 투여기간
        /// </summary>
        [DataMember]
        public virtual string Period
        {
            get { return period; }
            set { if (period != value) { period = value; OnPropertyChanged("Period"); } }
        }

        public string GetPeriod() { return Period; }
        public void SetPeriod(string _Period) { Period = _Period; }

        /// <summary>
        /// 투약코드 주성분(ATC)
        /// </summary>
        [DataMember]
        public virtual string MajorComponentCode
        {
            get { return majorComponentCode; }
            set { if (majorComponentCode != value) { majorComponentCode = value; OnPropertyChanged("MajorComponentCode"); } }
        }

        public string GetMajorComponentCode() { return MajorComponentCode; }
        public void SetMajorComponentCode(string _MajorComponentCode) { MajorComponentCode = _MajorComponentCode; }

        /// <summary>
        /// 주성분명
        /// </summary>
        [DataMember]
        public virtual string MajorComponent
        {
            get { return majorComponent; }
            set { if (majorComponent != value) { majorComponent = value; OnPropertyChanged("MajorComponent"); } }
        }

        public string GetMajorComponent() { return MajorComponent; }
        public void SetMajorComponent(string _MajorComponent) { MajorComponent = _MajorComponent; }

        /// <summary>
        /// 용법
        /// </summary>
        [DataMember]
        public virtual string Usage
        {
            get { return usage; }
            set { if (usage != value) { usage = value; OnPropertyChanged("Usage"); } }
        }

        public string GetUsage() { return Usage; }
        public void SetUsage(string _Usage) { Usage = _Usage; }

        /// <summary>
        /// 시작일
        /// </summary>
        [DataMember]
        public virtual string BeginningDate
        {
            get { return beginningDate; }
            set { beginningDate = value; OnPropertyChanged("BeginningDate"); }
        }
        public string GetBeginningDate() { return BeginningDate; }
        public void SetBeginningDate(string _BeginningDate) { BeginningDate = _BeginningDate; }

        #endregion

        #region :: Constructor
        public MedicationObject()
        {
            ClassCode = "SBADM";
            MoodCode = "EVN";

            EffectiveTimeType = "SXCM_TS[]";
            CodeType = string.Empty;
            ClassCodeType = "System.String";
            MoodCodeType = "x_DocumentSubstanceMood";
            StatusCodeType = "CS";

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;

            ConsumableCodeSystemName = "ATC";
            ConsumableCodeSystem = "2.16.840.1.113883.6.73";
        }
        #endregion
    }
}