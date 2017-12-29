using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using xave.web.structureset.dom;
using xave.web.structureset.dto;

namespace xave.web.structureset.biz
{
    public class BusinessLayer
    {
        #region Properties
        private static DomainLayer domain;
        private static DomainLayer Domain
        {
            get
            {
                if (domain == null)
                {
                    domain = new DomainLayer();
                }
                return domain;
            }
        }
        #endregion


        #region Default Controller

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
            connectionState = Domain.GetConnectionState();
            return connectionState;
        }

        public string GetConnectionString()
        {
            string connectionString = null;
            connectionString = Domain.GetConnectionString();
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

        #endregion


        #region Database CRUD

        public object Create(object obj)
        {
            object retVal = Domain.Create(obj);
            return retVal;
        }
        public void Update(object obj)
        {
            Domain.Update(obj);
        }

        public void SaveOrUpdate(object obj)
        {
            Domain.SaveOrUpdate(obj);
        }

        public List<T> Read<T>() where T : class
        {
            List<T> obj = Domain.Read<T>();
            return obj;
        }

        public List<T> Read<T>(string documentUid) where T : class
        {
            List<T> obj = Domain.Read<T>(documentUid);
            return obj;
        }

        public List<T> Read<T>(object value, string param, int MaxNCount = 0) where T : class
        {
            List<T> obj = Domain.Read<T>(value, param, MaxNCount);
            return obj;
        }

        public List<T> Read<T>(Dictionary<string, object> param, int MaxNCount = 0) where T : class
        {
            List<T> obj = Domain.Read<T>(param, MaxNCount);
            return obj;
        }

        #endregion


        #region Custom Method

        public Containers ReadContainer()
        {
            List<Document> DocumentType = Read<Document>() as List<Document>;
            List<HeaderMap> HeaderMapType = Read<HeaderMap>() as List<HeaderMap>;
            List<HeaderPart> HeaderPartType = Read<HeaderPart>() as List<HeaderPart>;
            List<HeaderStructure> HeaderStructureType = Read<HeaderStructure>() as List<HeaderStructure>;
            List<BodyStructure> BodyStructureType = Read<BodyStructure>() as List<BodyStructure>;
            List<Section> SectionType = Read<Section>() as List<Section>;
            List<SectionMap> SectionMapType = Read<SectionMap>() as List<SectionMap>;
            List<SectionPart> SectionPartType = Read<SectionPart>() as List<SectionPart>;
            Containers obj = new Containers()
            {
                DocumentType = DocumentType.Where(t => t.UseYN == "TRUE").ToList(),
                HeaderMapType = HeaderMapType.Where(t => t.UseYN == "TRUE").ToList(),
                HeaderPartType = HeaderPartType.Where(t => t.UseYN == "TRUE").ToList(),
                HeaderStructureType = HeaderStructureType.Where(t => t.UseYN == "TRUE").ToList(),
                BodyStructureType = BodyStructureType.Where(t => t.UseYN == "TRUE").ToList(),
                SectionType = SectionType.Where(t => t.UseYN == "TRUE").ToList(),
                SectionMapType = SectionMapType.Where(t => t.UseYN == "TRUE").ToList(),
                SectionPartType = SectionPartType.Where(t => t.UseYN == "TRUE").ToList(),
            };
            return obj;
        }

        #endregion
    }
}
