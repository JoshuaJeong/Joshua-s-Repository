using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Generator.ValueObject
{
    /// <summary>
    /// 영상검사결과
    /// </summary>
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    internal class RadiologyLabObject : ModelBase
    {
        #region :: Private Member
        private string testName;
        private string testName_KOSTOM;
        private string testCode;
        private string testCode_KOSTOM;
        private string resultValue;
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
        /// 검사명(EDI)
        /// </summary>
        [DataMember]
        internal string TestName
        {
            get { return testName; }
            set { testName = value; OnPropertyChanged("TestName"); }
        }

        /// <summary>
        /// 검사명(보건의료용어표준)
        /// </summary>
        [DataMember]
        internal string TestName_KOSTOM
        {
            get { return testName_KOSTOM; }
            set { testName_KOSTOM = value; OnPropertyChanged("TestName_KOSTOM"); }
        }
        
        /// <summary>
        /// 검사코드(EDI)
        /// </summary>
        [DataMember]
        internal string TestCode
        {
            get { return testCode; }
            set { testCode = value; OnPropertyChanged("TestCode"); }
        }
        
        /// <summary>
        /// 검사코드(보건의료용어표준)
        /// </summary>
        [DataMember]
        internal string TestCode_KOSTOM
        {
            get { return testCode_KOSTOM; }
            set { testCode_KOSTOM = value; OnPropertyChanged("TestCode_KOSTOM"); }
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
        #endregion
        
    }
}

