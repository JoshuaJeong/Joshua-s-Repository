using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Generator.ValueObject;

namespace Generator.ValueObject
{
    /// <summary>    
    /// 서식 기본 정보 Model
    /// </summary>

    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class DocumentInformationObject : ModelBase
    {
        #region :: Private Member
        private string title;
        private string type;
        private string documentID;
        private string requestNumber;
        #endregion

        private int documentInformationId;
        public virtual int DocumentInformationId
        {
            get { return documentInformationId; }
            set { documentInformationId = value; OnPropertyChanged("DocumentInformationId"); }
        }

        public int GetDocumentInformationId() { return DocumentInformationId; }
        public void SetDocumentInformationId(int _DocumentInformationId) { DocumentInformationId = _DocumentInformationId; }

        private int cdaObjectID;
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }

        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #region :: Public Property
        /// <summary>
        /// 문서 제목
        /// </summary>
        [DataMember]
        public virtual string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        public string GetTitle() { return Title; }
        public void SetTitle(string _Title) { Title = _Title; }

        [DataMember]
        public virtual string DocumentType
        {
            get { return type; }
            set { type = value; OnPropertyChanged("Type"); }
        }

        public string GetDocumentType() { return DocumentType; }
        public void SetDocumentType(string _Type) { DocumentType = _Type; }

        /// <summary>
        /// 문서 고유 ID
        /// </summary>
        [DataMember]
        public virtual string DocumentID
        {
            get { return documentID; }
            set { documentID = value; OnPropertyChanged("DocumentID"); }
        }

        public string GetDocumentID() { return DocumentID; }
        public void SetDocumentID(string _DocumentID) { DocumentID = _DocumentID; }

        /// <summary>
        /// 의뢰/회송번호
        /// </summary>
        [DataMember]
        public virtual string RequestNumber
        {
            get { return requestNumber; }
            set { requestNumber = value; OnPropertyChanged("RequestNumber"); }
        }
        public string GetRequestNumber() { return RequestNumber; }
        public void SetRequestNumber(string _RequestNumber) { RequestNumber = _RequestNumber; }

        #endregion

        #region :: Defaults
        private Confidentiality confidentialityCode;
        private string organizationOID;

        /// <summary>
        /// 보안 수준 코드구별        
        /// </summary>
        [DataMember]
        public virtual Confidentiality ConfidentialityCode
        {
            get { return confidentialityCode; }
            set { confidentialityCode = value; OnPropertyChanged("ConfidentialityCode"); }
        }

        public Confidentiality GetConfidentialityCode() { return ConfidentialityCode; }
        public void SetConfidentialityCode(Confidentiality _ConfidentialityCode) { ConfidentialityCode = _ConfidentialityCode; }

        [DataMember]
        public virtual string ConfidentialityCodeString
        {
            get { return confidentialityCode.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && confidentialityCode.ToString() != value)
                {
                    try
                    {
                        confidentialityCode = TryParse<Confidentiality>(value);
                        OnPropertyChanged("ConfidentialityCodeString");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public string GetConfidentialityCodeString() { return ConfidentialityCodeString; }
        public void SetConfidentialityCodeString(string _ConfidentialityCodeString) { ConfidentialityCodeString = _ConfidentialityCodeString; }

        private static T TryParse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

        /// <summary>
        /// 문서 생성기관 OID
        /// </summary>
        [DataMember]
        public virtual string OrganizationOID
        {
            get { return organizationOID; }
            set { organizationOID = value; OnPropertyChanged("OrganizationOID"); }
        }

        public string GetOrganizationOID() { return OrganizationOID; }
        public void SetOrganizationOID(string _OrganizationOID) { OrganizationOID = _OrganizationOID; }

        ///// <summary>
        ///// 문서종류 코드(LOINC)
        ///// </summary>
        //[DataMember]
        //public virtual string DocumentCodeValue { get; set; }

        ///// <summary>
        ///// 문서 종류 코드 명칭
        ///// </summary>
        //[DataMember]
        //public virtual string DocumentCodeName { get; set; }

        /// <summary>
        /// 국가 코드
        /// </summary>        
        [DataMember]
        public virtual string RealmCode { get; set; }

        public string GetRealmCode() { return RealmCode; }
        public void SetRealmCode(string _RealmCode) { RealmCode = _RealmCode; }

        /// <summary>
        /// 문서 사용 언어코드
        /// </summary>        
        [DataMember]
        public virtual string LanguageCode { get; set; }

        public string GetLanguageCode() { return LanguageCode; }
        public void SetLanguageCode(string _LanguageCode) { LanguageCode = _LanguageCode; }

        [DataMember]
        public virtual IdObject[] Id { get; set; }
        public IdObject[] GetId() { return Id; }
        public void SetId(IdObject[] _Id) { Id = _Id; }

        [DataMember]
        public virtual List<IdObject> IdList
        {
            get { return Id != null ? Id.ToList() : null; }
            set { Id = value != null ? value.ToArray() : null; OnPropertyChanged("Id"); }
        }

        public List<IdObject> GetIdList() { return IdList; }
        public void SetIdList(List<IdObject> _IdList) { IdList = _IdList; }

        #endregion

        #region :: Constructor
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public DocumentInformationObject()
        {
            this.LanguageCode = "ko";
            this.RealmCode = "KR";
            this.ConfidentialityCode = Confidentiality.Normal;
            this.Id = new IdObject[] { };
            //this.Id.Add(new IdObject() { root = OID.US_REALM_HEADER_TEMPLATE, extension = "2015-08-01" });
        }


        #endregion

        [DataContract]
        [System.SerializableAttribute()]
        [System.ServiceModel.XmlSerializerFormat]
        public class IdObject
        {
            [DataMember]
            public virtual string root { get; set; }
            public string Getroot() { return root; }
            public void Setroot(string _root) { root = _root; }

            [DataMember]
            public virtual string extension { get; set; }
            public string Getextension() { return extension; }
            public void Setextension(string _extension) { extension = _extension; }

        }
    }
}
