using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using xave.com.helper.model;

namespace xave.com.helper
{
    public static class Logger
    {
        private static object syncRoot = new Object();

        private static ISession session;
        private static ISession Session
        {
            get
            {
                if (session == null || !session.IsOpen)
                {
                    lock (syncRoot)
                    {
                        session = NHibernateHelper.GetSession("log", new List<Type>() { typeof(LogMap) });
                        //session = NHibernateHelper.OpenSession();
                    }
                }
                return session;
            }
        }

        public static void Save(System.Reflection.MethodBase mb, string AbsoluteUri, Exception e = null, string RequestMessage = null, string ResponseMessage = null, string UserMessage = null)
        {
            if (e != null)
                UserMessage = string.Format("{0}\r\n\r\nException:{1}\r\nInnerException:{2}\r\nStackTrace:{3}", UserMessage, e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty, e.StackTrace);
            Log log = new Log() { ApplicationEntity = mb.ReflectedType.Name, Endpoint = AbsoluteUri, Method = mb.Name, RequesterIPAddress = HttpContext.Current.Request.UserHostAddress, RequestMessage = RequestMessage, ResponseMessage = ResponseMessage, UserMessage = UserMessage };
            Logger.Save(log);
        }

        public static void Save(Log log)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(save), log);
        }

        private static void save(object _log)
        {
            Log log = _log as Log;
            if (log == null) return;

            try
            {
                Session.Save(log);
                Session.Flush();
            }
            catch (Exception e)
            {
                Session.Close();

                log.UserMessage = string.Format("{0}\r\n\r\nException:{1}\r\nInnerException:{2}\r\nStackTrace:{3}", log.UserMessage, e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty, e.StackTrace);

                save(log);
            }
        }

        private static void close()
        {
            Session.Close();
        }



        public static void CreateDirectoryIfNotExists(string filePath)
        {
            var directory = new FileInfo(filePath).Directory;
            if (directory == null) throw new Exception("Directory could not be determined for the filePath");

            Directory.CreateDirectory(directory.FullName);
        }

        public static void Write(string doc, string filePath = null)
        {
            CreateDirectoryIfNotExists(filePath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                file.WriteLine(doc);
            }
        }

        public static void Write(XmlDocument doc, string filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath)) filePath = filePath + DateTime.Now.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("HHmmssffffff") + ".txt";
            else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("HHmmssffffff") + ".txt";
            CreateDirectoryIfNotExists(filePath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                file.WriteLine(doc.InnerXml);
            }
        }

        public static void WriteLog(string userMessage, string applicationEntity = null, string filePath = null, int rotation = 0)
        {
            LogModel param = new LogModel() { userMessage = userMessage, actor = applicationEntity, filepath = filePath, rotation = rotation + 1 };
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLog), param);
        }

        private static void WriteLog(object param)
        {
            LogModel logModel = param as LogModel;
            if (logModel == null) return;
            string filePath = null;

            try
            {
                if (string.IsNullOrEmpty(logModel.actor)) logModel.actor = "NullActor";
                if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                CreateDirectoryIfNotExists(filePath);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(logModel.userMessage);
                }
            }
            catch
            {
                if (logModel.rotation < 10)
                    WriteLog(logModel.userMessage, logModel.actor, logModel.filepath, ++logModel.rotation);
            }
        }

        public static void ExceptionLog(Exception e, string applicationEntity = null, string filepath = null, int rotation = 0)
        {
            LogModel param = new LogModel() { e = e, rotation = rotation, filepath = filepath };
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        }

        public static void ExceptionLog(Exception e, string userMessage, string applicationEntity = null, string filepath = null, int rotation = 0)
        {
            LogModel param = new LogModel() { e = e, rotation = rotation, userMessage = userMessage, filepath = filepath };
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        }

        public static void ExceptionLog<T>(Exception e, string userMessage, T obj, string applicationEntity = null, string filepath = null, int rotation = 0)
        {
            LogModel param = new LogModel() { e = e, rotation = rotation, userMessage = userMessage, obj = obj, filepath = filepath };
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        }

        private static void ExceptionLog(object param)
        {
            LogModel logModel = param as LogModel;
            if (logModel == null) return;
            string filePath = null;

            try
            {
                if (string.IsNullOrEmpty(logModel.actor)) logModel.actor = "NullActor";
                string innerException = logModel.e.InnerException != null ? logModel.e.InnerException.Message : string.Empty;
                string errorMessage = string.Format("[{0}]\nError\nException: {1}\nInnerException: {2}\nStackTrace: {3}",
                                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                                                    logModel.e.Message.ToString(),
                                                    innerException,
                                                    logModel.e.StackTrace);
                if (!string.IsNullOrEmpty(logModel.userMessage)) errorMessage = errorMessage + "\n" + logModel.userMessage;
                if (!string.IsNullOrEmpty(logModel.transaction)) errorMessage = errorMessage + "\n" + logModel.transaction;

                if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\log\" + "Error_" + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                Write(errorMessage, filePath);

                if (logModel.obj != null)
                {
                    string xml = logModel.obj.SerializeObjectXml();
                    if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                    else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                    Write(xml, filePath);

                    string json = logModel.obj.SerializeObjectJson();
                    if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                    else filePath = Environment.CurrentDirectory + @"\log\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + logModel.actor + DateTime.Now.ToString("HHmmssffffff") + ".txt";
                    Write(json, filePath);
                }
            }
            catch
            {
                if (logModel.rotation < 10)
                    ExceptionLog(logModel.e, logModel.actor, logModel.filepath, ++logModel.rotation);
            }
        }

        public static string SerializeObjectJson<T>(this T toSerialize)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(toSerialize.GetType());
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, toSerialize);
            String json = Encoding.UTF8.GetString(ms.ToArray());
            return json;
        }

        public static string SerializeObjectXml<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// ex:
        /// - ExceptionLog(e, "ITI-70", "Document Metadata Notify", "Puller");
        /// </summary>
        /// <param name="e">Exception</param>
        /// <param name="transaction">string</param>
        /// <param name="transactionName">string</param>
        /// <param name="actor">string</param>
        public static void ExceptionLog(Exception e, string transaction, string transactionName, string actor, string filepath, int rotation = 0)
        {
            LogModel param = new LogModel() { e = e, rotation = rotation, transaction = transaction, actor = actor, transactionName = transactionName, filepath = filepath };
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
        }

        private class LogModel
        {
            public Exception e;
            public string transaction;
            public string transactionName;
            public string actor;
            public int rotation = 0;
            public string userMessage;
            public string serializedObject;
            public object obj;
            public string filepath;
        }
    }
}
