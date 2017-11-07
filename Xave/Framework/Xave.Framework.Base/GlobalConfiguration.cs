using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xave.Framework.Base.Data;
using Xave.Framework.Base.Orm;

namespace Xave.Framework.Base
{
    public class GlobalConfiguration : BaseConfigSection
    {
        private static GlobalConfiguration _gconfig;
        private static object _mutex = new object();

        /// <summary>
        /// 데이터베이스 정보를 가져오거나 설정합니다.
        /// </summary>
        [ConfigurationProperty("DatabaseGroup")]
        public DatabaseConfigurationGroup DatabaseGroup
        {
            get { return (DatabaseConfigurationGroup)this["DatabaseGroup"]; }
            set { this["DatabaseGroup"] = value; }
        }

        /// <summary>
        /// NHibernate ORM 데이터베이스 정보를 가져오거나 설정합니다.
        /// </summary>
        [ConfigurationProperty("OrmDBGroup")]
        public OrmConfigurationGroup OrmDBGroup
        {
            get { return (OrmConfigurationGroup)this["OrmDBGroup"]; }
            set { this["OrmDBGroup"] = value; }
        }

        ///// <summary>
        ///// 로그 정보를 가져오거나 설정합니다.
        ///// </summary>
        //[ConfigurationProperty("LogGroup")]
        //public LogConfigurationGroup LogGroup
        //{
        //    get { return (LogConfigurationGroup)this["LogGroup"]; }
        //    set { this["LogGroup"] = value; }
        //}

        ///// <summary>
        ///// 암호화 정보를 가져오거나 설정합니다.
        ///// </summary>
        //[ConfigurationProperty("CryptoGroup")]
        //public CryptoConfigurationGroup CryptoGroup
        //{
        //    get { return (CryptoConfigurationGroup)this["CryptoGroup"]; }
        //    set { this["CryptoGroup"] = value; }
        //}

        /// <summary>
        /// 환경 설정 구성 정보를 반환합니다.
        /// </summary>    
        /// <returns><c>GlobalConfiguration</c> 인스턴스</returns>
        public static GlobalConfiguration GetConfig()
        {
            if (_gconfig == null)
            {
                lock (_mutex)
                {
                    if (_gconfig == null)
                        _gconfig = ConfigurationManager.GetSection("GlobalSection") as GlobalConfiguration;

                    log4net.Config.XmlConfigurator.Configure();
                }
            }

            return _gconfig;
        }


    }
}
