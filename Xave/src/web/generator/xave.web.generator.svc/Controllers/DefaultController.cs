using System.ServiceModel.Configuration;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using xave.web.generator.biz;

namespace xave.web.generator.svc.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// http://localhost:50001/api/Default
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Default")]
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
