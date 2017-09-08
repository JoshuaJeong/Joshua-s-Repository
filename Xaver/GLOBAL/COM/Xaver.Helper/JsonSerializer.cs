using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Xaver.Helper
{
    public static class JsonSerializer<T>
    {
        public static string Serialize(T obj)
        {
            if (obj == null) return null;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public static T Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T obj = (T)ser.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}
