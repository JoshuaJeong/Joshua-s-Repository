using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xave.web.generator.helper.Logic;
using xave.com.generator.cus;
using System.Xml;
using xave.web.generator.helper.Util;
using xave.com.generator.cus.Voca;

namespace xave.web.generator.helper
{
    public class CdaExtractorLibrary
    {
        #region :  Memebers
        private BodyExtractLogic bodyExtractor = new BodyExtractLogic();
        private POCD_MT000040ClinicalDocument schemaObject;
        private string cdaXMLString;
        private POCD_MT000040StructuredBody cdaBody;

        #region [ Name ]
        private string familyName;
        private string givenName;
        #endregion
        #region [ Address ]
        private string countryValue;
        private string cityValue;
        private string stateValue;
        private string additionalLocatorValue;
        private string streetAddressValue;
        private string postalCodeValue;
        #endregion
        #region [ bool ]
        private bool isBppc = false;
        private bool isWithdrawal = false;
        #endregion
        #endregion

        #region :  Properties
        /// <summary>
        /// 오류 메세지
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Return 속성
        /// </summary>
        private CDAObject ReturnObject;
        #endregion

        #region :  Constructors
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public CdaExtractorLibrary()
        {
            Initialize();
        }

        private void Initialize()
        {
            ReturnObject = new CDAObject();
            ReturnObject.DocumentInformation = new DocumentInformationObject();
            ReturnObject.Custodian = new CustodianObject();
            ReturnObject.Patient = new RecordTargetObject();
            //ReturnObject.InformationRecipientList = new List<InformationRecipientObject>();
            //this.ReturnObject.DescriptionSection = new DescriptionObject();
        }
        #endregion

        /// <summary>
        /// CDA String의 값을 추출하여 CDAObject 속성으로 반환
        /// </summary>
        /// <param name="cdaXml">CDA 문자열</param>
        /// <returns>CDAObject</returns>
        public CDAObject ExtractCDA(string cdaXml)
        {
            Initialize();

            this.cdaXMLString = null;
            this.ExceptionMessage = null;

            if (!string.IsNullOrEmpty(cdaXml))
            {
                this.cdaXMLString = cdaXml;

                DeserializeCDA();
                ExtractHeader();
                ExtractBody();
                if (string.IsNullOrEmpty(this.ExceptionMessage))
                {
                    return ReturnObject;
                }
                else
                {
                    throw new ArgumentException(this.ExceptionMessage);
                }
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// CDA String > Schema Class로 Deserialize
        /// </summary>
        private void DeserializeCDA()
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(cdaXMLString);
                this.schemaObject = XmlSerializer<POCD_MT000040ClinicalDocument>.Deserialize(xd.InnerXml);
            }
            catch (Exception)
            {
                throw new ArgumentException("입력 문자열을 POCD_MT000040ClinicalDocument 개체로 Deserialize 할 수 없습니다.");
            }
        }

        #region :  Method
        /// <summary>
        /// Extract Header
        /// </summary>
        private void ExtractHeader()
        {
            try
            {
                ExtractDocumentInformation();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "Document Information(서식정보) 추출 Error";
            }
            try
            {
                ExtractRecordTarget();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "환자정보 추출 Error";
            }
            try
            {
                ExtractAutor();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "작성자 정보 추출 Error";
            }
            try
            {
                ExtractCustodian();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "기관정보 추출 Error";
            }
            try
            {
                ExtractInformationRecipient();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "수신처 정보 추출 Error";
            }
            try
            {
                ExtractEncompassingEncounter();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "기타정보 추출 Error";
            }
            try
            {
                ExtractParticipant();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "참여자 정보 추출 Error";
            }
            try
            {
                ExtractServiceEvent();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "동의정보 추출 Error";
            }
            try
            {
                ExtractAuthenticator();
            }
            catch (Exception)
            {
                this.ExceptionMessage += "작성자 정보 추출 Error";
            }

        }

