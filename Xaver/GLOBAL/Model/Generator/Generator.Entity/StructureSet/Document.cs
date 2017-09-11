using Generator.Entity.BodySet;
using Generator.Entity.HeaderSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Generator.Entity.StructureSet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class Document : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        #region 문서 당 CDA 의 Structure
        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int docType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int DocType
        {
            get { return docType; }
            set { docType = value; OnPropertyChanged("DocType"); }
        }



        private string docTypeCode;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string DocTypeCode
        {
            get { return docTypeCode; }
            set { docTypeCode = value; OnPropertyChanged("DocTypeCode"); }
        }

        private string docTypeName;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string DocTypeName
        {
            get { return docTypeName; }
            set { docTypeName = value; OnPropertyChanged("DocTypeName"); }
        }



        private string realmCode;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string RealmCode
        {
            get { return realmCode; }
            set { realmCode = value; OnPropertyChanged("RealmCode"); }
        }

        private string templateId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string TemplateId
        {
            get { return templateId; }
            set { templateId = value; OnPropertyChanged("TemplateId"); }
        }

        private string code;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged("Code"); }
        }

        private string codeSystem;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeSystem
        {
            get { return codeSystem; }
            set { codeSystem = value; OnPropertyChanged("CodeSystem"); }
        }

        private string codeSystemName;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeSystemName
        {
            get { return codeSystemName; }
            set { codeSystemName = value; OnPropertyChanged("CodeSystemName"); }
        }

        private string displayName;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string DisplayName
        {
            get { return displayName; }
            set { displayName = value; OnPropertyChanged("DisplayName"); }
        }

        private string title;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        private string languageCode;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string LanguageCode
        {
            get { return languageCode; }
            set { languageCode = value; OnPropertyChanged("LanguageCode"); }
        }



        private string useYN;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string UseYN
        {
            get { return useYN; }
            set { useYN = value; OnPropertyChanged("UseYN"); }
        }

        private DateTime regdate;
        //[DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual DateTime Regdate
        {
            get { return regdate; }
            set { regdate = value; OnPropertyChanged("Regdate"); }
        }
        #endregion





        #region Header
        private List<HeaderPart> headerPartListType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<HeaderPart> HeaderPartListType
        {
            get { return headerPartListType; }
            set { headerPartListType = value; OnPropertyChanged("HeaderPartListType"); }
        }
        #endregion

        #region Body
        private List<Section> sectionListType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<Section> SectionListType
        {
            get { return sectionListType; }
            set { sectionListType = value; OnPropertyChanged("SectionListType"); }
        }
        #endregion
    }
}
