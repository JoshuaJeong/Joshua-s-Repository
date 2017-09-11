using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 법적 보증자
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class LegalAuthenticatorObject
    {
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int GetId() { return Id; }
        public void SetId(int _Id) { Id = _Id; }

        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; }
        }

        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        /// <summary>
        /// 서명시간
        /// </summary>
        [DataMember]
        public virtual string Time { get; set; }
        public string GetTime() { return Time; }
        public void SetTime(string _Time) { Time = _Time; }

        /// <summary>
        /// 보증자 성명
        /// </summary>
        [DataMember]
        public virtual string PersonName { get; set; }
        public string GetPersonName() { return PersonName; }
        public void SetPersonName(string _PersonName) { PersonName = _PersonName; }

        /// <summary>
        /// 연락처
        /// </summary>
        [DataMember]
        public virtual string Telecom { get; set; }
        public string GetTelecom() { return Telecom; }
        public void SetTelecom(string _Telecom) { Telecom = _Telecom; }

        /// <summary>
        /// 기본주소
        /// </summary>
        [DataMember]
        public virtual string AdditionalLocator { get; set; }
        public string GetAdditionalLocator() { return AdditionalLocator; }
        public void SetAdditionalLocator(string _AdditionalLocator) { AdditionalLocator = _AdditionalLocator; }

        /// <summary>
        /// 상세주소
        /// </summary>
        [DataMember]
        public virtual string StreetAddressLine { get; set; }
        public string GetStreetAddressLine() { return StreetAddressLine; }
        public void SetStreetAddressLine(string _StreetAddressLine) { StreetAddressLine = _StreetAddressLine; }

        /// <summary>
        /// 우편번호
        /// </summary>
        [DataMember]
        public virtual string PostalCode { get; set; }
        public string GetPostalCode() { return PostalCode; }
        public void SetPostalCode(string _PostalCode) { PostalCode = _PostalCode; }

    }
}
