using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace xave.com.generator.cus
{
    [Serializable]
    [DataContract]
    [XmlSerializerFormat]
    public class DischargeMedicationObject : MedicationObject
    {
    }
}
