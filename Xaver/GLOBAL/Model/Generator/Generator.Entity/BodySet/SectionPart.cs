using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Xaver.Framework;

namespace Generator.Entity.BodySet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class SectionPart : BaseVo<Int64>, INotifyPropertyChanged
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

        #region 기본 SectionPart Properties
        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int sectionId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int SectionId
        {
            get { return sectionId; }
            set { sectionId = value; OnPropertyChanged("SectionId"); }
        }

        private string _value;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }




        private string templateIdRoot;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string TemplateIdRoot
        {
            get { return templateIdRoot; }
            set { templateIdRoot = value; OnPropertyChanged("TemplateIdRoot"); }
        }

        private string templateIdExtension;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string TemplateIdExtension
        {
            get { return templateIdExtension; }
            set { templateIdExtension = value; OnPropertyChanged("TemplateIdExtension"); }
        }

        private string code;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged("Code"); }
        }

        private string codeType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeType
        {
            get { return codeType; }
            set { codeType = value; OnPropertyChanged("CodeType"); }
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

        private string statusCode;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; OnPropertyChanged("StatusCode"); }
        }







        private string valueType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ValueType
        {
            get { return valueType; }
            set { valueType = value; OnPropertyChanged("ValueType"); }
        }

        private int sequence;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Sequence
        {
            get { return sequence; }
            set { sequence = value; OnPropertyChanged("Sequence"); }
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

        private int parent;
        [System.ComponentModel.DefaultValue(-1)]
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Parent
        {
            get { return parent; }
            set { parent = value; OnPropertyChanged("Parent"); }
        }

        private string sectionType;
        /// <summary>
        /// SectionType : Entry or Narrative 구분자
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string SectionType
        {
            get { return sectionType; }
            set { sectionType = value; OnPropertyChanged("SectionType"); }
        }

        private string detail;
        /// <summary>
        /// 상세구분자 : Narrative (table/list), ENTRY (act/observation) 등
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Detail
        {
            get { return detail; }
            set { detail = value; OnPropertyChanged("Detail"); }
        }

        private string bindType;
        /// <summary>
        /// BIND할 타입
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string BindType
        {
            get { return bindType; }
            set { bindType = value; OnPropertyChanged("BindType"); }
        }

        #endregion


        private List<SectionPart> children;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<SectionPart> Children
        {
            get { return children; }
            set { children = value; OnPropertyChanged("Children"); }
        }

        private List<BodyStructure> bodyStructureList;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<BodyStructure> BodyStructureList
        {
            get { return bodyStructureList; }
            set { bodyStructureList = value; OnPropertyChanged("BodyStructureList"); }
        }
    }
}
