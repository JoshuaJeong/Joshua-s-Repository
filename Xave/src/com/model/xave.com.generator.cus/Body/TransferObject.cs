using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 이송 관련 내용
    /// </summary>
    [DataContract]
    [Serializable]
    [XmlSerializerFormat]
    public class TransferObject : ModelBase
    {
        #region : Private Member
        private string reasonForTransfer;
        private string arrivalTime;
        private string transferDate;
        private string transportaion;
        private string carNumber;
        private string practitioner;
        #endregion

        #region : Public Property
        /// <summary>
        /// 도착시간
        /// </summary>
        [DataMember]
        public virtual string ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; OnPropertyChanged("ArrivalTime"); }
        }

        public string GetArrivalTime() { return ArrivalTime; }
        public void SetArrivalTime(string _ArrivalTime) { ArrivalTime = _ArrivalTime; }

        /// <summary>
        /// 다른 기관으로 이송한 시각
        /// </summary>
        [DataMember]
        public virtual string TransferDate
        {
            get { return transferDate; }
            set { transferDate = value; OnPropertyChanged("TransferDate"); }
        }

        public string GetTransferDate() { return TransferDate; }
        public void SetTransferDate(string _TransferDate) { TransferDate = _TransferDate; }

        /// <summary>
        /// 이송결정 이유
        /// </summary>
        [DataMember]
        public virtual string ReasonForTransfer
        {
            get { return reasonForTransfer; }
            set { reasonForTransfer = value; OnPropertyChanged("ReasonForTransfer"); }
        }

        public string GetReasonForTransfer() { return ReasonForTransfer; }
        public void SetReasonForTransfer(string _ReasonForTransfer) { ReasonForTransfer = _ReasonForTransfer; }        

        /// <summary>
        /// 이송수단
        /// </summary>
        [DataMember]
        public virtual string Transportaion
        {
            get { return transportaion; }
            set { if (transportaion != value) { transportaion = value; OnPropertyChanged("Transportaion"); } }
        }

        public string GetTransportaion() { return Transportaion; }
        public void SetTransportaion(string _Transportaion) { Transportaion = _Transportaion; }

        /// <summary>
        /// 차량번호
        /// </summary>
        [DataMember]
        public virtual string CarNumber
        {
            get { return carNumber; }
            set { if (carNumber != value) { carNumber = value; OnPropertyChanged("CarNumber"); } }
        }

        public string GetCarNumber() { return CarNumber; }
        public void SetCarNumber(string _CarNumber) { CarNumber = _CarNumber; }

        /// <summary>
        /// 동승 응급의료 종사자
        /// </summary>
        [DataMember]
        public virtual string Practitioner
        {
            get { return practitioner; }
            set { if (practitioner != value) { practitioner = value; OnPropertyChanged("Practitioner"); } }
        }
        public string GetPractitioner() { return Practitioner; }
        public void SetPractitioner(string _Practitioner) { Practitioner = _Practitioner; }

        #endregion
    }
}
