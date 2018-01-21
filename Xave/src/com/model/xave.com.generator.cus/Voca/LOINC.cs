using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus.Voca
{
    /// <summary>
    /// LOINC List
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class LOINC
    {
        //public const string Instructions = "69730-0";
        #region :: Document
        public const string REFERRAL_NOTE = "57133-1";
        public const string CONSULTATION_NOTE = "11488-4";
        public const string DIAGNOSTIC_IMAGING_REPORT = "18748-4";
        public const string CCD = "34133-9";
        public const string TRANSFER_NOTE = "18761-7";
        public const string BPPC = "57016-8";
        #endregion
        #region ::  Section
        public const string Problems = "11450-4";
        public const string Allergies = "48765-2";
        public const string Immunizations = "11369-6";
        public const string Medications = "10160-0";
        public const string Plan_of_care = "18776-5";
        public const string Procedures = "47519-4";
        public const string Reason_for_Referral = "42349-1";
        public const string Results = "30954-2";
        public const string Vital_Signs = "8716-3";
        public const string Assessment_And_Plan = " 51847-2";
        public const string Assessment = "51848-0";
        public const string Findings = "18782-3";
        public const string Encounters = "46240-8";
        public const string History_Of_Infection = "56838-6";
        public const string Reason_For_Visit = "29299-5";
        public const string Social_History = "29762-2";
        public const string Discharge_Instruction = "8653-8";
        public const string Admission_Medication = "42346-7";
        public const string Hospial_Discharge_Instruction = "8653-8";
        public const string Cheif_Complaint = "10154-3";
        public const string Signatures_Section = "57017-6";
        public const string Discharge_Medication = "10183-2";
        #endregion
        //public const string Functional_status = "47420-5";
        //public const string Chief_Complaint_And_Reason_For_Visit = "46239-0";        
        //public const string Reason_For_Hospitalization = "10154-3";                
        //public const string Medications_administered = "29549-3";
        //public const string Clinical_Instructions = "69730-0";
        //public const string Anesthesia = "59774-0";
        //public const string Complication = "55109-3";
        //public const string PostOperative_Diagnosis = "10218-6";
        //public const string PreOperative_Diagnosis = "10219-4";
        //public const string Procedure_Description = "29554-3";
        //public const string Procedure_Estimated_Blood_Loss = "59770-8";
        //public const string Procedure_Findings = "59776-5";
        //public const string Procedure_Specimens_Taken = "59773-2";
        //public const string Procedure_Indication = "59768-2";
        //public const string PostProcedure_Diagnosis = "59769-0";
        //public const string Physical_Exam = "29545-1";
        //public const string Reason_For_Visit = "29299-5";        
        public const string History_Of_Present_Illness = "10164-2";
        public const string History_Of_Past_Illness = "11348-0";
        //public const string Hospital_Course = "8648-8";
        //public const string Discharge_Diagnosis = "C-CDAV2-DDN";
        //public const string Discharge_Diagnosis = "11535-2";        
        //public const string Health_Concerns = "75310-3";
        //public const string Goals = "61146-7";
        public const string Interventions = "62387-6";
        public const string Review_Of_Systems = "10187-3";
        public const string Instructions = "69730-0";

        public const string Vital_Signs_Code = "74728-7";

        public const string Height = "8302-2";
        public const string Weight = "3141-9";
        public const string BP_Diastolic = "8462-4";
        public const string BP_Systolic = "8480-6";
        public const string BodyTemperature = "8310-5";
        public const string Tobacco_Smoking_Status = "2166-2";
        public const string Payers = "48768-6";
    }
}
