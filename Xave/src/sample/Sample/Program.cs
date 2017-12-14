using log4net;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = log4net.LogManager.GetLogger("NHibernateLogging");

            log.Error("Error!", new Exception("Exception123"));
            log.Warn("Warning");
        }
    }
}
