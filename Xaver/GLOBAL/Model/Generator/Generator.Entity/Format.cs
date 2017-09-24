using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Xaver.Framework;

namespace Generator.Entity
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class Format : BaseVo<Int64>, INotifyPropertyChanged
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

        private int formatID;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int FormatID
        {
            get { return formatID; }
            set { formatID = value; OnPropertyChanged("FormatID"); }
        }

        private string formatCode;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string FormatCode
        {
            get { return formatCode; }
            set { formatCode = value; OnPropertyChanged("FormatCode"); }
        }

        private string formatStyle;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string FormatStyle
        {
            get { return formatStyle; }
            set { formatStyle = value; OnPropertyChanged("FormatStyle"); }
        }

        private string formatType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string FormatType
        {
            get { return formatType; }
            set { formatType = value; OnPropertyChanged("FormatType"); }
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
