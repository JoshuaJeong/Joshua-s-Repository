using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using xave.com.generator.cus;
using xave.com.generator.cus.CodeModel;
using xave.com.generator.cus.StructureSetModel;
using xave.web.generator.helper.Logic;
using xave.web.generator.helper.Util;
using xave.com.generator.cus.Voca;
using System.Text;

namespace xave.web.generator.helper
{
    public class CdaDocumentLibrary
    {
        #region :  Properties
        private static string ErrorLogDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDirectory"];
        private static CodeContainers codeContainers = null;
        private static Containers containers = null;
        static Assembly[] assemblys;

        POCD_MT000040ClinicalDocument clinicalDocument = new POCD_MT000040ClinicalDocument();
        HeaderLogic headerLogic = new HeaderLogic();
        CDAObject cdaObject;
        Document currentDocument = null;

        //Body
        private POCD_MT000040Component2 component2 = new POCD_MT000040Component2();
        private List<POCD_MT000040Component3> componentList = new List<POCD_MT000040Component3>();
        private Random random = new Random();
        #endregion

        #region :  Method

        public CdaDocumentLibrary()
        {
            if (codeContainers == null || containers == null)
            {
                try
                {
                    System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                    document.LoadXml(xave.web.generator.helper.Properties.Resources.StructureSetContainer);
                    containers = XmlSerializer<Containers>.Deserialize(document.InnerXml);

                    document = new System.Xml.XmlDocument();
                    document.LoadXml(xave.web.generator.helper.Properties.Resources.CodeContainer);
                    codeContainers = XmlSerializer<CodeContainers>.Deserialize(document.InnerXml);

                    SetStructureSet();
                }
                catch (Exception e)
                {
                    Logger.ExceptionLog(e, "Code 및 StructureSet xml을 Deserialize 시 에러 발생되었습니다. 관리자에게 문의 바랍니다.", ErrorLogDirectory);
                    throw e;
                }
            }
        }

        #region Setter - Master Data Setters
        public void SetCodeContainer(CodeContainers codeContainers_in)
        {
            codeContainers = codeContainers_in;
        }
        public void SetContainerOfStructureSet(Containers containers_in)
        {
            containers = containers_in;
        }
        public void SetStructureSet()
        {
            Validation();
            Settings();
            CommonQuery.Set(containers, codeContainers);
        }
        public void SetCurrentDocument(string documentType)
        {
            if (containers == null || containers.DocumentType == null || containers.DocumentType.Count() < 1)
                throw new Exception("Document Set is empty!");

            currentDocument = containers.DocumentType.FirstOrDefault(t => !string.IsNullOrEmpty(t.TemplateId) && t.TemplateId.Split('/').Contains(documentType));

            if (currentDocument == null)
                throw new Exception(string.Format("{0}{1}", "문서 타입 (" + documentType + ")", "에 매칭되는 Document Structure Set이 없습니다!"));
            else if (currentDocument.HeaderPartListType == null || currentDocument.HeaderPartListType.Count() < 1)
                throw new Exception(string.Format("{0}{1}", "문서 타입 (" + documentType + ")", "에 매칭되는 Header Structure Set이 없습니다!"));
            else if (currentDocument.SectionListType == null || currentDocument.SectionListType.Count() < 1)
                throw new Exception(string.Format("{0}{1}", "문서 타입 (" + documentType + ")", "에 매칭되는 Section Structure Set이 없습니다!"));

            CommonQuery.Set(documentType);
            //171101 Validation 관련 로직 추가
            //currentDocument.SectionListType.All(s =>
            //{
            //    s.TemplateIdExtension = string.IsNullOrEmpty(s.TemplateIdExtension) ? null : s.TemplateIdExtension;
            //    s.EntryList.All(e =>
            //    {
            //        e.TemplateIdExtension = string.IsNullOrEmpty(e.TemplateIdExtension) ? null : e.TemplateIdExtension;
            //        return true;
            //    });
            //    return true;
            //});
        }
        public void Settings()
        {
            containers.DocumentType.ToList().ForEach(DocumentType => { SetStructureSet(DocumentType); });
        }
        private void SetStructureSet(Document documentStructure)
        {
            try
            {
                SetDocumentType(documentStructure);
                documentStructure.HeaderPartListType = SetHeaderValue(documentStructure);
                documentStructure.SectionListType = SetBodyValue(documentStructure);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, "Structure Set Setting 시 에러 발생되었습니다. 관리자에게 문의 바랍니다.", documentStructure, ErrorLogDirectory);
            }
        }

        private Section[] SetBodyValue(Document documentStructure)
        {
            IEnumerable<SectionMap> sectionMapList = containers.SectionMapType.Where(t => t.DocumentId == documentStructure.Id);
            if (sectionMapList == null || sectionMapList.Count() == 0) return null;
            return sectionMapList.Select(map => GetSection(map)).Where(e => e != null).ToArray();
        }

