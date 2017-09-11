using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Generator.ValueObject
{
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    internal class RadiologyObject : ModelBase
    {
        #region :: Private Member
        private string testName;
        private string testCode;
        #endregion

        #region :: Public Property
        /// <summary>
        /// 검사명
        /// </summary>
        [DataMember]
        internal string TestName
        {
            get { return testName; }
            set { testName = value; OnPropertyChanged("TestName"); }
        }

        /// <summary>
        /// 검사코드
        /// </summary>
        [DataMember]
        internal string TestCode
        {
            get { return testCode; }
            set { testCode = value; OnPropertyChanged("TestCode"); }
        }
        #endregion
    }
}
