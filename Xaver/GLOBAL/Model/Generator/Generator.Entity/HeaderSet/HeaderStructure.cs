using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Generator.Entity.HeaderSet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class HeaderStructure : INotifyPropertyChanged
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

        private int headerPartId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int HeaderPartId
        {
            get { return headerPartId; }
            set { headerPartId = value; OnPropertyChanged("HeaderPartId"); }
        }

        private string path;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Path
        {
            get { return path; }
            set { path = value; OnPropertyChanged("Path"); }
        }

        private string _value;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        private int parent;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Parent
        {
            get { return parent; }
            set { parent = value; OnPropertyChanged("Parent"); }
        }

        private string valueType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ValueType
        {
            get { return valueType; }
            set { valueType = value; OnPropertyChanged("ValueType"); }
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
    }
}