        private Section GetSection(SectionMap map)
        {
            Section section = containers.SectionType.FirstOrDefault(t => t.Id == map.SectionId);
            if (section == null) return null;

            List<SectionPart> sectionPartList = containers.SectionPartType.Where(t => t.SectionId == section.Id).ToList();
            if (sectionPartList == null || sectionPartList.Count() == 0) return null;

            sectionPartList.ForEach(part => part.BodyStructureList = containers.BodyStructureType.Where(t => t.SectionPartId == part.Id).ToArray());

            sectionPartList = SetChildren(sectionPartList); // 각 섹션 별로 Entry와 Narrative 세팅하는 Method. Entry와 Narrative 뿐 아니라, 하위 Child Entry를 엮어줌...
            section.EntryList = sectionPartList.Where(t => t.SectionType == "ENTRY").ToArray();
            section.NarrativeList = sectionPartList.Where(t => t.SectionType == "NARRATIVE").ToArray();
            return section;
        }

        private List<SectionPart> SetChildren(List<SectionPart> entrys)
        {
            List<SectionPart> entryList = new List<SectionPart>();

            for (int i = 0; i < entrys.Count(); i++)
            {
                SectionPart child = entrys[i];

                if (child.Parent > -1 && child.Parent != child.Id)
                {
                    SectionPart parent = entrys.FirstOrDefault(t => t.Id == child.Parent && t.SectionType == child.SectionType);
                    if (parent != null)
                    {
                        if (parent.Children != null)
                        {
                            SectionPart part = parent.Children.FirstOrDefault(t => t.Id == child.Id && t.SectionType == child.SectionType && t.SectionId == child.SectionId);
                            if (part != null) continue;
                        }
                        parent.Children = ArrayHandler.Add<SectionPart>(parent.Children, child);
                    }
                    else
                    {
                        entryList.Add(child);
                    }
                }
                else
                {
                    entryList.Add(child);
                }
            }

            return entryList;
        }

        private HeaderPart[] SetHeaderValue(Document documentStructure)
        {
            List<HeaderMap> headerListMap = containers.HeaderMapType.Where(t => t.DocumentId == documentStructure.Id).ToList();
            if (headerListMap == null) return null;

            return headerListMap.Select(map => GetHeaderPart(headerListMap, map)).Where(t => t != null).ToArray();
        }

        private static HeaderPart GetHeaderPart(List<HeaderMap> headerListMap, HeaderMap map)
        {
            HeaderPart headerPart = containers.HeaderPartType.FirstOrDefault(t => t.Id == map.HeaderPartId);
            if (headerPart == null) return null;

            IEnumerable<HeaderStructure> headerStructureList = containers.HeaderStructureType.Where(t => t.HeaderPartId == headerPart.Id);
            if (headerStructureList == null) return null;

            headerPart.StructureSet = headerStructureList.ToArray();
            return headerPart;
        }

        private void SetDocumentType(Document documentStructure)
        {
            Code code = codeContainers.CodeType.FirstOrDefault(t => t.CodeID == documentStructure.DocType);
            if (code == null) return;

            documentStructure.DocTypeCode = code.CodeCD;
            documentStructure.DocTypeName = code.CodeName;
        }

        public void Validation()
        {
            if (containers.DocumentType == null || containers.DocumentType.Count() < 1) throw new Exception("Document Set is empty!");
            if (containers.HeaderPartType == null || containers.HeaderPartType.Count() < 1) throw new Exception("Header Set is empty!");
            if (containers.SectionType == null || containers.SectionType.Count() < 1) throw new Exception("Section Set is empty!");
        }
        #endregion

