using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;

namespace xave.com.generator.cus
{
    /// <summary>
    /// Race Code List
    ///  codeSystemName="Race &amp; Ethnicity - CDC" codeSystem="2.16.840.1.113883.6.238" />
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class RaceCodeList
    {
        /// <summary>
        /// Race Code List
        /// </summary>
        [XmlElement("RaceCode")]
        internal RaceCode[] RaceCode { get; set; }

        internal RaceCode[] GetRaceCode() { return RaceCode; }
        internal void SetRaceCode(RaceCode[] _RaceCode) { RaceCode = _RaceCode; }
    }

    /// <summary>
    /// Race Code
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    internal class RaceCode
    {
        /// <summary>
        /// code
        /// </summary>
        [XmlAttribute("value")]
        [DataMember]
        public string value { get; set; }

        public string Getvalue() { return value; }
        public void Setvalue(string _value) { value = _value; }

        /// <summary>
        /// code 개념명
        /// </summary>
        [XmlAttribute("displayName")]
        [DataMember]
        public string displayName { get; set; }

        public string GetdisplayName() { return displayName; }
        public void SetdisplayName(string _displayName) { displayName = _displayName; }

        /// <summary>
        /// code system 명
        /// </summary>
        [XmlAttribute("codeSystemName")]
        [DataMember]
        public string codeSystemName { get; set; }

        public string GetcodeSystemName() { return codeSystemName; }
        public void SetcodeSystemName(string _codeSystemName) { codeSystemName = _codeSystemName; }

        /// <summary>
        /// codesystem OID
        /// </summary>
        [XmlAttribute("codeSystem")]
        [DataMember]
        public string codeSystem { get; set; }

        public string GetcodeSystem() { return codeSystem; }
        public void SetcodeSystem(string _codeSystem) { codeSystem = _codeSystem; }
    }

    /// <summary>
    /// LanguageCodeList
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    internal class LanguageCodeList
    {
        [XmlElement("LanguageCode")]
        internal LanguageCode[] LanguageCode { get; set; }

        public LanguageCode[] GetLanguageCode() { return LanguageCode; }
        public void SetLanguageCode(LanguageCode[] _LanguageCode) { LanguageCode = _LanguageCode; }
    }

    /// <summary>
    /// LanguageCode
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    internal class LanguageCode
    {
        /// <summary>
        /// code
        /// </summary>
        [XmlAttribute("code")]
        [DataMember]
        public string code { get; set; }

        public string Getcode() { return code; }
        public void Setcode(string _code) { code = _code; }

        /// <summary>
        /// displayName
        /// </summary>
        [XmlAttribute("displayName")]
        [DataMember]
        public string displayName { get; set; }

        public string GetdisplayName() { return displayName; }
        public void SetdisplayName(string _displayName) { displayName = _displayName; }
    }
}
