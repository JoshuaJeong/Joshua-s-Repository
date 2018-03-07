using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus.Voca
{
    /// <summary>
    /// Entry OID List
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class EntryOID
    {
        #region :  CCDA
        //Allergies
        public const string ALLERGY_PROBLEM_ACT = "2.16.840.1.113883.10.20.22.4.30";
        public const string ALLERGY_INTOLERANCE_OBSERVATION = "2.16.840.1.113883.10.20.22.4.7";
        public const string ALLERGY_STATUS_OBSERVATION = "2.16.840.1.113883.10.20.22.4.28";
        public const string REACTION_OBSERVATION = "2.16.840.1.113883.10.20.22.4.9";
        public const string SEVERITY_OBSERVATION = "2.16.840.1.113883.10.20.22.4.8";
        //Immunizations
        public const string IMMUNIZATION_ACTIVITY = "2.16.840.1.113883.10.20.22.4.52";
        public const string IMMUNIZATION_MEDICATION_INFORMATION = "2.16.840.1.113883.10.20.22.4.54";
        //Medications
        public const string MEDICATION_ACTIVITY = "2.16.840.1.113883.10.20.22.4.16";
        public const string MEDICATION_SUPPLY_ORDER = "2.16.840.1.113883.10.20.22.4.17";
        public const string DISCHARGE_MEDICATION = "2.16.840.1.113883.10.20.22.4.35";
        public const string MEDICATION_INFORMATION = "2.16.840.1.113883.10.20.22.4.23";
        //Problems
        public const string PROBLEM_CONCERN_ACT = "2.16.840.1.113883.10.20.22.4.3";
        public const string PROBLEM_OBSERVATION = "2.16.840.1.113883.10.20.22.4.4";
        //Procedures
        public const string PROCEDURE_ACTIVITY_ACT = "2.16.840.1.113883.10.20.22.4.12";
        public const string PROCEDURE_ACTIVITY_OBSERVATION = "2.16.840.1.113883.10.20.22.4.13";
        public const string PROCEDURE_ACTIVITY_PROCEDURE = "2.16.840.1.113883.10.20.22.4.14";
        //Results
        public const string RESULT_ORGANIZER = "2.16.840.1.113883.10.20.22.4.1";
        public const string RESULT_OBSERVATION = "2.16.840.1.113883.10.20.22.4.2";
        //Smoking status
        public const string SMOKING_STATUS_OBSERVATION = "2.16.840.1.113883.10.22.4.78";
        //Vital signs
        public const string VITAL_SIGNS_ORGANIZER = "2.16.840.1.113883.10.20.22.4.26";
        public const string VITAL_SIGN_OBSERVATION = "2.16.840.1.113883.10.20.22.4.27";
        //Encounter Diagnosis
        public const string ENCOUNTER_DIAGNOSIS = "2.16.840.1.113883.10.20.22.4.80";
        public const string GOAL_OBSERVATION = "2.16.840.1.113883.10.20.22.4.121";        
        #endregion

        #region :  Version 2.1

        //public const string Anesthesia = "2.16.840.1.113883.10.20.22.2.2";
        #endregion

        //5.11
        public const string SIGNATURE_IMAGE = "2.16.840.1.113883.3.445.20";
        public const string SIGNATURE_IMAGE_OBSERVATION = "2.16.840.1.113883.3.445.19";

        public const string AUTHOR_PARTICIPATION = "2.16.840.1.113883.10.20.22.4.119";

        public const string SMOKING_STATUS = "2.16.840.1.113883.10.20.22.4.78";

        public const string SOCIAL_HISTORY_OBSERVATION = "2.16.840.1.113883.10.20.22.4.38";
        public const string PATIENT_REFERRAL_ACT = "2.16.840.1.113883.10.20.22.4.140";

        public const string SMOKING_STATUS_MEANINGFUL_USE = "2.16.840.1.113883.10.20.22.4.78";
    }
}
