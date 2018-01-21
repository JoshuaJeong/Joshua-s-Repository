using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 검체검사결과
    /// </summary>
    [DataContract]
    [System.Serializable]    
    [System.ServiceModel.XmlSerializerFormat]
    internal class SpecimenLabObject : ModelBase
    {
        #region :: Private Member
        private string entryName;
        private string entryName_KOSTOM;
        private string entryCode;
        private string entryCode_KOSTOM;
        private string testName;
        private string resultValue;
        private string reference;
        #endregion  

        #region :: Public Property
        private string date;
        /// <summary>
        /// 시행일자
        /// </summary>
        [DataMember]
        public virtual string Date
        {
            get { return date; }
            set { if (date != value) { date = value; OnPropertyChanged("Date"); } }
        }

        /// <summary>
        /// 검사항목명(EDI)
        /// </summary>
        [DataMember]
        internal string EntryName
        {
            get { return entryName; }
            set { entryName = value; OnPropertyChanged("EntryName"); }
        }

        /// <summary>
        /// 검사항목명(보건의료용어표준)
        /// </summary>
        [DataMember]
        internal string EntryName_KOSTOM
        {
            get { return entryName_KOSTOM; }
            set { entryName_KOSTOM = value; OnPropertyChanged("EntryName_KOSTOM"); }
        }

        /// <summary>
        /// 검사항목코드(EDI)
        /// </summary>
        [DataMember]
        internal string EntryCode
        {
            get { return entryCode; }
            set { entryCode = value; OnPropertyChanged("EntryCode"); }
        }

        /// <summary>
        /// 검사항목코드(KOSTOM)
        /// </summary>
        [DataMember]
        internal string EntryCode_KOSTOM
        {
            get { return entryCode_KOSTOM; }
            set { entryCode_KOSTOM = value; OnPropertyChanged("EntryCode_KOSTOM"); }
        }

        /// <summary>
        /// 검사명(EDI)
        /// </summary>
        [DataMember]
        internal string TestName
        {
            get { return testName; }
            set { testName = value; OnPropertyChanged("TestName"); }
        }

        /// <summary>
        /// 검사결과값
        /// </summary>
        [DataMember]
        internal string ResultValue
        {
            get { return resultValue; }
            set { resultValue = value; OnPropertyChanged("ResultValue"); }
        }        

        /// <summary>
        /// 참고치
        /// </summary>
        [DataMember]
        internal string Reference
        {
            get { return reference; }
            set { reference = value; OnPropertyChanged("Reference"); }
        }
        #endregion
    }

}

