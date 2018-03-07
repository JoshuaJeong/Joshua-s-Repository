using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace xave.web.code.dto
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class CodeContainers : INotifyPropertyChanged// : HIEDTOBaseObject
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

        private List<Code> codeType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<Code> CodeType
        {
            get { return codeType; }
            set { codeType = value; OnPropertyChanged("CodeType"); }
        }

        private List<Format> formatType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<Format> FormatType
        {
            get { return formatType; }
            set { formatType = value; OnPropertyChanged("FormatType"); }
        }

        private List<KOSTOM_Diagnosis> kostom;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual List<KOSTOM_Diagnosis> Kostom
        {
            get { return kostom; }
            set { kostom = value; OnPropertyChanged("Kostom"); }
        }
    }
}
