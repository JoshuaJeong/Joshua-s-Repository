using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using xave.web.structureset.biz;

namespace xave.web.structureset.svc.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// http://localhost:50002/api/Default/Settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Settings()
        {
            StringBuilder sb = new StringBuilder();

            BusinessLayer businessLayer = new BusinessLayer();
            sb.Append(string.Format("{0}\r\n", businessLayer.GetConnectionInfo()));

            ClientSection clientSection = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
            ChannelEndpointElementCollection endpoints = clientSection.Endpoints;

            foreach (ChannelEndpointElement endpoint in endpoints)
                sb.Append(string.Format("[{0}:{1}] \r\n ", endpoint.Name, endpoint.Address.AbsoluteUri));

            return sb.ToString();
        }
    }
}
