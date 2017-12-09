using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace xave.com.helper
{
    public static class WebApiHandler
    {
        private static string certificatePath = System.Configuration.ConfigurationManager.AppSettings["CertificatePath"];
        private static string certificatePassword = System.Configuration.ConfigurationManager.AppSettings["CertificatePassword"];

        public static object Post<TI, TO>(string url, object obj = null, string CertificatePath = "", string CertificatePassword = "")
        {
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            CertificatePath = string.IsNullOrEmpty(CertificatePath) ? certificatePath : CertificatePath;
            CertificatePassword = string.IsNullOrEmpty(CertificatePassword) ? certificatePassword : CertificatePassword;
            certificates.Import(CertificatePath, CertificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            request.ClientCertificates = certificates;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            String json = Encoding.UTF8.GetString(ms.ToArray());

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(json);
            writer.Close();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                        response.StatusCode,
                                        response.StatusDescription));

                using (var responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string r = reader.ReadToEnd();

                        DataContractJsonSerializer sr = new DataContractJsonSerializer(typeof(TO));
                        ms = new MemoryStream(Encoding.UTF8.GetBytes(r));
                        if (ms.Length == 0) return null;

                        var rtnValue = (TO)sr.ReadObject(ms);
                        return rtnValue;
                    }
                }
            }
        }

        public static object Get<TI, TO>(string url, string CertificatePath = "", string CertificatePassword = "")
        {
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            CertificatePath = string.IsNullOrEmpty(CertificatePath) ? certificatePath : CertificatePath;
            CertificatePassword = string.IsNullOrEmpty(CertificatePassword) ? certificatePassword : CertificatePassword;
            certificates.Import(CertificatePath, CertificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            request.ClientCertificates = certificates;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                        response.StatusCode,
                                        response.StatusDescription));

                using (var responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string r = reader.ReadToEnd();

                        DataContractJsonSerializer sr = new DataContractJsonSerializer(typeof(TO));
                        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(r));
                        if (ms.Length == 0) return null;

                        var rtnValue = (TO)sr.ReadObject(ms);
                        return rtnValue;
                    }
                }
            }
        }

        public static void PostAsync<TI>(string url, TI model = default(TI), string CertificatePath = "", string CertificatePassword = "")
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            using (var sw = new System.IO.StreamWriter(request.GetRequestStream()))
            {
                string json = serializer.Serialize(model);
                sw.Write(json);
                sw.Flush();
            }
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
            }
        }
    }
}
