using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
//using Generator.ValueObject.Schema;

namespace Generator.ValueObject
{
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class NonXMLBodyObject
    {
        private string _MediaType;
        [DataMember]
        public string Base64String { get; set; }
        public string GetBase64String() { return Base64String; }
        public void SetBase64String(string _Base64String) { Base64String = _Base64String; }

        [DataMember]
        public string ReferenceURL { get; set; }
        public string GetReferenceURL() { return ReferenceURL; }
        public void SetReferenceURL(string _ReferenceURL) { ReferenceURL = _ReferenceURL; }

        [DataMember]
        public string MediaType
        {
            get { return _MediaType; }
            set { _MediaType = value; }
        }
        public string GetMediaType() { return MediaType; }
        public void SetMediaType(string _MediaType) { MediaType = _MediaType; }

    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    internal enum MediaType
    {
        application_msword = 0,
        application_pdf = 1,
        text_plain = 2,
        text_rtf = 3,
        text_html = 4,
        image_gif = 5,
        image_tiff = 6,
        image_jpeg = 7,
        image_pn = 8
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    internal enum ContentType
    {
        Reference = 0,
        Encoded = 1
    }
}