        /// <summary>
        /// CDAObject 객체를 매개변수로 하여 CDA XML을 반환합니다.
        /// 작성자   : 황성호
        /// 작성일자 : 2016. 02. 05
        /// </summary>
        /// <param name="param">CDAObject 객체의 인스턴스 입니다.</param>
        /// <returns>생성된 CDA XML 입니다.</returns>
        public string GenerateCDA(CDAObject _cdaObject)
        {
            try
            {
                clinicalDocument = new POCD_MT000040ClinicalDocument();
                //try
                //{
                //    Logger.Write(XmlSerializer<CDAObject>.Serialize(_cdaObject), @"C:\log\test.txt");
                //}
                //catch (Exception e)
                //{
                //    Logger.Write("Invalid CDA Object.", @"C:\log\test.txt");
                //}

                if (_cdaObject == null) throw new ArgumentException("CDAObject is null!");
                this.cdaObject = _cdaObject;
                SetCurrentDocument(this.cdaObject.DocumentInformation.DocumentType);
                Validate();
                SetLabCountProperties();
                SetKosUidandWebPacsURL();

                GenerateCDA(this.cdaObject.DocumentInformation.DocumentType);

                if (cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.1" || cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.2") //171113
                {
                    SetReferralTransferInformation();
                }

                string xml = XmlSerializer<POCD_MT000040ClinicalDocument>.Serialize(clinicalDocument, GetNamespaces());

                //try
                //{
                //    Logger.Write(xml, @"C:\log\cda.txt");
                //}
                //catch (Exception e)
                //{
                //    Logger.Write("Invalid CDA Object. 2017-09-22", @"C:\log\ex.txt");
                //}

                return xml;
            }
            catch (ArgumentException e)
            {
                Logger.ExceptionLog(e, "Exception-on-User-Input occured!", _cdaObject, ErrorLogDirectory);
                throw e;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, "Exception-on-Creating-CDA occured!", _cdaObject, ErrorLogDirectory);
                throw e;
            }
        }

        /// <summary>
        /// 의뢰회송정보 설정
        /// </summary>
        private void SetReferralTransferInformation() //170908
        {
            //if (cdaObject.ReferralTransferInformation != null && 
            //    componentList.Where(s => s != null && s.section != null && s.section.code != null && s.section.code.code.Equals(LOINC.Reason_for_Referral)).Any())

            //Not Null & 값 설정여부 판별
            if (cdaObject.ReferralTransferInformation != null &&
                cdaObject.ReferralTransferInformation.GetType().GetProperties().
                Any(a => a != null && a.GetValue(cdaObject.ReferralTransferInformation, null) != null && a.GetValue(cdaObject.ReferralTransferInformation, null).ToString() != null))
            {
                POCD_MT000040Section section = null;
                //의뢰사유 Section 존재여부 판별
                if (componentList.Where(s => s != null && s.section != null && s.section.code != null && s.section.code.code.Equals(LOINC.Reason_for_Referral)).Any())
                {
                    section = componentList.Where(w => w != null && w.section != null && w.section.code != null && w.section.code.code == LOINC.Reason_for_Referral).Select(s => s.section).FirstOrDefault();
                    section.entry = new POCD_MT000040Entry[] { SetPatientReferralAct() };
                }
                else
                {
                    POCD_MT000040Component3 component = new POCD_MT000040Component3()
                    {
                        section = new POCD_MT000040Section()
                        {
                            templateId = new II[] { new II() { root = SectionOID.Reason_For_Referral } },
                            id = new II() { root = Guid.NewGuid().ToString() },
                            title = new ST() { Text = new string[] { cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.1" ? "의뢰사유" : "회송사유" } },
                            code = new CE() { code = LOINC.Reason_for_Referral, codeSystem = OID.LOINC, codeSystemName = CodeSystemName.LOINC, displayName = LoincDisplayName.Reason_for_Referral },
                            //<code code="42349-1" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" displayName="Reason for Referral" />
                            entry = new POCD_MT000040Entry[] { SetPatientReferralAct() }
                        }
                    };
                    componentList.Add(component);
                }
            }
        }

        private POCD_MT000040Entry SetPatientReferralAct()
        {
            ReferralTransferInformationObject r = cdaObject.ReferralTransferInformation;
            POCD_MT000040Entry entry = new POCD_MT000040Entry();
            POCD_MT000040Act act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.PCPR, moodCode = x_DocumentActMood.EVN };
            act.templateId = new II[] { new II() { root = EntryOID.PATIENT_REFERRAL_ACT } };
            act.id = new II[] { new II() { root = Guid.NewGuid().ToString() } };
            act.code = new CD() { nullFlavor = "NI" };
            act.statusCode = new CS() { code = "active" };
            act.effectiveTime = new IVL_TS() { value = DateTime.Now.ToString("yyyyMMdd") };
            // 문자열 포맷 : {0}||&{1}||&{2}||&{3}||&{4}||&{5}
            // {0} : 의뢰.상태구분 , {1} : 의뢰.임상적 의뢰사유구분, {2} : 의뢰.비임상적 의뢰사유구분
            // {3} : 회송.회송유형구분, {4} : 회송.임상적 회송사유구분, {5} : 비임상적 회송사유구분
            // Xpath : Clinicaldocument/component/structuredBody/component[section/code/@code='42349-1']/section/entry/act[templateId/@root='2.16.840.1.113883.10.20.22.4.140']/text
            act.text = new ED()
            {
                Text = new string[] 
                    {
                        //string.Format("{0}&&{1}&&{2}&&{3}&&{4}&&{5}",
                        //string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                        string.Format("{0}||&{1}||&{2}||&{3}||&{4}||&{5}",
                        r.ReferralCurrentStatus, r.ReferralClinicalReason, r.NonClinicalReason, r.TransferType, r.TransferClinicalReason, r.TransferNonClinicalReason) 
                    }
            };
            entry.Item = act;

            return entry;
        }

        /// <summary>
        /// KOS UID / Web PACS URL을 설정합니다
        /// </summary>
        private void SetKosUidandWebPacsURL()
        {
            if (cdaObject.LaboratoryTests != null &&
                cdaObject.LaboratoryTests.Where
                (w => w.LabType == LaboratoryType.Radiology && !string.IsNullOrEmpty(w.AccessionNumber) && !string.IsNullOrEmpty(w.WebPacsBaseURL)).Any())
            {
                foreach (LaboratoryTestObject item in cdaObject.LaboratoryTests.
                    Where(w => w.LabType == LaboratoryType.Radiology && !string.IsNullOrEmpty(w.AccessionNumber) && !string.IsNullOrEmpty(w.WebPacsBaseURL)))
                {
                    // KOS UID 포맷 : 요양기관 OID.yyyyMMddHHmmss.난수23자
                    // ex) 1.2.410.100110.10.00000001.2017090711362412345678901234567890123
                    item.KosUid = string.Format("{0}.{1}", cdaObject.DocumentInformation.OrganizationOID, DateTime.Now.ToString("yyyyMMddHHmmss") + CreateRandomDigits(23, random));

                    // WEB PACS URL
                    // http://himeserver.irm.kr/hime-view/dicomWebView.jsp : 테스트용 주소
                    // https://ihie.mychart.kr/hime-view/dicomWebView.html : 통전 디렉토리 링크                    

                    //item.PacsURL = !string.IsNullOrEmpty(item.WebPacsBaseURL) ? string.Format("{0}?kos_uid={1}&access_token_key=null", item.WebPacsBaseURL, item.KosUid) : null;
                    item.PacsURL = item.WebPacsBaseURL;
                }
            }
        }

        private void Validate()
        {
            #region CDAObject 확인
            #region CDA Object 공통 Validation
            //if (this.cdaObject == null) throw new ArgumentException("CDAObject 가 null 입니다.");
            StringBuilder sb = new StringBuilder();

            if (this.cdaObject.DocumentInformation == null) sb.AppendLine("DocumentInformation 속성은 필수값입니다.");
            if (this.cdaObject.DocumentInformation != null)
            {
                if (string.IsNullOrEmpty(this.cdaObject.DocumentInformation.OrganizationOID)) sb.AppendLine("DocumentInformation.OrganizationOID 속성은 필수값입니다.");
            }

            if (this.cdaObject.Patient == null) sb.AppendLine("Patient 속성은 필수값입니다.");
            if (this.cdaObject.Patient != null)
            {
                if (string.IsNullOrEmpty(this.cdaObject.Patient.LocalId)) sb.AppendLine("Patient.LocalId 속성은 필수값입니다.");
                if (string.IsNullOrEmpty(this.cdaObject.Patient.PatientName)) sb.AppendLine("Patient.PatientName 속성은 필수값입니다.");
                if (string.IsNullOrEmpty(this.cdaObject.Patient.DateOfBirth)) sb.AppendLine("Patient.DateOfBirth 속성은 필수값입니다.");
            }

            if (this.cdaObject.Custodian == null) sb.AppendLine("Custodian 속성은 필수값입니다.");
            if (this.cdaObject.Custodian != null)
            {
                if (string.IsNullOrEmpty(this.cdaObject.Custodian.Id)) sb.AppendLine("Custodian.Id 속성은 필수값입니다.");
                if (string.IsNullOrEmpty(this.cdaObject.Custodian.CustodianName)) sb.AppendLine("Custodian.CustodianName 속성은 필수값입니다.");
                if (string.IsNullOrEmpty(this.cdaObject.Custodian.TelecomNumber)) sb.AppendLine("Custodian.TelecomNumber 속성은 필수값입니다.");
            }

            //if (this.cdaObject.Author == null) sb.AppendLine("Author 속성은 필수값입니다.");
            if (this.cdaObject.Author != null)
            {
                if (string.IsNullOrEmpty(this.cdaObject.Author.AuthorName)) sb.AppendLine("Author.AuthorName 속성은 필수값입니다.");
                //if (string.IsNullOrEmpty(this.cdaObject.Author.MedicalLicenseID)) sb.AppendLine("Author.MedicalLicenseID 속성은 필수값입니다.");
                //if (string.IsNullOrEmpty(this.cdaObject.Author.DepartmentName)) sb.AppendLine("Author.DepartmentName 속성은 필수값입니다.");
                //if (string.IsNullOrEmpty(this.cdaObject.Author.DepartmentCode)) sb.AppendLine("Author.DepartmentCode 속성은 필수값입니다.");
            }

            if (this.cdaObject.Authenticator == null) sb.AppendLine("Authenticator 속성은 필수값입니다.");
            if (this.cdaObject.Authenticator != null)
            {
                if (string.IsNullOrEmpty(this.cdaObject.Authenticator.Id)) sb.AppendLine("Authenticator.Id 속성은 필수값입니다.");
                if (string.IsNullOrEmpty(this.cdaObject.Authenticator.AuthenticatorName)) sb.AppendLine("Authenticator.AuthenticatorName 속성은 필수값입니다.");
            }
            #endregion

            #region 문서별 Validation
            if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.1") // 의뢰서
            {
                ValidateInformationRecipient(sb, "의뢰서");
                if (this.cdaObject.Problems == null) sb.AppendLine("의뢰서 작성 시, Problems 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.2") // 회송서
            {
                ValidateInformationRecipient(sb, "회송서");
                if (this.cdaObject.Problems == null) sb.AppendLine("회송서 작성 시, Problems 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.3") // 회신서
            {
                ValidateInformationRecipient(sb, "회신서");
                if (this.cdaObject.Problems == null) sb.AppendLine("회신서 작성 시, Problems 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.4") // 요약지
            {
                if (this.cdaObject.Problems == null) sb.AppendLine("요약지 작성 시, Problems 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.5") // 판독소견서
            {
                if (this.cdaObject.ImageReading == null) sb.AppendLine("판독소견서 작성 시, ImageReading 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.1.6") // 전원소견서
            {
                ValidateInformationRecipient(sb, "전원소견서");
                if (this.cdaObject.Assessment == null) sb.AppendLine("전원소견서 작성 시, Assessment 속성이 Null 일 수 없습니다!");
                if (this.cdaObject.Procedures == null && this.cdaObject.Procedures.Any()) sb.AppendLine("전원소견서 작성 시, Procedures 속성이 Null 일 수 없습니다!");
                if (this.cdaObject.VitalSigns == null && this.cdaObject.VitalSigns.Any()) sb.AppendLine("전원소견서 작성 시, VitalSigns 속성이 Null 일 수 없습니다!");
                //if (this.cdaObject.Guardian == null) throw new ArgumentException("전원소견서 작성 시, Guardian 속성이 Null 일 수 없습니다!");
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.2.1.1") // 동의서
            {
                if (this.cdaObject.Consent == null) sb.AppendLine("동의서 작성 시, Consent 속성이 Null 일 수 없습니다!");
                if (this.cdaObject.Consent != null)
                {
                    if (this.cdaObject.Consent.PolicyType == PrivacyPolicyType.NONE ||
                        this.cdaObject.Consent.PolicyType == PrivacyPolicyType.PARTIAL_WITHDRAWAL ||
                        this.cdaObject.Consent.PolicyType == PrivacyPolicyType.ENTIRE_WITHDRAWAL ||
                        this.cdaObject.Consent.PolicyType == PrivacyPolicyType.PARTIAL_CONSENT)
                    {
                        sb.AppendLine("동의서 작성 시, Consent.PolicyType 속성은 ENTIRE_CONSENT(전체동의) 로 설정하십시오.");
                    }
                }
            }
            else if (this.cdaObject.DocumentInformation.DocumentType == "1.2.410.100110.40.2.2.1.2") // 철회서
            {
                if (this.cdaObject.Withdrawal == null) sb.AppendLine("철회서 작성 시, Withdrawal 속성이 Null 일 수 없습니다!");
                if (this.cdaObject.Withdrawal != null)
                {
                    if (this.cdaObject.Withdrawal.PolicyType == PrivacyPolicyType.NONE ||
                        this.cdaObject.Withdrawal.PolicyType == PrivacyPolicyType.ENTIRE_CONSENT)
                    {
                        sb.AppendLine("철회서 작성 시, Consent.PolicyType 속성은 ENTIRE_WITHDRAWAL(전체철회) 또는 PARTIAL_WITHDRAWAL(부분철회) 로 설정하십시오.");
                    }
                }
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                throw new ArgumentException(sb.ToString());
            }
            #endregion
            #endregion
        }

        private void ValidateInformationRecipient(StringBuilder sb, string documentType = null)
        {
            if (this.cdaObject.InformationRecipient == null) sb.AppendLine(documentType + " 작성 시, InformationRecipient 속성이 Null 일 수 없습니다");
            if (this.cdaObject.InformationRecipient != null && this.cdaObject.InformationRecipient.Any())
            {
                this.cdaObject.InformationRecipient.All(d =>
                {
                    if (string.IsNullOrEmpty(d.OID)) sb.AppendLine("InformationRecipient.OID 속성은 필수값입니다.");
                    if (string.IsNullOrEmpty(d.Id)) sb.AppendLine("InformationRecipient.Id 속성은 필수값입니다.");
                    if (string.IsNullOrEmpty(d.OrganizationName)) sb.AppendLine("InformationRecipient.OrganizationName 속성은 필수값입니다.");
                    if (string.IsNullOrEmpty(d.TelecomNumber)) sb.AppendLine("InformationRecipient.TelecomNumber 속성은 필수값입니다.");
                    return true;
                });
            }
        }

        private void SetLabCountProperties()
        {
            if (this.cdaObject.LaboratoryTests != null && this.cdaObject.LaboratoryTests.Any())
            {
                this.cdaObject.SpecimenCount = string.Format("검체검사 : {0} 건", this.cdaObject.LaboratoryTests.Where(w => w.LabType == LaboratoryType.Specimen).Count());
                this.cdaObject.PathologyCount = string.Format("병리검사 : {0} 건", this.cdaObject.LaboratoryTests.Where(w => w.LabType == LaboratoryType.Pathology).Count());
                this.cdaObject.RadiologyCount = string.Format("영상검사 : {0} 건", this.cdaObject.LaboratoryTests.Where(w => w.LabType == LaboratoryType.Radiology).Count());
                this.cdaObject.FunctionalCount = string.Format("기능검사 : {0} 건", this.cdaObject.LaboratoryTests.Where(w => w.LabType == LaboratoryType.Functional).Count());
            }
        }

        private System.Xml.Serialization.XmlSerializerNamespaces GetNamespaces()
        {
            // extension namespace 설정
            var namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
            namespaces.Add("sdtc", "urn:hl7-org:sdtc");
            namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            return namespaces;
        }

        private void GenerateCDA(string documentType)
        {
            this.cdaObject.Patient.OID = this.cdaObject.DocumentInformation.OrganizationOID;
            this.cdaObject.Custodian.OID = this.cdaObject.DocumentInformation.OrganizationOID;

            CreateGeneralHeader();
            CreateHeader();
            CreateBody();
        }

        /// <summary>
        /// CDAObject의 필수 속성을 체크합니다.
        /// </summary>
        private void CheckRequireProperties()
        {
            if (this.cdaObject == null) throw new ArgumentException("CDAObject is null");
            if (this.cdaObject.DocumentInformation == null) throw new Exception("DocumentInformation 속성이 Null 입니다!");
            if (this.cdaObject.Custodian == null) throw new Exception("Custodian 속성이 Null 입니다!");
            if (this.cdaObject.Patient == null) throw new Exception("Patient 속성이 Null 입니다!");
        }

        #region :  Private
        /// <summary>
        /// 1
        /// CCDA Header 공통 요소 생성
        /// </summary>
        private void CreateGeneralHeader()
        {
            clinicalDocument.realmCode = headerLogic.GetCSArray(currentDocument.RealmCode);
            clinicalDocument.typeId = new POCD_MT000040InfrastructureRoottypeId() { extension = "POCD_HD000040", root = OID.CDA };

            string[] templateIds = currentDocument.TemplateId.Split('/');
            //■ 문서고유ID 생성 방식 : 서식별 OID +“.”+ 요양기관 기호 (8자) +“.” + TIMESTAMP(DATETIME타입:14자리)+난수 6자
            //string documentId = string.Format("{0}.{1}.{2}", templateIds.Where(w => w.Contains("1.2.410.100110.40.2")).FirstOrDefault(), cdaObject.Custodian.Id, DateTime.Now.ToString("yyyyMMddHHmmss") + CommonExtension.CreateRandomDigits(6));
            string documentId = string.Format("{0}.{1}.{2}", templateIds.Where(w => w.Contains("1.2.410.100110.40.2")).FirstOrDefault(), cdaObject.Custodian.Id, DateTime.Now.ToString("yyyyMMddHHmmss") + CreateRandomDigits(6, random));

            List<DocumentInformationObject.IdObject> templateIdArray = new List<DocumentInformationObject.IdObject>();
            templateIdArray.AddRange(templateIds.Select(t => new DocumentInformationObject.IdObject() { root = t }));
            clinicalDocument.templateId = headerLogic.GetTemplateId(templateIdArray); // ClinicalDocument/templateId/@root
            clinicalDocument.id = new II() { root = documentId, extension = cdaObject.DocumentInformation.RequestNumber };
            clinicalDocument.code = HeaderLogic.GetCE(currentDocument.Code, currentDocument.DisplayName, currentDocument.CodeSystemName, currentDocument.CodeSystem);
            clinicalDocument.title = HeaderLogic.GetST(currentDocument.Title);
            clinicalDocument.effectiveTime = headerLogic.GetTS(DateTime.Now.ToString("yyyyMMddHHmmsszzzz").Replace(":", ""));
            clinicalDocument.confidentialityCode = HeaderLogic.GetCE(cdaObject.DocumentInformation.ConfidentialityCode.GetDescription(), null, null, "2.16.840.1.113883.5.25");
            clinicalDocument.languageCode = headerLogic.GetCS(currentDocument.LanguageCode, null, null, null);
        }

        /// <summary>
        /// Header 생성
        /// </summary>
        private void CreateHeader()
        {
            foreach (HeaderPart headerPart in currentDocument.HeaderPartListType.OrderBy(t => t.Sequence))
            {
                if (headerPart.Name == "recordTarget")
                    clinicalDocument.recordTarget = headerLogic.CreateRecordTargetElement(cdaObject, headerPart);
                else if (headerPart.Name == "author")
                    clinicalDocument.author = headerLogic.CreateAuthorElement(cdaObject);
                else if (headerPart.Name == "custodian")
                    clinicalDocument.custodian = headerLogic.CreateCustodianElement(cdaObject.Custodian);
                else if (headerPart.Name == "informationRecipient")
                    //clinicalDocument.informationRecipient = headerLogic.CreateInformationRecipientElement(cdaObject.InformationRecipientList);
                    clinicalDocument.informationRecipient = headerLogic.CreateInformationRecipientElement(cdaObject.InformationRecipient);
                else if (headerPart.Name == "authenticator")
                    clinicalDocument.authenticator = headerLogic.CreateAuthenticator(cdaObject);
                else if (headerPart.Name == "documentationOf")
                    clinicalDocument.documentationOf = headerLogic.CreateServiceEventElement(cdaObject, this.cdaObject.DocumentInformation.DocumentType);
                else if (headerPart.Name == "componentOf")
                    //clinicalDocument.componentOf = headerLogic.CreateEncompassingEncounter(cdaObject.Patient.CareType);
                    clinicalDocument.componentOf = headerLogic.CreateEncompassingEncounter(cdaObject);
            }
        }
        #endregion

        #region :  Body
        private void CreateBody()
        {
            componentList = new List<POCD_MT000040Component3>();

            CreateSections();

            component2.Item = new POCD_MT000040StructuredBody() { component = componentList.ToArray() };

            clinicalDocument.component = component2;
        }

        private void CreateSections()
        {
            string section = null;
            string sectionCode = null;
            try
            {
                #region refactoring, by select from 구문
                // Refactoring Pattern with Loops (Martin Fowler) : https://martinfowler.com/articles/refactoring-pipelines.html
                componentList.AddRange(currentDocument.SectionListType.Where(t => t.UseYN == "TRUE").OrderBy(t => t.Sequence).Select(s => { section = s.DisplayName; sectionCode = s.Code; return CreateSection(s); }));
                // Parallel.ForEach(currentDocument.SectionListType.OrderBy(t => t.Sequence), s => { componentList.Add(CreateSection(s)); });
                #endregion

                #region Backup
                // Refactoring Pattern with Loops : https://www.experts-exchange.com/questions/28518308/Refactoring-two-foreach-loop-into-LINQ.html
                //componentList = (from section in currentDocument.BodyType.SectionList
                //                 select CreateSection(cdaObject, section) as POCD_MT000040Component3).ToList();
                #endregion
            }
            catch (ArgumentException e)
            {
                Logger.ExceptionLog(e, "Error occured on section, " + section + "<" + sectionCode + ">", ErrorLogDirectory);
                throw e;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, "Error occured on section, " + section + "<" + sectionCode + ">", ErrorLogDirectory);
                throw e;
            }
        }

        private POCD_MT000040Component3 CreateSection(Section _section)
        {
            if (!string.IsNullOrEmpty(_section.BindableVariable))
            {
                Type sectionType = FindType(_section.BindableVariable);
                //IList list = CreateList(sectionType);
                ///Type sectionTypeList = list.GetType();
                Type sectionTypeList = sectionType.MakeArrayType();
                object obj = SetBindableValue(_section, cdaObject);

                if ((obj != null && obj.GetType() == sectionType) || (obj != null && obj.GetType() == typeof(string)))
                    return MakeGenericMethod(_section, cdaObject, "CreateSection_Entry", sectionType, null, obj); // Object가 Generic Type이 아닐 때...
                //else if (obj != null && obj.GetType() == sectionTypeList)
                else if (obj != null && obj.GetType().IsArray)
                    return MakeGenericMethod(_section, cdaObject, "CreateSection_EntryList", sectionType, sectionTypeList, obj); // Object가 Generic Type일 때...
            }
            return null;
        }

        private object SetBindableValue(Section _section, CDAObject cdaObject)
        {
            object obj = null;

            switch (_section.Code)
            {
                case "11450-4": //진단내역
                    //obj = cdaObject.ProblemList;
                    obj = cdaObject.Problems;
                    break;
                case "10160-0": //투약
                    //obj = cdaObject.MedicationList;
                    obj = cdaObject.Medications;
                    break;
                case "30954-2": //검사
                    //obj = cdaObject.LaboratoryTestList;
                    obj = cdaObject.LaboratoryTests;
                    break;
                case "47519-4": // 수술
                    //obj = cdaObject.ProcedureList;
                    obj = cdaObject.Procedures;
                    break;
                case "8716-3": //생체정보
                    //obj = cdaObject.VitalSignList;
                    obj = cdaObject.VitalSigns;
                    break;
                case "11369-6": //예방접종
                    //obj = cdaObject.ImmunizationList;
                    obj = cdaObject.Immunizations;
                    break;
                case "48765-2": //알러지
                    //obj = cdaObject.AllergyList;
                    obj = cdaObject.Allergies;
                    break;
                case "18776-5": // 예약정보 // 향후 치료방침
                    obj = currentDocument.TemplateId == "1.2.410.100110.40.2.1.3" ? (object)cdaObject.PlannedCare : (object)cdaObject.PlanOfTreatment;
                    break;
                case "42349-1": //의뢰사유
                    switch (currentDocument.TemplateId)
                    {
                        case "1.2.410.100110.40.2.1.1": //진료의뢰서
                            obj = (object)cdaObject.ReasonForReferral;
                            break;
                        case "1.2.410.100110.40.2.1.2": //진료회송서
                            obj = (object)cdaObject.ReasonForTransfer;
                            break;
                        case "1.2.410.100110.40.2.1.6": //전원소견서
                            obj = (object)cdaObject.Transfer;
                            break;
                        default:
                            break;
                    }
                    break;
                case "51848-0": //Assessment  치료 및 수술 경과 // 소견 및 주의사항
                    //obj = currentDocument.TemplateId == "1.2.410.100110.40.2.1.3" ? (object)cdaObject.CareProgress : (object)cdaObject.Assessment;
                    obj = (object)cdaObject.Assessment;
                    break;
                case "10187-3": // 주요 검사결과
                    obj = cdaObject.LabSummary;
                    break;
                case "29762-2": // 사회력 (흡연상태/음주상태)
                    obj = cdaObject.SocialHistory;
                    break;
                case "56838-6": // 법정감염성 전염병
                    obj = cdaObject.Infection;
                    break;
                case "11348-0": // 과거 병력
                    obj = cdaObject.HistoryOfPastIllness;
                    break;
                case "57016-8": //동의서 section
                    obj = cdaObject;
                    break;
                case "57017-6": //서명
                    obj = cdaObject.Signature;
                    break;
                case "10183-2": //퇴원투약
                    //obj = cdaObject.DischargeMedicationList;
                    obj = cdaObject.DischargeMedications;
                    break;
                //case "11283-9": // 중중도 분류
                //    obj = cdaObject.AcuityScale;
                //    break;
                case "121070": //영상 판독정보
                    obj = cdaObject.ImageReading;
                    break;
                //case "48768-6": //건강보험 정보
                //    obj = cdaObject.HealthInsurance;
                //    break;
                default:
                    break;
            }
            return obj;
        }

        private IList CreateList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        public static void FindAssembly()
        {
            assemblys = AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.Contains("xave.com.generator.std") || t.FullName.Contains("xave.com.generator.cus") || t.FullName.Contains("mscorlib")).ToArray();
        }

        public static Type FindType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return typeof(string);
            if (assemblys == null) FindAssembly();
            return (from assembly in assemblys
                    select assembly.GetType(typeName)).FirstOrDefault(t => t != null);
        }

        private POCD_MT000040Component3 MakeGenericMethod(Section _section, CDAObject cdaObject, string MethodName, Type sectionType, Type sectionTypeList, object obj)
        {
            MethodInfo method, generic = null;

            method = typeof(SectionLogic).GetMethod(MethodName);
            if (obj != null && obj.GetType() == sectionType)
                generic = method.MakeGenericMethod(new Type[] { sectionType });
            else if (obj != null && obj.GetType() == typeof(string))
                generic = method.MakeGenericMethod(new Type[] { typeof(string) });
            else if (obj != null && obj.GetType() == sectionTypeList)
                generic = method.MakeGenericMethod(new Type[] { sectionType, sectionTypeList });

            return (POCD_MT000040Component3)generic.Invoke(null, new object[] { cdaObject, _section, obj });
        }

        //private void CreateUnstructuredDocument()
        //{
        //    if (cdaObject.NonXMLBody != null)
        //    {
        //        POCD_MT000040NonXMLBody nonXMLBody = new POCD_MT000040NonXMLBody();
        //        nonXMLBody.text = new ED()
        //        {
        //            Text = new string[] { cdaObject.NonXMLBody.Base64String },
        //            mediaType = cdaObject.NonXMLBody.MediaType,
        //            representation = BinaryDataEncoding.B64,
        //            reference = !string.IsNullOrEmpty(cdaObject.NonXMLBody.ReferenceURL) ? new TEL() { value = cdaObject.NonXMLBody.ReferenceURL } : null
        //        };
        //        component2.Item = nonXMLBody;
        //        clinicalDocument.component = component2;
        //    }
        //}
        #endregion


        internal string CreateRandomDigits(int length, Random rd = null)
        {
            Random random = null;
            if (rd == null)
            {
                random = new Random();
            }
            else
            {
                random = rd;
            }

            string value = string.Empty;
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    value = String.Concat(value, random.Next(1, 10).ToString());
                }
                else
                {
                    value = String.Concat(value, random.Next(10).ToString());
                }
            }
            return value;
        }

        #endregion
    }
}
