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
    public class Section : BaseVo<Int64>, INotifyPropertyChanged
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

        private int sequence;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Sequence
        {
            get { return sequence; }
            set { sequence = value; OnPropertyChanged("Sequence"); }
        }

        private string bindableVariable;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string BindableVariable
        {
            get { return bindableVariable; }
            set { bindableVariable = value; OnPropertyChanged("BindableVariable"); }
        }
        #endregion

        List<SectionPart> entryList = new List<SectionPart>();
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<SectionPart> EntryList
        {
            get { return entryList; }
            set { entryList = value; OnPropertyChanged("EntryList"); }
        }

        List<SectionPart> narrativeList = new List<SectionPart>();
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<SectionPart> NarrativeList
        {
            get { return narrativeList; }
            set { narrativeList = value; OnPropertyChanged("NarrativeList"); }
        }
    }
}
