using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Generator.Entity.HeaderSet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class HeaderPart : INotifyPropertyChanged
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

        #region HeaderValue 의 Properties
        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
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

        #endregion


        private List<HeaderStructure> structureSet = new List<HeaderStructure>();
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<HeaderStructure> StructureSet
        {
            get { return structureSet; }
            set { structureSet = value; OnPropertyChanged("StructureSet"); }
        }

    }
}
