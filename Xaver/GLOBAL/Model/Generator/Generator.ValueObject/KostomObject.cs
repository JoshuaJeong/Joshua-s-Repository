using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Generator.ValueObject
{
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class KostomObject : ModelBase
    {
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int objectID;
        [DataMember]
        public virtual int ObjectID
        {
            get { return objectID; }
            set { objectID = value; OnPropertyChanged("ObjectID"); }
        }

        private string codeSystem = string.Empty;
        private string codeSystemName = string.Empty;

        public virtual string CodeSystem { get { return codeSystem; } set { codeSystem = value; OnPropertyChanged("CodeSystem"); } }
        public virtual string CodeSystemName { get { return codeSystemName; } set { codeSystemName = value; OnPropertyChanged("CodeSystemName"); } }

        private string _code;
        [DataMember]
        public virtual string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged("Code"); }
        }

        private string _displayName;
        [DataMember]
        public virtual string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; OnPropertyChanged("DisplayName"); }
        }



        public int GetId() { return Id; }
        public void SetId(int _Id) { Id = _Id; }

        public int GetObjectID() { return ObjectID; }
        public void SetObjectID(int _ObjectID) { ObjectID = _ObjectID; }

        public string GetCodeSystem() { return CodeSystem; }
        public void SetCodeSystem(string _CodeSystem) { CodeSystem = _CodeSystem; }

        public string GetCodeSystemName() { return CodeSystemName; }
        public void SetCodeSystemName(string _CodeSystemName) { CodeSystemName = _CodeSystemName; }

        public string GetCode() { return Code; }
        public void SetCode(string _Code) { Code = _Code; }

        public string GetDisplayName() { return DisplayName; }
        public void SetDisplayName(string _DisplayName) { DisplayName = _DisplayName; }




        public KostomObject()
        {
            this.CodeSystem = "1.2.410.100110.40.2.3.1";
            this.CodeSystemName = "보건의료용어표준";
        }
    }
}
