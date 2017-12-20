using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
