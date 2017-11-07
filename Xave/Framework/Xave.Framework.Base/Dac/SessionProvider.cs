using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xave.Framework.Base
{
    internal class SessionProvider
    {
        #region Member

        /// <summary>
        /// 물리적인 Database 유형 입니다.
        /// </summary>
        protected OrmDbType _dbType = OrmDbType.DEFAULT;

        /// <summary>
        /// 정보교류 시스템 접속을 위한 System 유형 입니다.
        /// </summary>
        protected DBEnum _destination = DBEnum.DEFAULT;

        /// <summary>
        /// DBEnum 이 USER_DEFINE 인 경우 제공받은 사용자정의 DB 명 입니다.
        /// </summary>
        protected string _userDefineDBName = "";

        /// <summary>
        /// 물리적인 Database 접속을 위한 연결 문자열 입니다.
        /// </summary>
        protected string _connectionString = "";

        /// <summary>
        /// 스레드 트랜잭션의 클래스 ID 입니다.
        /// </summary>
        protected string _transactionClassId = "";

        /// <summary>
        /// OR-Mapping 을 위한 hbm 매핑 파일 의 상위 디렉토리 경로 입니다.
        /// </summary>
        protected string _mappingFilesPath = "";

        /// <summary>
        /// NHibernate.Cfg.Environment.ReleaseConnections 대응 Type 입니다.
        /// <para>
        /// AUTO (after_transaction, auto) : Transaction이 종료된 후에 Connection를 종료하며, Transaction 이 없다면 tatement가 실행된 후에 종료합니다.
        /// </para>
        /// <para>
        /// CLOSE (on_close) : Session이 Dispose된 후에 Connection를 종료합니다.
        /// </para>
        /// </summary>
        protected ReleaseConnectionsMode _connCloseMode = ReleaseConnectionsMode.AUTO;
        #endregion

        #region read-safe, lazy loading Singleton class
        /// <summary>
        /// NHibernate.ISessionFactory 에 Access 할 속성을 제공합니다.
        /// </summary>
        private class Nested
        {
            /// <summary>
            /// Thread-safe, lazy loading Singleton 클래스를 초기화 합니다.
            /// </summary>
            static Nested()
            {
            }
            #region 전역 NHibernate.ISessionFactory
            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _DefaultFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _ATNAFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _FHIRFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _MPIFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _PORTALFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _REGISTRYFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _REPOSITORYFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _DSUBFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _CDAFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _RESULTMONITORFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory 속성 입니다.
            /// </summary>
            internal static NHibernate.ISessionFactory _HIELOGFactory;

            /// <summary>
            /// 전역 NHibernate.ISessionFactory HashTable 속성 입니다.
            /// </summary>
            internal static System.Collections.Hashtable _USER_DEFINEFactoryHash = new System.Collections.Hashtable();
            #endregion
        }
        #endregion

        #region Constructor
        /// <summary>
        /// ISessionFactory 를 초기화 합니다.
        /// </summary>
        protected void InitSessionFactory()
        {
            if (SessionFactory == null)
            {
                NHibernate.Cfg.Configuration cfg = SetDatabaseConfig();

                System.IO.DirectoryInfo dir = GetMappingDir(_mappingFilesPath);
                cfg.AddDirectory(dir);

                SessionFactory = cfg.BuildSessionFactory();
            }
        }

        #endregion

        #region Property
        /// <summary>
        /// HttpContext 의 사용 여부를 가져오거나 설정 합니다.
        /// </summary>
        /// <returns></returns>
        private static bool IsInWebContext
        {
            get
            {
                return HttpContext.Current != null;
            }
        }

        /// <summary>
        /// 스레드 트랜잭션의 클래스 ID 를 설정 합니다.
        /// </summary>
        public void CreateNewClassID()
        {
            this._transactionClassId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 스레드 트랜잭션의 key 를 가져옵니다.
        /// </summary>
        private string TRANSACTION_KEY
        {
            get
            {
                if (this._destination != DBEnum.USER_DEFINE)
                    return this._destination.ToString() + "_" + this._transactionClassId + "_CONTEXT_TRANSACTION";
                else
                    return this._destination.ToString() + "_" + this._userDefineDBName + "_" + this._transactionClassId + "_CONTEXT_TRANSACTION";
            }
        }

        /// <summary>
        /// Stateless 스레드 트랜잭션의 key 를 가져옵니다.
        /// </summary>
        private string TRANSACTION_STATELESS_KEY
        {
            get
            {
                if (this._destination != DBEnum.USER_DEFINE)
                    return this._destination.ToString() + "_CONTEXT_STATELESS_TRANSACTION";
                else
                    return this._destination.ToString() + "_" + this._userDefineDBName + "_CONTEXT_STATELESS_TRANSACTION";
            }
        }

        /// <summary>
        /// 스레드 세션의 key 를 가져옵니다.
        /// </summary>
        private string SESSION_KEY
        {
            get
            {
                if (this._destination != DBEnum.USER_DEFINE)
                    return this._destination.ToString() + "CONTEXT_SESSION";
                else
                    return this._destination.ToString() + "_" + this._userDefineDBName + "_CONTEXT_SESSION";
            }
        }

        /// <summary>
        /// Stateless 스레드 세션의 key 를 가져옵니다.
        /// </summary>
        private string STATELESS_SESSION_KEY
        {
            get
            {
                if (this._destination != DBEnum.USER_DEFINE)
                    return this._destination.ToString() + "CONTEXT_STATELESS_SESSION";
                else
                    return this._destination.ToString() + "_" + this._userDefineDBName + "_CONTEXT_STATELESS_SESSION";
            }
        }

        /// <summary>
        /// 지정된 시스템에 해당하는 전역 ISessionFactory 를 반환 합니다.
        /// </summary>
        private ISessionFactory SessionFactory
        {
            get
            {
                ISessionFactory fac = null;

                #region 시스템 유형 별 ISessionFactory
                if (this._destination == DBEnum.DEFAULT)
                    fac = Nested._DefaultFactory;
                else if (this._destination == DBEnum.ATNA)
                    fac = Nested._ATNAFactory;
                else if (this._destination == DBEnum.FHIR)
                    fac = Nested._FHIRFactory;
                else if (this._destination == DBEnum.MPI)
                    fac = Nested._MPIFactory;
                else if (this._destination == DBEnum.PORTAL)
                    fac = Nested._PORTALFactory;
                else if (this._destination == DBEnum.REGISTRY)
                    fac = Nested._REGISTRYFactory;
                else if (this._destination == DBEnum.REPOSITORY)
                    fac = Nested._REPOSITORYFactory;
                else if (this._destination == DBEnum.DSUB)
                    fac = Nested._DSUBFactory;
                else if (this._destination == DBEnum.CDA)
                    fac = Nested._CDAFactory;
                else if (this._destination == DBEnum.HIE_LOG)
                    fac = Nested._HIELOGFactory;
                else if (this._destination == DBEnum.RESULT_ANALYSIS)
                    fac = Nested._RESULTMONITORFactory;
                else if (this._destination == DBEnum.USER_DEFINE)
                {
                    fac = (ISessionFactory)Nested._USER_DEFINEFactoryHash[this._userDefineDBName];
                }
                #endregion

                return fac;
            }
            set
            {
                #region 시스템 유형 별 ISessionFactory
                if (this._destination == DBEnum.DEFAULT)
                    Nested._DefaultFactory = value;
                else if (this._destination == DBEnum.ATNA)
                    Nested._ATNAFactory = value;
                else if (this._destination == DBEnum.FHIR)
                    Nested._FHIRFactory = value;
                else if (this._destination == DBEnum.MPI)
                    Nested._MPIFactory = value;
                else if (this._destination == DBEnum.PORTAL)
                    Nested._PORTALFactory = value;
                else if (this._destination == DBEnum.REGISTRY)
                    Nested._REGISTRYFactory = value;
                else if (this._destination == DBEnum.REPOSITORY)
                    Nested._REPOSITORYFactory = value;
                else if (this._destination == DBEnum.DSUB)
                    Nested._DSUBFactory = value;
                else if (this._destination == DBEnum.CDA)
                    Nested._CDAFactory = value;
                else if (this._destination == DBEnum.HIE_LOG)
                    Nested._HIELOGFactory = value;
                else if (this._destination == DBEnum.RESULT_ANALYSIS)
                    Nested._RESULTMONITORFactory = value;
                else if (this._destination == DBEnum.USER_DEFINE)
                {
                    Nested._USER_DEFINEFactoryHash[this._userDefineDBName] = value;
                }
                #endregion
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// 제공된 경로의 물리적인 System.IO.DirectoryInfo 를 반환 합니다.
        /// </summary>
        /// <param name="path">Nhibernate 매핑을 위한 절대 경로 또는 상대 경로 입니다.
        /// <para>
        ///    1. IIS 에서 실행되는 경우 : "C:\{...}\CurrentDir\", "/{....}/CurrentDir/", "./{....}/CurrentDir/", "CurrentDir/", "\{...}\CurrentDir\" 등의 구성 가능
        /// </para>
        /// <para>
        ///    2. Application 에서 실행 할 경우 : "C:\{...}\CurrentDir\", "/{....}/CurrentDir/", "\{...}\CurrentDir\" 등의 구성 가능
        /// </para>
        /// </param>
        /// <returns>지정된 Database 에 접속하기 위한 hbm file 이 등록된 최 상위 경로 입니다.</returns>
        private System.IO.DirectoryInfo GetMappingDir(string path)
        {
            System.IO.DirectoryInfo dir = null;

            if (HttpContext.Current != null)
            {
                try
                {
                    string dirPath = HttpContext.Current.Server.MapPath(path);
                    dir = new System.IO.DirectoryInfo(dirPath);
                }
                catch
                {
                    dir = new System.IO.DirectoryInfo(path);
                }
            }
            else
            {
                try
                {
                    Uri uri = new Uri(path, UriKind.Absolute);
                    dir = new System.IO.DirectoryInfo(uri.AbsolutePath);
                    if (dir.Exists)
                        return dir;
                }
                catch
                {
                    string dirPath = System.Reflection.Assembly.GetExecutingAssembly().Location + path;
                    dir = new System.IO.DirectoryInfo(dirPath);
                    if (dir.Exists)
                        return dir;
                    else
                    {
                        dir = new System.IO.DirectoryInfo(path);
                        if (dir.Exists)
                            return dir;
                    }
                }
            }
            return dir;
        }

        /// <summary>
        /// 물리적인 데이터베이스 유형에 따라 Configuration 정보를 변경 하여 반환 합니다.
        /// </summary>
        /// <returns>데이터베이스 유형이 적용된 Configuration 입니다.</returns>
        protected NHibernate.Cfg.Configuration SetDatabaseConfig()
        {
            NHibernate.Cfg.Configuration cfg = new Configuration();

            #region 모든 DB 를 지원할 경우 이 주석을 제거 하여 재구성 합니다.
            /* 모든 DB 를 지원할 경우 이 주석을 제거 하여 재구성 합니다.
            switch (_dbType)
            {

                #region : ormDbType 에 따라 DB 연결 속성을 변경 합니다.
                
                case OrmDbType.DB2_400:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.DB2400Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.DB2400Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.DB2:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.DB2Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.DB2Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.FIRE_BIRD:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.FirebirdClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.FirebirdDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.INFORMIX:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.IfxDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.InformixDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.INFORMIX_0940:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.IfxDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.InformixDialect0940).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.INFOMIX_1000:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.IfxDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.InformixDialect1000).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.INGRES:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.IngresDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.IngresDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.INGRES_9:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.IngresDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.Ingres9Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MDB:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.JetDriver.JetDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.JetDriver.JetDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MY_SQL:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQLDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MY_SQL55:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQL55Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MY_SQL55_INNO:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQL55InnoDBDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MY_SQL5:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQL5Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.MY_SQL5_INNO:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQL5InnoDBDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.ORACLE_10g:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.OracleDataClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.Oracle10gDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.ORACLE_9i:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.OracleDataClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.Oracle9iDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.ORACLE_8i:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.OracleDataClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.Oracle8iDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.ORACLE_LITE:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.OracleDataClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.OracleLiteDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                //case OrmDbType.POSTGRE_SQL:
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                //    break;
                //case OrmDbType.POSTGRE_SQL81:
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                //    break;
                //case OrmDbType.POSTGRE_SQL82:
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                //    break;
                case OrmDbType.SQL_2000:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2000Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_2005:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2005Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_2008:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2008Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_2012:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2012Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_7:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql7Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_AZURE2008:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSqlAzure2008Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_CE40:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlServerCeDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSqlCe40Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_CE:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlServerCeDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSqlCeDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SQL_LITE:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SQLite20Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SQLiteDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;

                case OrmDbType.SYBASE_ASA9:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SybaseAsaClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SybaseASA9Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SYBASE_ASE15:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SybaseAsaClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SybaseASE15Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SYBASE_SQL_ANY10:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SybaseSQLAnywhereDotNet4Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SybaseSQLAnywhere10Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SYBASE_SQL_ANY11:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SybaseSQLAnywhereDotNet4Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SybaseSQLAnywhere11Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.SYBASE_SQL_ANY12:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SybaseSQLAnywhereDotNet4Driver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.SybaseSQLAnywhere12Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
                case OrmDbType.DEFAULT:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2012Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    break;
               
                #endregion
            } 
             */
            #endregion

            if (this._connCloseMode == ReleaseConnectionsMode.AUTO)
                cfg.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "auto");
            else if (this._connCloseMode == ReleaseConnectionsMode.CLOSE)
                cfg.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close");

            switch (this._dbType)
            {
                #region : ormDbType 에 따라 DB 연결 속성을 변경 합니다.
                case OrmDbType.DEFAULT:
                case OrmDbType.SQL:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MsSql2012Dialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                    break;
                case OrmDbType.ORACLE:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.OracleDataClientDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.Oracle10gDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");

                    cfg.SetProperty(NHibernate.Cfg.Environment.UseQueryCache, "false");
                    cfg.SetProperty(NHibernate.Cfg.Environment.BatchSize, "20");
                    cfg.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, "60");
                    break;
                case OrmDbType.MY_SQL:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.MySQLDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                    break;
                case OrmDbType.POSTGRE_SQL:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.NpgsqlDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.PostgreSQLDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                    break;
                case OrmDbType.CUBRID:
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.Driver.CUBRIDDriver).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.Dialect.CUBRIDDialect).AssemblyQualifiedName);
                    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                    break;
                //case OrmDbType.MDB:
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(NHibernate.JetDriver.JetDriver).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(NHibernate.JetDriver.JetDialect).AssemblyQualifiedName);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this._connectionString);
                //    cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                //    cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                //    break;

                #endregion
            }
            return cfg;
        }

        #region ISession : 일반적인 DB Access 를 담당하는 Session 입니다.
        /// <summary>
        /// 새로운 세션에 인터셉터를 등록 합니다. 
        /// 이미 HttpContext에 attach 된 오픈 세션이있는 경우,이 호출되지 않을 가능성이 있습니다.
        /// 사용되는 인터셉터가 있는 경우, BeginTransaction() 을 호출하기 전에 이를 호출하기 위해 HttpModule을 변경 해야 합니다.
        /// </summary>
        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = ThreadSession;

            if (session != null && session.IsOpen)
            {
                throw new CacheException("세션이 이미 Open 되어 있으므로 인터셉터를 등록 할 수 없습니다.");
            }

            GetSession(interceptor);
        }

        /// <summary>
        /// Singleton 세션을 반환 합니다.
        /// </summary>
        /// <returns>Singleton 세션 입니다.</returns>
        public ISession GetSession()
        {
            return GetSession(null);
        }

        /// <summary>
        /// Non Singleton 세션을 반환 합니다.
        /// </summary>
        /// <returns>Non Singleton 세션 입니다.</returns>
        public ISession GetNonSingletonSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// 세션을 인터셉터 와 함께 또는 인터셉터 없이 반환 합니다.
        /// 이 메소드는 직접 호출되지 않고 별도의 메소드로 호출 됩니다.
        /// </summary>
        protected ISession GetSession(IInterceptor interceptor)
        {
            ISession session = ThreadSession;

            if (session == null)
            {
                InitSessionFactory();
                if (interceptor != null)
                {
                    session = SessionFactory.OpenSession(interceptor);
                }
                else
                {
                    session = SessionFactory.OpenSession();
                }

                ThreadSession = session;
            }
            else
            {
                if (!session.IsConnected)
                {
                    InitSessionFactory();
                    if (interceptor != null)
                    {
                        session = SessionFactory.OpenSession(interceptor);
                    }
                    else
                    {
                        session = SessionFactory.OpenSession();
                    }
                    ThreadSession = session;
                }
            }

            return session;
        }

        /// <summary>
        /// 세션을 닫습니다.
        /// </summary>
        public void CloseSession()
        {
            ISession session = ThreadSession;
            ThreadSession = null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }
        }


        //public void ClearSession()
        //{
        //    ISession session = ThreadSession;

        //    if (session != null)
        //        session.Close();
        //}

        /// <summary>
        /// 스레드 세션 인터페이스를 가져오거나 설정 합니다.
        /// <para>Web Context 내에서 사용되는 경우 &lt;see cref="HttpContext"&gt; 대신 &lt;see cref="CallContext"&gt; 를 사용 합니다.</para>
        /// </summary>
        protected ISession ThreadSession
        {
            get
            {
                if (IsInWebContext)
                {
                    //try
                    //{
                    //    return (ISession)HttpContext.Current.Application[SESSION_KEY];
                    //}
                    //catch
                    //{
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                    //}
                }
                else
                {
                    return (ISession)CallContext.GetData(SESSION_KEY);
                }
            }
            set
            {
                if (IsInWebContext)
                {
                    //try
                    //{
                    //    HttpContext.Current.Application[SESSION_KEY] = value;
                    //}
                    //catch
                    //{
                    HttpContext.Current.Items[SESSION_KEY] = value;
                    //}
                }
                else
                {
                    CallContext.SetData(SESSION_KEY, value);
                }
            }
        }

        #endregion

        #region IStatelessSession : Batch Service 등, 다량의 일괄 데이터 처리 에 필요한 DB Access 를 담당하는 Session 입니다.
        /// <summary>
        /// 새로운 세션에 인터셉터를 등록 합니다. 
        /// 이미 HttpContext에 attach 된 오픈 세션이있는 경우,이 호출되지 않을 가능성이 있습니다.
        /// 사용되는 인터셉터가 있는 경우, BeginTransaction() 을 호출하기 전에 이를 호출하기 위해 HttpModule을 변경 해야 합니다.
        /// </summary>
        public void RegisterStatelessInterceptor(IInterceptor interceptor)
        {
            IStatelessSession session = ThreadStatelessSession;

            if (session != null && session.IsOpen)
            {
                throw new CacheException("Stateless 세션이 이미 Open 되어 있으므로 인터셉터를 등록 할 수 없습니다.");
            }

            GetSession(interceptor);
        }

        /// <summary>
        /// IStateless Singleton 세션을 반환 합니다.
        /// </summary>
        /// <returns>IStateless Singleton 세션 입니다.</returns>
        public IStatelessSession GetStatelessSession()
        {
            return GetStatelessSession(null);
        }

        /// <summary>
        /// IStateless Non Singleton 세션을 반환 합니다.
        /// </summary>
        /// <returns>IStateless Non Singleton 세션 입니다.</returns>
        public IStatelessSession GetNonSingletonStatelessSession()
        {
            return SessionFactory.OpenStatelessSession();
        }

        /// <summary>
        /// Stateless 세션을 인터셉터 와 함께 또는 인터셉터 없이 반환 합니다.
        /// 이 메소드는 직접 호출되지 않고 별도의 메소드로 호출 됩니다.
        /// </summary>
        protected IStatelessSession GetStatelessSession(IInterceptor interceptor)
        {
            IStatelessSession session = ThreadStatelessSession;

            if (session == null)
            {
                if (interceptor != null)
                {
                    //OpenStatelessSession 에는 Interceptor 를 지원하지 않음.
                    session = SessionFactory.OpenStatelessSession();
                }
                else
                {
                    session = SessionFactory.OpenStatelessSession();
                }

                ThreadStatelessSession = session;
            }

            return session;
        }

        /// <summary>
        /// IStatelessSession 세션을 닫습니다.
        /// </summary>
        public void CloseStatelessSession()
        {
            IStatelessSession session = ThreadStatelessSession;
            ThreadStatelessSession = null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }
        }

        /// <summary>
        /// IStatelessSession 스레드 세션 인터페이스를 가져오거나 설정 합니다.
        /// <para>Web Context 내에서 사용되는 경우 &lt;see cref="HttpContext"&gt; 대신 &lt;see cref="CallContext"&gt; 를 사용 합니다.</para>
        /// </summary>
        protected IStatelessSession ThreadStatelessSession
        {
            get
            {
                if (IsInWebContext)
                {
                    return (IStatelessSession)HttpContext.Current.Items[STATELESS_SESSION_KEY];
                }
                else
                {
                    return (IStatelessSession)CallContext.GetData(STATELESS_SESSION_KEY);
                }
            }
            set
            {
                if (IsInWebContext)
                {
                    HttpContext.Current.Items[STATELESS_SESSION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(STATELESS_SESSION_KEY, value);
                }
            }
        }

        #endregion

        #region NHibernate Transaction : ISession
        /// <summary>
        /// 트랜잭션을 시작 합니다.
        /// </summary>
        public void BeginTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                ThreadTransaction = transaction;
            }
        }

        /// <summary>
        /// 트랜잭션을 Commit 합니다.
        /// </summary>
        public void CommitTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Commit();
                    ThreadTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 트랜잭션을 롤백 합니다.
        /// </summary>
        public void RollbackTransaction()
        {
            ITransaction transaction = ThreadTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Rollback();
                    ThreadTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// 스레드 트랜잭션 인터페이스를 가져오거나 설정 합니다.
        /// <para>Web Context 내에서 사용되는 경우 &lt;see cref="HttpContext"&gt; 대신 &lt;see cref="CallContext"&gt; 를 사용 합니다.</para>
        /// </summary>
        protected ITransaction ThreadTransaction
        {
            get
            {
                if (IsInWebContext)
                {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTION_KEY];
                }
                else
                {
                    return (ITransaction)CallContext.GetData(TRANSACTION_KEY);
                }
            }
            set
            {
                if (IsInWebContext)
                {
                    HttpContext.Current.Items[TRANSACTION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(TRANSACTION_KEY, value);
                }
            }
        }
        #endregion

        #region NHibernate Transaction : IStatelessSession
        /// <summary>
        /// Stateless 트랜잭션을 시작 합니다.
        /// </summary>
        public void BeginTransaction_Stateless()
        {
            ITransaction transaction = ThreadStatelessTransaction;

            if (transaction == null)
            {
                transaction = GetStatelessSession().BeginTransaction();
                ThreadStatelessTransaction = transaction;
            }
        }

        /// <summary>
        /// Stateless 트랜잭션을 Commit 합니다.
        /// </summary>
        public void CommitTransaction_Stateless()
        {
            ITransaction transaction = ThreadStatelessTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Commit();
                    ThreadStatelessTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                RollbackTransaction_Stateless();
                throw ex;
            }
        }

        /// <summary>
        /// Stateless 트랜잭션을 롤백 합니다.
        /// </summary>
        public void RollbackTransaction_Stateless()
        {
            ITransaction transaction = ThreadStatelessTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Rollback();
                    ThreadStatelessTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                throw ex;
            }
            finally
            {
                CloseStatelessSession();
            }
        }

        /// <summary>
        /// Stateless 스레드 트랜잭션 인터페이스를 가져오거나 설정 합니다.
        /// <para>Web Context 내에서 사용되는 경우 &lt;see cref="HttpContext"&gt; 대신 &lt;see cref="CallContext"&gt; 를 사용 합니다.</para>
        /// </summary>
        protected ITransaction ThreadStatelessTransaction
        {
            get
            {
                if (IsInWebContext)
                {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTION_STATELESS_KEY];
                }
                else
                {
                    return (ITransaction)CallContext.GetData(TRANSACTION_STATELESS_KEY);
                }
            }
            set
            {
                if (IsInWebContext)
                {
                    HttpContext.Current.Items[TRANSACTION_STATELESS_KEY] = value;
                }
                else
                {
                    CallContext.SetData(TRANSACTION_STATELESS_KEY, value);
                }
            }
        }
        #endregion

        #endregion

    }
}
