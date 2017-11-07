using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xave.Framework.Base.Config;

namespace Xave.Framework.Base.Data
{
    public sealed class DatabaseConfiguration : BaseConfigElement
    {
        #region Constants

        /// <summary>
        /// KEY_CONSTR = connectionString
        /// </summary>
        private const string KEY_CONSTR = "connectionString";
        /// <summary>
        /// KEY_DBTYPE = databaseType
        /// </summary>
        private const string KEY_DBTYPE = "databaseType";

        #endregion

        #region Properties

        /// <summary>
        /// 연결 문자열 정보를 가져오거나 설정합니다.
        /// </summary>
        [ConfigurationProperty(KEY_CONSTR, DefaultValue = "")]
        public string ConnectionString
        {
            get { return (string)this[KEY_CONSTR]; }
            set { this[KEY_CONSTR] = value; }
        }

        /// <summary>
        /// 데이터베이스 타입 정보를 가져오거나 설정합니다.
        /// </summary>
        [ConfigurationProperty(KEY_DBTYPE, DefaultValue = DatabaseType.SQL)]
        public DatabaseType DatabaseType
        {
            get { return (DatabaseType)this[KEY_DBTYPE]; }
            set { this[KEY_DBTYPE] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 기본 생성자입니다.
        /// </summary>
        public DatabaseConfiguration()
            : base()
        {
        }

        /// <summary>
        /// 이름을 갖고 생성을 합니다.
        /// </summary>
        /// <param name="name">연결 문자열 정보를 구분할 수 있는 이름</param>
        public DatabaseConfiguration(string name)
            : base(name)
        {
        }

        #endregion
    }
}
