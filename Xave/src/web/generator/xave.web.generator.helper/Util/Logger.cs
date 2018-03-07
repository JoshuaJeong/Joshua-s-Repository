using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

public static class Logger
{
    public static void CreateDirectoryIfNotExists(string filePath)
    {
        var directory = new FileInfo(filePath).Directory;
        if (directory == null) throw new Exception("Directory could not be determined for the filePath");

        Directory.CreateDirectory(directory.FullName);
    }

    public static void Write(string doc, string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath)) filePath = Environment.CurrentDirectory + @"\log\" + "MSG_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
        CreateDirectoryIfNotExists(filePath);
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        {
            file.WriteLine(doc);
        }
    }

    public static void Write(XmlDocument doc, string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath)) filePath = Environment.CurrentDirectory + @"\log\" + "MSG_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
        CreateDirectoryIfNotExists(filePath);
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
        {
            file.WriteLine(doc.InnerXml);
        }
    }

    public static void ExceptionLog(Exception e, string filepath, int rotation = 0)
    {
        LogModel param = new LogModel() { e = e, rotation = rotation, filepath = filepath };
        ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
    }

    public static void ExceptionLog(Exception e, string userMessage, string filepath, int rotation = 0)
    {
        LogModel param = new LogModel() { e = e, rotation = rotation, userMessage = userMessage, filepath = filepath };
        ThreadPool.QueueUserWorkItem(new WaitCallback(ExceptionLog), param);
    }

    public static void ExceptionLog<T>(Exception e, string userMessage, T obj, string filepath, int rotation = 0)
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
            string innerException = logModel.e.InnerException != null ? logModel.e.InnerException.Message : string.Empty;
            string errorMessage = string.Format("[{0}]\nError\nException: {1}\nInnerException: {2}\nStackTrace: {3}",
                                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                                logModel.e.Message.ToString(),
                                                innerException,
                                                logModel.e.StackTrace);
            if (!string.IsNullOrEmpty(logModel.userMessage)) errorMessage = errorMessage + "\n" + logModel.userMessage;
            if (!string.IsNullOrEmpty(logModel.transaction)) errorMessage = errorMessage + "\n" + logModel.transaction;

            if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + @"\Error_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
            else filePath = Environment.CurrentDirectory + @"\log\" + "Error_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
            //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\log\" + "Error_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
            Write(errorMessage, filePath);

            if (logModel.obj != null)
            {
                string xml = logModel.obj.SerializeObjectXml();
                if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + @"\ErrorObjectXml_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
                else filePath = Environment.CurrentDirectory + @"\log\" + "ErrorObjectXml_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
                Write(xml, filePath);

                string json = logModel.obj.SerializeObjectJson();
                if (!string.IsNullOrEmpty(logModel.filepath)) filePath = logModel.filepath + @"\ErrorObjectJson_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
                else filePath = Environment.CurrentDirectory + @"\log\" + "ErrorObjectJson_" + DateTime.Now.ToString("HHmmssfff") + ".txt";
                Write(json, filePath);
            }
        }
        catch
        {
            if (logModel.rotation < 10)
                ExceptionLog(logModel.e, logModel.filepath, ++logModel.rotation);
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
