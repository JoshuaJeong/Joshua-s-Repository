using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Generator.ValueObject
{
    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class SNOMED_CT
    {
        public const string Problem = "55607006";


        public const string Vital_Signs = "46680005";

        public const string Alcohol_Intake = "160573003";
    }

    [DataContract]
    [Serializable]
    [System.ServiceModel.XmlSerializerFormat]
    public class SNOMED_CT_DisplayName
    {
        public const string Problem = "Problem";
        public const string Vital_Signs = "Vital Signs";
        public const string Alcohol_Intake = "Alcohol Intake";
    }
}
