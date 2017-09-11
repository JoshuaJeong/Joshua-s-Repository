using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Generator.ValueObject
{
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class LoincDisplayName
    {
        public const string Problems = "Problem List";
        public const string Allergies = "Allergies, Adverse Reactions, Alerts";
        public const string Immunizations = "Immunizations";
        public const string Instructions = "Instructions";
        public const string Medications = "History of medication use";
        public const string Plan_of_care = "Plan of care";
        public const string Procedures = "Procedures";
        public const string Reason_for_Referral = "Reason for referral";
        public const string Results = "Relevant diagnostic tests and/or laboratory data";
        public const string Vital_Signs = "Vital signs";
        public const string Encounters = "History of encounters";
        public const string Discharge_Instruction = "Hospital discharge instructions";
        public const string Functional_status = "Functional status";
        public const string Reason_for_hospitalization = "Chief complaint";
        public const string Medications_administered = "Medication administered";
        public const string Clinical_Instructions = "Instructions";
        public const string Future_Scheduled_Tests = "Futured scheduled test";
        public const string Procedure_Indication = "Procedure Indication";
        public const string Physical_Exam = "Physical Findings";
        public const string Reporting_parameter = "Reporting Parameters";
        public const string Measure_section = "Measure section";
        public const string Patient_data = "Patient Data";
        public const string Reason_For_Visit = "Reason for Visit";
        public const string Social_History = "Social History";
        public const string History_Of_Present_Illness = "History of present illness";
        public const string Hospital_Course = "Hospital Course";
        public const string Discharge_Diagnosis = "Discharge Diagnosis";
        public const string Anesthesia = "Anesthesia";
        public const string Complications = "Complications";
        public const string PostOperative_Diagnosis = "PostOperative diagnosis";
        public const string PreOperative_Diagnosis = "PreOperative diagnosis";
        public const string Procedure_Description = "Procedure description";
        public const string Chief_Complaint_And_Reason_For_Visit = "Chief complaint and reason for visit";
        public const string Assessment_And_Plan = "Assessment and plan";
        public const string Assessment = "Assessment";
        public const string Procedure_Estimated_Blood_Loss = "Procedure Estimated Blood Loss";
        public const string Procedure_Findings = "Procedure Findings";
        public const string Procedure_Specimens_Taken = "Procedure specimens taken";
        public const string PostProcedure_Diagnosis = "PostProcedure Diagnosis";
        //public const string Findings = "Findings";
        public const string Health_Concerns = "Health concerns document";
        public const string Goals = "Goals";
        public const string History_Of_Infection = "History of infectious disease";
        public const string Admission_Medication = "Admission medication";
        public const string Interventions = "Interventions";

        public const string Vital_Signs_Code = "Vital signs, weight, height, head circumference, oximetry, BMI, and BSA panel";

        public static string Height = "Height";

        public static string Weight = "Patient Body Weight - Measured";

        public static string BP_Diastolic = "BP Diastolic";

        public static string BP_Systolic = "Intravascular Systolic";

        public static string BodyTemperature = "Body Temperature";

        public static string Tobacco_Smoking_Status = "Tobacco smoking status NHIS";
    }
}
