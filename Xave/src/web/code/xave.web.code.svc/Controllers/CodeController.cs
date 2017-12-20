using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using xave.com.helper;
using xave.com.helper.model;
using xave.web.code.biz;
using xave.web.code.dto;

namespace xave.web.code.svc.Controllers
{
    public class CodeController : ApiController
    {
        // GET api/code
        // url: http://localhost:50001/api/Code/Get
        public CodeContainers Get()
        {
            try
            {
                System.Reflection.MethodBase mb = System.Reflection.MethodBase.GetCurrentMethod();
                Log log = new Log() { ApplicationEntity = mb.ReflectedType.Name, Endpoint = Request.RequestUri.AbsoluteUri, Method = mb.Name, Regdate = DateTime.Now, RequesterIPAddress = HttpContext.Current.Request.UserHostAddress, RequestMessage = null, ResponseMessage = null, UserMessage = null };
                Logger.Save(log);

                BusinessLayer businessLayer = new BusinessLayer();
                CodeContainers container = businessLayer.ReadContainer();
                return container;
            }
            catch (ArgumentException e)
            {
                throw xave.com.helper.ExceptionHandler.WebException(e, "CodeController", "Get", null, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                throw xave.com.helper.ExceptionHandler.WebException(e, "CodeController", "Get", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
