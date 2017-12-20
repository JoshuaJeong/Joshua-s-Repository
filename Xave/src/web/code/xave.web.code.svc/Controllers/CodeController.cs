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
            catch (Exception e)
            {
                var ResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Message: {0}\r\nInnerException: {1}\r\nStackTrace: {2}", e.Message, e.InnerException != null ? e.InnerException.Message : string.Empty, e.StackTrace)),
                };

                throw new HttpResponseException(ResponseMessage);
            }
        }

    }
}
