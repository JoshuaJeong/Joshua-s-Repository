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
    public class HeaderMap : INotifyPropertyChanged
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

        #region 기본 Header Properties
        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int documentStructureId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int DocumentId
        {
            get { return documentStructureId; }
            set { documentStructureId = value; OnPropertyChanged("DocumentId"); }
        }

        private int headerPartId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int HeaderPartId
        {
            get { return headerPartId; }
            set { headerPartId = value; OnPropertyChanged("HeaderPartId"); }
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
    }

    public class HeaderMapMap : ClassMap<HeaderMap>
    {
        public HeaderMapMap()
        {
            Id(u => u.Id).Column("C_HEADERSECTIONMAPID");
            Map(u => u.HeaderPartId).Column("C_HEADERPARTID").Nullable();
            Map(u => u.DocumentId).Column("C_DOCUMENTID").Nullable();
            Map(u => u.Sequence).Column("C_SEQUENCE").Nullable();
            Map(u => u.Regdate).Column("C_REGDATE").Nullable();
            Map(u => u.UseYN).Column("C_USEYN").Nullable();
            Table("TB_HEADER_MAP");
        }
    }
}
