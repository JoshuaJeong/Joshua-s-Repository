﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xave.Framework.Base.Config;

namespace Xave.Framework.Base.Data
{
    public sealed class DatabaseConfigurationGroup : BaseConfigElement
    {
        #region Constants

        /// <summary>
        /// KEY_DEFDBNAME = defaultDatabaseName
        /// </summary>
        private const string KEY_DEFDBNAME = "defaultDatabaseName";
        /// <summary>
        /// KEY_DBS = Databases
        /// </summary>
        private const string KEY_DBS = "Databases";

        #endregion

        #region Properties

        /// <summary>
        /// 데이터베이스 기본 선택값을 가져옵니다.
        /// <remarks>
        /// 구성 요소에서 <c>defaultDatabaseName</c> 속성값과 동일합니다.
        /// </remarks>
        /// </summary>
        [ConfigurationProperty(KEY_DEFDBNAME)]
        public string DefaultDatabaseName
        {
            get { return (string)this[KEY_DEFDBNAME]; }
            set { this[KEY_DEFDBNAME] = value; }
        }

        /// <summary>
        /// 데이터베이스 설정 아이템을 가져옵니다.
        /// <remarks>
        /// 구성 요소에서 <c>Databases</c> 요소와 동일합니다.
        /// </remarks>
        /// </summary>
        [ConfigurationProperty(KEY_DBS)]
        public BaseConfigCollection<DatabaseConfiguration> Databases
        {
            get { return (BaseConfigCollection<DatabaseConfiguration>)this[KEY_DBS]; }
            set { this[KEY_DBS] = value; }
        }

        #endregion
    }
}
