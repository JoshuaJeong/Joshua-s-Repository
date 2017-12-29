using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus.Voca
{
    /// <summary>
    /// Code System List
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class CodeSystemName
    {
        public const string LOINC = "LOINC";
        public const string SNOMED_CT = "SNOMED-CT";
        public const string CVX = "CVX";
        public const string ICD_10 = "ICD-10";
        public const string RXNORM = "RxNorm";
        public const string PARTICIPATION_SIGNATURE = "Participation Signature";
        public const string ADMINISTRATIVE_GENDER = "Administrative sex";
        public const string RACE_AND_ETHNICITY = "Race and Ethnicity - CDC";
        public const string DCM = "DCM";
        public const string EDI = "EDI";
        public const string KD = "KD";

        public const string KCD = "KCD";
        public const string ATC  = "ATC";

        public const string ICD_9_CM = "ICD-9-CM";

        public static string KOSTOM = "보건의료용어표준";
    }
}
