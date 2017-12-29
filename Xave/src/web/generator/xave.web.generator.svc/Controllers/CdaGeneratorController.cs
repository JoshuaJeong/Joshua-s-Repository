using xave.com.helper;
using xave.com.generator.cus;
using xave.com.generator.cus.CodeModel;
using xave.com.generator.cus.StructureSetModel;
using xave.web.generator.biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web.Http;

namespace xave.web.generator.svc.Controllers
{
    public class CdaGeneratorController : ApiController
    {
        #region
        /// <summary>
        /// CdaLibrary
        /// - Re-Initialization을 줄이기 위해...
        /// </summary>
        //private static LogService.TransactionLog TransactionLog;

        private static BusinessLayer CdaLibrary = null;
        private static Containers containers = null;
        private static CodeContainers codeContainers = null;
        private static string SAVE_CDAOBJECT_YN = System.Configuration.ConfigurationManager.AppSettings["SAVE_CDAOBJECT_YN"];
        private static string SAVE_CDAXML_YN = System.Configuration.ConfigurationManager.AppSettings["SAVE_CDAXML_YN"];
        private static string SAVE_FILEPATH = System.Configuration.ConfigurationManager.AppSettings["SAVE_FILEPATH"];

        private static string LogServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["LogServiceEndpoint"];
        private static string ContainersEndpoint = System.Configuration.ConfigurationManager.AppSettings["ContainersEndpoint"];
        private static string CodeContainerServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["CodeContainerServiceEndpoint"];
        private static string CodeServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["CodeServiceEndpoint"];
        private static string StructureSetServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["StructureSetServiceEndpoint"];
        private static string DocumentServiceEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentServiceEndpoint"];

        private static readonly log4net.ILog ErrorLog = log4net.LogManager.GetLogger("ErrorLog");
        private static readonly log4net.ILog Timespan = log4net.LogManager.GetLogger("Timespan");
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("Logger");

        DateTime from = DateTime.MinValue;
        DateTime to = DateTime.MinValue;
        DateTime totalfrom = DateTime.MinValue;
        DateTime totalto = DateTime.MinValue;
        TimeSpan timespan;
        #endregion

        public CdaGeneratorController()
        {
            if (CdaLibrary == null)
                CdaLibrary = new BusinessLayer();

            Initialize();

            //TransactionLog = new LogService.TransactionLog(LogServiceEndpoint);
        }

        #region Initialize

