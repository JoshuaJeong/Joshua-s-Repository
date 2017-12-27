using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using xave.web.code.dom;
using xave.web.code.dto;

namespace xave.web.code.biz
{
    public class BusinessLayer
    {
        private static DomainLayer instance;
        private static DomainLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DomainLayer();
                }
                return instance;
            }
        }

        public string GetConnectionInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[IP_Addr:{0}] \r\n ", GetIP()));
            sb.Append(string.Format("[DB_Status:{0}] \r\n ", GetConnectionState()));
            sb.Append(string.Format("[ConnectionString:{0}] \r\n ", GetConnectionString()));
            return sb.ToString();
        }

        public string GetConnectionState()
        {
            string connectionState = null;
            connectionState = Instance.GetConnectionState();
            return connectionState;
        }

        public string GetConnectionString()
        {
            string connectionString = null;
            connectionString = Instance.GetConnectionString();
            return connectionString;
        }

        private static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }

        public object Create(object obj)
        {
            object retVal = Instance.Create(obj);
            return retVal;
        }
        public void Update(object obj)
        {
            Instance.Update(obj);
        }

        public void SaveOrUpdate(object obj)
        {
            Instance.SaveOrUpdate(obj);
        }

        public List<T> Read<T>() where T : class
        {
            List<T> obj = Instance.Read<T>();
            return obj;
        }

        public List<T> Read<T>(string documentUid) where T : class
        {
            List<T> obj = Instance.Read<T>(documentUid);
            return obj;
        }

        public List<T> Read<T>(object value, string param, int MaxNCount = 0) where T : class
        {
            List<T> obj = Instance.Read<T>(value, param, MaxNCount);
            return obj;
        }

        public List<T> Read<T>(Dictionary<string, object> param, int MaxNCount = 0) where T : class
        {
            List<T> obj = Instance.Read<T>(param, MaxNCount);
            return obj;
        }

        public CodeContainers ReadContainer()
        {
            List<Code> CodeType = Read<Code>() as List<Code>;
            List<Format> FormatType = Read<Format>() as List<Format>;
            CodeContainers obj = new CodeContainers()
            {
                CodeType = CodeType.Where(t => t.UseYN == "TRUE").ToList(),
                FormatType = FormatType.Where(t => t.UseYN == "TRUE").ToList(),
                //Kostom = Read<KOSTOM_Diagnosis>() as List<KOSTOM_Diagnosis>,
            };
            return obj;
        }
    }
}
