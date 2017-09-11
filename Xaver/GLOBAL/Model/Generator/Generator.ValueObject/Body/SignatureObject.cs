using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Generator.ValueObject
{
    public class SignatureObject : ModelBase
    {
        #region :: Private Member
        private string imageData;
        private string mediaType;
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

        public virtual string Value { get { return imageData; } set { imageData = value; OnPropertyChanged("Value"); } }

        public string GetValue() { return Value; }
        public void SetValue(string _Value) { Value = _Value; }


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



        public virtual string ValueClassType { get { return valueClassType; } set { valueClassType = value; OnPropertyChanged("ValueClassType"); } }
        public string GetValueClassType() { return ValueClassType; }
        public void SetValueClassType(string _ValueClassType) { ValueClassType = _ValueClassType; }

        #endregion

        #region :: Public Property
        /// <summary>
        /// 서명 이미지
        /// </summary>
        [DataMember]
        public virtual string ImageData
        {
            get { return imageData; }
            set { if (imageData != value) { imageData = value; OnPropertyChanged("ImageData"); } }
        }

        public string GetImageData() { return ImageData; }
        public void SetImageData(string _ImageData) { ImageData = _ImageData; }

        /// <summary>
        /// MediaType
        /// </summary>
        [DataMember]
        public virtual string MediaType
        {
            get { return mediaType; }
            set { if (mediaType != value) { mediaType = value; OnPropertyChanged("MediaType"); } }
        }
        public string GetMediaType() { return MediaType; }
        public void SetMediaType(string _MediaType) { MediaType = _MediaType; }

        #endregion

        #region :: Constructor
        public SignatureObject()
        {
            ClassCode = "OBS";
            MoodCode = "EVN";

            EffectiveTimeType = string.Empty;
            CodeType = string.Empty;
            ClassCodeType = "System.String";
            MoodCodeType = "System.String";
            StatusCodeType = string.Empty;

            TypeCode = string.Empty;
            TypeCodeType = string.Empty;

            this.mediaType = "img/png";
            this.valueClassType = "ED";
            this.codeType = "CD";
        }
        #endregion
    }
}
