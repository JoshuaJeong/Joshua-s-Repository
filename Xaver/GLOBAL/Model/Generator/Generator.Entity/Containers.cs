using Generator.Entity.BodySet;
using Generator.Entity.HeaderSet;
using Generator.Entity.StructureSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Xaver.Framework;

namespace Generator.Entity
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class Containers : BaseVo<Int64>, INotifyPropertyChanged
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

        #region StructureSet Container
        private int id;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private List<Document> documentStructureType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<Document> DocumentType
        {
            get { return documentStructureType; }
            set { documentStructureType = value; OnPropertyChanged("DocumentType"); }
        }


        #region Header
        private List<HeaderMap> headerMapType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<HeaderMap> HeaderMapType
        {
            get { return headerMapType; }
            set { headerMapType = value; OnPropertyChanged("HeaderMapType"); }
        }

        private List<HeaderStructure> headerStructureType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<HeaderStructure> HeaderStructureType
        {
            get { return headerStructureType; }
            set { headerStructureType = value; OnPropertyChanged("HeaderStructureType"); }
        }

        private List<HeaderPart> headerPartType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<HeaderPart> HeaderPartType
        {
            get { return headerPartType; }
            set { headerPartType = value; OnPropertyChanged("HeaderPartType"); }
        }
        #endregion


        #region Body
        private List<SectionMap> sectionMapType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<SectionMap> SectionMapType
        {
            get { return sectionMapType; }
            set { sectionMapType = value; OnPropertyChanged("SectionMapType"); }
        }

        private List<BodyStructure> bodyStructureType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<BodyStructure> BodyStructureType
        {
            get { return bodyStructureType; }
            set { bodyStructureType = value; OnPropertyChanged("BodyStructureType"); }
        }

        private List<Section> sectionType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<Section> SectionType
        {
            get { return sectionType; }
            set { sectionType = value; OnPropertyChanged("SectionType"); }
        }

        private List<SectionPart> sectionPartType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<SectionPart> SectionPartType
        {
            get { return sectionPartType; }
            set { sectionPartType = value; OnPropertyChanged("SectionPartType"); }
        }

        #endregion

        #endregion
    }
}
