using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace xave.com.helper
{
    public static class CommonMethod
    {
        public static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }

        public static string GetNewTransactionID()
        {
            return string.Format("{0}_{1}", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString());
        }
    }
}
