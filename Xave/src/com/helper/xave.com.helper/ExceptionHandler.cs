using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace xave.com.helper
{
    //public static class ExceptionHandler
    //{
    //    static ExceptionHandler()
    //    {
    //        //TransactionLog = new LogService.TransactionLog(LogServiceEndpoint);
    //    }

    //    static Exception exception;
    //    static HttpStatusCode httpStatusCode;
    //    static string serviceName;
    //    static string _OPCODE;
    //    static string ErrorMessage = null;
    //    static string TransactionID = null;
    //    static string ErrorLogDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDirectory"];

    //    public static HttpResponseException WebExcetion<T>(Exception e, HttpStatusCode code, string ServiceName, string OPCODE, string transactionID, T obj)
    //    {
    //        TransactionID = transactionID;

    //        var ExceptionMessage = MessageHandler.GetErrorResponse(TransactionID, ServiceName, OPCODE, e, WriteCDAObject(obj));
    //        Logger.ExceptionLog(e, ExceptionMessage, ServiceName, ErrorLogDirectory, 0);

    //        var ResponseMessage = new HttpResponseMessage(code)
    //        {
    //            Content = new StringContent(MessageHandler.GetErrorResponse(TransactionID, ServiceName, OPCODE, e, ErrorMessage)),
    //        };

    //        return new HttpResponseException(ResponseMessage);
    //    }

    //    public static string WriteCDAObject<T>(T obj)
    //    {
    //        if (obj != null)
    //        {
    //            try
    //            {
    //                if (obj.GetType() == typeof(string))
    //                {
    //                    return obj.ToString();
    //                }
    //                else
    //                {
    //                    return XmlSerializer<T>.Serialize(obj);
    //                }
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        return null;
    //    }

    //    //public static HttpResponseException WebExcetion(Exception e, HttpStatusCode code, string ServiceName, string OPCODE, string TRST_ID, string cda)
    //    //{
    //    //    //exception = e;
    //    //    //httpStatusCode = code;
    //    //    //serviceName = ServiceName;
    //    //    //_OPCODE = OPCODE;
    //    //    //_cdaObject = null;
    //    //    //_cda = cda;
    //    //    //TransactionID = TRST_ID;

    //    //    //BackgroundWorker bw = new BackgroundWorker();
    //    //    //bw.WorkerReportsProgress = false;
    //    //    //bw.WorkerSupportsCancellation = false;
    //    //    //bw.DoWork += new DoWorkEventHandler(ExceptionLog);
    //    //    //bw.RunWorkerAsync();

    //    //    var ExceptionMessage = MessageHandler.GetErrorResponse(TransactionID, ServiceName, OPCODE, e, ErrorMessage);
    //    //    var ResponseMessage = new HttpResponseMessage(code)
    //    //    {
    //    //        Content = new StringContent(MessageHandler.GetErrorResponse(TransactionID, ServiceName, OPCODE, e, ErrorMessage)),
    //    //        //ReasonPhrase = MessageHandler.GetErrorResponse(TransactionID, ServiceName, OPCODE, e, ErrorMessage),
    //    //    };

    //    //    return new HttpResponseException(ResponseMessage);
    //    //}
    //}
}
