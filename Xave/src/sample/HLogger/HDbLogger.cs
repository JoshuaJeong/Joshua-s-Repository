using log4net;

namespace HLogger
{
    public class HDbLogger
    {
        ILog log;
        string loggerName = string.Empty;

        public void HDbLogger()
        {
            log = log4net.LogManager.GetLogger("HDbLogger");
        }

        public void HDbLogger(string _loggerName)
        {
            loggerName = _loggerName;
            log = log4net.LogManager.GetLogger(_loggerName);
        }
    }
}