        private void ExtractParticipant()
        {
            if (schemaObject.participant != null && schemaObject.participant.Count() > 0)
            {
                foreach (var item in schemaObject.participant)
                {
                    switch (item.typeCode)
                    {
                        case "AUT": // 작성자
                            if (item.associatedEntity != null && item.associatedEntity.associatedPerson != null)
                            {
                                POCD_MT000040Person aut = item.associatedEntity.associatedPerson;
                                foreach (var name in aut.name)
                                {
                                    ExtractPersonName(name);
                                    //ReturnObject.Author.AuthorName = familyName + givenName;
                                }
                            }
                            break;
                        case "RESP": // 책임자
                            if (item.associatedEntity != null && item.associatedEntity.associatedPerson != null)
                            {
                                POCD_MT000040Person resp = item.associatedEntity.associatedPerson;
                                foreach (var name in resp.name)
                                {
                                    ExtractPersonName(name);
                                    //ReturnObject.Custodian.ResponsiblePersonName = familyName + givenName;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Extract encompassingEncounter
        /// </summary>
        private void ExtractEncompassingEncounter()
        {
            if (schemaObject.componentOf != null && schemaObject.componentOf.encompassingEncounter != null)
            {
                POCD_MT000040EncompassingEncounter obj = schemaObject.componentOf.encompassingEncounter;
                if (obj.code != null)
                {
                    switch (obj.code.code)
                    {
                        case "AMB":
                        case "AMP":
                            ReturnObject.Patient.CareType = CareTypes.AMBULATORY;
                            break;
                        case "IMP":
                            ReturnObject.Patient.CareType = CareTypes.INPATIENT;
                            break;
                        default:
                            ReturnObject.Patient.CareType = CareTypes.NONE;
                            break;
                    }
                }

                //if (obj.location != null && obj.location.healthCareFacility != null && obj.location.healthCareFacility.serviceProviderOrganization != null)
                //{
                //    //ReturnObject.Patient.EncounterDepartmentCode =
                //    //    obj.location.healthCareFacility.serviceProviderOrganization.id != null ?
                //    //    obj.location.healthCareFacility.serviceProviderOrganization.id.ElementAtOrDefault(0).extension : null;

                //    //ReturnObject.Patient.EncounterDepartmentName =
                //    //    obj.location.healthCareFacility.serviceProviderOrganization.name != null &&
                //    //    obj.location.healthCareFacility.serviceProviderOrganization.name.Any(w => w.Text != null) ?
                //    //    obj.location.healthCareFacility.serviceProviderOrganization.name.ElementAtOrDefault(0).Text.ElementAtOrDefault(0) : null;
                //}
                //if (obj.effectiveTime != null && obj.effectiveTime.Items != null)
                //{
                //    List<string> times = obj.effectiveTime.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                //    if (times != null)
                //    {
                //        //ReturnObject.Custodian.AdmissionDate = times.FirstOrDefault();
                //        //ReturnObject.Custodian.DischargeDate = times.LastOrDefault();
                //    }
                //}
            }
        }

        /// <summary>
        /// Extract Body
        /// </summary>
        private void ExtractBody()
        {
            cdaBody = schemaObject.component.Item as POCD_MT000040StructuredBody;
            if (cdaBody != null && cdaBody.component != null && cdaBody.component.Any())
            {
                foreach (POCD_MT000040Section section in cdaBody.component.Select(s => s.section))
                {
                    switch (section.code.code)
                    {
                        #region :: Problems
                        case LOINC.Problems:
                            try
                            {
                                ReturnObject.Problems = bodyExtractor.ExtractProblems(section).ConvertListToArray<ProblemObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Problems\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Medication
                        case LOINC.Medications:
                            try
                            {
                                ReturnObject.Medications = bodyExtractor.ExtractMedications(section, ReturnObject.DocumentInformation.DocumentType).ConvertListToArray<MedicationObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Medications\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Procedure
                        case LOINC.Procedures:
                            try
                            {
                                ReturnObject.Procedures = bodyExtractor.ExtractProcedure(section, ReturnObject.DocumentInformation.DocumentType).ConvertListToArray<ProcedureObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Procedure\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Result
                        case LOINC.Results:
                            try
                            {
                                ReturnObject.LaboratoryTests = bodyExtractor.ExtractLaboratoryTest(section, ReturnObject.DocumentInformation.DocumentType).ConvertListToArray<LaboratoryTestObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Laboratory Test\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Reason for referral
                        case LOINC.Reason_for_Referral:
                            try
                            {
                                switch (ReturnObject.DocumentInformation.DocumentType)
                                {
                                    case OID.REFERRAL_NOTE_KR: // 의뢰서
                                        ReturnObject.ReasonForReferral = bodyExtractor.ExtractDescription(section);
                                        break;
                                    case OID.TRANSFER_NOTE_KR: // 회송서
                                        ReturnObject.ReasonForTransfer = bodyExtractor.ExtractDescription(section);
                                        break;
                                    case OID.EMS_KR: // 전원소견서
                                        ReturnObject.Transfer = bodyExtractor.ExtractTransfer(section);
                                        break;
                                    default:
                                        break;
                                }
                                if (section.entry != null && section.entry.Any()) //170908 수정
                                {
                                    POCD_MT000040Act act = act = section.entry.Select(s => s.Item).OfType<POCD_MT000040Act>().FirstOrDefault();
                                    string[] array = act != null && act.text != null && act.text.Text != null && !string.IsNullOrEmpty(act.text.Text[0]) ?
                                        act.text.Text[0].Split(new string[] { "||&" }, StringSplitOptions.None) : null;
                                    if (array != null && array.Any())
                                    {
                                        ReturnObject.ReferralTransferInformation = new ReferralTransferInformationObject();
                                        ReturnObject.ReferralTransferInformation.ReferralCurrentStatus = array.ElementAtOrDefault(0);
                                        ReturnObject.ReferralTransferInformation.ReferralClinicalReason = array.ElementAtOrDefault(1);
                                        ReturnObject.ReferralTransferInformation.NonClinicalReason = array.ElementAtOrDefault(2);
                                        ReturnObject.ReferralTransferInformation.TransferType = array.ElementAtOrDefault(3);
                                        ReturnObject.ReferralTransferInformation.TransferClinicalReason = array.ElementAtOrDefault(4);
                                        ReturnObject.ReferralTransferInformation.TransferNonClinicalReason = array.ElementAtOrDefault(5);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Reason for Referral\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Assessment
                        case LOINC.Assessment:
                            try
                            {
                                switch (ReturnObject.DocumentInformation.DocumentType)
                                {
                                    case OID.CONSULTATION_NOTE_KR: // 회신서
                                        ReturnObject.CareProgress = bodyExtractor.ExtractDescription(section);
                                        ReturnObject.Assessment = new AssessmentObject();
                                        ReturnObject.Assessment.Assessment = bodyExtractor.ExtractDescription(section);
                                        break;
                                    default:
                                        ReturnObject.Assessment = new AssessmentObject();
                                        ReturnObject.Assessment.Assessment = bodyExtractor.ExtractDescription(section);
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Assessment\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Plan of Care
                        case LOINC.Plan_of_care:
                            try
                            {
                                switch (ReturnObject.DocumentInformation.DocumentType)
                                {
                                    case OID.CONSULTATION_NOTE_KR: // 회신서
                                        ReturnObject.PlannedCare = bodyExtractor.ExtractDescription(section);
                                        break;
                                    default:
                                        ReturnObject.PlanOfTreatment = bodyExtractor.ExtractPlanOfTreatment(section);
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Plan of Care\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Immunizations
                        case LOINC.Immunizations:
                            try
                            {
                                //ReturnObject.ImmunizationList = bodyExtractor.ExtractImmunizations(section);
                                ReturnObject.Immunizations = bodyExtractor.ExtractImmunizations(section).ConvertListToArray<ImmunizationObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Immunizations\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Allergies
                        case LOINC.Allergies:
                            try
                            {
                                //ReturnObject.AllergyList = bodyExtractor.ExtractAllergies(section);
                                ReturnObject.Allergies = bodyExtractor.ExtractAllergies(section).ConvertListToArray<AllergyObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Allergies\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Vital Signs
                        case LOINC.Vital_Signs:
                            try
                            {
                                //ReturnObject.VitalSignList = bodyExtractor.ExtractVitalSigns(section);
                                ReturnObject.VitalSigns = bodyExtractor.ExtractVitalSigns(section, ReturnObject.DocumentInformation.DocumentType).ConvertListToArray<VitalSignsObject>();
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Vital Sign\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Social History
                        case LOINC.Social_History:
                            try
                            {
                                ReturnObject.SocialHistory = bodyExtractor.ExtractSocialHistory(section);
                            }
                            catch
                            {
                                this.ExceptionMessage += "Extract Error : Social History\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: History Of Infection
                        case LOINC.History_Of_Infection:
                            try
                            {
                                ReturnObject.Infection = bodyExtractor.ExtractInfection(section);
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : History of Infection\r\n";
                                break;
                            }
                            break;
                        #endregion

                        #region :: BPPC
                        //16.11.08 철회서 관련 부분 추가
                        case LOINC.BPPC:
                            try
                            {
                                bodyExtractor.ExtractBppcSection(section, ReturnObject);
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : BPPC\r\n";
                                break;
                            }
                            break;
                        case LOINC.Signatures_Section:
                            try
                            {
                                ReturnObject.Signature = bodyExtractor.ExtractSignature(section);
                            }
                            catch
                            {
                                break;
                            }
                            //16. 05. 13 동의서 관련 부분 추가
                            //bodyExtractor.ExtractConsent(section, ReturnObject);                            
                            break;
                        #endregion
                        #region :: History Of Past Illness
                        case LOINC.History_Of_Present_Illness:
                        case LOINC.History_Of_Past_Illness:
                            try
                            {
                                ReturnObject.HistoryOfPastIllness = bodyExtractor.ExtractDescription(section);
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : History Of Past Illness\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Review of systems (주요 검사결과)
                        case LOINC.Review_Of_Systems:
                            try
                            {
                                ReturnObject.LabSummary = bodyExtractor.ExtractDescription(section);
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Review Of Systems\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: 퇴원투약 내역
                        case LOINC.Discharge_Medication:
                            try
                            {
                                var medications = bodyExtractor.ExtractMedications(section, ReturnObject.DocumentInformation.DocumentType);
                                var dischargemedications = medications != null ? medications.Select(s => new DischargeMedicationObject()
                                {
                                    StartDate = s.StartDate,
                                    MedicationName = s.MedicationName,
                                    MedicationCode = s.MedicationCode,
                                    MajorComponent = s.MajorComponent,
                                    MajorComponentCode = s.MajorComponentCode,
                                    DoseQuantity = s.DoseQuantity,
                                    DoseQuantityUnit = s.DoseQuantityUnit,
                                    RepeatNumber = s.RepeatNumber,
                                    Period = s.Period,
                                    Usage = s.Usage,
                                    EndDate = s.EndDate,
                                    BeginningDate = s.BeginningDate
                                }).ToList() : null;
                                ReturnObject.DischargeMedications = dischargemedications != null ? dischargemedications.ToArray() : null;
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Discharge Medications\r\n";
                                break;
                            }
                            break;
                        #endregion
                        #region :: Findings
                        case DicomCode.Findings:
                            try
                            {
                                ReturnObject.ImageReading = bodyExtractor.ExtractImageReading(section);
                            }
                            catch (Exception)
                            {
                                this.ExceptionMessage += "Extract Error : Findings\r\n";
                                break;
                            }
                            break;
                        #endregion
                    }
                }
            }
        }


        /// <summary>
        /// ClinicalDocument Class의 값을 Extract
        /// </summary>
        private void ExtractDocumentInformation()
        {
            if (schemaObject != null)
            {
                if (schemaObject.id != null) //서식 ID
                {
                    ReturnObject.DocumentInformation.DocumentID = schemaObject.id.root;
                    ReturnObject.DocumentInformation.RequestNumber = schemaObject.id.extension;
                }

                if (schemaObject.confidentialityCode != null) // 기밀성 수준코드
                {
                    ReturnObject.DocumentInformation.ConfidentialityCode = CommonExtension.GetValueFromDescription<Confidentiality>(schemaObject.confidentialityCode.code);
                }
                ReturnObject.DocumentInformation.Title = schemaObject.title != null && schemaObject.title.Text != null ? schemaObject.title.Text.ElementAtOrDefault(0) : null;
                ReturnObject.DocumentInformation.DocumentType = schemaObject.templateId.Any(w => w.root.Contains("1.2.410.100110.40")) ?
                    schemaObject.templateId.Where(w => w.root.Contains("1.2.410.100110.40")).FirstOrDefault().root : null;
            }
        }

        /// <summary>
        /// RecordTarget > PatientInformation object로 Extract
        /// </summary>
        private void ExtractRecordTarget()
        {
            if (schemaObject.recordTarget != null && schemaObject.recordTarget.Any())
            {
                schemaObject.recordTarget.All(r =>
                {
                    if (r.patientRole != null)
                    {
                        ReturnObject.Patient.LocalId = r.patientRole.id != null ? r.patientRole.id.ElementAtOrDefault(0).extension : null;
                        ReturnObject.Patient.OID = r.patientRole.id != null ? r.patientRole.id.ElementAtOrDefault(0).root : null;
                        ReturnObject.Patient.TelecomNumber = r.patientRole.telecom != null && !string.IsNullOrEmpty(r.patientRole.telecom.ElementAtOrDefault(0).value) ?
                            r.patientRole.telecom.ElementAtOrDefault(0).value.Replace("tel:", "") : null;

                        if (r.patientRole.addr != null)
                        {
                            ExtractAddress(r.patientRole.addr.ElementAtOrDefault(0));
                        }

                        ReturnObject.Patient.AdditionalLocator = additionalLocatorValue;
                        ReturnObject.Patient.StreetAddress = streetAddressValue;
                        ReturnObject.Patient.PostalCode = postalCodeValue;

                        if (r.patientRole.patient != null)
                        {
                            if (r.patientRole.patient.name != null)
                            {
                                ExtractPersonName(r.patientRole.patient.name.ElementAtOrDefault(0));
                            }

                            ReturnObject.Patient.PatientName = familyName + givenName;
                            ReturnObject.Patient.DateOfBirth = r.patientRole.patient.birthTime != null ? r.patientRole.patient.birthTime.value : null;

                            if (r.patientRole.patient.administrativeGenderCode != null)
                            {
                                ReturnObject.Patient.Gender = CommonExtension.GetValueFromDescription<GenderType>(r.patientRole.patient.administrativeGenderCode.code);
                            }
                            if (r.patientRole.patient.guardian != null && r.patientRole.patient.guardian.Any())
                            {
                                ExtractAddress(r.patientRole.patient.guardian.ElementAtOrDefault(0).addr.ElementAtOrDefault(0));
                                if (r.patientRole.patient.guardian.Any(s => s.Item != null) &&
                                    r.patientRole.patient.guardian.Select(s => s.Item).Any(w => w.GetType() == typeof(POCD_MT000040Person)) &&
                                    r.patientRole.patient.guardian.Select(s => s.Item).OfType<POCD_MT000040Person>().Any(w => w.name != null))
                                {
                                    ExtractPersonName((r.patientRole.patient.guardian.FirstOrDefault().Item as POCD_MT000040Person).name.FirstOrDefault());
                                }
                                ReturnObject.Guardian = new GuardianObject();
                                ReturnObject.Guardian.GuardianName = familyName + givenName;
                                ReturnObject.Guardian.AdditionalLocator = additionalLocatorValue;
                                ReturnObject.Guardian.StreetAddress = streetAddressValue;
                                ReturnObject.Guardian.PostalCode = postalCodeValue;
                                ReturnObject.Guardian.TelecomNumber = r.patientRole.patient.guardian.FirstOrDefault().telecom != null ?
                                    r.patientRole.patient.guardian.FirstOrDefault().telecom.FirstOrDefault().value.Replace("tel:", "") : null;
                                ReturnObject.Guardian.GType = CommonExtension.GetValueFromDescription<GuardianType>(r.patientRole.patient.guardian.FirstOrDefault().code.code);
                                //if (r.patientRole.patient.guardian[0].code != null)
                                //{
                                //    string displayName = r.patientRole.patient.guardian[0].code.displayName;
                                //    ReturnObject.Guardian.GType = string.IsNullOrEmpty(displayName) ? GuardianType.Self : (GuardianType)Enum.Parse(typeof(GuardianType), displayName, ignoreCase: true);
                                //}                                
                            }
                        }
                    }
                    return true;
                });

            }
        }

        /// <summary>
        /// P0CDAuthor > AuthorObject 
        /// </summary>
        private void ExtractAutor()
        {
            if (schemaObject.author != null && schemaObject.author.Any())
            {
                POCD_MT000040Author item = schemaObject.author.ElementAtOrDefault(0);
                if (item.assignedAuthor != null)
                {
                    POCD_MT000040AssignedAuthor assignedAuthor = item.assignedAuthor;

                    ReturnObject.Author = new AuthorObject();
                    ReturnObject.Author.MedicalLicenseID = assignedAuthor.id != null ? assignedAuthor.id.ElementAtOrDefault(0).extension : null;

                    if (assignedAuthor.Item != null && item.assignedAuthor.Item is POCD_MT000040Person)
                    {
                        //성명
                        POCD_MT000040Person person = item.assignedAuthor.Item as POCD_MT000040Person;
                        if (person.name != null)
                        {
                            ExtractPersonName(person.name.ElementAtOrDefault(0));
                            ReturnObject.Author.AuthorName = familyName + givenName;
                        }
                    }

                    ReturnObject.Author.TelecomNumber = assignedAuthor.telecom != null && !string.IsNullOrEmpty(assignedAuthor.telecom.ElementAtOrDefault(0).value) ?
                        assignedAuthor.telecom.ElementAtOrDefault(0).value.Replace("tel:", "") : null;

                    if (item.assignedAuthor.representedOrganization != null)
                    {
                        POCD_MT000040Organization org = item.assignedAuthor.representedOrganization;
                        ReturnObject.Author.DepartmentCode = org.id != null ? org.id.ElementAtOrDefault(0).extension : null;
                        ReturnObject.Author.DepartmentName = org.name != null && org.name.ElementAtOrDefault(0).Text != null ? org.name.ElementAtOrDefault(0).Text.ElementAtOrDefault(0) : null;
                    }
                }
            }
        }

        /// <summary>
        /// POCDCustodian > CustodianObject
        /// </summary>
        private void ExtractCustodian()
        {
            if (schemaObject.custodian != null && schemaObject.custodian.assignedCustodian != null && schemaObject.custodian.assignedCustodian.representedCustodianOrganization != null)
            {
                POCD_MT000040CustodianOrganization custodian = schemaObject.custodian.assignedCustodian.representedCustodianOrganization;

                if (custodian.id != null)
                {
                    II id = custodian.id.ElementAtOrDefault(0);
                    ReturnObject.Custodian.OID = id.root;
                    ReturnObject.Custodian.Id = id.extension;
                    ReturnObject.DocumentInformation.OrganizationOID = id.root;
                }
                ReturnObject.Custodian.CustodianName = custodian.name != null && custodian.name.Text != null ? custodian.name.Text.ElementAtOrDefault(0) : null;
                ReturnObject.Custodian.TelecomNumber = custodian.telecom != null && !string.IsNullOrEmpty(custodian.telecom.value) ? custodian.telecom.value.Replace("tel:", "") : null;

                if (custodian.addr != null)
                {
                    ExtractAddress(custodian.addr);
                    //ReturnObject.Custodian.Country = countryValue;
                    //ReturnObject.Custodian.City = cityValue;
                    //ReturnObject.Custodian.State = stateValue;
                    ReturnObject.Custodian.AdditionalLocator = additionalLocatorValue;
                    ReturnObject.Custodian.StreetAddress = streetAddressValue;
                    ReturnObject.Custodian.PostalCode = postalCodeValue;
                }
            }

        }

        /// <summary>
        /// POCD_MT000040InformationRecipient > InformationRecipientObject
        /// </summary>
        private void ExtractInformationRecipient()
        {
            if (schemaObject.informationRecipient != null && schemaObject.informationRecipient.Any(w => w.intendedRecipient != null))
            {
                //POCD_MT000040InformationRecipient informationRecipient = schemaObject.informationRecipient.ElementAtOrDefault(0);                
                //ReturnObject.InformationRecipientList = new List<InformationRecipientObject>(); 
                List<InformationRecipientObject> informationRecipientList = new List<InformationRecipientObject>();

                foreach (var item in schemaObject.informationRecipient)
                {
                    POCD_MT000040InformationRecipient informationRecipient = item;
                    POCD_MT000040IntendedRecipient intend = informationRecipient.intendedRecipient;
                    InformationRecipientObject temp = new InformationRecipientObject();

                    if (intend.id != null)
                    {
                        II id = intend.id.ElementAtOrDefault(0);
                        temp.Id = id.extension; // 기관 기호 id
                        temp.OID = id.root;
                        if (intend.addr != null) // Address
                        {
                            ExtractAddress(intend.addr.ElementAtOrDefault(0));
                            //temp.Country = countryValue;
                            //temp.State = stateValue;
                            //temp.City = cityValue;
                            temp.AdditionalLocator = additionalLocatorValue;
                            temp.StreetAddress = streetAddressValue;
                            temp.PostalCode = postalCodeValue;
                        }
                    }
                    if (intend.telecom != null) // 연락처
                    {
                        temp.TelecomNumber = !string.IsNullOrEmpty(intend.telecom.ElementAtOrDefault(0).value) ? intend.telecom.ElementAtOrDefault(0).value.Replace("tel:", "") : null;
                    }
                    if (intend.informationRecipient != null) // person Name
                    {
                        POCD_MT000040Person person = intend.informationRecipient;
                        if (person.name != null)
                        {
                            ExtractPersonName(person.name.ElementAtOrDefault(0));
                            temp.DoctorName = familyName + givenName;
                        }
                    }
                    if (intend.receivedOrganization != null) // department info.
                    {
                        POCD_MT000040Organization department = intend.receivedOrganization;
                        temp.DepartmentCode = department.id != null ? department.id[0].extension : null;
                        temp.DepartmentName = department.name != null && department.name.Any(w => w.Text != null) ? department.name.ElementAtOrDefault(0).Text.ElementAtOrDefault(0) : null;
                    }
                    if (intend.receivedOrganization != null &&
                        intend.receivedOrganization.asOrganizationPartOf != null &&
                        intend.receivedOrganization.asOrganizationPartOf.wholeOrganization != null) // organization info.
                    {
                        POCD_MT000040Organization organization = intend.receivedOrganization.asOrganizationPartOf.wholeOrganization;
                        II id = organization.id.ElementAtOrDefault(0);
                        ON orgName = organization.name.ElementAtOrDefault(0);

                        temp.Id = id != null ? id.extension : null;
                        temp.OID = id != null ? id.root : null;
                        temp.OrganizationName = orgName != null && orgName.Text != null ? orgName.Text.ElementAtOrDefault(0) : null;

                        if (organization.addr != null && organization.addr.Any()) // Address
                        {
                            ExtractAddress(organization.addr.ElementAtOrDefault(0));
                            //temp.Country = countryValue;
                            //temp.State = stateValue;
                            //temp.City = cityValue;
                            temp.AdditionalLocator = additionalLocatorValue;
                            temp.StreetAddress = streetAddressValue;
                            temp.PostalCode = postalCodeValue;
                        }
                    }
                    //ReturnObject.InformationRecipientList.Add(temp);
                    informationRecipientList.Add(temp);
                }
                ReturnObject.InformationRecipient = informationRecipientList.ToArray();
            }
            else
            {
                ReturnObject.InformationRecipient = null;
            }
        }

        /// <summary>
        /// POCD_MT000040ServiceEvent > Consent
        /// </summary>
        private void ExtractServiceEvent()
        {
            DistinguishWithdrawDocument();
            if (schemaObject.documentationOf != null && schemaObject.documentationOf.Any() && schemaObject.documentationOf.Any(w => w.serviceEvent != null))
            {
                foreach (POCD_MT000040ServiceEvent item in schemaObject.documentationOf.Select(s => s.serviceEvent))
                {
                    #region : 동의서
                    if (isBppc)
                    {
                        ReturnObject.Consent = new ConsentObject();
                        if (item.code != null)
                        {
                            switch (item.code.code)
                            {
                                case OID.ENTIRE_CONSENT:
                                    ReturnObject.Consent.PolicyType = PrivacyPolicyType.ENTIRE_CONSENT;
                                    break;
                                case OID.PARTIAL_CONSENT:
                                    ReturnObject.Consent.PolicyType = PrivacyPolicyType.PARTIAL_CONSENT;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (item.effectiveTime != null)
                        {
                            DateTime low;
                            DateTime high;
                            List<string> times = item.effectiveTime.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                            DateTime.TryParseExact(times.ElementAtOrDefault(0), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out low);
                            DateTime.TryParseExact(times.ElementAtOrDefault(1), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out high);
                            ReturnObject.Consent.ConsentTime = low.ToString("yyyyMMdd");
                        }
                    }
                    #endregion

                    #region : 철회서
                    if (isWithdrawal)
                    {
                        ReturnObject.Withdrawal = new WithdrawalObject();
                        if (item.code != null)
                        {
                            switch (item.code.code)
                            {
                                case OID.ENTIRE_WITHDRAWAL:
                                    ReturnObject.Withdrawal.PolicyType = PrivacyPolicyType.ENTIRE_WITHDRAWAL;
                                    break;
                                case OID.PARTIAL_WITHDRAWAL:
                                    ReturnObject.Withdrawal.PolicyType = PrivacyPolicyType.PARTIAL_WITHDRAWAL;
                                    break;
                                default:
                                    break;
                            }
                            if (item.effectiveTime != null)
                            {
                                DateTime low;
                                DateTime high;
                                List<string> times = item.effectiveTime.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                                DateTime.TryParseExact(times.ElementAtOrDefault(0), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out low);
                                DateTime.TryParseExact(times.ElementAtOrDefault(1), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out high);
                                ReturnObject.Withdrawal.WithdrawalDate = low.ToString("yyyyMMdd");
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// POCD_MT000040Authenticator > AuthenticatorObject
        /// </summary>
        private void ExtractAuthenticator()
        {
            if (schemaObject.authenticator != null && schemaObject.authenticator.Any(a => a.assignedEntity != null))
            {
                schemaObject.authenticator.All(a =>
                {
                    if (a.assignedEntity.assignedPerson != null && a.assignedEntity.assignedPerson.name != null)
                    {
                        ExtractPersonName(a.assignedEntity.assignedPerson.name.ElementAtOrDefault(0));
                    }
                    ReturnObject.Authenticator = new AuthenticatorObject();
                    ReturnObject.Authenticator.TelecomNumber = a.assignedEntity.telecom != null && a.assignedEntity.telecom.ElementAtOrDefault(0).value != null ?
                        a.assignedEntity.telecom.ElementAtOrDefault(0).value.Replace("tel:", "") : null;
                    ReturnObject.Authenticator.Id = a.assignedEntity.id != null ? a.assignedEntity.id.ElementAtOrDefault(0).extension : null;
                    ReturnObject.Authenticator.AuthenticatorName = familyName + givenName;

                    return true;
                });
            }
        }

        #region ::  Common
        private void ExtractAddress(object address)
        {
            countryValue = null;
            cityValue = null;
            stateValue = null;
            additionalLocatorValue = null;
            streetAddressValue = null;
            postalCodeValue = null;

            if (address != null && address is AD)
            {
                var obj = (AD)address;
                if (obj.Items != null && obj.Items.Count() > 0)
                {
                    foreach (var addr in obj.Items.Where(w => w.Text != null))
                    {
                        switch (addr.GetType().Name)
                        {
                            case "adxpcountry":
                                countryValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            case "adxpcity":
                                cityValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            case "adxpstate":
                                stateValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            case "adxpadditionalLocator":
                                additionalLocatorValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            case "adxpstreetAddressLine":
                                streetAddressValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            case "adxppostalCode":
                                postalCodeValue = addr.Text.ElementAtOrDefault(0);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private void ExtractPersonName(object NameObject)
        {
            familyName = null;
            givenName = null;

            if (NameObject != null && NameObject is PN)
            {
                PN pn = (PN)NameObject;
                if (pn.Items != null)
                {
                    IEnumerable<ENXP> nameArray = pn.Items.Where(w => w.Text != null);
                    foreach (ENXP name in nameArray)
                    {
                        switch (name.GetType().Name)
                        {
                            case "enfamily":
                                familyName = name.Text.ElementAtOrDefault(0);
                                break;
                            case "engiven":
                                givenName = name.Text.ElementAtOrDefault(0);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        private void DistinguishWithdrawDocument()
        {
            if (this.schemaObject.templateId.Any(w => w.root == OID.BPPC_CONSENT))
            {
                isBppc = true;
            }
            if (this.schemaObject.templateId.Any(w => w.root == OID.BPPC_WITHDRAWAL))
            {
                isWithdrawal = true;
            }
        }
        #endregion
    }
}
