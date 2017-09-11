using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Generator.ValueObject
{
    /// <summary>
    /// 문서작성자
    /// </summary>
    [DataContract]
    [Serializable]
    [XmlSerializerFormat]
    public class AuthenticatorObject : ModelBase
    {
        #region :: Key
        private int authenticatorId;
        public virtual int AuthenticatorId
        {
            get { return authenticatorId; }
            set { authenticatorId = value; OnPropertyChanged("AuthenticatorId"); }
        }

        public int GetAuthenticatorId() { return AuthenticatorId; }
        public void SetAuthenticatorId(int _AuthenticatorId) { AuthenticatorId = _AuthenticatorId; }

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
        private string id;
        private string authenticatorName;
        private string telecomNumber;
        #endregion

        #region :: Public Property
        /// <summary>
        /// 문서작성자 ID
        /// </summary>
        [DataMember]
        public virtual string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string GetId() { return Id; }
        public void SetId(string _Id) { Id = _Id; }

        /// <summary>
        /// 문서작성자 성명
        /// </summary>
        [DataMember]
        public virtual string AuthenticatorName
        {
            get { return authenticatorName; }
            set { authenticatorName = value; OnPropertyChanged("AuthenticatorName"); }
        }

        public string GetAuthenticatorName() { return AuthenticatorName; }
        public void SetAuthenticatorName(string _AuthenticatorName) { AuthenticatorName = _AuthenticatorName; }

        /// <summary>
        /// 문서작성자 연락처
        /// </summary>
        [DataMember]
        public virtual string TelecomNumber
        {
            get { return telecomNumber; }
            set { telecomNumber = value; OnPropertyChanged("TelecomNumber"); }
        }
        public string GetTelecomNumber() { return TelecomNumber; }
        public void SetTelecomNumber(string _TelecomNumber) { TelecomNumber = _TelecomNumber; }

        #endregion
    }
}

