using System.ServiceModel.Configuration;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using xave.web.code.biz;

namespace xave.web.code.web.Controllers
{
    public class DefaultController : ApiController
    {
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
