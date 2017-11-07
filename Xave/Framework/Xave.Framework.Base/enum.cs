using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xave.Framework.Base
{
    /// <summary>
    /// 관계형 Database 에 액세스 할 Database 의 Type 입니다.
    /// </summary>
    public enum DatabaseType : int
    {
        /// <summary>
        /// Sql Database 에 Access 합니다.
        /// </summary>
        SQL = 1,
        /// <summary>
        /// Oracle Database 에 Access 합니다.
        /// </summary>
        ORACLE = 2,
        /// <summary>
        /// Ole Database 에 Access 합니다.
        /// </summary>
        OLE = 3,
        /// <summary>
        /// Odbc Database 에 Access 합니다.
        /// </summary>
        ODBC = 4,
        /// <summary>
        /// MySQL Database 에 Access 합니다.
        /// </summary>
        MY_SQL = 5,
        /// <summary>
        /// PostgresSQL Database 에 Access 합니다.
        /// </summary>
        POSTGRES = 6,
        /// <summary>
        /// CUBRID Database 에 Access 합니다.
        /// </summary>
        CUBRID = 10,
        /// <summary>
        /// default Database 에 Access 합니다.
        /// default Database 는  SQL 로 설정 됩니다.
        /// </summary>
        DEFAULT = 99
    }

    /// <summary>
    /// DataObjectItem 의 정의된 Type 입니다.
    /// </summary>
    [Serializable]
    [Obfuscation(Feature = "renaming", Exclude = true)]
    public enum DataItemPackMemberType : int
    {
        /// <summary>
        /// DataObjectItem 의 int32, Uint32 Type 입니다.
        /// </summary>
        INT = 1,
        /// <summary>
        /// DataObjectItem 의 Byte ,SByte Type 입니다.
        /// </summary>
        TinyINT = 2,
        /// <summary>
        /// DataObjectItem 의 Int16, UInt16 Type 입니다.
        /// </summary>
        SmallINT = 3,
        /// <summary>
        /// DataObjectItem 의 Int64, UInt64 Type 입니다.
        /// </summary>
        BigINT = 4,
        /// <summary>
        /// DataObjectItem 의 Decimal Type 입니다.
        /// </summary>
        DECIMAL = 5,
        /// <summary>
        /// DataObjectItem 의 Char Type 입니다.
        /// </summary>
        CHAR = 6,
        /// <summary>
        /// DataObjectItem 의 String Type 입니다.
        /// </summary>
        VARCHAR = 7,
        /// <summary>
        /// DataObjectItem 의 Double Type 입니다.
        /// </summary>
        DOUBLE = 8,
        /// <summary>
        /// DataObjectItem 의 Single Type 입니다.
        /// </summary>
        SINGLE = 9,
        /// <summary>
        /// DataObjectItem 의 DateTime Type 입니다.
        /// </summary>
        DateTIME = 10,
        /// <summary>
        /// DataObjectItem 의 DateTime2 Type 입니다.
        /// </summary>
        TimeStamp = 11,
        /// <summary>
        /// DataObjectItem 의 null Type 입니다.
        /// null 을 반환 합니다.
        /// </summary>
        NULL = 12,
        /// <summary>
        /// DataObjectItem 의 Array Type 입니다.
        /// </summary>
        BINARY = 13,
        /// <summary>
        /// DataObjectItem 의 bool Type 입니다.
        /// </summary>
        BOOL = 14,
        /// <summary>
        /// 최대 가변길이 문자열 Type 입니다.
        /// </summary>
        TEXT = 15,
        /// <summary>
        /// DataObjectItem 의 기본 Type 입니다.
        /// String 을 반환 합니다.
        /// </summary>
        DEFAULT = 99,
        /// <summary>
        /// Oralce Database 인 경우 DataObjectItem 이 Ref Cursor 임을 정의 합니다.
        /// <para>Oracle Database 가 아닌 경우 HIE.Framework 에서는 문자열로 인식 합니다.</para>
        /// </summary>
        REF_CURSOR = 100,
    }

    public enum ReleaseConnectionsMode
    {
        /// <summary>
        /// after_transaction, auto 에 대응 합니다.
        /// </summary>
        AUTO = 1,

        /// <summary>
        /// on_close 에 대응 합니다.
        /// </summary>
        CLOSE = 2,
    }

    public enum OrmDbType
    {
        /// <summary>
        /// MS-SQL Server 2012 Std 이상의 데이터베이스 입니다.
        /// </summary>
        SQL = 1,
        /// <summary>
        /// 오라클 10g 이상의 데이터베이스 입니다.
        /// </summary>
        ORACLE = 2,
        /// <summary>
        /// MySQL / Maria 데이터베이스 입니다.
        /// </summary>
        MY_SQL = 3,
        /// <summary>
        /// firebird 데이터베이스 입니다.
        /// </summary>
        FIRE_BIRD = 4,
        /// <summary>
        /// PostgresSQL 데이터베이스 입니다.
        /// </summary>
        POSTGRE_SQL = 5,
        SQLITE = 6,
        SYBASE_ASE = 7,
        SYBASE_SQL_ANYWHERE = 8,
        /// <summary>
        /// MS-Access MDB 입니다.
        /// </summary>
        MDB = 9,
        /// <summary>
        /// 큐브리드 Database 입니다.
        /// </summary>
        CUBRID = 10,
        /// <summary>
        /// MS-SQL Server 2012 Std 이상의 데이터베이스를 기본으로 지정 합니다.
        /// </summary>
        DEFAULT = 99,
    }
}
