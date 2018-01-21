using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 의뢰정보
    /// </summary>
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class ReferralTransferInformationObject : ModelBase
    {
        private string referralCurrentStatus;

        /// <summary>
        /// 상태구분
        /// 01: 현 의료기관에서 치료 요양이 가능한 환자
        /// 02: 현 의료기관에서 치료 요양이 불가능한 환자
        /// </summary>
        [DataMember]
        public virtual string ReferralCurrentStatus
        {
            get { return referralCurrentStatus; }
            set { if ( (referralCurrentStatus != value) && (value.Equals("01") || value.Equals("02")) ) { referralCurrentStatus = value; OnPropertyChanged("ReferralCurrentStatus"); } }
        }

        private string referralClinicalReason;

        /// <summary>
        /// 임상적 의뢰사유구분
        /// 01: 진단 의뢰
        /// 02: 검사 의뢰
        /// 03: 수술 의뢰
        /// 04: 내과적 시술 및 약물치료 의뢰
        /// 기타 (Text)
        /// </summary>
        [DataMember]
        public virtual string ReferralClinicalReason
        {
            get { return referralClinicalReason; }            
            set { if (referralClinicalReason != value) { referralClinicalReason = value; OnPropertyChanged("ReferralClinicalReason"); } }
        }

        private string nonClinicalReason;

        /// <summary>
        /// 비임상적 의뢰사유구분
        /// 01: 환자 수용 불가(의료진 부족 등)
        /// 02: 환자 또는 가족의 요청
        /// 03: 기타
        /// </summary>
        [DataMember]
        public virtual string NonClinicalReason
        {
            get { return nonClinicalReason; }
            set { if ((nonClinicalReason != value) && (value.Equals("01") || value.Equals("02") || value.Equals("03"))) { nonClinicalReason = value; OnPropertyChanged("NonClinicalReason"); } }
        }

        private string transferType;

        /// <summary>
        /// 회송유형구분
        /// 01: 외래 되의뢰(의뢰 했던 1단계 진료기관으로 회송)
        /// 02: 외래 회송(의뢰하지 않았던 1단계 진료기관으로 회송)
        /// 03: 입원 회송(급성기 치료이후 지속적 입원치료를 위한 회송)
        /// </summary>
        [DataMember]
        public virtual string TransferType
        {
            get { return transferType; }
            set { if ((transferType != value) && (value.Equals("01") || value.Equals("02") || value.Equals("03"))) { transferType = value; OnPropertyChanged("TransferType"); } }
        }

        private string transferClinicalReason;

        /// <summary>
        /// 임상적 회송사유구분
        /// 01: 수술 후 관리 필요
        /// 02: 수술 이외의 치료 후 관리(복약, 관리 등 포함)필요
        /// </summary>
        [DataMember]
        public virtual string TransferClinicalReason
        {
            get { return transferClinicalReason; }
            set { if ((transferClinicalReason != value) && (value.Equals("01") || value.Equals("02"))) { transferClinicalReason = value; OnPropertyChanged("TransferClinicalReason"); } }
        }

        private string transferNonClinicalReason;

        /// <summary>
        /// 비임상적 회송사유구분
        /// 01: 환자 수용 불가(입원실/의료진 부족 등)
        /// 02: 환자 또는 가족의 요청
        /// 03: 기타
        /// </summary>
        [DataMember]
        public virtual string TransferNonClinicalReason
        {
            get { return transferNonClinicalReason; }
            set { if ((transferNonClinicalReason != value) && (value.Equals("01") || value.Equals("02") || value.Equals("03"))) { transferNonClinicalReason = value; OnPropertyChanged("TransferNonClinicalReason"); } }
        }
    }    
}
