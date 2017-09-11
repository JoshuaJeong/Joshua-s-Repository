using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 처방기관 정보
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class CustodianObject : ModelBase
    {
        #region :: Private Member
        private string id;
        private string oID;
        private string custodianName;
        private string telecomNumber;
        private string additionalLocator;
        private string streetAddress;
        private string postalCode;
        #endregion

        #region :: Key
        private int custodianId;
        public virtual int CustodianId
        {
            get { return custodianId; }
            set { custodianId = value; OnPropertyChanged("CustodianId"); }
        }

        public int GetCustodianId() { return CustodianId; }
        public void SetCustodianId(int _CustodianId) { CustodianId = _CustodianId; }

        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }
        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #endregion

        #region :: Public Property
        /// <summary>
        /// 요양기관 기호
        /// </summary>
        [DataMember]
        public virtual string Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged("Id"); }
        }

        public string GetId() { return Id; }
        public void SetId(string _Id) { Id = _Id; }

        /// <summary>
        /// 기관 OID
        /// </summary>
        [DataMember]
        public virtual string OID
        {
            get { return this.oID; }
            set { this.oID = value; OnPropertyChanged("OID"); }
        }

        public string GetOID() { return OID; }
        public void SetOID(string _OID) { OID = _OID; }

        /// <summary>
        /// 기관명
        /// </summary>
        [DataMember]
        public virtual string CustodianName
        {
            get { return custodianName; }
            set { this.custodianName = value; OnPropertyChanged("CustodianName"); }
        }

        public string GetCustodianName() { return CustodianName; }
        public void SetCustodianName(string _CustodianName) { CustodianName = _CustodianName; }

        /// <summary>
        /// 기관 연락처
        /// </summary>
        [DataMember]
        public virtual string TelecomNumber
        {
            get { return telecomNumber; }
            set { this.telecomNumber = value; OnPropertyChanged("TelecomNumber"); }
        }

        public string GetTelecomNumber() { return TelecomNumber; }
        public void SetTelecomNumber(string _TelecomNumber) { TelecomNumber = _TelecomNumber; }

        /// <summary>
        /// 주소 ( 기본 주소 )
        /// </summary>
        [DataMember]
        public virtual string AdditionalLocator
        {
            get { return additionalLocator; }
            set { this.additionalLocator = value; OnPropertyChanged("AdditionalLocator"); }
        }

        public string GetAdditionalLocator() { return AdditionalLocator; }
        public void SetAdditionalLocator(string _AdditionalLocator) { AdditionalLocator = _AdditionalLocator; }

        /// <summary>
        /// 주소 ( 상세 주소 )
        /// </summary>
        [DataMember]
        public virtual string StreetAddress
        {
            get { return streetAddress; }
            set { this.streetAddress = value; OnPropertyChanged("StreetAddress"); }
        }

        public string GetStreetAddress() { return StreetAddress; }
        public void SetStreetAddress(string _StreetAddress) { StreetAddress = _StreetAddress; }

        /// <summary>
        /// 주소 ( 우편번호 )
        /// </summary>
        [DataMember]
        public virtual string PostalCode
        {
            get { return postalCode; }
            set { this.postalCode = value; OnPropertyChanged("PostalCode"); }
        }
        public string GetPostalCode() { return PostalCode; }
        public void SetPostalCode(string _PostalCode) { PostalCode = _PostalCode; }

        #endregion
    }
}