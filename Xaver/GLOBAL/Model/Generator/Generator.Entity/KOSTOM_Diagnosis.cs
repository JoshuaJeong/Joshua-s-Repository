using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using Xaver.Framework;

namespace Generator.Entity
{
    /// <summary>
    /// KOSTOM 진단 DTO
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class KOSTOM_Diagnosis : BaseVo<Int64>, INotifyPropertyChanged
    {
        private string _VOC_CD;
        private string _CNPT_CD;
        private string _ENG_NM;
        private string _KOR_NM;
        private string _UMLS;
        private string _VER;
        private string _KCD_CD;
        private string _ICD9CM_CD;
        private string _LOINC_CD;
        private string _EDI_CD;
        private string _CCC_CD;
        private string _ICNP_CD;
        private string _SZ_TP;
        private string _CTG;
        private string _MED_USE_YN;
        private string _CHN_NM;

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

        /// <summary>
        /// 용어코드 PK
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string VOC_CD
        {
            get { return _VOC_CD; }
            set { if (_VOC_CD != value) { this._VOC_CD = value; this.OnPropertyChanged("VOC_CD"); } }
        }

        /// <summary>
        /// 개념코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CNPT_CD
        {
            get { return _CNPT_CD; }
            set { if (_CNPT_CD != value) { this._CNPT_CD = value; this.OnPropertyChanged("CNPT_CD"); } }
        }

        /// <summary>
        /// 영문명
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ENG_NM
        {
            get { return _ENG_NM; }
            set { if (_ENG_NM != value) { this._ENG_NM = value; this.OnPropertyChanged("ENG_NM"); } }
        }

        /// <summary>
        /// 한글명
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string KOR_NM
        {
            get { return _KOR_NM; }
            set { if (_KOR_NM != value) { this._KOR_NM = value; this.OnPropertyChanged("KOR_NM"); } }
        }

        /// <summary>
        /// UMLS
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string UMLS
        {
            get { return _UMLS; }
            set { if (_UMLS != value) { this._UMLS = value; this.OnPropertyChanged("UMLS"); } }
        }

        /// <summary>
        /// Version
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string VER
        {
            get { return _VER; }
            set { if (_VER != value) { this._VER = value; this.OnPropertyChanged("VER"); } }
        }

        /// <summary>
        /// KCD 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string KCD_CD
        {
            get { return _KCD_CD; }
            set { if (_KCD_CD != value) { this._KCD_CD = value; this.OnPropertyChanged("KCD_CD"); } }
        }

        /// <summary>
        /// ICD-9-CM 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ICD9CM_CD
        {
            get { return _ICD9CM_CD; }
            set { if (_ICD9CM_CD != value) { this._ICD9CM_CD = value; this.OnPropertyChanged("ICD9CM_CD"); } }
        }

        /// <summary>
        /// LOINC 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string LOINC_CD
        {
            get { return _LOINC_CD; }
            set { if (_LOINC_CD != value) { this._LOINC_CD = value; this.OnPropertyChanged("LOINC_CD"); } }
        }

        /// <summary>
        /// EDI 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string EDI_CD
        {
            get { return _EDI_CD; }
            set { if (_EDI_CD != value) { this._EDI_CD = value; this.OnPropertyChanged("EDI_CD"); } }
        }

        /// <summary>
        /// CCC 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CCC_CD
        {
            get { return _CCC_CD; }
            set { if (_CCC_CD != value) { this._CCC_CD = value; this.OnPropertyChanged("CCC_CD"); } }
        }

        /// <summary>
        /// ICDNP 코드
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string ICNP_CD
        {
            get { return _ICNP_CD; }
            set { if (_ICNP_CD != value) { this._ICNP_CD = value; this.OnPropertyChanged("ICNP_CD"); } }
        }

        /// <summary>
        /// 대중소 분류
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string SZ_TP
        {
            get { return _SZ_TP; }
            set { if (_SZ_TP != value) { this._SZ_TP = value; this.OnPropertyChanged("SZ_TP"); } }
        }

        /// <summary>
        /// 카테고리
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CTG
        {
            get { return _CTG; }
            set { if (_CTG != value) { this._CTG = value; this.OnPropertyChanged("CTG"); } }
        }

        /// <summary>
        /// 진료사용여부
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string MED_USE_YN
        {
            get { return _MED_USE_YN; }
            set { if (_MED_USE_YN != value) { this._MED_USE_YN = value; this.OnPropertyChanged("MED_USE_YN"); } }
        }

        /// <summary>
        /// 한자명
        /// </summary>
        [DataMember, System.Xml.Serialization.XmlElementAttribute]
        public virtual string CHN_NM
        {
            get { return _CHN_NM; }
            set { if (_CHN_NM != value) { this._CHN_NM = value; this.OnPropertyChanged("CHN_NM"); } }
        }
    }
}
