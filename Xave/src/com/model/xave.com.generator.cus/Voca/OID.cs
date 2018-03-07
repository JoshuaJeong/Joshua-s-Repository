using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace xave.com.generator.cus.Voca
{
    /// <summary>
    /// OID List
    /// </summary>

    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class OID
    {
        #region : Base
        public const string CDA = "2.16.840.1.113883.1.3";
        #endregion

        #region : Code system
        // International

        public const string LOINC = "2.16.840.1.113883.6.1";
        public const string SNOMED_CT = "2.16.840.1.113883.6.96";
        public const string CVX = "2.16.840.1.113883.12.292";
        public const string RXNORM = "2.16.840.1.113883.6.88";
        public const string ICD_10 = "2.16.840.1.113883.6.3";
        public const string CPT_4 = "2.16.840.1.113883.6.12";
        public const string ATC = "2.16.840.1.113883.6.73";
        public const string DCM = "1.2.840.10008.2.16.4";
        public const string ICD9_CM_PROCEDURES = "2.16.840.1.113883.6.104";
        public const string UCUM = "2.16.840.1.113883.6.8";
        public const string FIPS_5_2 = "2.16.840.1.113883.6.92";

        //KR
        public const string CODE_SYSTEM_KR = HIE_CONTENTS_KR + ".3";
        public const string KOSTOM = CODE_SYSTEM_KR + ".1";
        public const string KCD = CODE_SYSTEM_KR + ".2";
        public const string EDI = CODE_SYSTEM_KR + ".3";
        public const string KD = CODE_SYSTEM_KR + ".4";
        
        #endregion

        #region : Value set
        public const string GENERAL_HEADER_TEMPLATE = "2.16.840.1.113883.10.20.22.1.1";
        public const string CONFIDENTIALITY_CODE = "2.16.840.1.113883.5.25";
        public const string ADMINISTRATIVE_GENDER = "2.16.840.1.113883.5.1";
        public const string ISO_3166_1_COUNTRY_CODES = "1.0.3166.1";
        public const string INTERNET_SOCIETY_LANGUAGE = "2.16.840.1.113883.1.11.11526";
        public const string ACT_MOOD = "2.16.840.1.113883.5.1001";
        public const string RELIGIOUS_AFFILIATION = "2.16.840.1.113883.5.1076";
        public const string ROLE_CLASS = "2.16.840.1.113883.5.110";
        public const string ROLE_CODE = "2.16.840.1.113883.5.111";
        public const string ADDRESS_USE = "2.16.840.1.113883.5.1119";
        public const string ACT_STATUS = "2.16.840.1.113883.5.14";
        public const string MARITAL_STATUS = "2.16.840.1.113883.5.2";
        public const string NUCC_HEALTH_CARE_PROVIDER_TAXONOMY = "2.16.840.1.113883.6.101";
        public const string NATIONAL_CANCER_INSTITUTE_THESAURUS = "2.16.840.1.113883.3.26.1.1";
        public const string US_POSTAL_CODES = "2.16.840.1.113883.6.231";
        public const string RACE_AND_ETHNICITY = "2.16.840.1.113883.6.238";
        public const string HEALTHCARE_SERVICE_LOCATION = "2.16.840.1.113883.6.259";
        public const string ACT_CODE = "2.16.840.1.113883.5.4";
        public const string ENTITY_NAME_PART_QUALIFIER = "2.16.840.1.113883.5.43";
        public const string ENTITY_NAME_USE = "2.16.840.1.113883.5.45";
        public const string ASC_X12 = "2.16.840.1.113883.6.255.1336";
        public const string HL7_ACT_CLASS = "2.16.840.1.113883.5.6";
        public const string LANGUAGE_ABILITY_MODE = "2.16.840.1.113883.5.60";
        public const string LANGUAGE_ABILITY_PROFICIENCY = "2.16.840.1.113883.5.61";
        public const string NDF_RT = "2.16.840.1.113883.3.26.1.5";
        public const string ACT_PRIORITY = "2.16.840.1.113883.5.7";
        public const string UNIQUE_INGREDIENT_IDENTIFIER = "2.16.840.1.113883.4.9";
        public const string ACTREASON = "2.16.840.1.113883.5.8";
        public const string OBSERVATION_INTERPRETATION = "2.16.840.1.113883.5.83";
        public const string PARTICIPATION_FUNCTION = "2.16.840.1.113883.5.88";
        public const string PARTICIPATION_SIGNATURE = "2.16.840.1.113883.5.89"; 
        #endregion

        #region :  Document Template
        /// International
        //HL7
        public const string US_REALM_HEADER_TEMPLATE = "2.16.840.1.113883.10.20.22.1.1";
        public const string CCD = "2.16.840.1.113883.10.20.22.1.2";
        public const string REFERRAL_NOTE = "2.16.840.1.113883.10.20.22.1.14";
        public const string CONSULTATION_NOTE = "2.16.840.1.113883.10.20.22.1.4";
        public const string DIAGNOSTIC_IMAGING_REPORT = "2.16.840.1.113883.10.20.6";
        public const string TRANSFER_NOTE = "2.16.840.1.113883.10.20.22.1.13";
        public const string PRIVACY_CONSENT_DIRECTIVE = "2.16.840.1.113883.3.445.1.1";
        //IHE
        public const string MEDICAL_DOCUMENT = "1.3.6.1.4.1.19376.1.5.3.1.1.1";
        public const string BPPC_NO_SCANNED = "1.3.6.1.4.1.19376.1.5.3.1.1.7";
        public const string BPPC_SCANNED = "1.3.6.1.4.1.19376.1.5.3.1.1.7.1";
        public const string BPPC_SERVICEEVENT = "1.3.6.1.4.1.19376.1.5.3.1.2.6";        
        // Others
        public const string HITSP_C32 = "2.16.840.1.113883.3.88.11.32.1";        

        /// <summary>
        /// 보건복지부
        /// </summary>
        public const string MINISTRY_OF_HEALTH_AND_WELFARE = "1.2.410.100110";
        /// <summary>
        /// 진료정보교류
        /// </summary>
        public const string HIE_KR = "1.2.410.100110.40";
        public const string HIE_CONTENTS_KR = "1.2.410.100110.40.2";
        public const string REFERRAL_NOTE_KR = "1.2.410.100110.40.2.1.1";
        public const string TRANSFER_NOTE_KR = "1.2.410.100110.40.2.1.2";
        public const string CONSULTATION_NOTE_KR = "1.2.410.100110.40.2.1.3";
        public const string CRS_KR = "1.2.410.100110.40.2.1.4";
        public const string DIAGNOSTIC_IMAGING_REPORT_KR = "1.2.410.100110.40.2.1.5";
        public const string EMS_KR = "1.2.410.100110.40.2.1.6"; //전원소견서
        public const string BPPC_CONSENT = "1.2.410.100110.40.2.2.1.1";
        public const string BPPC_WITHDRAWAL = "1.2.410.100110.40.2.2.1.2";
                    
        #endregion

        #region :  Signature
        #endregion

        #region :  etc
        public const string US_SSN = "2.16.840.1.113883.4.1";
        
        /// <summary>
        /// 개인정보 보호정책 동의시스템(복지부)
        /// </summary>
        public const string PRIVACY_POLICY_SYSTEM = "1.2.410.100110.40.2.2";
        //public const string OPT_IN = "1.2.410.100110.40.2.2.1";
        //public const string OPT_OUT = "1.2.410.100110.40.2.2.2";
        public const string ENTIRE_CONSENT = "1.2.410.100110.40.2.2.2.2";
        public const string PARTIAL_CONSENT = "1.2.410.100110.40.2.2.2.3";
        public const string ENTIRE_WITHDRAWAL = "1.2.410.100110.40.2.2.2.4";
        public const string PARTIAL_WITHDRAWAL = "1.2.410.100110.40.2.2.2.5";
        #endregion        
    
        public const string ICD_9_CM  = "2.16.840.1.113883.6.42";
    }
}
