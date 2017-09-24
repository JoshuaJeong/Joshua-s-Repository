using Generator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xaver.CdaGenerator.BIZ;

namespace Xaver.CdaGenerator.SVC.Controllers
{
    public class CodeContainerController : ApiController
    {
        // GET api/code
        // url: http://localhost:65001/api/CodeContainer/Get
        public CodeContainers Get()
        {
            try
            {
                BusinessLayer containerBL = new BusinessLayer();
                CodeContainers container = containerBL.ReadCodeContainer();
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
