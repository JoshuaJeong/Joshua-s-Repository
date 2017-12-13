using System;

namespace xave.com.helper
{
    public static class MessageHandler
    {
        private const string Row = "【◈】";
        private const string Column = "【▣】";
        private const string End = "*@*";

        //public static string GetLogMessage(string result, string applicationEntity, string methodName, string serviceEndpoint, string requestMessage, string responseMessage, List<xave.com.helper.Logger.NextEndpoint> nextEndpoints, string Message)
        //{
        //    string row0 = string.Format("[TransactionResult]  {0}  {1}  {2}  {3}", result, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), applicationEntity, methodName);
        //    string row1 = string.Format("[ServiceEndpoint]\r\n{0}", serviceEndpoint);
        //    string row2 = string.Format("[RequestMessage]\r\n{0}", requestMessage);
        //    string row3 = string.Empty;
        //    foreach (xave.com.helper.Logger.NextEndpoint nextEndpoint in nextEndpoints)
        //    {
        //        row3 = row3 + string.Format("[NextEndpoint]  {0}\r\n{1}\r\n[RequestMessage]\r\n{2}\r\n[ResponseMessage]\r\n{3}\r\n", nextEndpoint.Result, nextEndpoint.ServiceEndpoint, nextEndpoint.RequestMessage, nextEndpoint.ResponseMessage);
        //    }
        //    string row4 = string.Format("[ResponseMessage]  {0}  {1}  {2}  {3}", result, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), applicationEntity, methodName);
        //    return string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}================================================\r\n\r\n", row0, row1, row2, row3, row4, Message);
        //}

        //public static string GetErrorMessage(Exception e, string result, string applicationEntity, string methodName, string serviceEndpoint, string requestMessage, string responseMessage, List<xave.com.helper.Logger.NextEndpoint> nextEndpoints, string Message)
        //{
        //    string row0 = string.Format("[TransactionResult]  {0}  {1}  {2}  {3}", result, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), applicationEntity, methodName);
        //    string row1 = string.Format("[ServiceEndpoint]\r\n{0}", serviceEndpoint);
        //    string row2 = string.Format("[RequestMessage]\r\n{0}", requestMessage);
        //    string row3 = string.Empty;
        //    foreach (xave.com.helper.Logger.NextEndpoint nextEndpoint in nextEndpoints)
        //    {
        //        row3 = row3 + string.Format("[NextEndpoint]  {0}\r\n{1}\r\n[RequestMessage]\r\n{2}\r\n[ResponseMessage]\r\n{3}\r\n", nextEndpoint.Result, nextEndpoint.ServiceEndpoint, nextEndpoint.RequestMessage, nextEndpoint.ResponseMessage);
        //    }
        //    string row4 = string.Format("[ResponseMessage]  {0}  {1}  {2}  {3}", result, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), applicationEntity, methodName);
        //    return string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}================================================\r\n\r\n", row0, row1, row2, row3, row4, Message);
        //}

        public static string GetErrorMessage(Exception e)
        {
            string errorMessage = string.Format("Message: {0}, \r\n \r\n, Source: {1}\r\n \r\nInnerException: {2}, \r\n \r\nStackTrace: {3}",
                        e.Message,
                        e.Source,
                        e.InnerException != null ? e.InnerException.Message : string.Empty,
                        e.StackTrace);

            //Debug.WriteLine(errorMessage);

            return errorMessage;
        }

        //public static string GetErrorMessage(Exception e, string additionalMessage = "")
        //{
        //    string errorMessage = string.Format("Message: {0}, \r\n \r\nInnerException: {1}, \r\n \r\nStackTrace: {2}",
        //                e.Message,
        //                e.InnerException != null ? e.InnerException.Message : string.Empty,
        //                e.StackTrace);

        //    return string.IsNullOrEmpty(additionalMessage) ? errorMessage : errorMessage + additionalMessage;
        //}

        //public static string GetErrorResponse(string operationCode, string transactionId, Exception e, string userMessage = "")
        //{
        //    return GetErrorResponse(null, operationCode, transactionId, e, userMessage);
        //}

        public static string GetErrorResponse(string service, string operationCode, string transactionId, Exception e, string userMessage = "")
        {
            return string.Format("{0} | {1} | {2} | {3} | {4}", transactionId, service, operationCode, GetErrorMessage(e), userMessage);
        }

        //public static string GenerateResponseMessage(string str)
        //{
        //    string retVal = string.Format("<root>{0}</root>", str);

        //    //Debug.WriteLine(retVal);

        //    return retVal;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="loggingType">Request / Response</param>
        /// <param name="operationCode"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string GetLogMessage(string operationCode, string loggingType, string transactionId, string Message)
        {
            return string.Format("{0} | {1} | {2} | {3}", operationCode, loggingType, transactionId, Message);
        }
    }
}
