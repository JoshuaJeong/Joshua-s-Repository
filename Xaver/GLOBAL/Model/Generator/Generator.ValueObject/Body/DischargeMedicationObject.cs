using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Generator.ValueObject
{
    [Serializable]
    [DataContract]
    [XmlSerializerFormat]
    public class DischargeMedicationObject : MedicationObject
    {
    }
}
