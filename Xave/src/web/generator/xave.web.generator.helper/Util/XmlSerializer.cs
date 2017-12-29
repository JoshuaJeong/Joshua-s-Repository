using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace xave.web.generator.helper.Util
{
    public class XmlSerializer<T> where T : class, new()
    {
        #region :  Methods
        public static T Deserialize(string xml)
        {
            return Deserialize(xml, Encoding.UTF8, null);
        }

        public static T Deserialize(string xml, Encoding encoding)
        {
            return Deserialize(xml, encoding, null);
        }

        public static T Deserialize(string xml, XmlReaderSettings settings)
        {
            return Deserialize(xml, Encoding.UTF8, settings);
        }

        public static T Deserialize(string xml, Encoding encoding, XmlReaderSettings settings)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("XML cannot be null or empty", "xml");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(xml)))
            {
                using (XmlReader xmlReader = XmlReader.Create(memoryStream, settings))
                {
                    return (T)xmlSerializer.Deserialize(xmlReader);
                }
            }
        }


        public static string Serialize(T source)
        {
            return Serialize(source, null, GetIndentedSettings());
        }

        public static string Serialize(T source, XmlSerializerNamespaces namespaces)
        {
            return Serialize(source, namespaces, GetIndentedSettings());
        }

        public static string Serialize(T source, XmlWriterSettings settings)
        {
            return Serialize(source, null, settings);
        }

        public static string Serialize(T source, XmlSerializerNamespaces namespaces, XmlWriterSettings settings)
        {
            if (source == null)
                throw new ArgumentNullException("source", "Object to serialize cannot be null");

            string xml = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));

                    x.Serialize(xmlWriter, source, namespaces);

                    xmlWriter.Close();
                    memoryStream.Position = 0;

                    using (StreamReader sr = new StreamReader(memoryStream))
                    {
                        xml = sr.ReadToEnd();
                    }
                }
            }
            return xml;
        }
        #endregion

        #region :  Private methods
        private static XmlWriterSettings GetIndentedSettings()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "\t";
            xmlWriterSettings.Encoding = Encoding.UTF8;

            return xmlWriterSettings;
        }
        #endregion
    }
}
