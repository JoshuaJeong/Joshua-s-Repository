using xave.com.helper;
using xave.com.generator.cus;
using xave.web.generator.biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace xave.web.generator.svc.Controllers
{
    public class CdaExtractorController : ApiController
    {

        #region Properties
        private static BusinessLayer CdaLibrary = null;
        #endregion


        public CdaExtractorController()
        {
            CdaLibrary = new BusinessLayer();
        }


        #region Service API

        [HttpPost]
        public CDAObject[] ExtractCDAList([FromBody]string[] cdaXmlList)
        {
            //string cda = null;
            try
            {
                if (cdaXmlList == null || cdaXmlList.Count() == 0) return null;
                //else cda = cdaXmlList[0];

                CDAObject[] cdaObjectList = cdaXmlList.Select(cdaXml => CdaLibrary.ExtractCDA(cdaXml)).ToArray();

                return cdaObjectList;
            }
            catch (ArgumentException e) // 입력값이 잘못되었을 때...
            {
                throw ExceptionHandler.WebExcetion(e, HttpStatusCode.BadRequest, "CdaExtractor Service", "ExtractCDA", null, string.Join("^^", cdaXmlList));
            }
            catch (Exception e) // CDA Service 내부 서버 오류일 때...
            {
                throw ExceptionHandler.WebExcetion(e, HttpStatusCode.InternalServerError, "CdaExtractor Service", "ExtractCDA", null, string.Join("^^", cdaXmlList));
            }
        }

        [HttpPost]
        public CDAObject ExtractCDA([FromBody]string cdaXml)
        {
            CDAObject retVal = null;
            try
            {
                retVal = CdaLibrary.ExtractCDA(cdaXml);
                return retVal;
            }
            catch (ArgumentException e) // 입력값이 잘못되었을 때...
            {
                throw ExceptionHandler.WebExcetion(e, HttpStatusCode.BadRequest, "CdaExtractor Service", "ExtractCDA", null, cdaXml);
            }
            catch (Exception e) // CDA Service 내부 서버 오류일 때...
            {
                throw ExceptionHandler.WebExcetion(e, HttpStatusCode.InternalServerError, "CdaExtractor Service", "ExtractCDA", null, cdaXml);
            }
        }

        [HttpPost]
        public string Extract([FromBody]string cdaXml)
        {
            CDAObject retVal = null;
            try
            {
                retVal = CdaLibrary.ExtractCDA(cdaXml);
                string strCdaObject = XmlSerializer<CDAObject>.Serialize(retVal);

                return strCdaObject;
            }
            catch (ArgumentException e) // 입력값이 잘못되었을 때...
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.BadRequest, "CdaExtractor Service", "Extract", null, cdaXml);
                throw e;
            }
            catch (Exception e) // CDA Service 내부 서버 오류일 때...
            {
                //throw ExceptionHandler.WebExcetion(e, HttpStatusCode.InternalServerError, "CdaExtractor Service", "Extract", null, cdaXml);
                throw e;
            }
        }
        #endregion

        // GET api/cdaextractor
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/cdaextractor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/cdaextractor
        public void Post([FromBody]string value)
        {
        }

        // PUT api/cdaextractor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cdaextractor/5
        public void Delete(int id)
        {
        }
    }
}
