using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Generator.Entity.BodySet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class BodyStructure : INotifyPropertyChanged
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

        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int sectionPartId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int SectionPartId
        {
            get { return sectionPartId; }
            set { sectionPartId = value; OnPropertyChanged("SectionPartId"); }
        }

        private string path;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Path
        {
            get { return path; }
            set { path = value; OnPropertyChanged("Path"); }
        }

        private int parent;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Parent
        {
            get { return parent; }
            set { parent = value; OnPropertyChanged("Parent"); }
        }

        private string _value;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        private string code;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged("Code"); }
        }

        private string valueType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ValueType
        {
            get { return valueType; }
            set { valueType = value; OnPropertyChanged("ValueType"); }
        }

        private string property;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Property
        {
            get { return property; }
            set { property = value; OnPropertyChanged("Property"); }
        }

        private string bindType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string BindType
        {
            get { return bindType; }
            set { bindType = value; OnPropertyChanged("BindType"); }
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

        private string[] pathSplited;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string[] PathSplited
        {
            get { return pathSplited; }
            set { pathSplited = value; OnPropertyChanged("PathSplited"); }
        }

        //private HashSet<string> hashDict;
        //[DataMember, System.Xml.Serialization.XmlElementAttribute]
        //public virtual HashSet<string> HashDict
        //{
        //    get { return hashDict; }
        //    set { hashDict = value; OnPropertyChanged("HashDict"); }
        //}

        private int group;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Group
        {
            get { return group; }
            set { group = value; OnPropertyChanged("Group"); }
        }
    }
}
