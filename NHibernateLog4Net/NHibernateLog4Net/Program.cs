using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using log4net;
using System.Net;
using System.Net.Sockets;

namespace NHibernateLog4Net
{
	class Program
	{
		static void Main(string[] args)
		{
			log4net.Config.XmlConfigurator.Configure();

            try
            {
                using (ISession db = NHibernateHelper.OpenSession())
                {
                    db.Save(new Log() { ApplicationEntity = "JaewonTester", Method = "Main", Endpoint = "http://localhost:9088", RequesterIPAddress = GetIP(), Regdate = DateTime.Now, RequestMessage = "RequestMessage", ResponseMessage = "ResponseMessage", UserMessage = "UserMessage" });

                    //db.Save(new Product() { ProductId = 2, Name = "Jaewon", Store = 2 }, 2);
                    //db.Save(new Store() { Id = 2, Address = "Test", City = "City", Name = "Name", Zip = "Zip" }, 2);
                    db.Flush();

                    //var query = from p in db.Query<Product>()
                    //                        join s in db.Query<Store>() on p.Store equals s.Id
                    //                        select p;

                    //foreach (var item in query)
                    //{
                    //    if (item != null)
                    //    {
                    //        string name = item.Name;
                    //        Console.WriteLine(name);
                    //    }
                    //}
                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
		}


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
	}
}
