using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Xaver.GLOBAL.COM.Helper
{
    public static class Logger
    {
        public static void WriteLog<TI, TO>(TI request, TO response, string applicationEntity, string methodName, string result, string serviceEndpoint, string userMessage, List<NextEndpoint> nextEndpoints, string filePath = null, int rotation = 0)
        {
            filePath = string.IsNullOrEmpty(filePath) ? System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] : filePath;
            LogModel<TI, TO> param = new LogModel<TI, TO>()
            {
                Request = request,
                Response = response,
                ApplicationEntity = applicationEntity,
                MethodName = methodName,
                Result = result,
                ServiceEndpoint = serviceEndpoint,
                UserMessage = userMessage,
                NextEndpoints = nextEndpoints,
                Filepath = filePath,
                Rotation = rotation + 1
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog<TI, TO>), param);
        }

        public static void SuccessLog<TI, TO>(TI request, TO response, string applicationEntity, string methodName, string serviceEndpoint, string userMessage, List<NextEndpoint> nextEndpoints, string filePath = null, int rotation = 0)
        {
            filePath = string.IsNullOrEmpty(filePath) ? System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] : filePath;
            LogModel<TI, TO> param = new LogModel<TI, TO>()
            {
                Request = request,
                Response = response,
                ApplicationEntity = applicationEntity,
                MethodName = methodName,
                Result = "Success",
                ServiceEndpoint = serviceEndpoint,
                UserMessage = userMessage,
                NextEndpoints = nextEndpoints,
                Filepath = filePath,
                Rotation = rotation + 1
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog<TI, TO>), param);
        }

        public static void ExceptionLog<TI, TO>(Exception e, TI request, TO response, string applicationEntity, string methodName, string serviceEndpoint, string userMessage, List<NextEndpoint> nextEndpoints, string filePath = null, int rotation = 0)
        {
            filePath = string.IsNullOrEmpty(filePath) ? System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] : filePath;
            LogModel<TI, TO> param = new LogModel<TI, TO>()
            {
                e = e,
                Request = request,
                Response = response,
                ApplicationEntity = applicationEntity,
                MethodName = methodName,
                Result = "Error",
                ServiceEndpoint = serviceEndpoint,
                UserMessage = userMessage,
                NextEndpoints = nextEndpoints,
                Filepath = filePath,
                Rotation = rotation + 1
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog<TI, TO>), param);
        }

        public static void WriteLog<TI, TO>(Exception e, TI request, TO response, string applicationEntity, string methodName, string result, string serviceEndpoint, string userMessage, List<NextEndpoint> nextEndpoints, string filePath = null, int rotation = 0)
        {
            filePath = string.IsNullOrEmpty(filePath) ? System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] : filePath;
            LogModel<TI, TO> param = new LogModel<TI, TO>()
            {
                e = e,
                Request = request,
                Response = response,
                ApplicationEntity = applicationEntity,
                MethodName = methodName,
                Result = result,
                ServiceEndpoint = serviceEndpoint,
                UserMessage = userMessage,
                NextEndpoints = nextEndpoints,
                Filepath = filePath,
                Rotation = rotation + 1
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog<TI, TO>), param);
        }

        private static void WriteLog<TI, TO>(object param)
        {
            LogModel<TI, TO> logModel = param as LogModel<TI, TO>;
            if (logModel == null) return;

            try
            {
                #region Set FileName
                string fileName = string.Format("{0}{1}{2}{3}.txt", DateTime.Now.ToString("HHmmssfff"), logModel.Result, logModel.ApplicationEntity, logModel.MethodName);
                #endregion

                #region Set FilePath
                string filePath = fileName;
                if (!string.IsNullOrEmpty(logModel.Filepath))
                {
                    filePath = logModel.Filepath + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName;
                    CreateDirectoryIfNotExists(filePath);
                }
                #endregion

                #region Set MessageFormat
                string logMessage = null;
                if (logModel.e == null)
                    logMessage = MessageHandler.GetLogMessage(logModel.Result, logModel.ApplicationEntity, logModel.MethodName, logModel.ServiceEndpoint, logModel.XmlRequest, logModel.XmlResponse, logModel.NextEndpoints, logModel.UserMessage);
                else
                    logMessage = MessageHandler.GetErrorMessage(logModel.e, logModel.Result, logModel.ApplicationEntity, logModel.MethodName, logModel.ServiceEndpoint, logModel.XmlRequest, logModel.XmlResponse, logModel.NextEndpoints, logModel.UserMessage);
                #endregion

                #region Logging
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(logMessage);
                }
                #endregion
            }
            catch
            {
                if (logModel.Rotation < 10)
                    WriteLog(logModel.e, logModel.Request, logModel.Response, logModel.ApplicationEntity, logModel.MethodName, logModel.Result, logModel.ServiceEndpoint, logModel.UserMessage, logModel.NextEndpoints, logModel.Filepath, ++logModel.Rotation);
            }
        }

        private static void CreateDirectoryIfNotExists(string filePath)
        {
            var directory = new FileInfo(filePath).Directory;
            if (directory == null) throw new Exception("Directory could not be determined for the filePath");

            Directory.CreateDirectory(directory.FullName);
        }

        //public static void WriteLog(string userMessage, string applicationEntity = null, string filePath = null, int rotation = 0)
        //{
        //    LogModel param = new LogModel() { userMessage = userMessage, actor = applicationEntity, filepath = filePath, rotation = rotation + 1 };
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog), param);
        //}


        //public static void ExceptionLog(Exception e, string applicationEntity = null, string filepath = null, int rotation = 0)
        //{
        //    LogModel param = new LogModel() { e = e, rotation = rotation, filepath = filepath };
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        //}

        //public static void ExceptionLog(Exception e, string userMessage, string applicationEntity = null, string filepath = null, int rotation = 0)
        //{
        //    LogModel param = new LogModel() { e = e, rotation = rotation, userMessage = userMessage, filepath = filepath };
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        //}

        //public static void ExceptionLog<T>(Exception e, string userMessage, T obj, string applicationEntity = null, string filepath = null, int rotation = 0)
        //{
        //    LogModel param = new LogModel() { e = e, rotation = rotation, userMessage = userMessage, obj = obj, filepath = filepath };
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        //}

        ///// <summary>
        ///// ex:
        ///// - ExceptionLog(e, "ITI-70", "Document Metadata Notify", "Puller");
        ///// </summary>
        ///// <param name="e">Exception</param>
        ///// <param name="transaction">string</param>
        ///// <param name="transactionName">string</param>
        ///// <param name="actor">string</param>
        //public static void ExceptionLog(Exception e, string transaction, string transactionName, string actor, string filepath, int rotation = 0)
        //{
        //    LogModel param = new LogModel() { e = e, rotation = rotation, transaction = transaction, actor = actor, transactionName = transactionName, filepath = filepath };
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        //}

        //private static void ExceptionLog(object param)
        //{
        //    LogModel logModel = param as LogModel;
        //    if (logModel == null) return;
        //    string filePath = null;

        //    try
        //    {
        //        if (string.IsNullOrEmpty(logModel.actor)) logModel.actor = "NullActor";
        //        string innerException = logModel.e.InnerException != null ? logModel.e.InnerException.Message : string.Empty;
        //        string errorMessage = string.Format("[{0}]\nError\nException: {1}\nInnerException: {2}\nStackTrace: {3}",
        //                                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
        //                                            logModel.e.Message.ToString(),
        //                                            innerException,
        //                                            logModel.e.StackTrace);
        //        if (!string.IsNullOrEmpty(logModel.userMessage)) errorMessage = errorMessage + "\n" + logModel.userMessage;
        //        if (!string.IsNullOrEmpty(logModel.transaction)) errorMessage = errorMessage + "\n" + logModel.transaction;

        //        if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //        else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //        //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\log\" + "Error_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //        Write(errorMessage, filePath);

        //        if (logModel.obj != null)
        //        {
        //            string xml = logModel.obj.SerializeObjectXml();
        //            if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //            else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //            Write(xml, filePath);

        //            string json = logModel.obj.SerializeObjectJson();
        //            if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //            else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //            Write(json, filePath);
        //        }
        //    }
        //    catch
        //    {
        //        if (logModel.rotation < 10)
        //            ExceptionLog(logModel.e, logModel.actor, logModel.filepath, ++logModel.rotation);
        //    }
        //}

        private static string SerializeObjectJson<T>(this T toSerialize)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(toSerialize.GetType());
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, toSerialize);
            String json = Encoding.UTF8.GetString(ms.ToArray());
            return json;
        }

        private static string SerializeObjectXml<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        //private static void WriteLog(object param)
        //{
        //    LogModel logModel = param as LogModel;
        //    if (logModel == null) return;
        //    string filePath = null;

        //    try
        //    {
        //        if (string.IsNullOrEmpty(logModel.actor)) logModel.actor = "NullActor";
        //        if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //        else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssfff") + ".txt";
        //        CreateDirectoryIfNotExists(filePath);
        //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        //        {
        //            file.WriteLine(logModel.userMessage);
        //        }
        //    }
        //    catch
        //    {
        //        if (logModel.rotation < 10)
        //            WriteLog(logModel.userMessage, logModel.actor, logModel.filepath, ++logModel.rotation);
        //    }
        //}

        //private static void CreateDirectoryIfNotExists(string filePath)
        //{
        //    var directory = new FileInfo(filePath).Directory;
        //    if (directory == null) throw new Exception("Directory could not be determined for the filePath");

        //    Directory.CreateDirectory(directory.FullName);
        //}

        //private static void Write(string doc, string filePath = null)
        //{
        //    CreateDirectoryIfNotExists(filePath);
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        //    {
        //        file.WriteLine(doc);
        //    }
        //}

        //private static void Write(XmlDocument doc, string filePath = null)
        //{
        //    CreateDirectoryIfNotExists(filePath);
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        //    {
        //        file.WriteLine(doc.InnerXml);
        //    }
        //}

        private class LogModel<TI, TO>
        {
            public Exception e;

            public TI Request;
            public TO Response;

            public string ApplicationEntity;
            public string MethodName;
            public string Result;
            public string ServiceEndpoint;
            public string UserMessage;
            public List<NextEndpoint> NextEndpoints = null;

            public string Filepath;

            public int Rotation = 0;


            public string JsonRequest
            {
                get
                {
                    if (Request is string) return Request as string;
                    return Request != null ? Request.SerializeObjectJson() : null;
                }
            }
            public string XmlRequest
            {
                get
                {
                    if (Request is string) return Request as string;
                    return Request != null ? Request.SerializeObjectXml() : null;
                }
            }

            public string JsonResponse
            {
                get
                {
                    if (Response is string) return Response as string;
                    return Response != null ? Response.SerializeObjectJson() : null;
                }
            }
            public string XmlResponse
            {
                get
                {
                    if (Response is string) return Response as string;
                    return Response != null ? Response.SerializeObjectXml() : null;
                }
            }
        }

        public class NextEndpoint
        {
            public string RequestMessage;
            public string ResponseMessage;

            public string ServiceEndpoint;
            public string MethodName;
            public string Result;
        }
    }
}
