using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace xave.web.structureset.dto
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class Document : INotifyPropertyChanged
    {
        #region PropertyChanged
        public virtual event PropertyChangedEventHandler PropertyChanged;
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

    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap()
        {
            Id(u => u.Id).Column("C_DOCUMENTID");
            Map(u => u.RealmCode).Column("C_REALMCODE").Nullable();
            Map(u => u.TemplateId).Column("C_TEMPLATE_ID").Nullable();
            Map(u => u.Code).Column("C_CODE").Nullable();
            Map(u => u.CodeSystem).Column("C_CODESYSTEM").Nullable();
            Map(u => u.CodeSystemName).Column("C_CODESYSTEMNAME").Nullable();
            Map(u => u.DisplayName).Column("C_DISPLAYNAME").Nullable();
            Map(u => u.Title).Column("C_TITLE").Nullable();
            Map(u => u.LanguageCode).Column("C_LANGUAGECODE").Nullable();
            Map(u => u.UseYN).Column("C_USEYN").Nullable();
            Map(u => u.Regdate).Column("C_REGDATE").Nullable();
            Map(u => u.DocType).Column("C_DOCUMENTTYPE").Nullable();
            Table("TB_DOCUMENT");
        }
    }
}
