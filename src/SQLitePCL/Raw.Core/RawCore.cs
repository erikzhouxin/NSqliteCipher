using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 列表委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int strdelegate_collation(object user_data, string s1, string s2);
    /// <summary>
    /// 更新委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="type"></param>
    /// <param name="database"></param>
    /// <param name="table"></param>
    /// <param name="rowid"></param>
    public delegate void strdelegate_update(object user_data, int type, string database, string table, long rowid);
    /// <summary>
    /// 日志委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="errorCode"></param>
    /// <param name="msg"></param>
    public delegate void strdelegate_log(object user_data, int errorCode, string msg);
    /// <summary>
    /// 认证委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="action_code"></param>
    /// <param name="param0"></param>
    /// <param name="param1"></param>
    /// <param name="dbName"></param>
    /// <param name="inner_most_trigger_or_view"></param>
    /// <returns></returns>
    public delegate int strdelegate_authorizer(object user_data, int action_code, string param0, string param1, string dbName, string inner_most_trigger_or_view);
    /// <summary>
    /// 跟踪委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s"></param>
    public delegate void strdelegate_trace(object user_data, string s);
    /// <summary>
    /// 用户配置委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="statement"></param>
    /// <param name="ns"></param>
    public delegate void strdelegate_profile(object user_data, string statement, long ns);
    /// <summary>
    /// 执行委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="values"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public delegate int strdelegate_exec(object user_data, string[] values, string[] names);
    /// <summary>
    /// 核心
    /// </summary>
    public static class RawCore
    {
        private static ISQLite3Provider _imp;
        private static bool _frozen;

        static RawCore()
        {
            _frozen = false;
        }
        /// <summary>
        /// 设置提供程序
        /// </summary>
        /// <param name="imp"></param>
        public static void SetProvider(ISQLite3Provider imp)
        {
            if (_frozen) return;
            int version = imp.sqlite3_libversion_number();
#if not // don't do this, because it ends up calling sqlite3_initialize
		IntPtr db;
		int rc;
	        rc = imp.sqlite3_open(":memory:", out db);
		if (rc != 0) throw new Exception();
		rc = imp.sqlite3_close(db);
		if (rc != 0) throw new Exception();
#endif
            _imp = imp;
        }
        /// <summary>
        /// 固定提供者
        /// </summary>
        /// <param name="b"></param>
        public static void FreezeProvider(bool b = true)
        {
            _frozen = b;
        }

        private static ISQLite3Provider Provider
        {
            get => _imp ?? throw new Exception("需要调用SQLitePCL.Raw.Core.RawCore.SetProvider方法");
        }
        /// <summary>
        /// 获取执行名称
        /// </summary>
        /// <returns></returns>
        public static string GetNativeLibraryName()
        {
            return Provider.GetNativeLibraryName();
        }
        /// <summary>
        /// 如果为空设置为ESqlcipher
        /// </summary>
        public static void SetProviderIfNull()
        {
            _imp = _imp ?? new SQLitePCL.Raw.Core.ProviderESqlcipher();
        }
        /// <summary>
        /// UTF-8编码
        /// </summary>
        public const int SQLITE_UTF8 = 1;
        /// <summary>
        /// UTF16L编码
        /// </summary>
        public const int SQLITE_UTF16LE = 2;
        /// <summary>
        /// UTF16BE编码
        /// </summary>
        public const int SQLITE_UTF16BE = 3;
        /// <summary>
        /// UTF16编码
        /// </summary>
        public const int SQLITE_UTF16 = 4;  /* Use native byte order */
        /// <summary>
        /// 其他编码
        /// </summary>
        public const int SQLITE_ANY = 5;  /* sqlite3_create_function only */
        /// <summary>
        /// UTF16校验
        /// </summary>
        public const int SQLITE_UTF16_ALIGNED = 8;  /* sqlite3_create_function only */
        /// <summary>
        /// 确定性参数
        /// </summary>
        public const int SQLITE_DETERMINISTIC = 0x800;
        /// <summary>
        /// 限制长度
        /// </summary>
        public const int SQLITE_LIMIT_LENGTH = 0;
        /// <summary>
        /// SQL长度限制
        /// </summary>
        public const int SQLITE_LIMIT_SQL_LENGTH = 1;
        /// <summary>
        /// 列长度限制
        /// </summary>
        public const int SQLITE_LIMIT_COLUMN = 2;
        /// <summary>
        /// 表达式深度
        /// </summary>
        public const int SQLITE_LIMIT_EXPR_DEPTH = 3;
        /// <summary>
        /// 限制组合查询
        /// </summary>
        public const int SQLITE_LIMIT_COMPOUND_SELECT = 4;
        /// <summary>
        /// 字节代码操作
        /// </summary>
        public const int SQLITE_LIMIT_VDBE_OP = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_FUNCTION_ARG = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_ATTACHED = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_LIKE_PATTERN_LENGTH = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_VARIABLE_NUMBER = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_TRIGGER_DEPTH = 10;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LIMIT_WORKER_THREADS = 11;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_SINGLETHREAD = 1;  /* nil */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_MULTITHREAD = 2;  /* nil */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_SERIALIZED = 3;  /* nil */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_MALLOC = 4;  /* sqlite3_mem_methods* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_GETMALLOC = 5;  /* sqlite3_mem_methods* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_SCRATCH = 6;  /* void*, int utf8z, int N */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_PAGECACHE = 7;  /* void*, int utf8z, int N */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_HEAP = 8;  /* void*, int nByte, int min */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_MEMSTATUS = 9;  /* boolean */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_MUTEX = 10;  /* sqlite3_mutex_methods* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_GETMUTEX = 11;  /* sqlite3_mutex_methods* */
        /* previously SQLITE_CONFIG_CHUNKALLOC 12 which is now unused. */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_LOOKASIDE = 13;  /* int int */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_PCACHE = 14;  /* no-op */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_GETPCACHE = 15;  /* no-op */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_LOG = 16;  /* xFunc, void* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_URI = 17;  /* int */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_PCACHE2 = 18;  /* sqlite3_pcache_methods2* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_GETPCACHE2 = 19;  /* sqlite3_pcache_methods2* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_COVERING_INDEX_SCAN = 20;  /* int */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONFIG_SQLLOG = 21;  /* xSqllog, void* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_MAINDBNAME = 1000; /* const char* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_LOOKASIDE = 1001; /* void* int int */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_FKEY = 1002; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_TRIGGER = 1003; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_FTS3_TOKENIZER = 1004; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_LOAD_EXTENSION = 1005; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_NO_CKPT_ON_CLOSE = 1006; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_QPSG = 1007; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_TRIGGER_EQP = 1008; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_RESET_DATABASE = 1009; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_DEFENSIVE = 1010; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_WRITABLE_SCHEMA = 1011; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_LEGACY_ALTER_TABLE = 1012; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_DQS_DML = 1013; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_DQS_DDL = 1014; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_ENABLE_VIEW = 1015; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_LEGACY_FILE_FORMAT = 1016; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_TRUSTED_SCHEMA = 1017; /* int int* */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBCONFIG_MAX = 1017; /* Largest DBCONFIG */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_READONLY = 0x00000001;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_READWRITE = 0x00000002;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_CREATE = 0x00000004;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_DELETEONCLOSE = 0x00000008;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_EXCLUSIVE = 0x00000010;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_AUTOPROXY = 0x00000020;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_URI = 0x00000040;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_MEMORY = 0x00000080;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_MAIN_DB = 0x00000100;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_TEMP_DB = 0x00000200;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_TRANSIENT_DB = 0x00000400;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_MAIN_JOURNAL = 0x00000800;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_TEMP_JOURNAL = 0x00001000;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_SUBJOURNAL = 0x00002000;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_MASTER_JOURNAL = 0x00004000;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_NOMUTEX = 0x00008000;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_FULLMUTEX = 0x00010000;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_SHAREDCACHE = 0x00020000;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_PRIVATECACHE = 0x00040000;  /* Ok for sqlite3_open_v2() */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OPEN_WAL = 0x00080000;  /* VFS only */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PREPARE_PERSISTENT = 0x01;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PREPARE_NORMALIZE = 0x02;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PREPARE_NO_VTAB = 0x04;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_INTEGER = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_FLOAT = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TEXT = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_BLOB = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NULL = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_OK = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ERROR = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_INTERNAL = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PERM = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ABORT = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_BUSY = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LOCKED = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOMEM = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READONLY = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_INTERRUPT = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR = 10;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CORRUPT = 11;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOTFOUND = 12;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_FULL = 13;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CANTOPEN = 14;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PROTOCOL = 15;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_EMPTY = 16;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_SCHEMA = 17;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TOOBIG = 18;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT = 19;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_MISMATCH = 20;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_MISUSE = 21;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOLFS = 22;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_AUTH = 23;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_FORMAT = 24;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_RANGE = 25;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOTADB = 26;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOTICE = 27;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_WARNING = 28;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ROW = 100;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DONE = 101;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_READ = (SQLITE_IOERR | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SHORT_READ = (SQLITE_IOERR | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_WRITE = (SQLITE_IOERR | (3 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_FSYNC = (SQLITE_IOERR | (4 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_DIR_FSYNC = (SQLITE_IOERR | (5 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_TRUNCATE = (SQLITE_IOERR | (6 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_FSTAT = (SQLITE_IOERR | (7 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_UNLOCK = (SQLITE_IOERR | (8 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_RDLOCK = (SQLITE_IOERR | (9 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_DELETE = (SQLITE_IOERR | (10 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_BLOCKED = (SQLITE_IOERR | (11 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_NOMEM = (SQLITE_IOERR | (12 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_ACCESS = (SQLITE_IOERR | (13 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_CHECKRESERVEDLOCK = (SQLITE_IOERR | (14 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_LOCK = (SQLITE_IOERR | (15 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_CLOSE = (SQLITE_IOERR | (16 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_DIR_CLOSE = (SQLITE_IOERR | (17 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SHMOPEN = (SQLITE_IOERR | (18 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SHMSIZE = (SQLITE_IOERR | (19 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SHMLOCK = (SQLITE_IOERR | (20 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SHMMAP = (SQLITE_IOERR | (21 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_SEEK = (SQLITE_IOERR | (22 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_DELETE_NOENT = (SQLITE_IOERR | (23 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_MMAP = (SQLITE_IOERR | (24 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_GETTEMPPATH = (SQLITE_IOERR | (25 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IOERR_CONVPATH = (SQLITE_IOERR | (26 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_LOCKED_SHAREDCACHE = (SQLITE_LOCKED | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_BUSY_RECOVERY = (SQLITE_BUSY | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_BUSY_SNAPSHOT = (SQLITE_BUSY | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CANTOPEN_NOTEMPDIR = (SQLITE_CANTOPEN | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CANTOPEN_ISDIR = (SQLITE_CANTOPEN | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CANTOPEN_FULLPATH = (SQLITE_CANTOPEN | (3 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CANTOPEN_CONVPATH = (SQLITE_CANTOPEN | (4 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CORRUPT_VTAB = (SQLITE_CORRUPT | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READONLY_RECOVERY = (SQLITE_READONLY | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READONLY_CANTLOCK = (SQLITE_READONLY | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READONLY_ROLLBACK = (SQLITE_READONLY | (3 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READONLY_DBMOVED = (SQLITE_READONLY | (4 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ABORT_ROLLBACK = (SQLITE_ABORT | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_CHECK = (SQLITE_CONSTRAINT | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_COMMITHOOK = (SQLITE_CONSTRAINT | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_FOREIGNKEY = (SQLITE_CONSTRAINT | (3 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_FUNCTION = (SQLITE_CONSTRAINT | (4 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_NOTNULL = (SQLITE_CONSTRAINT | (5 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_PRIMARYKEY = (SQLITE_CONSTRAINT | (6 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_TRIGGER = (SQLITE_CONSTRAINT | (7 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_UNIQUE = (SQLITE_CONSTRAINT | (8 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_VTAB = (SQLITE_CONSTRAINT | (9 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CONSTRAINT_ROWID = (SQLITE_CONSTRAINT | (10 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOTICE_RECOVER_WAL = (SQLITE_NOTICE | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_NOTICE_RECOVER_ROLLBACK = (SQLITE_NOTICE | (2 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_WARNING_AUTOINDEX = (SQLITE_WARNING | (1 << 8));
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_INDEX = 1;    /* Index Name      Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TABLE = 2;    /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TEMP_INDEX = 3;    /* Index Name      Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TEMP_TABLE = 4;    /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TEMP_TRIGGER = 5;    /* Trigger Name    Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TEMP_VIEW = 6;    /* View Name       NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_TRIGGER = 7;    /* Trigger Name    Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_VIEW = 8;    /* View Name       NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DELETE = 9;    /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_INDEX = 10;   /* Index Name      Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TABLE = 11;   /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TEMP_INDEX = 12;   /* Index Name      Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TEMP_TABLE = 13;   /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TEMP_TRIGGER = 14;   /* Trigger Name    Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TEMP_VIEW = 15;   /* View Name       NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_TRIGGER = 16;   /* Trigger Name    Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_VIEW = 17;   /* View Name       NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_INSERT = 18;   /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_PRAGMA = 19;   /* Pragma Name     1st arg or NULL */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_READ = 20;   /* Table Name      Column Name     */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_SELECT = 21;   /* NULL            NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TRANSACTION = 22;   /* Operation       NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_UPDATE = 23;   /* Table Name      Column Name     */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ATTACH = 24;   /* Filename        NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DETACH = 25;   /* Database Name   NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ALTER_TABLE = 26;   /* Database Name   Table Name      */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_REINDEX = 27;   /* Index Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_ANALYZE = 28;   /* Table Name      NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CREATE_VTABLE = 29;   /* Table Name      Module Name     */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DROP_VTABLE = 30;   /* Table Name      Module Name     */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_FUNCTION = 31;   /* NULL            Function Name   */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_SAVEPOINT = 32;   /* Operation       Savepoint Name  */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_COPY = 0;    /* No longer used */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_RECURSIVE = 33;   /* NULL            NULL            */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CHECKPOINT_PASSIVE = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CHECKPOINT_FULL = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CHECKPOINT_RESTART = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_CHECKPOINT_TRUNCATE = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_LOOKASIDE_USED = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_CACHE_USED = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_SCHEMA_USED = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_STMT_USED = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_LOOKASIDE_HIT = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_LOOKASIDE_MISS_SIZE = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_LOOKASIDE_MISS_FULL = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_CACHE_HIT = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_CACHE_MISS = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_CACHE_WRITE = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_DBSTATUS_DEFERRED_FKS = 10;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_MEMORY_USED = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_PAGECACHE_USED = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_PAGECACHE_OVERFLOW = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_SCRATCH_USED = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_SCRATCH_OVERFLOW = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_MALLOC_SIZE = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_PARSER_STACK = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_PAGECACHE_SIZE = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_SCRATCH_SIZE = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STATUS_MALLOC_COUNT = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STMTSTATUS_FULLSCAN_STEP = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STMTSTATUS_SORT = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STMTSTATUS_AUTOINDEX = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_STMTSTATUS_VM_STEP = 4;
        /// <summary>
        /// Authorizer Return Codes
        /// </summary>
        public const int SQLITE_DENY = 1;   /* Abort the SQL statement with an error */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_IGNORE = 2;   /* Don't allow access, but don't generate an error */
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TRACE_STMT = 0x01;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TRACE_PROFILE = 0x02;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TRACE_ROW = 0x04;
        /// <summary>
        /// 
        /// </summary>
        public const int SQLITE_TRACE_CLOSE = 0x08;
        /// <summary>
        /// 打开
        /// </summary>
        public static int sqlite3_open(Utf8z filename, out sqlite3 db)
        {
            int rc = Provider.sqlite3_open(filename, out var p_db);
            // TODO check rc?
            db = sqlite3.New(p_db);
            return rc;
        }
        /// <summary>
        /// 打开
        /// </summary>
        public static int sqlite3_open(string filename, out sqlite3 db)
        {
            return sqlite3_open(filename.ToUtf8z(), out db);
        }
        /// <summary>
        /// 打开v2
        /// </summary>
        public static int sqlite3_open_v2(Utf8z filename, out sqlite3 db, int flags, Utf8z vfs)
        {
            int rc = Provider.sqlite3_open_v2(filename, out var p_db, flags, vfs);
            // TODO check rc?
            db = sqlite3.New(p_db);
            return rc;
        }
        /// <summary>
        /// 打开v2
        /// </summary>
        public static int sqlite3_open_v2(string filename, out sqlite3 db, int flags, string vfs)
        {
            return sqlite3_open_v2(filename.ToUtf8z(), out db, flags, vfs.ToUtf8z());
        }
        /// <summary>
        /// vfs删除
        /// </summary>
        public static int sqlite3__vfs__delete(Utf8z vfs, Utf8z pathname, int syncdir)
        {
            return Provider.sqlite3__vfs__delete(vfs, pathname, syncdir);
        }
        /// <summary>
        /// vfs删除
        /// </summary>
        public static int sqlite3__vfs__delete(string vfs, string pathname, int syncdir)
        {
            return sqlite3__vfs__delete(vfs.ToUtf8z(), pathname.ToUtf8z(), syncdir);
        }

        /// <summary>
        /// 关闭连接v2
        /// called by the SafeHandle
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        static internal int internal_sqlite3_close_v2(IntPtr p)
        {
            return Provider.sqlite3_close_v2(p);
        }

        /// <summary>
        /// 关闭连接
        /// called by the SafeHandle
        /// </summary>
        static internal int internal_sqlite3_close(IntPtr p)
        {
            return Provider.sqlite3_close(p);
        }

        /// <summary>
        /// 关闭连接v2
        /// called by apps that want the return code
        /// </summary>
        public static int sqlite3_close_v2(sqlite3 db)
        {
            return db.manual_close_v2();
        }

        /// <summary>
        /// 关闭连接
        /// called by apps that want the return code
        /// </summary>
        public static int sqlite3_close(sqlite3 db)
        {
            return db.manual_close();
        }

        /// <summary>
        /// 启用共享内存
        /// </summary>
        public static int sqlite3_enable_shared_cache(int enable)
        {
            return Provider.sqlite3_enable_shared_cache(enable);
        }

        /// <summary>
        /// 中断
        /// </summary>
        public static void sqlite3_interrupt(sqlite3 db)
        {
            Provider.sqlite3_interrupt(db);
        }

        /// <summary>
        /// 配置日志
        /// </summary>
        public static int sqlite3_config_log(delegate_log f, object v)
        {
            return Provider.sqlite3_config_log(f, v);
        }
        /// <summary>
        /// 配置日志
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int sqlite3_config_log(strdelegate_log f, object v)
        {
            delegate_log cb = f == null ? null : ((ob, e, msg) => f(ob, e, msg.utf8_to_string()));
            return sqlite3_config_log(cb, v);
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="s"></param>
        public static void sqlite3_log(int errcode, Utf8z s)
        {
            Provider.sqlite3_log(errcode, s);
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="s"></param>
        public static void sqlite3_log(int errcode, string s)
        {
            sqlite3_log(errcode, s.ToUtf8z());
        }
        /// <summary>
        /// 提交回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_commit_hook(sqlite3 db, delegate_commit f, object v)
        {
            Provider.sqlite3_commit_hook(db, f, v);
        }
        /// <summary>
        /// 回滚回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_rollback_hook(sqlite3 db, delegate_rollback f, object v)
        {
            Provider.sqlite3_rollback_hook(db, f, v);
        }
        /// <summary>
        /// 跟踪回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_trace(sqlite3 db, delegate_trace f, object v)
        {
            Provider.sqlite3_trace(db, f, v);
        }
        /// <summary>
        /// 跟踪回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_trace(sqlite3 db, strdelegate_trace f, object v)
        {
            delegate_trace cb = f == null ? null : ((ob, sp) => f(v, sp.utf8_to_string()));
            sqlite3_trace(db, cb, v);
        }
        /// <summary>
        /// 用户设置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_profile(sqlite3 db, delegate_profile f, object v)
        {
            Provider.sqlite3_profile(db, f, v);
        }
        /// <summary>
        /// 用户设置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_profile(sqlite3 db, strdelegate_profile f, object v)
        {
            delegate_profile cb = f == null ? null : ((ob, sp, ns) => f(v, sp.utf8_to_string(), ns));
            sqlite3_profile(db, cb, v);
        }
        /// <summary>
        /// 处理应对
        /// </summary>
        /// <param name="db"></param>
        /// <param name="instructions"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public static void sqlite3_progress_handler(sqlite3 db, int instructions, delegate_progress func, object v)
        {
            Provider.sqlite3_progress_handler(db, instructions, func, v);
        }
        /// <summary>
        /// 更新回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_update_hook(sqlite3 db, delegate_update f, object v)
        {
            Provider.sqlite3_update_hook(db, f, v);
        }
        /// <summary>
        /// 更新回调
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="v"></param>
        public static void sqlite3_update_hook(sqlite3 db, strdelegate_update f, object v)
        {
            delegate_update cb = f == null ? null : ((ob, typ, dbname, tbl, rowid) => f(ob, typ, dbname.utf8_to_string(), tbl.utf8_to_string(), rowid));
            sqlite3_update_hook(db, cb, v);
        }
        /// <summary>
        /// 创建列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int sqlite3_create_collation(sqlite3 db, string name, object v, strdelegate_collation f)
        {
            delegate_collation cb = f == null ? null : ((ob, s1, s2) => f(ob, s1.utf8_span_to_string(), s2.utf8_span_to_string()));
            return Provider.sqlite3_create_collation(db, name.ToUtf8WithZ(), v, cb);
        }
        /// <summary>
        /// note the extra underscore in the name of the following function.
        /// this function is not in sqlite.
        /// it is being added for SQLitePCLRaw 2.0.5.
        /// but making this an overload for sqlite3_create_collation() would break existing code
        /// because a null value becomes an ambiguous call.
        /// so we give it a gratuitously different name, but we don't want it to look
        /// like (or possibly in the future clash with) the name of an actual function
        /// in sqlite itself.  thus the extra underscore.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int sqlite3__create_collation_utf8(sqlite3 db, string name, object v, delegate_collation f)
        {
            var p = name.ToUtf8WithZ();
            return Provider.sqlite3_create_collation(db, p, v, f);
        }
        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="flags"></param>
        /// <param name="v"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int sqlite3_create_function(sqlite3 db, string name, int nArg, int flags, object v, delegate_function_scalar func)
        {
            var p = name.ToUtf8WithZ();
            var rc = Provider.sqlite3_create_function(db, p, nArg, flags, v, func);
            return rc;
        }
        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="flags"></param>
        /// <param name="v"></param>
        /// <param name="func_step"></param>
        /// <param name="func_final"></param>
        /// <returns></returns>
        public static int sqlite3_create_function(sqlite3 db, string name, int nArg, int flags, object v, delegate_function_aggregate_step func_step, delegate_function_aggregate_final func_final)
        {
            var p = name.ToUtf8WithZ();
            var rc = Provider.sqlite3_create_function(db, p, nArg, flags, v, func_step, func_final);
            return rc;
        }
        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="v"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int sqlite3_create_function(sqlite3 db, string name, int nArg, object v, delegate_function_scalar func)
        {
            return sqlite3_create_function(db, name, nArg, 0, v, func);
        }
        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="v"></param>
        /// <param name="func_step"></param>
        /// <param name="func_final"></param>
        /// <returns></returns>
        public static int sqlite3_create_function(sqlite3 db, string name, int nArg, object v, delegate_function_aggregate_step func_step, delegate_function_aggregate_final func_final)
        {
            return sqlite3_create_function(db, name, nArg, 0, v, func_step, func_final);
        }
        /// <summary>
        /// 数据库状态
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="current"></param>
        /// <param name="highest"></param>
        /// <param name="resetFlg"></param>
        /// <returns></returns>
        public static int sqlite3_db_status(sqlite3 db, int op, out int current, out int highest, int resetFlg)
        {
            return Provider.sqlite3_db_status(db, op, out current, out highest, resetFlg);
        }

#if NET40
        /// <summary>
        /// TODO do we need this to be public?
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string utf8_span_to_string(this byte[] p)
        {
            if (p == null || p.Length == 0) { return ""; }
            return System.Text.Encoding.UTF8.GetString(p);
        }
#else
        /// <summary>
        /// TODO do we need this to be public?
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string utf8_span_to_string(this ReadOnlySpan<byte> p)
        {
            if (p == null || p.Length == 0) { return ""; }
            unsafe
            {
                fixed (byte* q = p)
                {
                    return System.Text.Encoding.UTF8.GetString(q, p.Length);
                }
            }
        }
#endif

#if NET40
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_key(sqlite3 db, byte[] k)
        {
            return Provider.sqlite3_key(db, k);
        }
        /// <summary>
        /// 密码2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_key_v2(sqlite3 db, Utf8z name, byte[] k)
        {
            return Provider.sqlite3_key_v2(db, name, k);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_rekey(sqlite3 db, byte[] k)
        {
            return Provider.sqlite3_rekey(db, k);
        }
        /// <summary>
        /// 重置密码2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_rekey_v2(sqlite3 db, Utf8z name, byte[] k)
        {
            return Provider.sqlite3_rekey_v2(db, name, k);
        }
#else
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_key(sqlite3 db, ReadOnlySpan<byte> k)
        {
            return Provider.sqlite3_key(db, k);
        }
        /// <summary>
        /// 密码2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_key_v2(sqlite3 db, Utf8z name, ReadOnlySpan<byte> k)
        {
            return Provider.sqlite3_key_v2(db, name, k);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_rekey(sqlite3 db, ReadOnlySpan<byte> k)
        {
            return Provider.sqlite3_rekey(db, k);
        }
        /// <summary>
        /// 重置密码2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int sqlite3_rekey_v2(sqlite3 db, Utf8z name, ReadOnlySpan<byte> k)
        {
            return Provider.sqlite3_rekey_v2(db, name, k);
        }
#endif
        /// <summary>
        /// 库版本
        /// </summary>
        /// <returns></returns>
        public static Utf8z sqlite3_libversion()
        {
            return Provider.sqlite3_libversion();
        }
        /// <summary>
        /// 库版本号
        /// </summary>
        /// <returns></returns>
        public static int sqlite3_libversion_number()
        {
            return Provider.sqlite3_libversion_number();
        }
        /// <summary>
        /// 线程安全
        /// </summary>
        /// <returns></returns>
        public static int sqlite3_threadsafe()
        {
            return Provider.sqlite3_threadsafe();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static int sqlite3_initialize()
        {
            return Provider.sqlite3_initialize();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public static int sqlite3_shutdown()
        {
            return Provider.sqlite3_shutdown();
        }
        /// <summary>
        /// 限制
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        public static int sqlite3_limit(sqlite3 db, int id, int newVal)
        {
            return Provider.sqlite3_limit(db, id, newVal);
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static int sqlite3_config(int op)
        {
            return Provider.sqlite3_config(op);
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_config(int op, int val)
        {
            return Provider.sqlite3_config(op, val);
        }
        /// <summary>
        /// 库配置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_db_config(sqlite3 db, int op, Utf8z val)
        {
            return Provider.sqlite3_db_config(db, op, val);
        }
        /// <summary>
        /// 库配置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int sqlite3_db_config(sqlite3 db, int op, int val, out int result)
        {
            return Provider.sqlite3_db_config(db, op, val, out result);
        }
        /// <summary>
        /// 库配置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="ptr"></param>
        /// <param name="int0"></param>
        /// <param name="int1"></param>
        /// <returns></returns>
        public static int sqlite3_db_config(sqlite3 db, int op, IntPtr ptr, int int0, int int1)
        {
            return Provider.sqlite3_db_config(db, op, ptr, int0, int1);
        }
        /// <summary>
        /// 启用加载扩展
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public static int sqlite3_enable_load_extension(sqlite3 db, int onoff)
        {
            return Provider.sqlite3_enable_load_extension(db, onoff);
        }
        /// <summary>
        /// 资源标识
        /// </summary>
        /// <returns></returns>
        public static Utf8z sqlite3_sourceid()
        {
            return Provider.sqlite3_sourceid();
        }
        /// <summary>
        /// 内存使用
        /// </summary>
        /// <returns></returns>
        public static long sqlite3_memory_used()
        {
            return Provider.sqlite3_memory_used();
        }
        /// <summary>
        /// 内存最高位
        /// </summary>
        /// <param name="resetFlag"></param>
        /// <returns></returns>
        public static long sqlite3_memory_highwater(int resetFlag)
        {
            return Provider.sqlite3_memory_highwater(resetFlag);
        }
        /// <summary>
        /// 软件堆限制
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long sqlite3_soft_heap_limit64(long n)
        {
            return Provider.sqlite3_soft_heap_limit64(n);
        }
        /// <summary>
        /// 硬件堆限制
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long sqlite3_hard_heap_limit64(long n)
        {
            return Provider.sqlite3_hard_heap_limit64(n);
        }
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="op"></param>
        /// <param name="current"></param>
        /// <param name="highwater"></param>
        /// <param name="resetFlag"></param>
        /// <returns></returns>
        public static int sqlite3_status(int op, out int current, out int highwater, int resetFlag)
        {
            return Provider.sqlite3_status(op, out current, out highwater, resetFlag);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_errmsg(sqlite3 db)
        {
            return Provider.sqlite3_errmsg(db);
        }
        /// <summary>
        /// 库只读
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static int sqlite3_db_readonly(sqlite3 db, Utf8z dbName)
        {
            return Provider.sqlite3_db_readonly(db, dbName);
        }
        /// <summary>
        /// 库只读
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static int sqlite3_db_readonly(sqlite3 db, string dbName)
        {
            return sqlite3_db_readonly(db, dbName.ToUtf8z());
        }
        /// <summary>
        /// 库文件
        /// </summary>
        /// <param name="db"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_db_filename(sqlite3 db, Utf8z att)
        {
            return Provider.sqlite3_db_filename(db, att);
        }
        /// <summary>
        /// 库文件
        /// </summary>
        /// <param name="db"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_db_filename(sqlite3 db, string att)
        {
            return sqlite3_db_filename(db, att.ToUtf8z());
        }
        /// <summary>
        /// 最后新增的列id
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static long sqlite3_last_insert_rowid(sqlite3 db)
        {
            return Provider.sqlite3_last_insert_rowid(db);
        }
        /// <summary>
        /// 变更
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int sqlite3_changes(sqlite3 db)
        {
            return Provider.sqlite3_changes(db);
        }
        /// <summary>
        /// 变更行
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int sqlite3_total_changes(sqlite3 db)
        {
            return Provider.sqlite3_total_changes(db);
        }
        /// <summary>
        /// 获取自动提交
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int sqlite3_get_autocommit(sqlite3 db)
        {
            return Provider.sqlite3_get_autocommit(db);
        }
        /// <summary>
        /// 繁忙超时
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static int sqlite3_busy_timeout(sqlite3 db, int ms)
        {
            return Provider.sqlite3_busy_timeout(db, ms);
        }
        /// <summary>
        /// 扩展结果代码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public static int sqlite3_extended_result_codes(sqlite3 db, int onoff)
        {
            return Provider.sqlite3_extended_result_codes(db, onoff);
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int sqlite3_errcode(sqlite3 db)
        {
            return Provider.sqlite3_errcode(db);
        }
        /// <summary>
        /// 扩展错误代码
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int sqlite3_extended_errcode(sqlite3 db)
        {
            return Provider.sqlite3_extended_errcode(db);
        }
        /// <summary>
        /// 错误字符
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_errstr(int rc)
        {
            return Provider.sqlite3_errstr(rc);
        }

#if NET40
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, byte[] sql, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#else
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, ReadOnlySpan<byte> sql, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#endif
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, Utf8z sql, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
        /// <summary>
        /// 预编译v2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, string sql, out sqlite3_stmt stmt)
        {
            var ba = sql.ToUtf8WithZ();
#if NET40
            int rc = sqlite3_prepare_v2(db, ba, out stmt, out var sp_tail);
#else
            int rc = sqlite3_prepare_v2(db, new ReadOnlySpan<byte>(ba), out stmt, out var sp_tail);
#endif
            return rc;
        }
#if NET40
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, byte[] sql, out sqlite3_stmt stmt, out byte[] tail)
        {
            // #430 happens here
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#else
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, ReadOnlySpan<byte> sql, out sqlite3_stmt stmt, out ReadOnlySpan<byte> tail)
        {
            // #430 happens here
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#endif
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, Utf8z sql, out sqlite3_stmt stmt, out Utf8z tail)
        {
            int rc = Provider.sqlite3_prepare_v2(db, sql, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v2(sqlite3 db, string sql, out sqlite3_stmt stmt, out string tail)
        {
            var ba = sql.ToUtf8WithZ();
#if NET40
            int rc = sqlite3_prepare_v2(db, ba, out stmt, out var sp_tail);
            tail = utf8_span_to_string(GetSlice(sp_tail));
#else
            int rc = sqlite3_prepare_v2(db, new ReadOnlySpan<byte>(ba), out stmt, out var sp_tail);
            tail = utf8_span_to_string(sp_tail.Slice(0, sp_tail.Length - 1));
#endif
            return rc;
        }

#if NET40
        /// <summary>
        /// 截取
        /// </summary>
        /// <param name="sp_tail"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        private static byte[] GetSlice(byte[] sp_tail, int dist = 1)
        {
            var ta = new byte[sp_tail.Length - dist];
            for (int i = 0; i < ta.Length; i++)
            {
                ta[i] = sp_tail[i];
            }
            return ta;
        }
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, byte[] sql, uint flags, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#else
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, ReadOnlySpan<byte> sql, uint flags, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#endif
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, Utf8z sql, uint flags, out sqlite3_stmt stmt)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out var sp_tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, string sql, uint flags, out sqlite3_stmt stmt)
        {
            var ba = sql.ToUtf8WithZ();
#if NET40
            int rc = sqlite3_prepare_v3(db, ba, flags, out stmt, out var sp_tail);
#else
            int rc = sqlite3_prepare_v3(db, new ReadOnlySpan<byte>(ba), flags, out stmt, out var sp_tail);
#endif
            return rc;
        }
#if NET40
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, byte[] sql, uint flags, out sqlite3_stmt stmt, out byte[] tail)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#else
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, ReadOnlySpan<byte> sql, uint flags, out sqlite3_stmt stmt, out ReadOnlySpan<byte> tail)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
#endif
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, Utf8z sql, uint flags, out sqlite3_stmt stmt, out Utf8z tail)
        {
            int rc = Provider.sqlite3_prepare_v3(db, sql, flags, out var p, out tail);
            stmt = sqlite3_stmt.From(p, db);
            return rc;
        }
        /// <summary>
        /// 预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int sqlite3_prepare_v3(sqlite3 db, string sql, uint flags, out sqlite3_stmt stmt, out string tail)
        {
            var ba = sql.ToUtf8WithZ();
#if NET40
            int rc = sqlite3_prepare_v3(db, ba, flags, out stmt, out var sp_tail);
            tail = utf8_span_to_string(GetSlice(sp_tail));
#else
            int rc = sqlite3_prepare_v3(db, new ReadOnlySpan<byte>(ba), flags, out stmt, out var sp_tail);
            tail = utf8_span_to_string(sp_tail.Slice(0, sp_tail.Length - 1));
#endif
            return rc;
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="callback"></param>
        /// <param name="user_data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int sqlite3_exec(sqlite3 db, string sql, strdelegate_exec callback, object user_data, out string errMsg)
        {
            delegate_exec cb = callback == null ? null : ((ob, values, names) =>
            {
                var a_v = new string[values.Length];
                var a_n = new string[names.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    a_v[i] = CompatAssist.FromUtf8z(values[i]);
                    a_n[i] = CompatAssist.FromUtf8z(names[i]);
                }
                return callback(ob, a_v, a_n);
            });
            var rc = Provider.sqlite3_exec(db, sql.ToUtf8z(), cb, user_data, out var p_errMsg);
            if (p_errMsg == IntPtr.Zero)
            {
                errMsg = null;
            }
            else
            {
                errMsg = CompatAssist.FromUtf8z(p_errMsg);
                Provider.sqlite3_free(p_errMsg);
            }
            return rc;
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int sqlite3_exec(sqlite3 db, string sql, out string errMsg)
        {
            var rc = Provider.sqlite3_exec(db, sql.ToUtf8z(), null, null, out var p_errMsg);
            if (p_errMsg == IntPtr.Zero)
            {
                errMsg = null;
            }
            else
            {
                errMsg = CompatAssist.FromUtf8z(p_errMsg);
                Provider.sqlite3_free(p_errMsg);
            }
            return rc;
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int sqlite3_exec(sqlite3 db, string sql)
        {
            var rc = Provider.sqlite3_exec(db, sql.ToUtf8z(), null, null, out var p_errMsg);
            if (p_errMsg != IntPtr.Zero)
            {
                Provider.sqlite3_free(p_errMsg);
            }
            return rc;
        }
        /// <summary>
        /// 执行预编译
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_step(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_step(stmt);
        }

        /// <summary>
        /// called by apps that want the return code
        /// 释放关闭
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_finalize(sqlite3_stmt stmt)
        {
            return stmt.manual_close();
        }

        /// <summary>
        /// called by the SafeHandle
        /// 释放句柄
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int internal_sqlite3_finalize(IntPtr stmt)
        {
            return Provider.sqlite3_finalize(stmt);
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_reset(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_reset(stmt);
        }
        /// <summary>
        /// 清除绑定
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_clear_bindings(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_clear_bindings(stmt);
        }
        /// <summary>
        /// stmt状态
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="op"></param>
        /// <param name="resetFlg"></param>
        /// <returns></returns>
        public static int sqlite3_stmt_status(sqlite3_stmt stmt, int op, int resetFlg)
        {
            return Provider.sqlite3_stmt_status(stmt, op, resetFlg);
        }
        /// <summary>
        /// 完整
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int sqlite3_complete(Utf8z sql)
        {
            return Provider.sqlite3_complete(sql);
        }
        /// <summary>
        /// 完整
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int sqlite3_complete(string sql)
        {
            return sqlite3_complete(sql.ToUtf8z());
        }
        /// <summary>
        /// 编译选项应用
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int sqlite3_compileoption_used(Utf8z s)
        {
            return Provider.sqlite3_compileoption_used(s);
        }
        /// <summary>
        /// 编译选项应用
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int sqlite3_compileoption_used(string s)
        {
            return sqlite3_compileoption_used(s.ToUtf8z());
        }
        /// <summary>
        /// 获取编译选项
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_compileoption_get(int n)
        {
            return Provider.sqlite3_compileoption_get(n);
        }
        /// <summary>
        /// 表列元数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <param name="tblName"></param>
        /// <param name="colName"></param>
        /// <param name="dataType"></param>
        /// <param name="collSeq"></param>
        /// <param name="notNull"></param>
        /// <param name="primaryKey"></param>
        /// <param name="autoInc"></param>
        /// <returns></returns>
        public static int sqlite3_table_column_metadata(sqlite3 db, Utf8z dbName, Utf8z tblName, Utf8z colName, out Utf8z dataType, out Utf8z collSeq, out int notNull, out int primaryKey, out int autoInc)
        {
            return Provider.sqlite3_table_column_metadata(db, dbName, tblName, colName, out dataType, out collSeq, out notNull, out primaryKey, out autoInc);
        }
        /// <summary>
        /// 表列元数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <param name="tblName"></param>
        /// <param name="colName"></param>
        /// <param name="dataType"></param>
        /// <param name="collSeq"></param>
        /// <param name="notNull"></param>
        /// <param name="primaryKey"></param>
        /// <param name="autoInc"></param>
        /// <returns></returns>
        public static int sqlite3_table_column_metadata(sqlite3 db, string dbName, string tblName, string colName, out string dataType, out string collSeq, out int notNull, out int primaryKey, out int autoInc)
        {
            var rc = sqlite3_table_column_metadata(db, dbName.ToUtf8z(), tblName.ToUtf8z(), colName.ToUtf8z(), out var p_dataType, out var p_collSeq, out notNull, out primaryKey, out autoInc);
            dataType = p_dataType.utf8_to_string();
            collSeq = p_collSeq.utf8_to_string();
            return rc;
        }
        /// <summary>
        /// 预编译sql
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_sql(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_sql(stmt);
        }
        /// <summary>
        /// 库处理
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static sqlite3 sqlite3_db_handle(sqlite3_stmt stmt)
        {
#if not
            IntPtr p = Provider.sqlite3_db_handle(stmt.ptr);
            Assert(p == stmt.db);
#endif
            return stmt.db;
        }
        /// <summary>
        /// 下一预编译
        /// </summary>
        /// <param name="db"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static sqlite3_stmt sqlite3_next_stmt(sqlite3 db, sqlite3_stmt stmt)
        {
            IntPtr p = Provider.sqlite3_next_stmt(db, (stmt != null) ? stmt.ptr : IntPtr.Zero);
            return p == IntPtr.Zero ? null : db.find_stmt(p);
        }
        /// <summary>
        /// 绑定〇大对象
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int sqlite3_bind_zeroblob(sqlite3_stmt stmt, int index, int size)
        {
            return Provider.sqlite3_bind_zeroblob(stmt, index, size);
        }
        /// <summary>
        /// 绑定参数名称
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_bind_parameter_name(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_bind_parameter_name(stmt, index);
        }
        /// <summary>
        /// probably unnecessary since we pass user_data back as one of the
        /// params to xFunc, xStep, and xFinal.
        /// 用户数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object sqlite3_user_data(sqlite3_context context)
        {
            return context.user_data;
        }
        /// <summary>
        /// 空结果
        /// </summary>
        /// <param name="context"></param>
        public static void sqlite3_result_null(sqlite3_context context)
        {
            Provider.sqlite3_result_null(context.ptr);
        }

#if NET40
        /// <summary>
        /// 大对象结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_blob(sqlite3_context context, byte[] val)
        {
            Provider.sqlite3_result_blob(context.ptr, val);
        }
        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_error(sqlite3_context context, byte[] val)
        {
            Provider.sqlite3_result_error(context.ptr, val);
        }
#else
        /// <summary>
        /// 大对象结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_blob(sqlite3_context context, ReadOnlySpan<byte> val)
        {
            Provider.sqlite3_result_blob(context.ptr, val);
        }
        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_error(sqlite3_context context, ReadOnlySpan<byte> val)
        {
            Provider.sqlite3_result_error(context.ptr, val);
        }
#endif
        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_error(sqlite3_context context, Utf8z val)
        {
            Provider.sqlite3_result_error(context.ptr, val);
        }
        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_error(sqlite3_context context, string val)
        {
            sqlite3_result_error(context, val.ToUtf8z());
        }

#if NET40
        /// <summary>
        /// 文本结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_text(sqlite3_context context, byte[] val)
        {
            Provider.sqlite3_result_text(context.ptr, val);
        }
#else
        /// <summary>
        /// 文本结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_text(sqlite3_context context, ReadOnlySpan<byte> val)
        {
            Provider.sqlite3_result_text(context.ptr, val);
        }
#endif
        /// <summary>
        /// 文本结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_text(sqlite3_context context, Utf8z val)
        {
            Provider.sqlite3_result_text(context.ptr, val);
        }
        /// <summary>
        /// 文本结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_text(sqlite3_context context, string val)
        {
            sqlite3_result_text(context, val.ToUtf8z());
        }
        /// <summary>
        /// 双精度结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_double(sqlite3_context context, double val)
        {
            Provider.sqlite3_result_double(context.ptr, val);
        }
        /// <summary>
        /// 整型结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_int(sqlite3_context context, int val)
        {
            Provider.sqlite3_result_int(context.ptr, val);
        }
        /// <summary>
        /// 长整型结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        public static void sqlite3_result_int64(sqlite3_context context, long val)
        {
            Provider.sqlite3_result_int64(context.ptr, val);
        }
        /// <summary>
        /// 〇大对象结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="n"></param>
        public static void sqlite3_result_zeroblob(sqlite3_context context, int n)
        {
            Provider.sqlite3_result_zeroblob(context.ptr, n);
        }

        // TODO sqlite3_result_value

        /// <summary>
        /// 错误结果太大
        /// </summary>
        /// <param name="context"></param>
        public static void sqlite3_result_error_toobig(sqlite3_context context)
        {
            Provider.sqlite3_result_error_toobig(context.ptr);
        }
        /// <summary>
        /// 错误结果内存
        /// </summary>
        /// <param name="context"></param>
        public static void sqlite3_result_error_nomem(sqlite3_context context)
        {
            Provider.sqlite3_result_error_nomem(context.ptr);
        }
        /// <summary>
        /// 错误结果代码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="code"></param>
        public static void sqlite3_result_error_code(sqlite3_context context, int code)
        {
            Provider.sqlite3_result_error_code(context.ptr, code);
        }

#if NET40
        /// <summary>
        /// 值大对象
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] sqlite3_value_blob(sqlite3_value val)
        {
            return Provider.sqlite3_value_blob(val.ptr);
        }
#else
        /// <summary>
        /// 值大对象
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ReadOnlySpan<byte> sqlite3_value_blob(sqlite3_value val)
        {
            return Provider.sqlite3_value_blob(val.ptr);
        }
#endif
        /// <summary>
        /// 值字节
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_value_bytes(sqlite3_value val)
        {
            return Provider.sqlite3_value_bytes(val.ptr);
        }
        /// <summary>
        /// 值浮点数
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double sqlite3_value_double(sqlite3_value val)
        {
            return Provider.sqlite3_value_double(val.ptr);
        }
        /// <summary>
        /// 值整型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_value_int(sqlite3_value val)
        {
            return Provider.sqlite3_value_int(val.ptr);
        }
        /// <summary>
        /// 值长整型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long sqlite3_value_int64(sqlite3_value val)
        {
            return Provider.sqlite3_value_int64(val.ptr);
        }
        /// <summary>
        /// 值类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_value_type(sqlite3_value val)
        {
            return Provider.sqlite3_value_type(val.ptr);
        }
        /// <summary>
        /// 值文本
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_value_text(sqlite3_value val)
        {
            return Provider.sqlite3_value_text(val.ptr);
        }

#if NET40
        /// <summary>
        /// 绑定大对象
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_bind_blob(sqlite3_stmt stmt, int index, byte[] blob)
        {
            return Provider.sqlite3_bind_blob(stmt, index, blob);
        }
#else
        /// <summary>
        /// 绑定大对象
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_bind_blob(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> blob)
        {
            return Provider.sqlite3_bind_blob(stmt, index, blob);
        }
#endif
        /// <summary>
        /// 绑定浮点数
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_double(sqlite3_stmt stmt, int index, double val)
        {
            return Provider.sqlite3_bind_double(stmt, index, val);
        }
        /// <summary>
        /// 绑定整型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_int(sqlite3_stmt stmt, int index, int val)
        {
            return Provider.sqlite3_bind_int(stmt, index, val);
        }
        /// <summary>
        /// 绑定长整型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_int64(sqlite3_stmt stmt, int index, long val)
        {
            return Provider.sqlite3_bind_int64(stmt, index, val);
        }
        /// <summary>
        /// 绑定null
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int sqlite3_bind_null(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_bind_null(stmt, index);
        }

#if NET40
        /// <summary>
        /// 绑定文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text(sqlite3_stmt stmt, int index, byte[] val)
        {
            return Provider.sqlite3_bind_text(stmt, index, val);
        }
        /// <summary>
        /// 绑定长文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text16(sqlite3_stmt stmt, int index, string val)
        {
            return Provider.sqlite3_bind_text16(stmt, index, val);
        }
#else
        /// <summary>
        /// 绑定文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> val)
        {
            return Provider.sqlite3_bind_text(stmt, index, val);
        }
        /// <summary>
        /// 绑定长文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text16(sqlite3_stmt stmt, int index, ReadOnlySpan<char> val)
        {
            return Provider.sqlite3_bind_text16(stmt, index, val);
        }
#endif
        /// <summary>
        /// 绑定文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text(sqlite3_stmt stmt, int index, Utf8z val)
        {
            return Provider.sqlite3_bind_text(stmt, index, val);
        }
        /// <summary>
        /// 绑定文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int sqlite3_bind_text(sqlite3_stmt stmt, int index, string val)
        {
            const int OptimizedLengthThreshold = 512;

            // stackalloc the conversion to bytes for small strings to help avoid unnecessary GC pressure. This is
            // ultimately safe as we pass the SQLITE_TRANSIENT (https://www.sqlite.org/c3ref/c_static.html) flag to
            // sqlite, which causes them to create their own copy.

            // Don't bother doing the linear GetByteCount check for strings that are certainly too large to fit within
            // our byte count threshold.
            if (val != null && val.Length <= OptimizedLengthThreshold)
            {
                var utf8ByteCount = Encoding.UTF8.GetByteCount(val);
                if ((utf8ByteCount <= OptimizedLengthThreshold) && (utf8ByteCount > 0))
                {
#if NET40
                    var bytes = new byte[utf8ByteCount];
#else
                    Span<byte> bytes = stackalloc byte[utf8ByteCount];
#endif
                    unsafe
                    {
                        fixed (char* charsPtr = val)
                        fixed (byte* bytesPtr = bytes)
                        {
                            Encoding.UTF8.GetBytes(charsPtr, val.Length, bytesPtr, utf8ByteCount);
                        }
                    }

                    return sqlite3_bind_text(stmt, index, bytes);
                }
            }

            return sqlite3_bind_text(stmt, index, val.ToUtf8z());
        }
        /// <summary>
        /// 绑定参数计数
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_bind_parameter_count(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_bind_parameter_count(stmt);
        }
        /// <summary>
        /// 绑定参数索引
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static int sqlite3_bind_parameter_index(sqlite3_stmt stmt, Utf8z strName)
        {
            return Provider.sqlite3_bind_parameter_index(stmt, strName);
        }
        /// <summary>
        /// 绑定参数索引
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static int sqlite3_bind_parameter_index(sqlite3_stmt stmt, string strName)
        {
            return sqlite3_bind_parameter_index(stmt, strName.ToUtf8z());
        }
        /// <summary>
        /// stmt是解释
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_stmt_isexplain(sqlite3_stmt stmt)
        {
            return _imp.sqlite3_stmt_isexplain(stmt);
        }
        /// <summary>
        /// stmt繁忙
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_stmt_busy(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_stmt_busy(stmt);
        }
        /// <summary>
        /// stmt只读
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_stmt_readonly(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_stmt_readonly(stmt);
        }
        /// <summary>
        /// 列库名
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_database_name(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_database_name(stmt, index);
        }
        /// <summary>
        /// 列名
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_name(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_name(stmt, index);
        }
        /// <summary>
        /// 列原名
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_origin_name(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_origin_name(stmt, index);
        }
        /// <summary>
        /// 列表名
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_table_name(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_table_name(stmt, index);
        }
        /// <summary>
        /// 列文本
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_text(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_text(stmt, index);
        }
        /// <summary>
        /// 列计数
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_column_count(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_column_count(stmt);
        }
        /// <summary>
        /// 数据计数
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static int sqlite3_data_count(sqlite3_stmt stmt)
        {
            return Provider.sqlite3_data_count(stmt);
        }
        /// <summary>
        /// 列浮点型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double sqlite3_column_double(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_double(stmt, index);
        }
        /// <summary>
        /// 列整型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int sqlite3_column_int(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_int(stmt, index);
        }
        /// <summary>
        /// 列长整型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static long sqlite3_column_int64(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_int64(stmt, index);
        }

#if NET40
        /// <summary>
        /// 列大对象
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte[] sqlite3_column_blob(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_blob(stmt, index);
        }
#else
        /// <summary>
        /// 列大对象
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ReadOnlySpan<byte> sqlite3_column_blob(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_blob(stmt, index);
        }
#endif
        /// <summary>
        /// 列字节
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int sqlite3_column_bytes(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_bytes(stmt, index);
        }
        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int sqlite3_column_type(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_type(stmt, index);
        }
        /// <summary>
        /// 表达式数据类型
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Utf8z sqlite3_column_decltype(sqlite3_stmt stmt, int index)
        {
            return Provider.sqlite3_column_decltype(stmt, index);
        }
        /// <summary>
        /// 备份初始化
        /// </summary>
        /// <param name="destDb"></param>
        /// <param name="destName"></param>
        /// <param name="sourceDb"></param>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public static sqlite3_backup sqlite3_backup_init(sqlite3 destDb, string destName, sqlite3 sourceDb, string sourceName)
        {
            return Provider.sqlite3_backup_init(destDb, destName.ToUtf8z(), sourceDb, sourceName.ToUtf8z());
        }
        /// <summary>
        /// 备份编译
        /// </summary>
        /// <param name="backup"></param>
        /// <param name="nPage"></param>
        /// <returns></returns>
        public static int sqlite3_backup_step(sqlite3_backup backup, int nPage)
        {
            return Provider.sqlite3_backup_step(backup, nPage);
        }
        /// <summary>
        /// 备份剩余
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        public static int sqlite3_backup_remaining(sqlite3_backup backup)
        {
            return Provider.sqlite3_backup_remaining(backup);
        }
        /// <summary>
        /// 备份页计数
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        public static int sqlite3_backup_pagecount(sqlite3_backup backup)
        {
            return Provider.sqlite3_backup_pagecount(backup);
        }

        /// <summary>
        /// called by something that wants the return code
        /// 备份完成
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        public static int sqlite3_backup_finish(sqlite3_backup backup)
        {
            return backup.manual_close();
        }

        /// <summary>
        /// 备份完成
        /// this is called by the SafeHandle
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        static internal int internal_sqlite3_backup_finish(IntPtr p)
        {
            return Provider.sqlite3_backup_finish(p);
        }
        /// <summary>
        /// 快照获取
        /// </summary>
        /// <param name="db"></param>
        /// <param name="schema"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static int sqlite3_snapshot_get(sqlite3 db, string schema, out sqlite3_snapshot snap)
        {
            var rc = Provider.sqlite3_snapshot_get(db, schema.ToUtf8z(), out var p);
            snap = sqlite3_snapshot.From(p);
            return rc;
        }
        /// <summary>
        /// 快照单核心
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static int sqlite3_snapshot_cmp(sqlite3_snapshot p1, sqlite3_snapshot p2)
        {
            return Provider.sqlite3_snapshot_cmp(p1, p2);
        }
        /// <summary>
        /// 快照打开
        /// </summary>
        /// <param name="db"></param>
        /// <param name="schema"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        public static int sqlite3_snapshot_open(sqlite3 db, string schema, sqlite3_snapshot snap)
        {
            return Provider.sqlite3_snapshot_open(db, schema.ToUtf8z(), snap);
        }
        /// <summary>
        /// 快照覆盖
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int sqlite3_snapshot_recover(sqlite3 db, string name)
        {
            return Provider.sqlite3_snapshot_recover(db, name.ToUtf8z());
        }
        /// <summary>
        /// 快照释放
        /// </summary>
        /// <param name="snap"></param>
        public static void sqlite3_snapshot_free(sqlite3_snapshot snap)
        {
            snap.manual_close();
        }

        /// <summary>
        /// this is called by the SafeHandle
        /// 内部快照释放
        /// </summary>
        /// <param name="p"></param>
        static internal void internal_sqlite3_snapshot_free(IntPtr p)
        {
            Provider.sqlite3_snapshot_free(p);
        }
        /// <summary>
        /// 大对象打开
        /// </summary>
        /// <param name="db"></param>
        /// <param name="db_utf8"></param>
        /// <param name="table_utf8"></param>
        /// <param name="col_utf8"></param>
        /// <param name="rowid"></param>
        /// <param name="flags"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_blob_open(sqlite3 db, Utf8z db_utf8, Utf8z table_utf8, Utf8z col_utf8, long rowid, int flags, out sqlite3_blob blob)
        {
            return Provider.sqlite3_blob_open(db, db_utf8, table_utf8, col_utf8, rowid, flags, out blob);
        }
        /// <summary>
        /// 大对象打开
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sdb"></param>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <param name="rowid"></param>
        /// <param name="flags"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_blob_open(sqlite3 db, string sdb, string table, string col, long rowid, int flags, out sqlite3_blob blob)
        {
            return sqlite3_blob_open(db, sdb.ToUtf8z(), table.ToUtf8z(), col.ToUtf8z(), rowid, flags, out blob);
        }
        /// <summary>
        /// 大对象字节
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_blob_bytes(sqlite3_blob blob)
        {
            return Provider.sqlite3_blob_bytes(blob);
        }
        /// <summary>
        /// 大对象重新打开
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static int sqlite3_blob_reopen(sqlite3_blob blob, long rowid)
        {
            return Provider.sqlite3_blob_reopen(blob, rowid);
        }

#if NET40
        /// <summary>
        /// 大对象写
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int sqlite3_blob_write(sqlite3_blob blob, byte[] b, int offset)
        {
            return Provider.sqlite3_blob_write(blob, b, offset);
        }
        /// <summary>
        /// 大对象读
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int sqlite3_blob_read(sqlite3_blob blob, byte[] b, int offset)
        {
            return Provider.sqlite3_blob_read(blob, b, offset);
        }
#else
        /// <summary>
        /// 大对象写
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int sqlite3_blob_write(sqlite3_blob blob, ReadOnlySpan<byte> b, int offset)
        {
            return Provider.sqlite3_blob_write(blob, b, offset);
        }
        /// <summary>
        /// 大对象读
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int sqlite3_blob_read(sqlite3_blob blob, Span<byte> b, int offset)
        {
            return Provider.sqlite3_blob_read(blob, b, offset);
        }
#endif
        /// <summary>
        /// called by something that wants the return code
        /// 大对象关闭
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static int sqlite3_blob_close(sqlite3_blob blob)
        {
            return blob.manual_close();
        }

        /// <summary>
        /// this is called by the SafeHandle
        /// 大对象关闭
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        static internal int internal_sqlite3_blob_close(IntPtr blob)
        {
            return Provider.sqlite3_blob_close(blob);
        }
        /// <summary>
        /// wal自动检查点
        /// </summary>
        /// <param name="db"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int sqlite3_wal_autocheckpoint(sqlite3 db, int n)
        {
            return Provider.sqlite3_wal_autocheckpoint(db, n);
        }
        /// <summary>
        /// wal检查点
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static int sqlite3_wal_checkpoint(sqlite3 db, string dbName)
        {
            return Provider.sqlite3_wal_checkpoint(db, dbName.ToUtf8z());
        }
        /// <summary>
        /// wal检查点2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <param name="eMode"></param>
        /// <param name="logSize"></param>
        /// <param name="framesCheckPointed"></param>
        /// <returns></returns>
        public static int sqlite3_wal_checkpoint_v2(sqlite3 db, string dbName, int eMode, out int logSize, out int framesCheckPointed)
        {
            return Provider.sqlite3_wal_checkpoint_v2(db, dbName.ToUtf8z(), eMode, out logSize, out framesCheckPointed);
        }
        /// <summary>
        /// 设置认证
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="user_data"></param>
        /// <returns></returns>
        public static int sqlite3_set_authorizer(sqlite3 db, delegate_authorizer f, object user_data)
        {
            return Provider.sqlite3_set_authorizer(db, f, user_data);
        }
        /// <summary>
        /// 设置认证
        /// </summary>
        /// <param name="db"></param>
        /// <param name="f"></param>
        /// <param name="user_data"></param>
        /// <returns></returns>
        public static int sqlite3_set_authorizer(sqlite3 db, strdelegate_authorizer f, object user_data)
        {
            delegate_authorizer cb = f == null ? null : ((ob, a, p0, p1, dbname, v)
                => f(ob, a, p0.utf8_to_string(), p1.utf8_to_string(), dbname.utf8_to_string(), v.utf8_to_string()));
            return sqlite3_set_authorizer(db, cb, user_data);
        }
        /// <summary>
        /// 设置目录
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int sqlite3_win32_set_directory(int typ, string path)
        {
            return Provider.sqlite3_win32_set_directory(typ, path.ToUtf8z());
        }
        /// <summary>
        /// 关键字计数
        /// </summary>
        /// <returns></returns>
        public static int sqlite3_keyword_count()
        {
            return Provider.sqlite3_keyword_count();
        }
        /// <summary>
        /// 关键字名称
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int sqlite3_keyword_name(int i, out string name)
        {
            return Provider.sqlite3_keyword_name(i, out name);
        }
    }
}
