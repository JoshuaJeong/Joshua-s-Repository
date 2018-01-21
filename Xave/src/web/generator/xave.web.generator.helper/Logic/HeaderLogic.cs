using System;
using System.Collections.Generic;
using System.Linq;
using xave.com.generator.cus;
using xave.com.generator.cus.StructureSetModel;
using xave.com.generator.cus.Voca;
using xave.web.generator.helper.Util;

namespace xave.web.generator.helper.Logic
{
    /// <summary>
    /// CDA Header Logic Class
    /// </summary>
    [Serializable]
    internal class HeaderLogic : DataTypeLogic
    {
        #region :  Members
        private CDAObject _CDAObject;
        #endregion

        #region :  Properties
        #endregion

        #region :  Constructors
        internal HeaderLogic()
        {
        }
        internal HeaderLogic(CDAObject cDAObject)
        {
            this._CDAObject = cDAObject;
        }
        #endregion

        #region :  Methods

        #region ::  Participants

        /// <summary>
        /// Author 생성
        /// </summary>
        /// <param name="item">CustodianObject</param>
        /// <returns>POCD_MT000040Author[]</returns>
        internal POCD_MT000040Author[] CreateAuthorElement(CDAObject item)
        {
            if (item != null && item.Author != null)
            {
                List<POCD_MT000040Author> returnObjs = new List<POCD_MT000040Author>();
                //POCD_MT000040Person person = CreatePersonElement(item.DoctorName);
                POCD_MT000040Person person = CreatePersonElement(item.Author.AuthorName);
                POCD_MT000040AssignedAuthor assignedAuthor = new POCD_MT000040AssignedAuthor();
                POCD_MT000040Author returnobj = new POCD_MT000040Author();
                returnobj.time = GetTS(DateTime.Now.ToString("yyyyMMddHHmmsszzzz").Replace(":", ""));

                //assignedAuthor.id = GetIIArray(string.IsNullOrEmpty(item.Author.MedicalLicenseID) ? Guid.NewGuid().ToString() : item.Author.MedicalLicenseID, item.Custodian.OID);
                assignedAuthor.id = !string.IsNullOrEmpty(item.Author.MedicalLicenseID) ? new II[] { GetII(item.Author.MedicalLicenseID, item.Custodian.OID) } : null;
                assignedAuthor.telecom = !string.IsNullOrEmpty(item.Author.TelecomNumber) ? new TEL[] { GetTEL(item.Author.TelecomNumber) } : null;
                if (!string.IsNullOrEmpty(item.Author.DepartmentName)) //진료과 , 진료과 코드
                {
                    assignedAuthor.representedOrganization = new POCD_MT000040Organization();
                    assignedAuthor.representedOrganization.id = string.IsNullOrEmpty(item.Custodian.OID) && string.IsNullOrEmpty(item.Author.DepartmentCode) ?
                        null : new II[] { new II() { root = item.Custodian.OID, extension = item.Author.DepartmentCode } };
                    assignedAuthor.representedOrganization.name = GetONArray(new List<string>() { item.Author.DepartmentName });
                    assignedAuthor.representedOrganization.asOrganizationPartOf = new POCD_MT000040OrganizationPartOf();
                    assignedAuthor.representedOrganization.asOrganizationPartOf.id = new II[] { new II() { extension = item.Author.DepartmentCode, root = item.Custodian.OID } };
                    assignedAuthor.representedOrganization.asOrganizationPartOf.wholeOrganization = new POCD_MT000040Organization() { };
                    assignedAuthor.representedOrganization.asOrganizationPartOf.wholeOrganization.id = GetIIArray(item.Custodian.Id, item.Custodian.OID);
                    assignedAuthor.representedOrganization.asOrganizationPartOf.wholeOrganization.name = GetONArray(new List<string>() { item.Custodian.CustodianName });
                    assignedAuthor.representedOrganization.asOrganizationPartOf.wholeOrganization.addr = GetADArray(null, null, null, item.Custodian.AdditionalLocator, item.Custodian.StreetAddress, item.Custodian.PostalCode);
                }

                assignedAuthor.addr = GetADArray(null, null, null, item.Custodian.AdditionalLocator, item.Custodian.StreetAddress, item.Custodian.PostalCode);
                assignedAuthor.Item = person;

                returnobj.assignedAuthor = assignedAuthor;
                returnObjs.Add(returnobj);
                return returnObjs.ToArray();
            }
            else
            {
                return new POCD_MT000040Author[] 
                {
                    new POCD_MT000040Author() 
                    {
                        nullFlavor = "NI", 
                        time = new TS(){ nullFlavor = "NI" }, 
                        assignedAuthor = new POCD_MT000040AssignedAuthor()
                        {
                            nullFlavor = "NI",
                            id = new II[]{ new II(){ nullFlavor = "NI" } },
                            Item = new POCD_MT000040Person(){ nullFlavor = "NI" } 
                        }
                    } 
                };
            }
        }

