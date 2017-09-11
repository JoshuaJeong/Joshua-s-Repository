using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{    
    internal class DicomObject
    {
        internal StudyObject[] Study { get; set; }
    }
 
    internal class StudyObject
    {
        /// <summary>
        /// Study Instance UID (0020, 000D)
        /// </summary>
        [DataMember]
        internal string InstanceUID { get; set; }
        /// <summary>
        /// Study Description (0008, 1030)
        /// </summary>
        [DataMember]
        internal string StudyDescription { get; set; }
        /// <summary>
        /// Study Date (0008, 0020)
        /// </summary>
        [DataMember]
        internal string StudyDate { get; set; }
        /// <summary>
        /// Study Time (0008, 0030)
        /// </summary>
        [DataMember]
        internal string StudyTime { get; set; }
        /// <summary>
        /// Series
        /// </summary>
        [DataMember]
        internal SeriesObject[] Series { get; set; }
    }
    
    internal class SeriesObject
    {
        /// <summary>
        /// Series Instance UID (0020, 000E)
        /// </summary>        
        internal string InstanceUID { get; set; }
        /// <summary>
        /// Series Date (0008,0021)
        /// </summary>        
        internal string SeriesDate { get; set; }
        /// <summary>
        /// Series Time (0008,0031)
        /// </summary>        
        internal string SeriesTime { get; set; }
        /// <summary>
        /// Series Description (0008, 103E)
        /// </summary>        
        internal string SeriesDescription { get; set; }
        /// <summary>
        /// Sop Instance
        /// </summary>        
        internal SopInstanceObject[] SopInstance { get; set; }
    }

    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    internal class SopInstanceObject
    {
        /// <summary>
        /// SopInstance Instance UID (0008,0018)
        /// </summary>        
        internal string InstanceUID { get; set; }
        /// <summary>
        /// SOP Class UID Code (0008, 0016)
        /// </summary>        
        internal string UIDCode { get; set; }        
        internal string DerivedName { get; set; }        
        internal string WadoReference { get; set; }
        /// <summary>
        /// Content Date (0008, 0023)
        /// </summary>        
        internal string ContentDate { get; set; }
        /// <summary>
        /// Content Time (0008, 0033)
        /// </summary>        
        internal string ContentTime { get; set; }
    }
}
