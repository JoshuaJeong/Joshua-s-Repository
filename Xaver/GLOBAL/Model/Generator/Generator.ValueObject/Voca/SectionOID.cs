using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// Section OID
    /// </summary>

    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class SectionOID
    {
        public const string Problems = "2.16.840.1.113883.10.20.22.2.5";
        public const string Problems_Entry = "2.16.840.1.113883.10.20.22.2.5.1";
        public const string Reason_For_Visit = "2.16.840.1.113883.10.20.22.2.12";
        public const string Allergies = "2.16.840.1.113883.10.20.22.2.6.1";
        public const string Allergies_NoEntry = "2.16.840.1.113883.10.20.22.2.6";
        //public const string Functional_Status = "2.16.840.1.113883.10.20.22.2.14";
        public const string Immunizations_Entry = "2.16.840.1.113883.10.20.22.2.2.1";
        public const string Immunizations = "2.16.840.1.113883.10.20.22.2.2";
        public const string Instructions = "2.16.840.1.113883.10.20.22.2.45";
        public const string Medications = "2.16.840.1.113883.10.20.22.2.1.1";
        public const string Admission_Medications = "2.16.840.1.113883.10.20.22.4.36";
        public const string Plan_Of_Care = "2.16.840.1.113883.10.20.22.2.10";
        public const string Procedures = "2.16.840.1.113883.10.20.22.2.7.1";
        public const string Reason_For_Referral = "1.3.6.1.4.1.19376.1.5.3.1.3.1";
        public const string Results = "2.16.840.1.113883.10.20.22.2.3.1";
        public const string Social_History = "2.16.840.1.113883.10.20.22.2.17";
        public const string Vital_Signs = "2.16.840.1.113883.10.20.22.2.4";
        public const string Vital_Signs_Entry = "2.16.840.1.113883.10.20.22.2.4.1";
        //public const string Encounters = "2.16.840.1.113883.10.20.22.2.22.1";
        public const string Encounters = "2.16.840.1.113883.10.20.22.2.22";

        public const string History_Of_Infection = "1.3.6.1.4.1.19376.1.5.3.1.1.16.2.1.1";
        public const string Cheif_Complaint = "1.3.6.1.4.1.19376.1.5.3.1.1.13.2.1";
        //public const string Reason_For_Visit = "2.16.840.1.113883.10.20.22.2.12";
        //public const string Discharge_Instruction = "2.16.840.1.113883.10.20.22.2.41";        
        //public const string Reason_For_Hospitalization = "1.3.6.1.4.1.19376.1.5.3.1.1.13.2.1";        
        //public const string Clinical_Instructions = "2.16.840.1.113883.10.20.22.2.45";                
        //public const string Chief_Complaint_and_Reason_for_Visit = "2.16.840.1.113883.10.20.22.2.13";
        //public const string Anesthesia = "2.16.840.1.113883.10.20.22.2.25";
        //public const string Complication = "2.16.840.1.113883.10.20.22.2.37";
        //public const string PreOperative_Diagnosis = "2.16.840.1.113883.10.20.22.2.34";
        //public const string PreOperative_Diagnosis = "2.16.840.1.113883.10.20.22.2.34";
        //public const string PostOperative_Diagnosis = "2.16.840.1.113883.10.20.22.2.35";
        public const string Procedure_Description = "2.16.840.1.113883.10.20.22.2.27";
        public const string Procedure_Estimated_Blood_Loss = "2.16.840.1.113883.10.20.18.2.9";
        public const string Procedure_Findings = "2.16.840.1.113883.10.20.22.2.28";
        public const string Procedure_Specimens_Taken = "2.16.840.1.113883.10.20.22.2.31";
        public const string Procedure_Indication = "2.16.840.1.113883.10.20.22.2.29";
        //public const string PostProcedure_Diagnosis = "2.16.840.1.113883.10.20.22.2.36";
        //public const string Assessment_And_Plan = "2.16.840.1.113883.10.20.22.2.9";
        public const string Assessment = "2.16.840.1.113883.10.20.22.2.8";
        //public const string PhysicalExam = "2.16.840.1.113883.10.20.2.10";
        //public const string ReasonForVisit = "2.16.840.1.113883.10.20.22.2.12";
        public const string History_Of_Present_Illness = "1.3.6.1.4.1.19376.1.5.3.1.3.4";
        //public const string Hospital_Course = "1.3.6.1.4.1.19376.1.5.3.1.3.5";
        //public const string Discharge_Diagnosis = "2.16.840.1.113883.10.20.22.2.24";
        public const string Dicom_Object_Catalog = "2.16.840.1.113883.10.20.6.1.1";
        //public const string Health_Concerns = "2.16.840.1.113883.10.20.22.2.58";
        //public const string Goals = "2.16.840.1.113883.10.20.22.2.60";
        public const string Interventions = "2.16.840.1.113883.10.20.21.2.3";
        //16.5.11
        public const string SIGNATURES = "2.16.840.1.113883.3.445.18";
        public const string PRIVACY_CONSENT_DETAILS = "2.16.840.1.113883.3.445.17";

        public const string Findings = "2.16.840.1.113883.10.20.6.1.2";
    }
}

