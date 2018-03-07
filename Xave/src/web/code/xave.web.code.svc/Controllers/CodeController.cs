using System;
using System.Net;
using System.Web.Http;
using xave.com.helper;
using xave.web.code.biz;
using xave.web.code.dto;

namespace xave.web.code.svc.Controllers
{
    [RoutePrefix("api")]
    public class CodeController : ApiController
    {
        // GET api/code
        // url: http://localhost:50001/api/Code
        //[RequireHttps]
        [Route("Code")]
        public CodeContainers Get()
        {
            try
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri);

                BusinessLayer businessLayer = new BusinessLayer();
                CodeContainers container = businessLayer.ReadContainer();
                return container;
            }
            catch (ArgumentException e)     // 입력 값이 잘못되었을 때 발생되는 Exception
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri, e);
                throw xave.com.helper.ExceptionHandler.WebException(e, "CodeController", "Get", null, HttpStatusCode.BadRequest);
            }
            catch (Exception e)             // 서버 내부 오류로 인하여 발생되는 Exception
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri, e);
                throw xave.com.helper.ExceptionHandler.WebException(e, "CodeController", "Get", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
