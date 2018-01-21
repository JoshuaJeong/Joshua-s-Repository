using System;
using System.Runtime.Serialization;
using System.Threading;

namespace xave.com.helper
{
    public static class LogService
    {
        private static readonly string LogServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["LogServiceEndpoint"];

        /// <summary>
        /// Logging Service 이용하여 사용자 정의 로그를 기록합니다.
        /// </summary>
        /// <param name="_TransactionID">트랜잭션 ID</param>
        /// <param name="_BusinessID">업무유형 ID</param>
        /// <param name="_LogLevel">로깅 레벨 (0=Debug, 1=Info, 2=Warning, 3=Error, 4=Fatal, 4이상=Error) </param>
        /// <param name="_TransactionCode">트랜잭션 유형코드</param>
        /// <param name="_ApplicationName">시스템명</param>
        /// <param name="_RequestMessage">요청 메세지</param>
        /// <param name="_RequestStatus">요청 결과</param>
        /// <param name="_ResponseMessage">수신 메세지</param>
        /// <param name="_UserMessage">사용자 정의 메세지</param>
        /// <param name="_ResponseStatus">수신 결과</param>        
        public static void LogString(string _TransactionID, string _BusinessID, int _LogLevel, string _TransactionCode, string _ApplicationName, string _RequestMessage, string _RequestStatus, string _ResponseMessage, string _UserMessage, string _ResponseStatus)
        {
            TransactionLogModel param = new TransactionLogModel()
            {
                TransactionID = string.IsNullOrEmpty(_TransactionID) ? GetNewTransactionID() : _TransactionID,
                BusinessID = string.IsNullOrEmpty(_BusinessID) ? GetNewTransactionID() : _BusinessID,
                LogLevel = _LogLevel,
                TransactionCode = _TransactionCode, //"PatientRegistryGetIdentifiersQueryBridge",
                ApplicationName = _ApplicationName, // "Puller",
                RequestIpAddress = GetIP(),//"192.166.0.1",
                TransactionTime = DateTime.Now,
                RequestMessage = _RequestMessage,//"요청 메시지",
                RequestStatus = _RequestStatus,//"SUCCESS",
                ResponseMessage = _ResponseMessage,//"수신 메시지",
                UserMessage = _UserMessage,//"테스트 메시지",
                ResponseStatus = _ResponseStatus,//"SUCCESS"
            };
            //ThreadPool.QueueUserWorkItem(new WaitCallback(LogString), Model);
            ThreadPool.QueueUserWorkItem(new WaitCallback(LogString), param);
        }

        private static void LogString(object param)
        {
            try
            {
                TransactionLogModel model = param as TransactionLogModel;
                WebApiHandler.PostAsync<TransactionLogModel>(LogServiceEndpoint, model);
            }
            catch
            {
            }
        }

        private static string GetIP()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }

        private static string GetNewTransactionID()
        {
            return string.Format("{0}_{1}", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString());
        }

        class TransactionLogModel
        {
            #region Property
            /// <summary>
            /// 트랜잭션 추적을 위한 ID를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string TransactionID { get; set; }

            /// <summary>
            /// 정보교류 업무 유형 아이디를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string BusinessID { get; set; }

            /// <summary>
            /// 각 수준의 로깅 레벨을 가져오거나 설정 합니다.
            /// <para>0=Debug, 1=Info, 2=Warning, 3=Error, 4=Fatal, 4이상=Error</para>
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual int LogLevel { get; set; }

            /// <summary>
            /// 트랜잭션 유형 코드를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string TransactionCode { get; set; }

            /// <summary>
            /// 트랜잭션이 발생하는 시스템 명(exe / dll)을 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string ApplicationName { get; set; }

            /// <summary>
            /// 트랜잭션을 요청한 Client 의 IP를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string RequestIpAddress { get; set; }

            /// <summary>
            /// 트랜잭션을 요청한 날짜와 시간을 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual DateTime TransactionTime { get; set; }

            /// <summary>
            /// 요청 메시지를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string RequestMessage { get; set; }

            /// <summary>
            /// 요청 결과 값을 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string RequestStatus { get; set; }

            /// <summary>
            /// 수신 메시지를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string ResponseMessage { get; set; }

            /// <summary>
            /// 수신 결과 값을 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string ResponseStatus { get; set; }

            /// <summary>
            /// 사용자 정의 메시지를 가져오거나 설정 합니다.
            /// </summary>
            [DataMember(IsRequired = true)]
            public virtual string UserMessage { get; set; }
            #endregion
        }
    }
}
