using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using xave.generator.test.CdaModel;
using xave.generator.test.Model;
using xave.com.helper;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace xave.generator.test.ViewModel
{
    public class CdaTesterViewModel
    {
        #region Properties
        private static readonly log4net.ILog Timespan = log4net.LogManager.GetLogger("Timespan");
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("Logger");
        private static readonly log4net.ILog Audit = log4net.LogManager.GetLogger("Audit");

        private static string XslFilePath;
        private static string CdaGeneratorService;
        private static string CdaExtractorService;
        private CdaTesterModel model;
        public CdaTesterModel Model
        {
            get { return model; }
            set { model = value; }
        }
        #endregion

        xave.web.generator.helper.CdaDocumentLibrary generator = new xave.web.generator.helper.CdaDocumentLibrary();
        StringBuilder sb = new StringBuilder();
        int index;
        //StringBuilder sb2 = new StringBuilder();

        public CdaTesterViewModel()
        {
            Model = new CdaTesterModel();
            CdaGeneratorService = System.Configuration.ConfigurationManager.AppSettings["CdaGeneratorService"];
            CdaExtractorService = System.Configuration.ConfigurationManager.AppSettings["CdaExtractorService"];
            XslFilePath = System.Configuration.ConfigurationManager.AppSettings["XslFilePath"];

            ServerCertificateValidation();
        }

        private static void ServerCertificateValidation()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                if (errors == SslPolicyErrors.None)
                    return true;
                if (errors == SslPolicyErrors.RemoteCertificateNameMismatch || errors == SslPolicyErrors.RemoteCertificateChainErrors)
                    return true;

                return false;
            };
        }

        #region ICommand Property
        private ICommand mCommander;
        public ICommand BtnCommand
        {
            get
            {
                return mCommander = new Commander(new Action<object>(Btn_Click));
            }
            set
            {
                mCommander = value;
            }
        }
        public ICommand CbxCommand
        {
            get
            {
                return mCommander = new Commander(new Action<object>(Cbx_Click));
            }
            set
            {
                mCommander = value;
            }
        }
        public ICommand SelectionChangedCommand
        {
            get
            {
                return mCommander = new Commander(new Action<object>(SelectionChanged));
            }
            set
            {
                mCommander = value;
            }
        }
        public ICommand TextChangedCommand
        {
            get
            {
                return mCommander = new Commander(new Action<object>(TextChanged));
            }
            set
            {
                mCommander = value;
            }
        }
        public ICommand DoubleClickCommand
        {
            get
            {
                return mCommander = new Commander(new Action<object>(DoubleClicked));
            }
            set
            {
                mCommander = value;
            }
        }
        #endregion

        #region Method (for ICommand)
        private void Btn_Click(object obj)
        {
            #region Btn_Click Command Handler
            try
            {
                System.Windows.Controls.Button btn = obj as System.Windows.Controls.Button;
                string tag = btn.Tag as string;

                if (!string.IsNullOrEmpty(tag) && tag == "CDAGenerate")
                    RequestReferralCDA();

                if (!string.IsNullOrEmpty(tag) && tag == "CDAExtract")
                {
                    //if (string.IsNullOrEmpty(Model.CdaDocument))
                    //{
                    //    RequestReferralCDA();   
                    //}                    
                    ExtractCDATest();
                }
                if (!string.IsNullOrEmpty(tag) && tag == "JsonTest")
                {
                    JsonTest();
                }
                if (!string.IsNullOrEmpty(tag) && tag == "LibTest")
                {
                    LibTesting();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                MessageBox.Show(e.Message);
            }
            #endregion
        }

        private void LibTesting()
        {
            index = 0;
            #region CDA 객체 생성
            CDAObject dto = new CDAObject();
            // 1-1. 문서정보
            dto.DocumentInformation = new DocumentInformationObject()
            {
                DocumentType = "1.2.410.100110.40.2.1.1",                                                               //의뢰서
                //DocumentType = "1.2.410.100110.40.2.1.2",                                                               //회송서
                //DocumentType = "1.2.410.100110.40.2.1.3",                                                               //회신서
                //DocumentType = "1.2.410.100110.40.2.2.1.1", //동의서
                //DocumentType = "1.2.410.100110.40.2.2.1.2",   //철회서
                OrganizationOID = "1.2.410.100110.10.10000002",                                                         //의료기관 OID
                // 요양기관기호(8자리) + 발생일(yyyyMMdd) + 일련번호(5자리)
                // 예시 : 요양기관 기호 - 10000002 , 발생일 - 2017년 8월 1일 , 일련번호 - 00001
                // 예시값 : 100000022017080100001
                RequestNumber = "10000002" + DateTime.Now.ToString("yyyyMMdd") + "00001"
            };
            // 1-2. 환자정보
            dto.Patient = new RecordTargetObject()
            {
                LocalId = "pt001",                                                                                      //환자 ID
                PatientName = "홍길동",                                                                                 //환자명
                DateOfBirth = "19991231",                                                                               //생년월일
                Gender = GenderType.Male, //성별
                TelecomNumber = "000-0000-0000",                                                                        //연락처
                AdditionalLocator = "경기도 성남시 분당구",                                                              //주소(기본)
                StreetAddress = "대왕판교로 34번길 *****",                                                               //주소(상세)
                PostalCode = "13549",                                                                                   //주소(우편번호)
                CareType = CareTypes.AMBULATORY                                                                          //진료구분
            };
            // 1-3. 의료기관정보
            dto.Custodian = new CustodianObject()
            {
                OID = "1.2.410.100110.10.10000002",                                                                     //의료기관식별번호
                Id = "10000002",                                                                                        //의료기관기호
                CustodianName = "1차의료기관",                                                                          //의료기관명
                TelecomNumber = "000-0000-0000",                                                                        //의료기관연락처
                AdditionalLocator = "경기도 성남시 분당구",                                                              //주소(기본)
                StreetAddress = "구미로 00",                                                                            //주소(상세)
                PostalCode = "00000"                                                                                    //주소(우편번호)
            };
            // 1-4. 진료의
            dto.Author = new AuthorObject()
            {
                AuthorName = "이순신",                                                                                  //의료진명
                MedicalLicenseID = "00001",                                                                             //의료진면허번호
                TelecomNumber = "000-0000-0000",                                                                        //의료진연락처
                DepartmentName = "외과",                                                                                //진료과명
                DepartmentCode = "0400"                                                                                 //진료과코드
            };
            // 1-5. 문서작성자
            dto.Authenticator = new AuthenticatorObject()
            {
                Id = "0001",                                                                                            //문서작성자 ID
                AuthenticatorName = "이순신",                                                                           //문서작성자명
                TelecomNumber = "000-0000-0000"                                                                         //문서작성자연락처
            };
            // 1-6. 수신/전원기관정보
            dto.InformationRecipient = new InformationRecipientObject[] 
                {
                    new InformationRecipientObject
                    {
                        OID = "1.2.410.100110.10.91000000",                                                                //요양기관식별번호
                        Id = "91000000",                                                                                   //요양기관기호
                        OrganizationName = "3차의료기관",                                                                  //요양기관명
                        TelecomNumber = "1599-1004",                                                                        //요양기관연락처
                        DoctorName = "김의사",                                                                              //진료의명
                        AdditionalLocator = "서울시 서대문구 oo동",                                                          //주소(기본)
                        StreetAddress = "000 번지",                                                                         //주소(상세)
                        PostalCode = "00000",                                                                               //주소(우편번호)
                        DepartmentName = "가정의학과",                                                                       //진료과명
                        DepartmentCode = "2300",                                                                            //진료과코드
                    }
                };
            // 2-1. 진단내역
            dto.Problems = new ProblemObject[]
                {
                    new ProblemObject
                    {
                        StartDate = "20160201",                                                                             //진단일자
                        ProblemName = "Cerebral venous sinus thrombosis",                                                   //상병명
                        ProblemCode = "I676"                                                                                //상병코드
                    }
                };
            // 2-2. 약물처방내역
            dto.Medications = new MedicationObject[]
                {
                    new MedicationObject
                    {
                        StartDate = "20160201",                                                                             //처방일시
                        MedicationName = "Trolac 30mg inj (KetoROLAC)",                                                     //처방약품명
                        MedicationCode = "A11600682",                                                                       //처방약품코드
                        MajorComponent = "chlorpromazine",                                                                  //주성분명
                        MajorComponentCode = "N05AA01",                                                                     //주성분코드
                        DoseQuantity = "8",                                                                                 //용량
                        DoseQuantityUnit = "amp",                                                                           //복용단위
                        RepeatNumber = "1",                                                                                 //횟수
                        Period = "3",                                                                                       //투여기간
                        Usage = "식후 30분 이내 복용"                                                                        //용법
                    }
                };
            // 2-3. 검사결과
            dto.LaboratoryTests = new LaboratoryTestObject[]
                {
                    // (1) 검체검사
                    new LaboratoryTestObject
                    {
                        LabType = LaboratoryType.Specimen,                                                                  //검사유형구별
                        Date = "20160202",                                                                                  //검사일시
                        EntryName = "ABGA",                                                                                 //검사항목명
                        EntryCode = "G2101",                                                                                //검사항목코드
                        TestName = "pH",                                                                                    //검사명
                        TestCode = "C3811",                                                                                 //검사코드
                        ResultValue = "7.274",                                                                              //검사결과값
                        Reference = "7.38 ~ 7.46"                                                                           //참고치
                    },
                    // (2) 병리검사
                    new LaboratoryTestObject
                    {
                        LabType = LaboratoryType.Pathology,                                                                 //검사유형구별
                        Date = "20160205",                                                                                  //검사일시
                        TestName = "QEEG",                                                                                  //검사명
                        TestCode = "L8720",                                                                                 //검사코드
                        ResultValue = "Summary: Negative for M. tuberculosis complex"                                       //검사결과값
                    },
                    // (3) 영상검사 - 1
                    new LaboratoryTestObject
                    {
                        LabType = LaboratoryType.Radiology,                                                                 //검사유형구별
                        Date = "20160205",                                                                                  //검사일시
                        TestName = "Chest PA",                                                                              //검사명
                        TestCode = "RG2011",                                                                                //검사코드
                        ResultValue = "The cardiomediastinum is within normal limits. The trachea is midline. "
                        + "The previously described opacity at the medial right lung base has cleared.",                    //검사결과값
                        // 3차 배포 추가분
                        WebPacsBaseURL = "https://www.webpacs.co.kr",                                                       //WEB PACS 기본 주소
                        AccessionNumber = "1210491"                                                                         //PACS Accession Number
                    },
                    // (3) 영상검사 - 2
                    new LaboratoryTestObject
                    {
                        LabType = LaboratoryType.Radiology,                                                                 //검사유형구별
                        Date = "20160209",                                                                                  //검사일시
                        TestName = "Chest PA",                                                                              //검사명
                        TestCode = "RG2011",                                                                                //검사코드
                        ResultValue = "The cardiomediastinum is within normal limits. The trachea is midline. "
                        + "The previously described opacity at the medial right lung base has cleared.",                    //검사결과값
                        // 3차 배포 추가분
                        WebPacsBaseURL = "https://www.webpacs.co.kr",                                                       //WEB PACS 기본 주소
                        AccessionNumber = "14321679"                                                                        //PACS Accession Number
                    },
                    // (4) 기능검사
                    new LaboratoryTestObject
                    {
                        LabType = LaboratoryType.Functional,                                                                //검사유형구별
                        Date = "20160205",                                                                                  //검사일시
                        TestName = "ECG",                                                                                   //검사명
                        TestCode = "KL1100",                                                                                //검사코드
                        ResultValue = "SINUS RHYTHM : normal P axis, V-rate 50-99"                                          //검사결과값
                    }
                };
            // 2-4. 수술/처치내역
            dto.Procedures = new ProcedureObject[]
                {
                    new ProcedureObject
                    {
                        Date = "20121212",                                                                                  //수술일자
                        ProcedureName_ICD9CM = "laparoscopic cholecystectomy",                                              //수술명
                        ProcedureCode_ICD9CM = "51.23",                                                                     //수술코드
                        PostDiagnosisName = "간경화(Hepatic sclerosis)",                                                    //수술 후 진단명
                        Anesthesia = "국부 마취",                                                                           //마취종류
                    }
                };
            //2-5. 알러지 및 부작용
            dto.Allergies = new AllergyObject[]
                {
                    new AllergyObject
                    {
                        StartDate = "20170201",                                                                            //등록일자
                        AllergyType = "약물",                                                                              //알러지유형
                        AllergyTypeCode = "H00501805",                                                                     //알러지요인코드
                        Allergy = "조영제",                                                                                //알러지명
                        Reaction = "Wheezing"                                                                              //반응
                    }
                };
            // 2-6. 소견 및 주의사항
            dto.Assessment = new AssessmentObject
            {
                PhysicalScienceLab = "large lymphoid cells and abundant karyorrhectic debris",
                Assessment = "차가운 음식을 삼가하시기 바랍니다."
            };
            // 2-7. 의뢰사유(의뢰서)
            dto.ReasonForReferral = "가슴 통증을 호소하셨습니다. 고진선처 바랍니다.";
            //// 2-7. 회송사유(회송서)
            //dto.ReasonForTransfer = "환자의 요청에 의해 회송하고자 합니다.";
            //// 2-7. 과거병력(회신서, 회송서)
            //dto.HistoryOfPastIllness = "(2017-02-01) Chest Pain";
            // 2-8. 예약관련정보(의뢰서)
            dto.PlanOfTreatment = new PlanOfTreatmentObject
            {
                PlannedDate = DateTime.Now.AddMonths(1).ToString("yyyyMMdd"),
                Text = "Asprin 투약 후 상태 호전됨. 약물요법 시행 필요"
            };
            // 2-9. 의뢰/회송정보(의뢰서) - 4차 배포 추가분(심평원연계 보험관련정보)
            dto.ReferralTransferInformation = new ReferralTransferInformationObject
            {
                ReferralCurrentStatus = "01",                                                                           //의뢰상태구분
                ReferralClinicalReason = "02",                                                                          //임상적 의뢰사유구분
                NonClinicalReason = "03"                                                                                //비임상적 의뢰사유구분
            };

            dto.ImageReading = new ImageReadingObject();
            dto.ImageReading.PerformDate = "20160628";
            dto.ImageReading.ReadingDate = "20160701";
            dto.ImageReading.DoctorName = "홍길동";
            dto.ImageReading.ImageURL = @"https://www.google.co.kr/";
            dto.ImageReading.StudyUID = "1.2.410.1.11388.10.20.6.1.1.13458843787324";
            dto.ImageReading.SeriesUID = "1.2.410.1.11388.10.20.6.1.1.134588431233287324";
            dto.ImageReading.SopUID = "1.2.410.1.11388.10.20.6.1.1.1345884312332871232498";
            dto.ImageReading.Conclusion = "1. LM - 20%, calcified plaque pLAD - 20%, mixed plaque";
            dto.ImageReading.TestCode = "RG2011";
            dto.ImageReading.TestName = "Chest PA";

            //// 2-9. 의뢰/회송정보(회송서) - 4차 배포 추가분(심평원연계 보험관련정보)
            dto.ReferralTransferInformation = new ReferralTransferInformationObject()
            {
                TransferType = "01",                                                                                    //회송유형구분
                TransferClinicalReason = "02",                                                                          //임상적 회송사유구분
                TransferNonClinicalReason = "03"                                                                        //비임상적 회송사유구분
            };


            //동의정보 설정
            dto.Consent = new ConsentObject();
            dto.Consent.ConsentTime = DateTime.Now.ToString("yyyyMMdd"); // 동의일자
            dto.Consent.PolicyType = PrivacyPolicyType.ENTIRE_CONSENT; // 동의정책 유형
            dto.Consent.Relationship = RelationshipType.ETC; //환자와의 관계 유형                                
            dto.Consent.ConsentSubjectName = "법정대리인이름";  // 동의주체 성명
            dto.Consent.ConsentSubjectName = "기타";  // 동의주체 성명
            dto.Consent.ConsentSubjectContact = "010-1111-2222";  // 동의주체 연락처

            //철회정보 설정
            dto.Withdrawal = new WithdrawalObject();
            dto.Withdrawal.WithdrawalDate = "20160704";
            dto.Withdrawal.WithdrawalOrganizations = new WithdrawalObject.ExceptOrganizationObject[]
                {
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1001", OrganizationName  = "A병원" },
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1002", OrganizationName = "B병원" },
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1003", OrganizationName = "C병원" }
                };
            dto.Withdrawal.WithdrawalOrganizationReason = "기관 철회 사유";
            dto.Withdrawal.WithdrawalDepartmentCodes = new string[] { "5400", "5500", "5600", "5700", "5800", "5900" }; //비뇨기과, 산부인과, 정신과, 피부과
            dto.Withdrawal.WithdrawalDepartmentReason = "진료과 철회 사유";
            dto.Withdrawal.WholeWithdrawalReason = "전체 철회사유";

            dto.Withdrawal.PolicyType = PrivacyPolicyType.PARTIAL_WITHDRAWAL;
            dto.Withdrawal.Relationship = RelationshipType.LegalRepresentive;
            dto.Withdrawal.Relationship = RelationshipType.ETC;
            dto.Withdrawal.WithdrawalSubjectName = "법정대리인성명";
            dto.Withdrawal.WithdrawalSubjectName = "기타";
            dto.Withdrawal.WithdrawalSubjectContact = "000-1111-2222";


            //서명정보 설정
            dto.Signature = new SignatureObject();
            dto.Signature.MediaType = "image/png";
            dto.Signature.ImageData = "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";


            #endregion
            try
            {
                xave.web.generator.helper.CdaDocumentLibrary generator = new xave.web.generator.helper.CdaDocumentLibrary();
                StringBuilder sb = new StringBuilder();
                //sb = new StringBuilder();
                //Parallel.For(0, 10, (i) => GenerateCdaTest(i, dto));

                //for (int i = 0; i < 1000; i++)
                //{
                //    string xml = generator.GenerateCDA(dto);
                //    XDocument xd = XDocument.Parse(xml);
                //    string value = xd.Descendants().Where(w => w.Name.LocalName == "id").FirstOrDefault().Attribute("root").Value;
                //    sb.AppendLine(value);
                //}
                File.WriteAllText(@"C:\ThreadTest\" + Guid.NewGuid().ToString() + ".txt", sb.ToString());
                MessageBox.Show("Complete");
                //Model.CdaXml = new string[] { string.Empty, xml };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "index : " + index.ToString());
            }
        }

        private void GenerateCdaTest(int i, CDAObject dto)
        {
            string xml = generator.GenerateCDA(dto);
            XDocument xd = XDocument.Parse(xml);
            string value = xd.Descendants().Where(w => w.Name.LocalName == "id").FirstOrDefault().Attribute("root").Value;
            sb.AppendLine(value);
            index = i;
        }

        private void JsonTest()
        {
            if (!string.IsNullOrEmpty(Model.RequestJson))
            {
                try
                {
                    string url = CdaGeneratorService + "api/CdaGenerator/GenerateCDA";
                    System.Runtime.Serialization.Json.DataContractJsonSerializer jsonSerializer
                            = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(CDAObject));
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Model.RequestJson)))
                    {
                        var obj = (CDAObject)jsonSerializer.ReadObject(ms);
                        Model.ResultXml = Post<CDAObject, string>(url, obj) as string;
                        Model.CdaXml = new string[] { string.Empty, Model.ResultXml };
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void RequestReferralCDA()
        {
            try
            {
                sb = new StringBuilder();

                Model.Dto.DocumentInformation.DocumentType = Model.SelectedCode.code;
                string url = CdaGeneratorService + "api/CdaGenerator/GenerateCDA";
                object obj = Model.SelectedIndex == 2 ? (object)Model.CdaObjectXml : (object)Model.Dto;
                if (obj == null)
                {
                    MessageBox.Show("CDAObject is null\r\nGive me some CDAObject for test!");
                    return;
                }
                DateTime from = DateTime.Now;

                //for (int i = 0; i < 100; i++)
                //{
                //    //string xml = generator.GenerateCDA(dto);
                //    Model.CdaDocument = Post<CDAObject, string>(url, obj) as string;
                //    XDocument xd = XDocument.Parse(Model.CdaDocument);
                //    string value = xd.Descendants().Where(w => w.Name.LocalName == "id").FirstOrDefault().Attribute("root").Value;
                //    sb.AppendLine(value);
                //}

                //File.WriteAllText(@"C:\ThreadTest\" + Guid.NewGuid().ToString() + ".txt", sb.ToString());
                //MessageBox.Show("Complete");

                Model.CdaDocument = Post<CDAObject, string>(url, obj) as string;
                Log_Timespan("GenerateCDA Total Timespan: ", from);

                Model.SelectedIndex = 0;
                Model.SelectedCda = new string[] { XslFilePath, Model.CdaDocument };
                Model.CdaXml = new string[] { string.Empty, Model.CdaDocument };
            }
            catch (WebException e)
            {
                var response = (e.Response as HttpWebResponse);
                var error = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                var exception = string.Format("{0}_{1}", response.StatusCode, error.ToString());

                Debug.WriteLine(exception);
                MessageBox.Show(exception);
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                MessageBox.Show(MessageHandler.GetErrorMessage(e));
            }
        }

        public static string UrlEncode(string instring)
        {
            StringReader strRdr = new StringReader(instring);
            StringWriter strWtr = new StringWriter();
            int charValue = strRdr.Read();
            while (charValue != -1)
            {
                if (((charValue >= 48) && (charValue <= 57)) // 0-9
                    || ((charValue >= 65) && (charValue <= 90)) // A-Z
                    || ((charValue >= 97) && (charValue <= 122))) // a-z
                    strWtr.Write((char)charValue);
                else if (charValue == 32)    // Space
                    strWtr.Write('+');
                else
                    strWtr.Write("%{0:x2}", charValue);

                charValue = strRdr.Read();
            }

            return strWtr.ToString();
        }

        private void ExtractCDATest()
        {
            try
            {
                OpenFileDialog diag = new OpenFileDialog();
                if (diag.ShowDialog() == true)
                {
                    XmlDocument xd = new XmlDocument();
                    xd.Load(diag.FileName);
                    //CdaExtractorLibrary extractor = new CdaExtractorLibrary();
                    //RemoveNamespace(xd.InnerXml);

                    string url = CdaExtractorService + "api/CdaExtractor/ExtractCDA";
                    CDAObject obj = PostAString<string, CDAObject>(url, xd.InnerXml) as CDAObject;

                    //object cdaObject = extractor.ExtractCDA(xd.InnerXml);

                    string cdaObj = XmlSerializer<CDAObject>.Serialize(obj);

                    Model.Result = new string[] { string.Empty, cdaObj };
                    Model.CdaXml = new string[] { string.Empty, xd.InnerXml };
                }

                //if (!string.IsNullOrEmpty(Model.CdaDocument))
                //{
                //    string url = CdaExtractorService + "api/CdaExtractor/ExtractCDA";
                //    //string value = Post<string, string>(url, Model.CdaDocument) as string;
                //    object value = postXMLData(url, Model.CdaDocument);
                //    System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                //    xd.LoadXml(RemoveNamespace(value.ToString()));                    
                //    CDAObject obj = XmlSerializer<CDAObject>.Deserialize(xd.InnerXml);
                //    Model.Result = new string[] { string.Empty, XmlSerializer<CDAObject>.Serialize(obj) };
                //}

            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                MessageBox.Show(MessageHandler.GetErrorMessage(e));
            }
        }
        private static void Log_Timespan(string message, DateTime f)
        {
            if (Timespan.IsDebugEnabled)
                Timespan.Debug(string.Format("{0}{1}{2}", DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss.fff)"), message, (DateTime.Now - f).ToString()));
        }
        private void Cbx_Click(object obj)
        {
            #region Cbx_Click Command Handler
            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            #endregion
        }
        private void SelectionChanged(object obj)
        {
            #region SelectionChanged Command Handler
            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            #endregion
        }
        private void TextChanged(object obj)
        {
            #region TextChanged Command Handler
            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            #endregion
        }
        private void DoubleClicked(object obj)
        {
            #region DoubleClicked Command Handler
            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            #endregion
        }
        #endregion

        #region ICommand Classes
        private class Commander : ICommand
        {
            private Action<object> _action;

            public Commander(Action<object> action)
            {
                _action = action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (parameter != null)
                {
                    _action(parameter);
                }
                else
                {
                    //_action("");
                }
            }
        }
        #endregion

        #region Common Methods - Restful API


        public static TO PostAString<TI, TO>(string url, string str)
        {
            try
            {
                str = HttpUtility.UrlEncode(str);
                str = "=" + str;
                byte[] bytes = Encoding.UTF8.GetBytes(str);

                HttpWebRequest webRequest = null;

                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                webRequest.Method = "POST";
                //webRequest.Timeout = 5000;
                webRequest.ContentLength = bytes.Length;

                using (Stream requeststream = webRequest.GetRequestStream())
                {
                    requeststream.Write(bytes, 0, bytes.Length);
                    requeststream.Close();
                }

                using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                            response.StatusCode,
                                            response.StatusDescription));
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(TO));
                    using (var responseStream = response.GetResponseStream())
                    {
                        var rtnValue = (TO)dataContractSerializer.ReadObject((responseStream));
                        return rtnValue;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public static object Post<TI, TO>(string url, object obj = null)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";

                if (obj is string)
                {
                    try
                    {
                        CDAObject cdaObject = XmlSerializer<CDAObject>.Deserialize((string)obj);

                        DataContractJsonSerializer ser = new DataContractJsonSerializer(cdaObject.GetType());
                        MemoryStream ms = new MemoryStream();
                        ser.WriteObject(ms, cdaObject);
                        String json = Encoding.UTF8.GetString(ms.ToArray());

                        StreamWriter writer = new StreamWriter(request.GetRequestStream());
                        writer.Write(json);
                        writer.Close();
                    }
                    catch
                    {
                    }

                    #region Backup
                    //request.ProtocolVersion = HttpVersion.Version10;
                    //byte[] bytes = Encoding.ASCII.GetBytes((string)obj);
                    //request.ContentType = "text/xml";
                    //request.ContentLength = bytes.Length;
                    //using (Stream requeststream = request.GetRequestStream())
                    //{
                    //    requeststream.Write(bytes, 0, bytes.Length);
                    //    requeststream.Close();
                    //}
                    #endregion
                }
                else
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
                    MemoryStream ms = new MemoryStream();
                    ser.WriteObject(ms, obj);
                    String json = Encoding.UTF8.GetString(ms.ToArray());

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Close();
                }

                DateTime from = DateTime.Now;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Log_Timespan("Only GenerateCDA Timespan: ", from);
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                            response.StatusCode,
                                            response.StatusDescription));
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(TO));
                    using (var responseStream = response.GetResponseStream())
                    {
                        var rtnValue = (TO)dataContractSerializer.ReadObject((responseStream));
                        return rtnValue;
                    }
                }

                //using (HttpClient client = new HttpClient())
                //{
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //    var response = client.PostAsJsonAsync(url, obj).Result;

                //    if (response.IsSuccessStatusCode)
                //    {
                //        dynamic respContent = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                //        TO retVal = respContent;

                //        return retVal;
                //    }
                //    else
                //    {
                //        dynamic respContent = response.Content.ReadAsStringAsync().Result;

                //        TO retVal = respContent;

                //        MessageBox.Show(retVal.ToString());

                //        return retVal;
                //    }
                //}
            }
            catch (WebException e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
        }

        public static object Get<TI, TO>(string url, object obj = null)
        {
            try
            {
                if (obj is string)
                {
                    url = url + (string)obj;
                }

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                            response.StatusCode,
                                            response.StatusDescription));
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(TO));
                    using (var responseStream = response.GetResponseStream())
                    {
                        var rtnValue = (TO)dataContractSerializer.ReadObject((responseStream));
                        return rtnValue;
                    }
                }

                //using (HttpClient client = new HttpClient())
                //{
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //    var response = client.PostAsJsonAsync(url, obj).Result;

                //    if (response.IsSuccessStatusCode)
                //    {
                //        dynamic respContent = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                //        TO retVal = respContent;

                //        return retVal;
                //    }
                //    else
                //    {
                //        dynamic respContent = response.Content.ReadAsStringAsync().Result;

                //        TO retVal = respContent;

                //        MessageBox.Show(retVal.ToString());

                //        return retVal;
                //    }
                //}
            }
            catch (WebException e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
            catch (Exception e)
            {
                Debug.WriteLine(MessageHandler.GetErrorMessage(e));
                throw e;
            }
        }


        public object postXMLData(string destinationUrl, string requestXml)
        {
            var content = new StringContent(requestXml, System.Text.Encoding.UTF8, "text/xml");
            var client = new HttpClient();

            try
            {
                var retVal = client.PostAsync(destinationUrl, content).Result.Content.ReadAsStringAsync();
                return retVal.Result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static string RemoveNamespace(string xml)
        {
            XElement xElement = XElement.Parse(xml);

            foreach (XElement e in xElement.DescendantsAndSelf())
            {
                if (e.Name.Namespace != XNamespace.None)
                {
                    e.Name = XNamespace.None.GetName(e.Name.LocalName);
                }
                if (e.Attributes().Where(a => a.IsNamespaceDeclaration || a.Name.Namespace != XNamespace.None).Any())
                {
                    e.ReplaceAttributes(e.Attributes().Select(a => a.IsNamespaceDeclaration ? null : a.Name.Namespace != XNamespace.None ? new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value) : a));
                }
            }

            XDocument xDoc = new XDocument(xElement);

            return xDoc.ToString();
        }
        #endregion
    }
}
