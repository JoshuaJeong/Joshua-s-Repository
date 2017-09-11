using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 예방접종 모델
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ImmunizationObject : ModelBase
    {
        #region :: Private Member
        private string immunizationName;
        private string immunizationCode;
        private string vaccineName;
        private string repeatNumber;

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
        private string period;
        /// <summary>
        /// 기간
        /// </summary>
        [DataMember]
        public virtual string Period
        {
            get { return period; }
            set { if (period != value) { period = value; OnPropertyChanged("Period"); } }
        }

        public string GetPeriod() { return Period; }
        public void SetPeriod(string _Period) { Period = _Period; }

        private string startDate;
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
        /// 예방접종명
        /// </summary>
        [DataMember]
        public virtual string ImmunizationName
        {
            get { return immunizationName; }
            set { if (immunizationName != value) { immunizationName = value; OnPropertyChanged("ImmunizationName"); } }
        }

        public string GetImmunizationName() { return ImmunizationName; }
        public void SetImmunizationName(string _ImmunizationName) { ImmunizationName = _ImmunizationName; }

        /// <summary>
        /// 예방접종코드(보건의료용어표준)
        /// </summary>
        [DataMember]
        public virtual string ImmunizationCode
        {
            get { return immunizationCode; }
            set { if (immunizationCode != value) { immunizationCode = value; OnPropertyChanged("ImmunizationCode"); } }
        }

        public string GetImmunizationCode() { return ImmunizationCode; }
        public void SetImmunizationCode(string _ImmunizationCode) { ImmunizationCode = _ImmunizationCode; }

        /// <summary>
        /// 백신명
        /// </summary>
        [DataMember]
        public virtual string VaccineName
        {
            get { return vaccineName; }
            set { if (vaccineName != value) { vaccineName = value; OnPropertyChanged("VaccineName"); } }
        }

        public string GetVaccineName() { return VaccineName; }
        public void SetVaccineName(string _VaccineName) { VaccineName = _VaccineName; }

        /// <summary>
        /// 접종차수
        /// </summary>
        [DataMember]
        public virtual string RepeatNumber
        {
            get { return repeatNumber; }
            set { if (repeatNumber != value) { repeatNumber = value; OnPropertyChanged("RepeatNumber"); } }
        }
        public string GetRepeatNumber() { return RepeatNumber; }
        public void SetRepeatNumber(string _RepeatNumber) { RepeatNumber = _RepeatNumber; }

        #endregion

        #region : Constructor
        public ImmunizationObject()
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

            ConsumableCodeSystemName = "EDI";
            ConsumableCodeSystem = "1.2.410.100110.40.2.3.3";
        }
        #endregion
    }
}