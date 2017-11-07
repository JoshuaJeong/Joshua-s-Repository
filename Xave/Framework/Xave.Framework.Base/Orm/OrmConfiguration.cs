using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xave.Framework.Base.Config;

namespace Xave.Framework.Base.Orm
{
    public sealed class OrmConfiguration : BaseConfigElement
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
        /// <summary>
        /// KEY_MAPDIR = hbmMappingFilesDir
        /// </summary>
        private const string KEY_MAPDIR = "hbmMappingFilesDir";
        /// <summary>
        /// RELEASE_MODE = Mode(NHibernate.Cfg.Environment.ReleaseConnections)
        /// </summary>
        private const string RELEASE_MODE = "CloseMode";
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
        [ConfigurationProperty(KEY_DBTYPE, DefaultValue = OrmDbType.SQL)]
        public OrmDbType DatabaseType
        {
            get { return (OrmDbType)this[KEY_DBTYPE]; }
            set { this[KEY_DBTYPE] = value; }
        }

        /// <summary>
        /// 해당 데이터베이스와 연결될 매핑파일 root 경로 입니다.
        /// </summary>
        [ConfigurationProperty(KEY_MAPDIR, DefaultValue = "")]
        public string HbmMappingFilesDir
        {
            get { return (string)this[KEY_MAPDIR]; }
            set { this[KEY_MAPDIR] = value; }
        }

        /// <summary>
        /// NHibernate.Cfg.Environment.ReleaseConnections 대응 Type 정보를 가져오거나 설정 합니다.
        /// <para>
        /// AUTO (after_transaction, auto) : Transaction이 종료된 후에 Connection를 종료하며, Transaction 이 없다면 tatement가 실행된 후에 종료합니다.
        /// </para>
        /// <para>
        /// CLOSE (on_close) : Session이 Dispose된 후에 Connection를 종료합니다.
        /// </para>
        /// </summary>
        [ConfigurationProperty(RELEASE_MODE, DefaultValue = ReleaseConnectionsMode.AUTO)]
        public ReleaseConnectionsMode ConnectionCloseMode
        {
            get { return (ReleaseConnectionsMode)this[RELEASE_MODE]; }
            set { this[RELEASE_MODE] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 기본 생성자입니다.
        /// </summary>
        public OrmConfiguration()
            : base()
        {
        }

        /// <summary>
        /// 이름을 갖고 생성을 합니다.
        /// </summary>
        /// <param name="name">연결 문자열 정보를 구분할 수 있는 이름</param>
        public OrmConfiguration(string name)
            : base(name)
        {
        }

        #endregion
    }
}
