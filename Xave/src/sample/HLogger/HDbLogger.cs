using log4net;

namespace HLogger
{
    public class HDbLogger
    {
        ILog log;
        string loggerName = string.Empty;

        public HDbLogger()
        {
            log = log4net.LogManager.GetLogger("HDbLogger");
        }

        public HDbLogger(string _loggerName)
        {
            loggerName = _loggerName;
            log = log4net.LogManager.GetLogger(_loggerName);
        }
    }
}
