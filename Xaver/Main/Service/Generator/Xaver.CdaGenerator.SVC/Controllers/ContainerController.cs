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
    public class ContainerController : ApiController
    {
        // GET api/Containers
        // url: http://localhost:65001/api/Container/Get
        public Containers Get()
        {
            try
            {
                BusinessLayer biznessLayer = new BusinessLayer();
                Containers containers = biznessLayer.ReadContainer();
                return containers;
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
