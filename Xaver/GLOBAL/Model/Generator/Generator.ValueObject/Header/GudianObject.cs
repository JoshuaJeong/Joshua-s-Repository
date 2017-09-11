using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Generator.ValueObject
{
    [DataContract]
    [Serializable]
    [XmlSerializerFormat]
    public class GudianObject : ModelBase
    {
        #region :: Key
        private int guardianId;
        public virtual int GuardianId
        {
            get { return guardianId; }
            set { guardianId = value; OnPropertyChanged("GuardianId"); }
        }

        public int GetGuardianId() { return GuardianId; }
        public void SetGuardianId(int _GuardianId) { GuardianId = _GuardianId; }

        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }
        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #endregion

        #region :: Private Member
        private string name;
        private string telecomNumber;
        private string additionalLocator;
        private string streetAddress;
        private string postalCode;
        private GuardianType gType;
        #endregion

        #region :: Public Property
        /// <summary>
        /// 보호자 성명
        /// </summary>
        [DataMember]
        public virtual string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string GetName() { return Name; }
        public void SetName(string _Name) { Name = _Name; }

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
            set { gType = value; OnPropertyChanged("PostalCode"); }
        }
        public GuardianType GetGType() { return GType; }
        public void SetType(GuardianType _Type) { GType = _Type; }

        #endregion
    }
}