        /// <summary>
        /// RecordTarget 생성
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal POCD_MT000040RecordTarget[] CreateRecordTargetElement(CDAObject item, HeaderPart headerPart = null)
        {
            // RecordTarget Element 생성
            if (item != null && item.Patient != null)
            {
                List<POCD_MT000040RecordTarget> returnObjs = new List<POCD_MT000040RecordTarget>();
                POCD_MT000040RecordTarget returnObj = new POCD_MT000040RecordTarget();
                POCD_MT000040PatientRole patientRole = new POCD_MT000040PatientRole();
                patientRole.patient = new POCD_MT000040Patient();
                patientRole.id = new II[] { GetII(item.Patient.LocalId, item.Patient.OID) };
                //patientRole.addr = GetADArray(item.Patient.Country, item.Patient.City, item.Patient.State, item.Patient.AdditionalLocator, item.Patient.StreetAddress, item.Patient.PostalCode);
                patientRole.addr = GetADArray(null, null, null, item.Patient.AdditionalLocator, item.Patient.StreetAddress, item.Patient.PostalCode);
                patientRole.telecom = new TEL[] { GetTEL(item.Patient.TelecomNumber) };

                patientRole.patient.name = new PN[] { GetPN(item.Patient.PatientName) };
                string codeSystemName = CommonQuery.GetHeaderValue("recordTarget/patientRole/patient/administrativeGenderCode/@codeSystemName", headerPart);
                string codeSystem = CommonQuery.GetHeaderValue("recordTarget/patientRole/patient/administrativeGenderCode/@codeSystem", headerPart);

                patientRole.patient.administrativeGenderCode = GetCE(item.Patient.Gender.GetDescription(), item.Patient.Gender.ToString(), codeSystemName, codeSystem);
                //switch (item.Patient.Gender)
                //{
                //    case GenderType.Male:
                //        patientRole.patient.administrativeGenderCode = GetCE("M", "Male", codeSystemName, codeSystem);
                //        break;
                //    case GenderType.Female:
                //        patientRole.patient.administrativeGenderCode = GetCE("F", "Female", codeSystemName, codeSystem);
                //        break;
                //    case GenderType.Undifferentiated:
                //        patientRole.patient.administrativeGenderCode = GetCE("UNK", "Unknown", codeSystemName, codeSystem);
                //        break;
                //    default:
                //        break;
                //}
                patientRole.patient.birthTime = GetTS(item.Patient.DateOfBirth);

                #region :  Race & Ethnicity
                //Race Code Deserialize
                //RaceCodeList raceCode = new RaceCodeList();
                //raceCode = HIE.CCDA.LIB.Util.XmlSerializer<RaceCodeList>.Deserialize(Properties.Resources.RaceCode);

                //Language Code Deserialize
                //LanguageCodeList languageCodeList = new LanguageCodeList();
                //languageCodeList = HIE.CCDA.LIB.Util.XmlSerializer<LanguageCodeList>.Deserialize(Properties.Resources.Language);

                //string raceCodeValue = raceCode.RaceCode.Where(w => w.displayName.Trim().ToUpper() == "Asian".ToUpper()).Select(s => s.value).FirstOrDefault();

                ////2135-2	Hispanic or Latino
                ////2186-5	Not Hispanic or Latino
                //string ethnicityCodeValue = item.Ethnicity.Trim().ToUpper() == "Hispanic or Latino".ToUpper() ? "2135-2" : "2186-5";
                //patientRole.patient.raceCode = GetCE("2028-9", "Asian", CodeSystemName.RACE_AND_ETHNICITY, OID.RACE_AND_ETHNICITY);

                //patientRole.patient.ethnicGroupCode =
                //    GetCE
                //    ("2186-5",
                //    "Not Hispanic or Latino",
                //    CodeSystemName.RACE_AND_ETHNICITY,
                //    OID.RACE_AND_ETHNICITY);
                #endregion
                #region :  languageCommunication
                //languageCommunication
                //if (item.PreferredLanguage != null && item.PreferredLanguage.Count() > 0)
                //{
                //    List<POCD_MT000040LanguageCommunication> preferredLanguageList = new List<POCD_MT000040LanguageCommunication>();
                //    foreach (string code in item.PreferredLanguage)
                //    {
                //        POCD_MT000040LanguageCommunication preferredLanguage = new POCD_MT000040LanguageCommunication() { };
                //        //languageCommunication languageCode SHALL be selected from ValueSet LanguageCode STATIC where the @code SHALL be selected from the set of alpha-3 codes of ISO 639-2 that are in ISO 639-1
                //        //입력 Language Code 명칭과 리소스의 Code 명칭을 비교하여 두 값이 일치하는 code값을 설정함
                //        LanguageCode lang = languageCodeList.LanguageCode.Where(w => w.displayName.Trim().ToUpper() == code.Trim().ToUpper()).FirstOrDefault();
                //        preferredLanguage.languageCode = GetCS(lang == null ? "eng" : lang.code, null, null, null);
                //        preferredLanguageList.Add(preferredLanguage);
                //    }
                //    patientRole.patient.languageCommunication = preferredLanguageList.ToArray();
                //}
                #endregion
                #region :  Guardian
                if (item.Guardian != null)
                {
                    POCD_MT000040Guardian guardian = new POCD_MT000040Guardian();
                    guardian.id = GetIIArray();
                    guardian.code = GetCE(item.Guardian.GType.GetDescription(), item.Guardian.GType.ToString(), null, "2.16.840.1.113883.5.111");
                    guardian.addr = GetADArray(null, null, null, item.Guardian.AdditionalLocator, item.Guardian.StreetAddress, item.Guardian.PostalCode);
                    guardian.telecom = GetTELArray(new List<string>() { item.Guardian.TelecomNumber });
                    guardian.Item = CreatePersonElement(item.Guardian.GuardianName);
                    patientRole.patient.guardian = new POCD_MT000040Guardian[] { guardian };
                }
                #endregion

                returnObj.patientRole = patientRole;
                returnObjs.Add(returnObj);
                return returnObjs.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// custodian 생성
        /// </summary>
        /// <param name="item">CustodianObject</param>
        /// <returns>POCD_MT000040Custodian</returns>
        internal POCD_MT000040Custodian CreateCustodianElement(CustodianObject item)
        {
            if (item != null)
            {
                POCD_MT000040CustodianOrganization custodianOrganization = new POCD_MT000040CustodianOrganization();
                custodianOrganization.id = GetIIArray(item.Id, item.OID);
                custodianOrganization.name = GetON(item.CustodianName);
                custodianOrganization.telecom = GetTEL(item.TelecomNumber);
                //custodianOrganization.addr = GetAD(item.Country, item.City, item.State, item.AdditionalLocator, item.StreetAddress, item.PostalCode);
                custodianOrganization.addr = GetAD(string.Empty, string.Empty, string.Empty, item.AdditionalLocator, item.StreetAddress, item.PostalCode);

                POCD_MT000040Custodian returnObj = new POCD_MT000040Custodian();
                returnObj.assignedCustodian = new POCD_MT000040AssignedCustodian();
                returnObj.assignedCustodian.representedCustodianOrganization = custodianOrganization;

                return returnObj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// informationRecipient 생성
        /// </summary>
        /// <param name="item">InformationRecipientObject</param>
        /// <returns>POCD_MT000040InformationRecipient[]</returns>        
        internal POCD_MT000040InformationRecipient[] CreateInformationRecipientElement(InformationRecipientObject[] items)
        {
            if (items != null && items.Any())
            {
                List<POCD_MT000040InformationRecipient> returnObjs = new List<POCD_MT000040InformationRecipient>();
                foreach (var item in items)
                {
                    POCD_MT000040IntendedRecipient intendedRecipient = new POCD_MT000040IntendedRecipient();
                    intendedRecipient.id = GetIIArray(item.MedicalLicenseID, item.OID);
                    //intendedRecipient.addr = GetADArray(item.Country, item.City, item.State, item.AdditionalLocator, item.StreetAddress, item.PostalCode);
                    //intendedRecipient.addr = GetADArray(null, null, null, item.AdditionalLocator, item.StreetAddress, item.PostalCode);
                    intendedRecipient.telecom = GetTELArray(new List<string>() { item.TelecomNumber });
                    intendedRecipient.informationRecipient = CreatePersonElement(item.DoctorName);
                    intendedRecipient.receivedOrganization = new POCD_MT000040Organization();
                    //진료과명
                    intendedRecipient.receivedOrganization.name = !string.IsNullOrEmpty(item.DepartmentName) ? GetONArray(new List<string>() { item.DepartmentName }) : null;
                    intendedRecipient.receivedOrganization.asOrganizationPartOf = new POCD_MT000040OrganizationPartOf();
                    // 진료과 코드
                    intendedRecipient.receivedOrganization.id = !string.IsNullOrEmpty(item.DepartmentCode) ? new II[] { new II() { root = item.OID, extension = item.DepartmentCode } } : null;
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.id = !string.IsNullOrEmpty(item.DepartmentCode) ? new II[] { new II() { root = item.OID, extension = item.DepartmentCode } } : null;
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.wholeOrganization = new POCD_MT000040Organization();
                    // 수신기관명
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.wholeOrganization.id = GetIIArray(item.Id, item.OID);
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.wholeOrganization.name = !string.IsNullOrEmpty(item.OrganizationName) ? GetONArray(new List<string>() { item.OrganizationName }) : null;
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.wholeOrganization.telecom = GetTELArray(new List<string>() { item.TelecomNumber });
                    intendedRecipient.receivedOrganization.asOrganizationPartOf.wholeOrganization.addr = GetADArray(null, null, null, item.AdditionalLocator, item.StreetAddress, item.PostalCode);
                    POCD_MT000040InformationRecipient returnObj = new POCD_MT000040InformationRecipient();
                    returnObj.intendedRecipient = intendedRecipient;
                    returnObjs.Add(returnObj);
                }
                return returnObjs.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// legalAuthenticator 항목 생성
        /// </summary>
        /// <param name="item">LegalAuthenticatorObject</param>
        /// <returns>POCD_MT000040LegalAuthenticator</returns>
        internal POCD_MT000040LegalAuthenticator CreateLegalAuthenticator(LegalAuthenticatorObject item)
        {

            POCD_MT000040LegalAuthenticator returnobj = new POCD_MT000040LegalAuthenticator();

            returnobj.time = GetTS(item.Time);
            returnobj.signatureCode = GetCS("S", null, null, null);
            returnobj.assignedEntity = new POCD_MT000040AssignedEntity();
            returnobj.assignedEntity.id = GetIIArray();
            returnobj.assignedEntity.addr = GetADArray(null, null, null, item.AdditionalLocator, item.StreetAddressLine, item.PostalCode);
            returnobj.assignedEntity.telecom = GetTELArray(new List<string>() { item.Telecom });
            returnobj.assignedEntity.assignedPerson = new POCD_MT000040Person();
            returnobj.assignedEntity.assignedPerson.name = GetPNArray(new List<string>() { item.PersonName });
            return returnobj;
        }

        /// <summary>
        /// ClinicalDocument/authenticator
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal POCD_MT000040Authenticator[] CreateAuthenticator(CDAObject item)
        {
            if (item.Authenticator != null)
            {
                POCD_MT000040Authenticator returnobj = new POCD_MT000040Authenticator();

                returnobj.time = GetTS(DateTime.Now.ToString("yyyyMMddHHmmsszzzz").Replace(":", ""));
                returnobj.signatureCode = new CS() { code = "S" }; //추가
                returnobj.assignedEntity = new POCD_MT000040AssignedEntity();
                returnobj.assignedEntity.id = GetIIArray(item.Authenticator.Id, item.Custodian.OID);
                returnobj.assignedEntity.telecom = GetTELArray(new List<string>() { item.Authenticator.TelecomNumber });
                returnobj.assignedEntity.assignedPerson = new POCD_MT000040Person();
                returnobj.assignedEntity.assignedPerson.name = GetPNArray(new List<string>() { item.Authenticator.AuthenticatorName });
                return new POCD_MT000040Authenticator[] { returnobj };
            }
            else return null;
        }


        #endregion

        #region ::  Related Acts
        internal POCD_MT000040DocumentationOf[] CreateServiceEventElement(CDAObject item, string documentType)
        {
            var returnobj = new List<POCD_MT000040DocumentationOf>();
            POCD_MT000040DocumentationOf obj = new POCD_MT000040DocumentationOf() { typeCode = "DOC" };
            obj.serviceEvent = new POCD_MT000040ServiceEvent() { classCode = "ACT", moodCode = "EVN" };
            obj.serviceEvent.templateId = new II[] { new II() { root = OID.BPPC_SERVICEEVENT } };
            obj.serviceEvent.id = GetIIArray();

            string time = null;
            PrivacyPolicyType policy = PrivacyPolicyType.NONE;
            string policyName = null;

            switch (documentType)
            {
                case OID.BPPC_CONSENT:
                    time = item.Consent != null ? item.Consent.ConsentTime : null;
                    policy = item.Consent != null ? item.Consent.PolicyType : PrivacyPolicyType.NONE;
                    policyName = policy.GetDescription();
                    break;
                case OID.BPPC_WITHDRAWAL:
                    time = item.Withdrawal != null ? item.Withdrawal.WithdrawalDate : null;
                    policy = item.Withdrawal != null ? item.Withdrawal.PolicyType : PrivacyPolicyType.NONE;
                    policyName = policy.GetDescription();
                    break;
                default:
                    break;
            }

            obj.serviceEvent.effectiveTime = GetIVL_TS(time, null);

            switch (policy)
            {
                case PrivacyPolicyType.ENTIRE_CONSENT:
                    obj.serviceEvent.code = GetCE(OID.ENTIRE_CONSENT, policyName, null, OID.PRIVACY_POLICY_SYSTEM);
                    break;
                case PrivacyPolicyType.PARTIAL_CONSENT:
                    obj.serviceEvent.code = GetCE(OID.PARTIAL_CONSENT, policyName, null, OID.PRIVACY_POLICY_SYSTEM);
                    break;
                case PrivacyPolicyType.ENTIRE_WITHDRAWAL:
                    obj.serviceEvent.code = GetCE(OID.ENTIRE_WITHDRAWAL, policyName, null, OID.PRIVACY_POLICY_SYSTEM);
                    break;
                case PrivacyPolicyType.PARTIAL_WITHDRAWAL:
                    obj.serviceEvent.code = GetCE(OID.PARTIAL_WITHDRAWAL, policyName, null, OID.PRIVACY_POLICY_SYSTEM);
                    break;
                case PrivacyPolicyType.NONE:
                    obj.serviceEvent.code = GetCE(null, policyName, null, OID.PRIVACY_POLICY_SYSTEM);
                    break;
                default:
                    break;
            }
            returnobj.Add(obj);
            return returnobj.ToArray();

        }

        //internal POCD_MT000040Component1 CreateEncompassingEncounter(CustodianObject item, string type)
        //{
        //    POCD_MT000040Component1 returnObj = new POCD_MT000040Component1();
        //    POCD_MT000040EncompassingEncounter encounter = new POCD_MT000040EncompassingEncounter();
        //    switch (item.Patient.CareType)
        //    { }
        //    encounter.id = GetIIArray();
        //    //encounter.effectiveTime = GetIVL_TS(item.AdmissionDate, item.DischargeDate);
        //    //example <code codeSystem="2.16.840.1.113883.6.12" codeSystemName="CPT-4" code="99213" displayName="Evaluation and Management"/>
        //    switch (type)
        //    {
        //        case "34133-9":
        //            encounter.code = GetCE("AMP", null, null, OID.ACT_CODE);
        //            break;
        //        //case "34133-9":
        //        //    encounter.code = GetCE("IMP", null, null, OID.ACT_CODE);
        //        //    break;
        //        default:
        //            break;
        //    }

        //    returnObj.encompassingEncounter = encounter;
        //    return returnObj;
        //}

        internal POCD_MT000040Component1 CreateEncompassingEncounter(CareTypes type)
        {
            POCD_MT000040Component1 returnObj = new POCD_MT000040Component1();
            POCD_MT000040EncompassingEncounter encounter = new POCD_MT000040EncompassingEncounter() { effectiveTime = new IVL_TS() { nullFlavor = "UNK" } };

            switch (type)
            {
                case CareTypes.AMBULATORY:
                    encounter.code = GetCE("AMB", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.INPATIENT:
                    encounter.code = GetCE("IMP", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.EMERGENCY:
                    encounter.code = GetCE("EMER", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.NONE:
                default:
                    return null;
            }
            returnObj.encompassingEncounter = encounter;
            return returnObj;
        }

        internal POCD_MT000040Component1 CreateEncompassingEncounter(CDAObject obj)
        {
            POCD_MT000040Component1 returnObj = new POCD_MT000040Component1();
            POCD_MT000040EncompassingEncounter encounter = new POCD_MT000040EncompassingEncounter() { effectiveTime = new IVL_TS() { nullFlavor = "UNK" } };

            switch (obj.Patient.CareType)
            {
                case CareTypes.AMBULATORY:
                    encounter.code = GetCE("AMB", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.INPATIENT:
                    encounter.code = GetCE("IMP", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.EMERGENCY:
                    encounter.code = GetCE("EMER", null, null, OID.ACT_CODE);
                    break;
                case CareTypes.NONE:
                default:
                    encounter.code = null;
                    break;
            }

            //if (!string.IsNullOrEmpty(obj.Patient.EncounterDepartmentName) || !string.IsNullOrEmpty(obj.Patient.EncounterDepartmentCode))
            //{
            //    encounter.location = new POCD_MT000040Location();
            //    encounter.location.healthCareFacility = new POCD_MT000040HealthCareFacility();
            //    encounter.location.healthCareFacility.serviceProviderOrganization = new POCD_MT000040Organization();
            //    encounter.location.healthCareFacility.serviceProviderOrganization.id = GetIIArray(obj.Patient.EncounterDepartmentCode, obj.Custodian.OID); // 진료과
            //    encounter.location.healthCareFacility.serviceProviderOrganization.name = GetONArray(new List<string>() { obj.Patient.EncounterDepartmentName });
            //    encounter.location.healthCareFacility.serviceProviderOrganization.addr = GetADArray(null, null, null, obj.Custodian.AdditionalLocator, obj.Custodian.StreetAddress, obj.Custodian.PostalCode);

            //    encounter.location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf = new POCD_MT000040OrganizationPartOf(); // 기관정보
            //    encounter.location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization = new POCD_MT000040Organization();
            //    encounter.location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization.id = GetIIArray(obj.Custodian.Id, obj.Custodian.OID);
            //    encounter.location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization.name = GetONArray(new List<string>() { obj.Custodian.CustodianName });
            //    encounter.location.healthCareFacility.serviceProviderOrganization.asOrganizationPartOf.wholeOrganization.addr = GetADArray(null, null, null, obj.Custodian.AdditionalLocator, obj.Custodian.StreetAddress, obj.Custodian.PostalCode);
            //}

            returnObj.encompassingEncounter = encounter;
            return returnObj;
        }
        #endregion

        #region ::  Private Method

        #region ::: Entity
        /// <summary>
        /// POCD_MT000040Person
        /// </summary>
        /// <param name="nameValue"></param>
        /// <returns></returns>
        private POCD_MT000040Person CreatePersonElement(string nameValue)
        {
            return CreatePersonElement(new List<string>() { nameValue });
            //POCD_MT000040Person returnObj = new POCD_MT000040Person();
            //returnObj.name = GetPNArray(new List<string>() { nameValue });

            //personNameObject = null;
            //return returnObj;
        }

        /// <summary>
        /// POCD_MT000040Person ( Name Array )
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        private POCD_MT000040Person CreatePersonElement(List<string> nameList)
        {
            POCD_MT000040Person returnObj = new POCD_MT000040Person();
            returnObj.name = GetPNArray(nameList);

            //personNameObject = null;
            return returnObj;
        }

        /// <summary>
        /// oragnization(기관) / asOrganizationPartOf(부서) 생성
        /// </summary>
        /// <returns></returns>
        private POCD_MT000040Organization CreateOrganizationDepartment(string organizationName)
        {
            if (string.IsNullOrEmpty(organizationName))
            {
                return null;
            }
            else
            {
                POCD_MT000040Organization returnObj = new POCD_MT000040Organization();
                returnObj.name = GetONArray(new List<string>() { organizationName });
                return returnObj;
            }
        }

        /// <summary>
        /// place Element 생성
        /// </summary>
        /// <returns>place</returns>
        private POCD_MT000040Place CreatePlaceElement(string name)
        {
            POCD_MT000040Place place = new POCD_MT000040Place();
            place.name = new EN() { Text = new string[] { name } };
            return place;
        }
        #endregion

        #endregion

        #endregion
    }
}
