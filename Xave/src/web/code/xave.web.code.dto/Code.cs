using FluentNHibernate.Mapping;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace xave.web.code.dto
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class Code : INotifyPropertyChanged// HIEDTOBaseObject
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

        private int codeId;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int CodeID
        {
            get { return codeId; }
            set { codeId = value; OnPropertyChanged("CodeID"); }
        }

        private string codeCD;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeCD
        {
            get { return codeCD; }
            set { codeCD = value; OnPropertyChanged("CodeCD"); }
        }

        private string codeName;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeName
        {
            get { return codeName; }
            set { codeName = value; OnPropertyChanged("CodeName"); }
        }

        private int parent;
        [System.ComponentModel.DefaultValue(-1)]
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual int Parent
        {
            get { return parent; }
            set { parent = value; OnPropertyChanged("Parent"); }
        }


        private string codeType;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeType
        {
            get { return codeType; }
            set { codeType = value; OnPropertyChanged("CodeType"); }
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

        private string codeClassification;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CodeClassification
        {
            get { return codeClassification; }
            set { codeClassification = value; OnPropertyChanged("CodeClassification"); }
        }

        private string bindableVariable;
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string BindableVariable
        {
            get { return bindableVariable; }
            set { bindableVariable = value; OnPropertyChanged("BindableVariable"); }
        }
    }

    public class CodeMap : ClassMap<Code>
    {
        public CodeMap()
        {
            Id(u => u.CodeID).Column("C_CODEID");
            Map(u => u.CodeCD).Column("C_CODE").Nullable();
            Map(u => u.CodeName).Column("C_CODENAME").Nullable();
            Map(u => u.Parent).Column("C_PARENT").Nullable();
            Map(u => u.CodeType).Column("C_CODETYPE").Nullable();
            Map(u => u.UseYN).Column("C_USEYN").Nullable();
            Map(u => u.Regdate).Column("C_REGDATE").Nullable();
            Map(u => u.CodeClassification).Column("C_CODECLASSIFICATION").Nullable();
            Map(u => u.BindableVariable).Column("C_BINDABLE").Nullable();
            Table("TB_CODE");
        }
    }
}
