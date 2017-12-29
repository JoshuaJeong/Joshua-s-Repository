using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 진료의
    /// </summary>
    [DataContract]
    [Serializable]
    [XmlSerializerFormat]
    public class AuthorObject : ModelBase
    {
        #region :: Private Member
        private string authorName;
        private string medicalLicenseID;
        private string telecomNumber;
        private string departmentName;
        private string departmentCode;
        #endregion

        #region :: Public Property
        /// <summary>
        /// 진료의 성명
        /// </summary>
        [DataMember]
        public virtual string AuthorName
        {
            get { return authorName; }
            set { authorName = value; OnPropertyChanged("AuthorName"); }
        }

        public string GetAuthorName() { return AuthorName; }
        public void SetAuthorName(string _AuthorName) { AuthorName = _AuthorName; }

        /// <summary>
        /// 진료의 면허번호
        /// </summary>
        [DataMember]
        public virtual string MedicalLicenseID
        {
            get { return medicalLicenseID; }
            set { medicalLicenseID = value; OnPropertyChanged("MedicalLicenseID"); }
        }

        public string GetMedicalLicenseID() { return MedicalLicenseID; }
        public void SetMedicalLicenseID(string _MedicalLicenseID) { MedicalLicenseID = _MedicalLicenseID; }

        /// <summary>
        /// 의료진 연락처
        /// </summary>
        [DataMember]
        public virtual string TelecomNumber
        {
            get { return telecomNumber; }
            set { telecomNumber = value; OnPropertyChanged("TelecomNumber"); }
        }

        public string GetTelecomNumber() { return TelecomNumber; }
        public void SetTelecomNumber(string _TelecomNumber) { TelecomNumber = _TelecomNumber; }

        /// <summary>
        /// 진료과명
        /// </summary>
        [DataMember]
        public virtual string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; OnPropertyChanged("DepartmentName"); }
        }

        public string GetDepartmentName() { return DepartmentName; }
        public void SetDepartmentName(string _DepartmentName) { DepartmentName = _DepartmentName; }

        /// <summary>
        /// 진료과 코드
        /// </summary>
        [DataMember]
        public virtual string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; OnPropertyChanged("DepartmentCode"); }
        }
        public string GetDepartmentCode() { return DepartmentCode; }
        public void SetDepartmentCode(string _DepartmentCode) { DepartmentCode = _DepartmentCode; }
        #endregion
    }
}