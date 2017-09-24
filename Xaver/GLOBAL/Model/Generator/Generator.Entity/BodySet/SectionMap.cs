using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Xaver.Framework;

namespace Generator.Entity.BodySet
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class SectionMap : BaseVo<Int64>, INotifyPropertyChanged
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

        private int documentId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int DocumentId
        {
            get { return documentId; }
            set { documentId = value; OnPropertyChanged("DocumentId"); }
        }

        private int sectionId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int SectionId
        {
            get { return sectionId; }
            set { sectionId = value; OnPropertyChanged("SectionId"); }
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

    }
}