        private void Initialize()
        {
            try
            {
                totalfrom = DateTime.Now;
                if (codeContainers == null || containers == null)
                {
                    from = DateTime.Now;
                    codeContainers = Initialize<CodeContainers>(codeContainers, CodeContainerServiceEndpoint) as CodeContainers;
                    Log_Timespan("INIT-GetCodeSet", from);

                    from = DateTime.Now;
                    containers = Initialize<Containers>(containers, ContainersEndpoint) as Containers;
                    Log_Timespan("INIT-GetStructureSet", from);

                    from = DateTime.Now;
                    //CdaLibrary.SetCodeList(codeList);
                    CdaLibrary.SetCodeContainer(codeContainers);
                    CdaLibrary.SetContainerOfStructureSet(containers);
                    CdaLibrary.SetStructureSet();
                    Log_Timespan("INIT-SetStructureSet", from);
                }
                timespan = DateTime.Now.Subtract(totalfrom);
                if (Timespan.IsDebugEnabled)
                    Timespan.Debug(string.Format("{0}{1}{2}\r\n", DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss.fff)"), "Initialize Total Timespan: ", timespan.ToString()));
            }
            catch (Exception e)
            {
                string errorMessage = MessageHandler.GetErrorMessage(e);
                if (Logger.IsErrorEnabled)
                    Logger.Error(errorMessage);

                throw e;
            }
        }

        private void ResetCodeSet()
        {
            containers = null;
            codeContainers = null;
        }

        private object Initialize<T>(object obj, string url)
        {
            if (obj == null)
            {
                try
                {
                    obj = RestfulApiHandler.Get<string, T>(url);
                    return obj;
                }
                catch (Exception e)
                {
                    if (Logger.IsErrorEnabled)
                        Logger.Error(MessageHandler.GetErrorMessage(e));

                    throw e;
                }
            }
            return obj;
        }

        #endregion

        #region Service APIs
        [System.Web.Mvc.AsyncTimeout(60000)]
        [System.Web.Mvc.HandleError(ExceptionType = typeof(TimeoutException), View = "Timeout")]
        [HttpPost]
        public string GenerateCDA(CDAObject obj)
        {
            DateTime from = DateTime.Now;
            string retVal = null;
            string REQ_MSG = null;

            try
            {
                #region CDA Object를 Deserialize 후 FILE SAVE 하는 로직
                REQ_MSG = WriteCDAObject(obj);
                #endregion

                #region CDA 생성 로직
                retVal = CdaLibrary.GenerateCDA(obj);
                #endregion

                #region CDA XML을 FILE SAVE 하는 로직
                if (SAVE_CDAXML_YN == "Y" && !string.IsNullOrEmpty(retVal))
                    xave.com.helper.Logger.Save(null, null, null, null, null, retVal);
                #endregion

                #region TRANSACTION LOG 로직
                string TRST_ID = obj == null ? null : obj.TRST_ID;
                //TransactionLog.LogString(TRST_ID,
                //                         null,
                //                         (int)HIE.Framework.LoggingLevel.Info,
                //                         CommonCodes.HL7_CDA_GenerateCDA,
                //                         "CDAService",
                //                         GetIP(),
                //                         DateTime.Now,
                //                         REQ_MSG,
                //                         CommonCodes.STATUS_SUCCESS,
                //                         retVal,
                //                         string.Empty,
                //                         CommonCodes.STATUS_SUCCESS);
                #endregion
            }
            catch (ArgumentException e) // CDAObject 입력값이 잘못되었을 때...
            {
                throw ExceptionHandler.WebExcetion<CDAObject>(e, HttpStatusCode.BadRequest, "CdaGenerator Service", "GenerateCDA", obj.TRST_ID, obj);
            }
            catch (Exception e) // CDA Service 내부 서버 오류일 때...
            {
                throw ExceptionHandler.WebExcetion<CDAObject>(e, HttpStatusCode.InternalServerError, "CdaGenerator Service", "GenerateCDA", obj.TRST_ID, obj);
            }

            timespan = DateTime.Now.Subtract(from);
            if (Timespan.IsDebugEnabled)
                Timespan.Debug(string.Format("{0}{1}{2}\r\n", DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss.fff)"), "GenerateCDA Total Timespan: ", timespan.ToString()));
            //Log_Timespan("GenerateCDA Total Timespan", from);

            return retVal;
        }

        private static string WriteCDAObject(CDAObject obj)
        {
            if (obj != null)
            {
                string REQ_MSG = XmlSerializer<CDAObject>.Serialize(obj);

                if (SAVE_CDAOBJECT_YN == "Y")
                    xave.com.helper.Logger.Save(null, null, null, null, null, REQ_MSG);

                return REQ_MSG;
            }
            return null;
        }

        [HttpGet]
        public string RefreshCodeSet(string TransactionId)
        {
            string RefreshResult = "FAILURE";

            try
            {
                ResetCodeSet();
                Initialize();
                RefreshResult = "SUCCESS";

                //TransactionLog.LogString(TransactionId, null, (int)HIE.Framework.LoggingLevel.Info, CommonCodes.HL7_CDA_RefreshCodeSet, "CDAService", GetIP(), DateTime.Now, null, CommonCodes.STATUS_SUCCESS, RefreshResult, string.Empty, CommonCodes.STATUS_SUCCESS);
            }
            catch (ArgumentException e)
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.BadRequest, "CdaGenerator Service", "RefreshCodeSet", null);
                throw ExceptionHandler.WebExcetion<string>(e, HttpStatusCode.BadRequest, "CdaGenerator Service", "RefreshCodeSet", TransactionId, null);
            }
            catch (Exception e)
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.InternalServerError, "CdaGenerator Service", "RefreshCodeSet", null);
                throw ExceptionHandler.WebExcetion<string>(e, HttpStatusCode.InternalServerError, "CdaGenerator Service", "RefreshCodeSet", TransactionId, null);
            }

