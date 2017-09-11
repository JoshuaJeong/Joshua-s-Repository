using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// entry option 여부 구별
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum EntryOption
    {
        /// <summary>
        /// Required
        /// </summary>
        REQUIRED = 0,
        /// <summary>
        /// Optional
        /// </summary>
        OPTIONAL = 1
    }
}
