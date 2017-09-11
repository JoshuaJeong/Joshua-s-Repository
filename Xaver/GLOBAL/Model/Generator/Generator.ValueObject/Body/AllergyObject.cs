using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 알러지 정보 Model    
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class AllergyObject : ModelBase
    {
        #region :: Private Member
        private string startDate;
        private string endDate;
        private string allergyType;
        private string allergyTypeCode;
        private string allergy;
        private string reaction;

        private string medicationName;
        private string medicationCode;
        private string adverseReaction;
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

        public virtual string Value { get { return AllergyTypeCode; } set { AllergyTypeCode = value; OnPropertyChanged("Value"); } }
        public string GetValue() { return Value; }
        public void SetValue(string _Value) { Value = _Value; }

        public virtual string Name { get { return AllergyType; } set { AllergyType = value; OnPropertyChanged("Name"); } }
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
        /// 알러지 등록일자
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
        /// 알러지요인 코드(보건의료용어표준)
        /// </summary>
        [DataMember]
        public virtual string AllergyTypeCode
        {
            get { return allergyTypeCode; }
            set { if (allergyTypeCode != value) { allergyTypeCode = value; OnPropertyChanged("AllergyTypeCode"); } }
        }

        public string GetAllergyTypeCode() { return AllergyTypeCode; }
        public void SetAllergyTypeCode(string _AllergyTypeCode) { AllergyTypeCode = _AllergyTypeCode; }

        /// <summary>
        /// 알러지명
        /// </summary>
        [DataMember]
        public virtual string Allergy
        {
            get { return allergy; }
            set { if (allergy != value) { allergy = value; OnPropertyChanged("Allergy"); } }
        }

        public string GetAllergy() { return Allergy; }
        public void SetAllergy(string _Allergy) { Allergy = _Allergy; }

        /// <summary>
        /// 반응
        /// </summary>
        [DataMember]
        public virtual string Reaction
        {
            get { return reaction; }
            set { if (reaction != value) { reaction = value; OnPropertyChanged("Reaction"); } }
        }

        public string GetReaction() { return Reaction; }
        public void SetReaction(string _Reaction) { Reaction = _Reaction; }

        /// <summary>
        /// 알러지 요인
        /// </summary>
        [DataMember]
        public virtual string AllergyType
        {
            get { return allergyType; }
            set { if (allergyType != value) { allergyType = value; OnPropertyChanged("AllergyType"); } }
        }

        public string GetAllergyType() { return AllergyType; }
        public void SetAllergyType(string _AllergyType) { AllergyType = _AllergyType; }

        /// <summary>
        /// 약품명
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
        /// 약품코드
        /// </summary>
        [DataMember]
        public virtual string MedicationCode
        {
            get { return medicationCode; }
            set { if (medicationCode != value) { medicationCode = value; OnPropertyChanged("MedicatioCode"); } }
        }

        public string GetMedicationCode() { return MedicationCode; }
        public void SetMedicationCode(string _MedicationCode) { MedicationCode = _MedicationCode; }

        /// <summary>
        /// 부작용
        /// </summary>
        [DataMember]
        public virtual string AdverseReaction
        {
            get { return adverseReaction; }
            set { if (adverseReaction != value) { adverseReaction = value; OnPropertyChanged("AdverseReaction"); } }
        }
        public string GetAdverseReaction() { return AdverseReaction; }
        public void SetAdverseReaction(string _AdverseReaction) { AdverseReaction = _AdverseReaction; }

        #endregion

        #region :: Constructor
        public AllergyObject()
        {
            ClassCode = "ACT";
            MoodCode = "EVN";
            EffectiveTimeType = "IVL_TS";
            CodeType = "CD";
            ClassCodeType = "x_ActClassDocumentEntryAct";
            MoodCodeType = "x_DocumentActMood";
            StatusCodeType = "CS";
            TypeCode = string.Empty;
            TypeCodeType = string.Empty;
            valueClassType = "CD";
        }
        #endregion
    }
}