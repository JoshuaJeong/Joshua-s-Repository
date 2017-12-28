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
    public class HeaderPart : INotifyPropertyChanged
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

    public class HeaderPartMap : ClassMap<HeaderPart>
    {
        public HeaderPartMap()
        {
            Id(u => u.Id).Column("C_HEADERPARTID");
            Map(u => u.Name).Column("C_NAME").Nullable();
            Map(u => u.Regdate).Column("C_REGDATE").Nullable();
            Map(u => u.UseYN).Column("C_USEYN").Nullable();
            Map(u => u.Sequence).Column("C_SEQUENCE").Nullable();
            Table("TB_HEADER_PART");
        }
    }
}
