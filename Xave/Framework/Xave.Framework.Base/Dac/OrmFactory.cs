using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xave.Framework.Base
{
    internal class DatabaseFactory : SessionProvider
    {
        #region Member
        /// <summary>
        /// config 정보를 수집 합니다.
        /// </summary>
        protected GlobalConfiguration _config = null;

        #endregion

        #region Thread-safe, lazy loading Singleton

        /// <summary>
        /// Thread 에 안전한 lazy loading Singleton 방식의 ORM Instance 를 반환 합니다.
        /// </summary>
        /// <param name="dataSourceName">적용될 Datasource 입니다.</param>
        /// <returns>Singleton 방식의 ORM Instance 입니다.</returns>
        public static DatabaseFactory Instance(string dataSourceName)
        {
            return Nested.GetOrmFactory(dataSourceName);
        }

        /// <summary>
        /// Non Singleton 방식의 ORM Instance 를 반환 합니다.
        /// </summary>
        /// <param name="dataSourceName">적용될 Datasource 입니다.</param>
        /// <returns>Non Singleton 방식의 ORM Instance 입니다.</returns>
        public static DatabaseFactory NonSingletoneInstance(string dataSourceName)
        {
            return new DatabaseFactory(dataSourceName);
        }

        /// <summary>
        /// NHibernate session factory 를 생성합니다.
        /// </summary>
        /// <param name="dataSourceName">적용될 Datasource 입니다.</param>
        private DatabaseFactory(string databaseName)
        {
            _config = GlobalConfiguration.GetConfig();
            this._destination = DBEnum.USER_DEFINE;
            try
            {
                if (_config.OrmDBGroup.Databases[databaseName] != null)
                {
                    OrmConfiguration config = _config.OrmDBGroup.Databases[databaseName] as OrmConfiguration;
                    this._userDefineDBName = databaseName;
                    this._dbType = config.DatabaseType;
                    this._connectionString = config.ConnectionString;
                    this._mappingFilesPath = config.HbmMappingFilesDir;
                    this._connCloseMode = config.ConnectionCloseMode;
                    InitSessionFactory();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Thread-safe, lazy loading Singleton 클래스 입니다.
        /// </summary>
        private class Nested
        {
            public static string DBName = "";
            /// <summary>
            /// Thread-safe, lazy loading Singleton 클래스를 초기화 합니다.
            /// </summary>
            static Nested()
            {
            }

            /// <summary>
            /// Thread-safe, lazy loading Singleton ORM Instance 를 관리하는 hashTable 입니다.
            /// </summary>
            public static readonly System.Collections.Hashtable Hash = new System.Collections.Hashtable();

            /// <summary>
            /// hashTable 로부터 지정된 ORM Instance 를 반환 합니다.
            /// </summary>
            /// <param name="dataSourceName">지정된 dataSource 입니다.</param>
            /// <returns>Singleton ORM Instance 입니다.</returns>
            public static DatabaseFactory GetOrmFactory(string databaseName)
            {
                if (Hash.ContainsKey(databaseName))
                    return Hash[databaseName] as DatabaseFactory;
                else
                {
                    DBName = databaseName;
                    Hash.Add(databaseName, new DatabaseFactory(databaseName));
                    return Hash[databaseName] as DatabaseFactory;
                }
            }

            /// <summary>
            /// 현재 커넥션을 닫습니다.
            /// </summary>
            internal static void Close()
            {
                if (Hash.ContainsKey(DBName))
                    (Hash[DBName] as DatabaseFactory).CloseSession();
            }
        }

        /// <summary>
        /// 현재 커넥션을 닫습니다.
        /// </summary>
        public static void Close()
        {
            Nested.Close();
        }

        #endregion
    }
}
