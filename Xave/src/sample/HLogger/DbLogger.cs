using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using log4net.Filter;

namespace HLogger
{
    public class DbLogger
    {
        static AdoNetAppender adoNetAppender = null;
        public DbLogger()
        {
            adoNetAppender = new AdoNetAppender()
            {
                Name = "AdoNetAppender",
                BufferSize = 1,
                CommandText = @"INSERT INTO TB_LOG (C_APPLICATIONENTITY,C_TRANSACTION,C_LEVEL,C_REGDATE,C_REQUESTMESSAGE,C_RESPONSEMESSAGE,C_USERMESSAGE,C_SERVICEENDPOINT,C_REQUESTERIP) VALUES (@date,@appcode,@thread,@level,@logger,@user,@message,@exception)",
            };
        }

        public static IAppender CreateAdoNetAppender(string cs)
        {
            AdoNetAppender appender = new AdoNetAppender();
            appender.Name = "AdoNetAppender";
            appender.BufferSize = 1;
            appender.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            appender.ConnectionString = cs;
            appender.CommandText = @"INSERT INTO LogNet 
([DateUtc],[Thread],[Level],[Logger],[User],[Message],[Exception]) VALUES 
(@date,@appcode,@thread,@level,@logger,@user,@message,@exception)";
            AddDateTimeParameterToAppender(appender, "date");
            AddStringParameterToAppender(appender, "thread", 20, "%thread");
            AddStringParameterToAppender(appender, "level", 10, "%level");
            AddStringParameterToAppender(appender, "logger", 200, "%logger");
            AddStringParameterToAppender(appender, "user", 20, "%property{user}");
            AddStringParameterToAppender(appender, "message", 1000,
            "%message%newline%property");
            AddErrorParameterToAppender(appender, "exception", 4000);
            appender.ActivateOptions();
            return appender;
        }

        public static void AddStringParameterToAppender(this log4net.Appender.AdoNetAppender appender, string paramName, int size, string conversionPattern)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = System.Data.DbType.String;
            param.Size = size;
            param.Layout = new Layout2RawLayoutAdapter(new PatternLayout(conversionPattern));
            appender.AddParameter(param);
        }
        public static void AddDateTimeParameterToAppender(this log4net.Appender.AdoNetAppender appender, string paramName)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = System.Data.DbType.DateTime;
            param.Layout = new RawUtcTimeStampLayout();
            appender.AddParameter(param);
        }

        public static void AddErrorParameterToAppender(this log4net.Appender.AdoNetAppender appender, string paramName, int size)
        {
            AdoNetAppenderParameter param = new AdoNetAppenderParameter();
            param.ParameterName = paramName;
            param.DbType = System.Data.DbType.String;
            param.Size = size;
            param.Layout = new Layout2RawLayoutAdapter(new ExceptionLayout());
            appender.AddParameter(param);
        }




    }
}