            return RefreshResult;
        }

        [HttpGet]
        public string RefreshCodeSet()
        {
            string RefreshResult = "FAILURE";

            try
            {
                ResetCodeSet();
                Initialize();
                RefreshResult = "SUCCESS";

                //TransactionLog.LogString(null, null, (int)HIE.Framework.LoggingLevel.Info, CommonCodes.HL7_CDA_RefreshCodeSet, "CDAService", GetIP(), DateTime.Now, null, CommonCodes.STATUS_SUCCESS, RefreshResult, string.Empty, CommonCodes.STATUS_SUCCESS);
            }
            catch (ArgumentException e)
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.BadRequest, "CdaGenerator Service", "RefreshCodeSet", null);
                throw ExceptionHandler.WebExcetion<string>(e, HttpStatusCode.BadRequest, "CdaGenerator Service", "RefreshCodeSet", null, null);
            }
            catch (Exception e)
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.InternalServerError, "CdaGenerator Service", "RefreshCodeSet", null);
                throw ExceptionHandler.WebExcetion<string>(e, HttpStatusCode.InternalServerError, "CdaGenerator Service", "RefreshCodeSet", null, null);
            }

            return RefreshResult;
        }



        /// <summary>
        /// http://localhost:30032/api/CdaGenerator/GetCdaJsonString
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CDAObject GetCdaJsonString()
        {
            #region GetCdaJsonString
            var dto = new CDAObject();

            dto.DocumentInformation = new DocumentInformationObject() { RealmCode = "KR", LanguageCode = "ko", ConfidentialityCode = Confidentiality.Normal, DocumentType = "1.2.410.100110.40.2.1.1" };
            dto.DocumentInformation.OrganizationOID = "1.2.410.100110.10.31100813";

            // 2-2. 환자 정보 설정
            dto.Patient = new RecordTargetObject();
            dto.Patient.LocalId = "00000000";
            dto.Patient.DateOfBirth = "19731001";
            dto.Patient.PatientName = "김환자";
            dto.Patient.Gender = GenderType.Male;
            dto.Patient.TelecomNumber = "010-0000-1000";
            dto.Patient.AdditionalLocator = "경기도 용인시 수지구";
            dto.Patient.StreetAddress = "oo로 00번길 *****";
            dto.Patient.PostalCode = "00000";
            dto.Patient.CareType = CareTypes.AMBULATORY;

            // 2-3 의료기관 , 의료진   정보 설정
            dto.Custodian = new CustodianObject();
            dto.Custodian.CustodianName = "분당서울대학교 병원";
            dto.Custodian.Id = "31100813";
            dto.Custodian.TelecomNumber = "1588-3369";
            dto.Custodian.AdditionalLocator = "경기도 성남시 분당구";
            dto.Custodian.StreetAddress = "정자3동 구미로173번길 82";
            dto.Custodian.PostalCode = "13620";

            dto.Author = new AuthorObject();
            dto.Author.AuthorName = "최의사";
            dto.Author.DepartmentCode = "0400";
            dto.Author.DepartmentName = "외과";
            dto.Author.MedicalLicenseID = "00000";
            dto.Author.TelecomNumber = "000-0000-0000";

            //2-4 의뢰대상 기관(수신기관) 정보                
            InformationRecipientObject info1 = new InformationRecipientObject();
            info1.Id = "23456789";
            info1.OID = "1.2.410.23456789";
            info1.OrganizationName = "참조은병원";
            info1.DoctorName = "김의사";
            info1.TelecomNumber = "1600-9955";
            info1.AdditionalLocator = "경기 광주시 광주대로 45";
            info1.StreetAddress = "000 번지";
            info1.PostalCode = "00000";
            info1.DepartmentName = "외과";
            info1.DepartmentCode = "0400";
            dto.InformationRecipient = new InformationRecipientObject[] { info1 };
            //dto.InformationRecipientArray = new InformationRecipientObject[] { info1 };

            // 2-5 진단내역 설정 ( 복수 Row 일 경우, 반복문을 이용하여   객체를 반복 생성 / 값 설정 후 Add 합니다.)                
            ProblemObject p1 = new ProblemObject();
            ProblemObject p2 = new ProblemObject();
            ProblemObject p3 = new ProblemObject();
            p1.StartDate = "20170501";
            p1.ProblemCode = "I614";
            p1.ProblemName = "Cerebellar hemorrhage";
            p1.AcuityScale = "1등급";
            //p1.ProblemName_KOR = "두개내정맥계통의 비화농성 혈전증";

            p2.StartDate = "20170501";
            p2.ProblemCode = "I615";
            p2.ProblemName = "Intraventricular hemorrhage";
            p2.AcuityScale = "1등급";

            p3.StartDate = "20170501";
            p3.ProblemCode = "N200";
            p3.ProblemName = "Renal stone";

            //dto.Problems = new ProblemObject[] { p1, p2 };
            dto.Problems = new ProblemObject[] { p1, p2, p3, };
            //dto.ProblemArray = new ProblemObject[] { p1, p2 };

            // 2-6 투약내역 설정 ( 복수 Row 일 경우, 반복문을 이용하여   객체를 반복 생성 / 값 설정 후 Add 합니다.)
            //dto.Medications = new List<MedicationsObject>();
            MedicationObject m1 = new MedicationObject();
            MedicationObject m2 = new MedicationObject();
            MedicationObject m3 = new MedicationObject();
            MedicationObject m4 = new MedicationObject();
            //1 row                
            m1.StartDate = "201705010551"; //처방일시
            m1.BeginningDate = "201705010625"; //투약시작일시
            m1.MedicationCode = "644903260";
            m1.MedicationName = "Pot chloride-40 중외 20ml plastic";
            m1.DoseQuantity = "1";
            m1.DoseQuantityUnit = "amp";
            m1.RepeatNumber = "1";
            m1.Period = "3";
            m1.MajorComponentCode = "B05XA01";
            m1.MajorComponent = "Potassium chloride";
            m1.Usage = "식후 30분 이내 복용";

            //2 row
            m2.StartDate = "201705010551"; //처방일시
            m2.BeginningDate = "201705010625"; //투약시작일시         
            m2.MedicationCode = "678900720";
            m2.MedicationName = "Normal saline 500ml bag 중외";
            m2.DoseQuantity = "1";
            m2.DoseQuantityUnit = "bag";
            m2.RepeatNumber = "1";
            m2.Period = "3";
            m2.MajorComponentCode = "B05XA03";
            m2.MajorComponent = "Sodium chloride";
            m2.Usage = "식전 1시간 이내 복용";

            //3 row
            m3.StartDate = "201705010603"; //처방일시
            m3.BeginningDate = "201705010604"; //투약시작일시
            m3.MedicationCode = "664600110";
            m3.MedicationName = "Norphin* 4ml amp (Norepinephrine)";
            m3.DoseQuantity = "12";
            m3.DoseQuantityUnit = "mg";
            m3.RepeatNumber = "1";
            m3.Period = "3";
            m3.MajorComponentCode = "C01CA03";
            m3.MajorComponent = "norepine phrine";
            m3.Usage = "식후 1시간 이내 복용";

            //4 row
            m4.StartDate = "201705010603"; //처방일시
            m4.BeginningDate = "201705010625"; //투약시작일시         
            m4.MedicationCode = "645100980";
            m4.MedicationName = "Dextrose 5% 200ml bag대한";
            m4.DoseQuantity = "1";
            m4.DoseQuantityUnit = "bag";
            m4.RepeatNumber = "1";
            m4.Period = "3";
            m4.MajorComponentCode = "B05BA03";
            m4.MajorComponent = "carbohydrates";
            m4.Usage = "식후 1시간 이내 복용";

            dto.Medications = new MedicationObject[] { m1, m2, m3 };
            //dto.MedicationArray = new MedicationObject[] { m1, m2 };
            //dto.Medications = new MedicationObject[] { m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, m1, m2, m3, m4, };

            // 2-7 검사결과 설정 (   복수 Row 일 경우, 반복문을 이용하여   객체를 반복 생성 / 값 설정 후 Add 합니다.)
            // 2-7.1 검사결과 설정(검체)            
            LaboratoryTestObject l3 = new LaboratoryTestObject();
            LaboratoryTestObject l3_1 = new LaboratoryTestObject();
            LaboratoryTestObject l3_2 = new LaboratoryTestObject();
            LaboratoryTestObject l3_3 = new LaboratoryTestObject();
            LaboratoryTestObject l3_4 = new LaboratoryTestObject();
            LaboratoryTestObject l3_5 = new LaboratoryTestObject();
            LaboratoryTestObject l3_6 = new LaboratoryTestObject();
            LaboratoryTestObject l3_7 = new LaboratoryTestObject();
            LaboratoryTestObject l3_8 = new LaboratoryTestObject();
            LaboratoryTestObject l3_9 = new LaboratoryTestObject();
            LaboratoryTestObject l3_10 = new LaboratoryTestObject();
            LaboratoryTestObject l3_11 = new LaboratoryTestObject();
            LaboratoryTestObject l3_12 = new LaboratoryTestObject();
            LaboratoryTestObject l3_13 = new LaboratoryTestObject();
            l3_1.KostomCodes = new KostomObject[] { new KostomObject() { Code = "CODE", DisplayName = "DISPLAY" } };
            //1 row
            l3.LabType = LaboratoryType.Specimen;
            l3.Date = "20170501";
            l3.EntryName = "ABGA";
            l3.EntryCode = "C3811";
            l3.TestName = "pH";
            l3.ResultValue = "7.274";
            l3.Reference = "7.38 ~ 7.46";

            l3_1.LabType = LaboratoryType.Specimen;
            l3_1.Date = "20170501";
            l3_1.EntryName = "ABGA";
            l3_1.EntryCode = "C3811";
            l3_1.TestName = "pCO₂";
            l3_1.ResultValue = "34.0";
            l3_1.Reference = "32 ~ 46";

            l3_2.LabType = LaboratoryType.Specimen;
            l3_2.Date = "20170501";
            l3_2.EntryName = "ABGA";
            l3_2.EntryCode = "C3811";
            l3_2.TestName = "pO₂";
            l3_2.ResultValue = "62.2";
            l3_2.Reference = "74 ~ 108";

            l3_3.LabType = LaboratoryType.Specimen;
            l3_3.Date = "20170501";
            l3_3.EntryName = "ABGA";
            l3_3.EntryCode = "C3811";
            l3_3.TestName = "HCO3-";
            l3_3.ResultValue = "15.7";
            l3_3.Reference = "21 ~ 29";

            l3_4.LabType = LaboratoryType.Specimen;
            l3_4.Date = "20170501";
            l3_4.EntryName = "ABGA";
            l3_4.EntryCode = "C3811";
            l3_4.TestName = "BE";
            l3_4.ResultValue = "-10.2";
            l3_4.Reference = "-2 ~ 2";

            l3_5.LabType = LaboratoryType.Specimen;
            l3_5.Date = "20170501";
            l3_5.EntryName = "ABGA";
            l3_5.EntryCode = "C3811";
            l3_5.TestName = "O₂SAT";
            l3_5.ResultValue = "90.6";
            l3_5.Reference = "92 ~ 96";

            l3_6.LabType = LaboratoryType.Specimen;
            l3_6.Date = "20170501";
            l3_6.EntryName = "ABGA";
            l3_6.EntryCode = "C3811";
            l3_6.TestName = "Hct";
            l3_6.ResultValue = "31.6";
            l3_6.Reference = "39 ~ 52";

            l3_7.LabType = LaboratoryType.Specimen;
            l3_7.Date = "20170501";
            l3_7.EntryName = "ABGA";
            l3_7.EntryCode = "C3811";
            l3_7.TestName = "Na";
            l3_7.ResultValue = "138";
            l3_7.Reference = "135 ~ 145";

            l3_8.LabType = LaboratoryType.Specimen;
            l3_8.Date = "20170501";
            l3_8.EntryName = "ABGA";
            l3_8.EntryCode = "C3811";
            l3_8.TestName = "K";
            l3_8.ResultValue = "2.5";
            l3_8.Reference = "3.5 ~ 5.5";

            l3_9.LabType = LaboratoryType.Specimen;
            l3_9.Date = "20170501";
            l3_9.EntryName = "ABGA";
            l3_9.EntryCode = "C3811";
            l3_9.TestName = "Cl";
            l3_9.ResultValue = "120";
            l3_9.Reference = "98 ~ 110";

            l3_10.LabType = LaboratoryType.Specimen;
            l3_10.Date = "20170501";
            l3_10.EntryName = "ABGA";
            l3_10.EntryCode = "C3811";
            l3_10.TestName = "Glocose";
            l3_10.ResultValue = "105";
            l3_10.Reference = "70 ~ 110";

            l3_11.LabType = LaboratoryType.Specimen;
            l3_11.Date = "20170501";
            l3_11.EntryName = "ABGA";
            l3_11.EntryCode = "C3811";
            l3_11.TestName = "iCa";
            l3_11.ResultValue = "0.78";
            l3_11.Reference = "1.05 ~ 1.35";

            l3_12.LabType = LaboratoryType.Specimen;
            l3_12.Date = "20170501";
            l3_12.EntryName = "ABGA";
            l3_12.EntryCode = "C3811";
            l3_12.TestName = "Lactate";
            l3_12.ResultValue = "0.6";
            l3_12.Reference = "0.7 ~ 2.5";

            l3_13.LabType = LaboratoryType.Specimen;
            l3_13.Date = "20170501";
            l3_13.EntryName = "ABGA";
            l3_13.EntryCode = "C3811";
            l3_13.TestName = "Hb";
            l3_13.ResultValue = "10.3";
            l3_13.Reference = "13 ~ 17";

            //2 row
            //l3_1.LabType = LaboratoryType.Specimen;
            ////l3_1.TestCode = "1212-1";
            ////l3_1.OrderDate = "20160203";
            //l3_1.Date = "20170504";
            ////l3_1.ResultDate = "20160207";
            //l3_1.TestName = "CRC-혈액검사";
            //l3_1.EntryName = "WBC";
            //l3_1.EntryCode = "1212-2";
            //l3_1.ResultValue = "6.7 10+3/ul";
            //l3_1.Reference = "4.3-10.8 10+3/ul";

            //dto.LaboratoryResults.Add(l3); dto.LaboratoryResults.Add(l3_1);

            // 2-7.2 검사결과 설정(병리)            
            LaboratoryTestObject l4 = new LaboratoryTestObject();
            LaboratoryTestObject l4_1 = new LaboratoryTestObject();
            //1 row
            l4.LabType = LaboratoryType.Pathology;
            //l4.OrderDate = "20160203";
            l4.Date = "20170501";
            //l4.ResultDate = "20160207";
            l4.TestName = "분자병리학적검사";
            l4.TestCode = "pat001";
            l4.ResultValue = "Summary: Negative for M. tuberculosis complex";
            //2 row
            l4_1.LabType = LaboratoryType.Pathology;
            //l4_1.OrderDate = "20160203";
            l4_1.Date = "20170502";
            //l4_1.ResultDate = "20160207";
            l4_1.TestName = "세포비부인과검사";
            l4_1.TestCode = "pat002";
            l4_1.ResultValue = @"Satisfactory for evaluation Benign Mixed population of small and
large lymphoid cells and abundant karyorrhectic debris ";

            //dto.LaboratoryResults.Add(l4);
            //dto.LaboratoryResults.Add(l4_1);

            // 2-7.3 검사결과 설정(영상)            
            LaboratoryTestObject l5 = new LaboratoryTestObject();
            l5.LabType = LaboratoryType.Radiology;
            l5.Date = "20170511";
            l5.TestName = "Chest AP";
            l5.TestCode = "RG2011";
            l5.ResultValue = "Pulmonary congestion";
            //l5.ResultValue = "The cardiomediastinum is within normal limits. The trachea is   midline. The previously described opacity at the medial right lung base has   cleared. There are no new infiltrates. There is a new round density at the   left hilus, superiorly (diameter about 45mm).";
            l5.PacsURL = @"http://google.com";
            l5.OffLineYN = true;

            //dto.LaboratoryResults.Add(l5);

            // 2-7.4 검사결과 설정(기능)            
            LaboratoryTestObject l6 = new LaboratoryTestObject();
            l6.LabType = LaboratoryType.Functional;
            //l6.OrderDate = "20160203";
            l6.Date = "20170501";
            //l6.ResultDate = "20160207";
            l6.TestName = "ECG(응급실기기사용)";
            l6.TestCode = "KL1100";
            l6.ResultValue = @"SINUS RHYTHM : normal P axis, V-rate 50-99
BORDERLINE INTRAVENTRICCULAR CONDUCTION DELAY : QRSd > 105 mS
BORDERLINE PROLONGED QT INTERVAL : QTc >475mS
BASELINE WANDER IN LEAD(S) V4,V5,V6 :";
            //dto.LaboratoryResults.Add(l6);

            dto.LaboratoryTests = new LaboratoryTestObject[] { l3, l3_1, l4, l4_1, l5, l6 };
            //dto.LaboratoryTestArray = new LaboratoryTestObject[] { l3, l3_1, l4, l4_1, l5, l6 };
            //dto.LaboratoryTests = new LaboratoryTestObject[] { l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, l3, l3_1, l3_2, l3_3, l3_4, l3_5, l3_6, l3_7, l3_8, l3_9, l3_10, l3_11, l3_12, l3_13, l5, l6, };

            // 2-8 수술내역 설정 (   복수 Row 일 경우, 반복문을 이용하여   객체를 반복 생성 / 값 설정 후 Add 합니다.)
            //dto.Procedures = new List<ProceduresObject>();
            ProcedureObject pr1 = new ProcedureObject();
            pr1.Date = "20170501";
            pr1.PostDiagnosisName = "간경화(Hepatic sclerosis)";
            pr1.ProcedureCode_ICD9CM = "51.23";
            pr1.ProcedureName_ICD9CM = "laparoscopic cholecystectomy";
            pr1.Anesthesia = "국부 마취";
            pr1.Text = "BP 70 torr 측정되어 NE start함";
            dto.Procedures = new ProcedureObject[] { pr1 };
            //dto.Procedures.Add(pr1);

            //2-9 예약 / 치료계획   관련 정보 설정            
            dto.PlanOfTreatment = new PlanOfTreatmentObject() { PlannedDate = DateTime.Now.ToString("yyyyMMddHHmm"), Text = "Asprin 투약 후 상태 호전됨. 약물요법 시행 필요" };
            dto.PlanOfTreatment.Text = "Asprin 투약 후 상태 호전됨. 약물요법 시행 필요";

            //2-10 서술형 Text 설정
            //dto.DescriptionSection = new DescriptionObject();
            //dto.DescriptionSection.Assessment = "신장질환이 의심됩니다.";
            //dto.DescriptionSection.ReasonForReferral = "고진 선처 바랍니다.";

            //2-11 알러지 정보 설정   ( 복수 Row 일 경우, 반복문을 이용하여   객체를 반복 생성 / 값 설정 후 Add 합니다.)
            AllergyObject al1 = new AllergyObject();
            al1.StartDate = "20170501";
            al1.AllergyTypeCode = "H00501805";
            al1.AllergyType = "약물";
            al1.Allergy = "조영제";
            al1.Reaction = "Wheezing";
            al1.MedicationCode = "678900720";
            al1.MedicationName = "Normal saline 500ml bag 중외";
            al1.AdverseReaction = "부작용";
            dto.Allergies = new AllergyObject[] { al1 };

            dto.ReasonForReferral = "의뢰사유 입니다.";
            dto.HistoryOfPastIllness = "과거병력 입니다";
            dto.ReasonForTransfer = "회송사유 입니다.";

            //2-12 입원 투약내역 설정  ( 복수 Row 일 경우, 반복문을 이용하여 객체를 반복 생성 / 값 설정 후 Add 합니다.)
            //AdmissionMedicationObject a1 = new AdmissionMedicationObject();
            //a1.StartDate = "20160201";
            //a1.MedicationCode = "A11600682";
            //a1.MajorComponentCode = "QN05AX90";
            //a1.MedicationName = "Aspirin";
            //dto.AdmissionMedication = new AdmissionMedicationObject[] { a1 };
            ////dto.AdmissionMedication.Add(a1);

            //2-13 교육정보
            //EducationCheckObject edu = new EducationCheckObject();
            //dto.EducationCheck = new EducationCheckObject[] { edu };
            //dto.EducationCheck.Add(edu);

            //소견 및 주의사항
            dto.Assessment = new AssessmentObject();
            //dto.Assessment.PhysicalScienceLab = "이학적 검사결과 입니다.";
            dto.Assessment.Assessment = @"h/o multifocal infarction, Rt. cerebellum, PICA territory, 2015/03/30 - aspirin 복용 중.
상환, 내원일 1a10경 샤워하던 중, 이상한 느낌이 들어 보호자에게 119
불러달라고 하였으며, 이후 구토 및 의식 저하 발생하여 아주대병원 방문.";


            //향후치료 방침
            //주요검사결과
            //치료 및 경과
            dto.PlannedCare = "향후 치료방침 test";
            dto.LabSummary = "주요검사결과 test";
            dto.CareProgress = "치료 및 경과 test";
            dto.HistoryOfPastIllness = "과거 병력 test";

            //문서 작성자
            dto.Authenticator = new AuthenticatorObject();
            dto.Authenticator.Id = "0001";
            dto.Authenticator.AuthenticatorName = "이순신";
            dto.Authenticator.TelecomNumber = "000-0000-0000";

            //예방접종내역
            ImmunizationObject im1 = new ImmunizationObject();
            im1.Date = "20170501";
            //im1.VaccineCode = "F00200012";
            im1.ImmunizationCode = "H01750893";
            im1.ImmunizationName = "결핵";
            im1.VaccineName = "Recombivax HB Ped/Adol";
            im1.RepeatNumber = "3";
            dto.Immunizations = new ImmunizationObject[] { im1 };

            //2-12 생체정보 설정
            VitalSignsObject v1 = new VitalSignsObject();
            VitalSignsObject v2 = new VitalSignsObject();
            v1.HeartRate = "82";
            v1.AwarenessCondition = "unresponsive";
            v1.ETC = "주호소-복통";
            v1.Date = "20170501";
            //v1.Height = "175";
            //v1.Weight = "59";
            v1.BP_Diastolic = "150";
            v1.BP_Systolic = "176";
            v1.BodyTemperature = "36.0";
            v1.Distinction = DistinctionType.PreProcedure;

            v2.HeartRate = "63";
            v2.AwarenessCondition = "coma";
            v2.ETC = "주호소-복통";
            v2.Date = "20170501";
            //v2.Height = "175";
            //v2.Weight = "58.5";
            v2.BP_Diastolic = "68";
            v2.BP_Systolic = "97";
            v2.BodyTemperature = "34.2";
            v2.Distinction = DistinctionType.PostProcedure;

            dto.VitalSigns = new VitalSignsObject[] { v1, v2 };

            //2-14 흡연상태 설정
            dto.SocialHistory = new SocialHistoryObject();
            dto.SocialHistory.SmokingStatusCode = "H03097153";
            dto.SocialHistory.SmokingStatus = "매일피움";
            dto.SocialHistory.FrequencyCode = "LA18927-6";
            dto.SocialHistory.Frequency = "2-4 times a month";
            dto.SocialHistory.AlcoholConsumptionCode = "LA15694-5";
            dto.SocialHistory.AlcoholConsumption = "1 or 2";
            dto.SocialHistory.OverdrinkingCode = "LA6270-8";
            dto.SocialHistory.Overdrinking = "Never";

            //2-16 법정 감염성 전염병
            dto.Infection = new InfectionObject();
            dto.Infection.OnsetDate = "20170201";
            dto.Infection.DiagnosisDate = "20170202";
            dto.Infection.InfectionName = "비브리오패혈증";
            dto.Infection.ReportedDate = "20160203";
            dto.Infection.Classification = "병원체보유자";
            dto.Infection.TestResult = "확진";
            dto.Infection.AdmissionYN = true;
            dto.Infection.SuspectedArea = "경기도 oo군 oo읍";

            //동의정보 설정
            dto.Consent = new ConsentObject();
            dto.Consent.ConsentTime = "20161122";
            dto.Consent.PolicyType = PrivacyPolicyType.ENTIRE_CONSENT;
            dto.Consent.Relationship = RelationshipType.Myself;
            dto.Consent.ExceptDepartmentCodes = new string[] { "1500", "1000", "0300", "1400" }; //비뇨기과, 산부인과, 정신과, 피부과


            //철회정보 설정
            dto.Withdrawal = new WithdrawalObject();
            dto.Withdrawal.WithdrawalDate = "20160704";
            dto.Withdrawal.WithdrawalOrganizations = new WithdrawalObject.ExceptOrganizationObject[] 
                {
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1001", OrganizationName = "A병원" },
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1002", OrganizationName = "B병원" },
                    new WithdrawalObject.ExceptOrganizationObject(){ OID = "1.2.410.6.1003", OrganizationName = "C병원" }
                };
            dto.Withdrawal.WithdrawalOrganizationReason = "기관 철회 사유";
            dto.Withdrawal.WithdrawalDepartmentCodes = new string[] { "1500", "1000", "0300", "1400" }; //비뇨기과, 산부인과, 정신과, 피부과
            dto.Withdrawal.WithdrawalDepartmentReason = "진료과 철회 사유";
            dto.Withdrawal.WholeWithdrawalReason = "전체 철회사유";

            dto.Withdrawal.PolicyType = PrivacyPolicyType.PARTIAL_WITHDRAWAL;

            //서명정보 설정
            dto.Signature = new SignatureObject();
            dto.Signature.MediaType = "image/png";
            dto.Signature.ImageData = "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";

            #region : 전원소견서
            dto.Transfer = new TransferObject();
            dto.Transfer.ArrivalTime = "201705010510";
            dto.Transfer.TransferDate = "201705011004";
            dto.Transfer.ReasonForTransfer = "중환자실이 부족하여 전원";
            dto.Transfer.Transportaion = "사설 구급차";
            dto.Transfer.CarNumber = "00가0000";
            dto.Transfer.Practitioner = "응급구조사";

            dto.Guardian = new GuardianObject();
            dto.Guardian.GuardianName = "김보호";
            dto.Guardian.AdditionalLocator = "서울시 oo구 oo동";
            dto.Guardian.StreetAddress = "000번지 00빌딩";
            dto.Guardian.PostalCode = "00000";
            dto.Guardian.TelecomNumber = "000-0000-0000";
            dto.Guardian.GType = GuardianType.Family;

            //

            DischargeMedicationObject dm1 = new DischargeMedicationObject();
            DischargeMedicationObject dm2 = new DischargeMedicationObject();
            //1 row                
            dm1.StartDate = "201705010551"; //처방일시
            dm1.BeginningDate = "201705010625"; //투약시작일시
            dm1.MedicationCode = "644903260";
            dm1.MedicationName = "Pot chloride-40 중외 20ml plastic";
            dm1.DoseQuantity = "1";
            dm1.DoseQuantityUnit = "amp";
            dm1.RepeatNumber = "1";
            dm1.Period = "3";
            dm1.MajorComponentCode = "B05XA01";
            dm1.MajorComponent = "Potassium chloride";
            dm1.Usage = "식후 30분 이내 복용";

            //2 row
            dm2.StartDate = "201705010551"; //처방일시
            dm2.BeginningDate = "201705010625"; //투약시작일시         
            dm2.MedicationCode = "678900720";
            dm2.MedicationName = "Normal saline 500ml bag 중외";
            dm2.DoseQuantity = "1";
            dm2.DoseQuantityUnit = "bag";
            dm2.RepeatNumber = "1";
            dm2.Period = "3";
            dm2.MajorComponentCode = "B05XA03";
            dm2.MajorComponent = "Sodium chloride";
            dm2.Usage = "식전 1시간 이내 복용";

            dto.DischargeMedications = new DischargeMedicationObject[] { dm1, dm2 };
            //dto.AcuityScale = "1등급";
            #endregion

            #region : 판독소견서
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
            #endregion

            #endregion

            return dto;
        }


        #endregion

        #region General Method

        public static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }

        private static void Log_Timespan(string message, DateTime f)
        {
#if DEBUG
            if (Timespan.IsDebugEnabled)
                Timespan.Debug(string.Format("{0}{1}{2}", DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss.fff)"), message, (DateTime.Now - f).ToString()));
#endif
        }

        #endregion
    }
}
