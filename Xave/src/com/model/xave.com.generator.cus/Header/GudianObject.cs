using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus
{
    [DataContract]
    [Serializable]
    [XmlSerializerFormat]
    public class GuardianObject : ModelBase
    {
        #region :: Private Member
        private string guardianName;
        private string telecomNumber;
        private string additionalLocator;
        private string streetAddress;
        private string postalCode;
        private GuardianType gType;

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
        #endregion

        #region :: Public Property
        /// <summary>
        /// 보호자 성명
        /// </summary>
        [DataMember]
        public virtual string GuardianName
        {
            get { return guardianName; }
            set { guardianName = value; OnPropertyChanged("GuardianName"); }
        }

        public string GetName() { return GuardianName; }
        public void SetName(string _Name) { GuardianName = _Name; }

        /// <summary>
        /// 보호자 연락처
        /// </summary>
        [DataMember]
        public virtual string TelecomNumber
        {
            get { return telecomNumber; }
            set { telecomNumber = value; OnPropertyChanged("TelecomNumber"); }
        }

        public string GetTelecomNumber() { return TelecomNumber; }
        public void SetTelecomNumber(string _TelecomNumber) { TelecomNumber = _TelecomNumber; }

        /// <summary>
        /// 보호자 주소(기본주소)
        /// </summary>
        [DataMember]
        public virtual string AdditionalLocator
        {
            get { return additionalLocator; }
            set { additionalLocator = value; OnPropertyChanged("AdditionalLocator"); }
        }

        public string GetAdditionalLocator() { return AdditionalLocator; }
        public void SetAdditionalLocator(string _AdditionalLocator) { AdditionalLocator = _AdditionalLocator; }

        /// <summary>
        /// 보호자 주소(상세주소)
        /// </summary>
        [DataMember]
        public virtual string StreetAddress
        {
            get { return streetAddress; }
            set { streetAddress = value; OnPropertyChanged("StreetAddress"); }
        }

        public string GetStreetAddress() { return StreetAddress; }
        public void SetStreetAddress(string _StreetAddress) { StreetAddress = _StreetAddress; }

        /// <summary>
        /// 보호자 주소(우편번호)
        /// </summary>
        [DataMember]
        public virtual string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; OnPropertyChanged("PostalCode"); }
        }

        public string GetPostalCode() { return PostalCode; }
        public void SetPostalCode(string _PostalCode) { PostalCode = _PostalCode; }

        /// <summary>
        /// 보호자 관계
        /// </summary>
        [DataMember]
        public virtual GuardianType GType
        {
            get { return gType; }
            set { gType = value; OnPropertyChanged("GType"); }
        }
        public GuardianType GetGType() { return GType; }
        public void SetType(GuardianType _Type) { GType = _Type; }

        [DataMember]
        public virtual string GTypeString
        {
            get { return GType.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && GType.ToString() != value)
                {
                    try
                    {
                        GType = TryParse<GuardianType>(value);
                        OnPropertyChanged("GTypeString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetGTypeString() { return GTypeString; }
        public void SetGTypeString(string _GTypeString) { GTypeString = _GTypeString; }

        #endregion
    }
}
