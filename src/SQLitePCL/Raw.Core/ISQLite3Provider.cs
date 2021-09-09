using System;

namespace SQLitePCL.Raw.Core
{
#if NET40
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int delegate_collation(object user_data, byte[] s1, byte[] s2);
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int delegate_collation(object user_data, ReadOnlySpan<byte> s1, ReadOnlySpan<byte> s2);
#endif
    /// <summary>
    /// 更新委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="type"></param>
    /// <param name="database"></param>
    /// <param name="table"></param>
    /// <param name="rowid"></param>
    public delegate void delegate_update(object user_data, int type, Utf8z database, Utf8z table, long rowid);
    /// <summary>
    /// 日志委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="errorCode"></param>
    /// <param name="msg"></param>
    public delegate void delegate_log(object user_data, int errorCode, Utf8z msg);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="action_code"></param>
    /// <param name="param0"></param>
    /// <param name="param1"></param>
    /// <param name="dbName"></param>
    /// <param name="inner_most_trigger_or_view"></param>
    /// <returns></returns>
    public delegate int delegate_authorizer(object user_data, int action_code, Utf8z param0, Utf8z param1, Utf8z dbName, Utf8z inner_most_trigger_or_view);

    /// <summary>
    /// this delegate returns strings as IntPtrs because they are in an array,
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="values"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public delegate int delegate_exec(object user_data, IntPtr[] values, IntPtr[] names);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <returns></returns>
    public delegate int delegate_commit(object user_data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    public delegate void delegate_rollback(object user_data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="statement"></param>
    public delegate void delegate_trace(object user_data, Utf8z statement);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="statement"></param>
    /// <param name="ns"></param>
    public delegate void delegate_profile(object user_data, Utf8z statement, long ns);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user_data"></param>
    /// <returns></returns>
    public delegate int delegate_progress(object user_data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    /// <param name="args"></param>
    public delegate void delegate_function_scalar(sqlite3_context ctx, object user_data, sqlite3_value[] args);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    /// <param name="args"></param>
    public delegate void delegate_function_aggregate_step(sqlite3_context ctx, object user_data, sqlite3_value[] args);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    public delegate void delegate_function_aggregate_final(sqlite3_context ctx, object user_data);

    /// <summary>
    ///
    /// This interface provides core functionality of the SQLite3 API.  It is the
    /// boundary between the portable class library and the platform-specific code
    /// below.
    ///
    /// In general, it is defined to be as low-level as possible while still remaninig
    /// "portable".  For example, a sqlite3 connection handle appears here as an IntPtr.
    /// Same goes for the C-level sqlite3_stmt pointer, also an IntPtr.
    ///
    /// This whole library is designed in 4 layers:
    ///
    /// (1)  The SQLite C API itself
    ///
    /// (2)  The declarations of the C API.  pinvoke.
    ///
    /// (3)  A C# layer in the platform assembly which implements this interface.
    ///
    /// (4)  The raw API, here in the PCL, which wraps an instance of this interface in
    ///      an API which replaces all the IntPtrs with strong typed (but still opaque) 
    ///      counterparts and converts strings to/from utf8..
    ///
    /// Even the top layer is still very low-level, which is why it is called "raw".
    /// This API is not intended to be used by app developers.  Rather it is designed
    /// to be a portable foundation for higher-level SQLite APIs.
    ///
    /// The philosophy of this library is to remain as similar to the underlying
    /// SQLite API as possible, even to the point of keeping the sqlite3_style_names
    /// and style.  It is expected that higher-level APIs built on this wrapper
    /// would present an API which is friendlier to C# developers.
    ///
    /// </summary>
    public interface ISQLite3Provider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetNativeLibraryName();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_open(Utf8z filename, out IntPtr db);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <param name="vfs"></param>
        /// <returns></returns>
        int sqlite3_open_v2(Utf8z filename, out IntPtr db, int flags, Utf8z vfs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_close_v2(IntPtr db); /* 3.7.14+ */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_close(IntPtr db);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        int sqlite3_enable_shared_cache(int enable);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        void sqlite3_interrupt(sqlite3 db);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vfs"></param>
        /// <param name="pathname"></param>
        /// <param name="syncDir"></param>
        /// <returns></returns>
        int sqlite3__vfs__delete(Utf8z vfs, Utf8z pathname, int syncDir);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int sqlite3_threadsafe();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Utf8z sqlite3_libversion();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int sqlite3_libversion_number();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Utf8z sqlite3_sourceid();
        long sqlite3_memory_used();
        long sqlite3_memory_highwater(int resetFlag);
        long sqlite3_soft_heap_limit64(long n);
        long sqlite3_hard_heap_limit64(long n);
        int sqlite3_status(int op, out int current, out int highwater, int resetFlag);

        int sqlite3_db_readonly(sqlite3 db, Utf8z dbName);
        Utf8z sqlite3_db_filename(sqlite3 db, Utf8z att);
        Utf8z sqlite3_errmsg(sqlite3 db);
        long sqlite3_last_insert_rowid(sqlite3 db);
        int sqlite3_changes(sqlite3 db);
        int sqlite3_total_changes(sqlite3 db);
        int sqlite3_get_autocommit(sqlite3 db);
        int sqlite3_busy_timeout(sqlite3 db, int ms);

        int sqlite3_extended_result_codes(sqlite3 db, int onoff);
        int sqlite3_errcode(sqlite3 db);
        int sqlite3_extended_errcode(sqlite3 db);
        Utf8z sqlite3_errstr(int rc); /* 3.7.15+ */

#if NET40
        int sqlite3_prepare_v2(sqlite3 db, byte[] sql, out IntPtr stmt, out byte[] remain);
        int sqlite3_prepare_v3(sqlite3 db, byte[] sql, uint flags, out IntPtr stmt, out byte[] remain);
#else
        int sqlite3_prepare_v2(sqlite3 db, ReadOnlySpan<byte> sql, out IntPtr stmt, out ReadOnlySpan<byte> remain);
        int sqlite3_prepare_v3(sqlite3 db, ReadOnlySpan<byte> sql, uint flags, out IntPtr stmt, out ReadOnlySpan<byte> remain);
#endif

        [Obsolete]
        int sqlite3_prepare_v2(sqlite3 db, Utf8z sql, out IntPtr stmt, out Utf8z remain);

        [Obsolete]
        int sqlite3_prepare_v3(sqlite3 db, Utf8z sql, uint flags, out IntPtr stmt, out Utf8z remain);

        int sqlite3_step(sqlite3_stmt stmt);
        int sqlite3_finalize(IntPtr stmt);
        int sqlite3_reset(sqlite3_stmt stmt);
        int sqlite3_clear_bindings(sqlite3_stmt stmt);
        int sqlite3_stmt_status(sqlite3_stmt stmt, int op, int resetFlg);
        Utf8z sqlite3_sql(sqlite3_stmt stmt);
        IntPtr sqlite3_db_handle(IntPtr stmt);
        IntPtr sqlite3_next_stmt(sqlite3 db, IntPtr stmt);

        int sqlite3_bind_zeroblob(sqlite3_stmt stmt, int index, int size);
        Utf8z sqlite3_bind_parameter_name(sqlite3_stmt stmt, int index);
#if NET40
        int sqlite3_bind_blob(sqlite3_stmt stmt, int index, byte[] blob);
#else
        int sqlite3_bind_blob(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> blob);
#endif
        int sqlite3_bind_double(sqlite3_stmt stmt, int index, double val);
        int sqlite3_bind_int(sqlite3_stmt stmt, int index, int val);
        int sqlite3_bind_int64(sqlite3_stmt stmt, int index, long val);
        int sqlite3_bind_null(sqlite3_stmt stmt, int index);
#if NET40
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, byte[] text);
        int sqlite3_bind_text16(sqlite3_stmt stmt, int index, string text);
#else
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> text);
        int sqlite3_bind_text16(sqlite3_stmt stmt, int index, ReadOnlySpan<char> text);
#endif
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, Utf8z text);
        int sqlite3_bind_parameter_count(sqlite3_stmt stmt);
        int sqlite3_bind_parameter_index(sqlite3_stmt stmt, Utf8z strName);

        Utf8z sqlite3_column_database_name(sqlite3_stmt stmt, int index);
        Utf8z sqlite3_column_name(sqlite3_stmt stmt, int index);
        Utf8z sqlite3_column_origin_name(sqlite3_stmt stmt, int index);
        Utf8z sqlite3_column_table_name(sqlite3_stmt stmt, int index);
        Utf8z sqlite3_column_text(sqlite3_stmt stmt, int index);
        int sqlite3_data_count(sqlite3_stmt stmt);
        int sqlite3_column_count(sqlite3_stmt stmt);
        double sqlite3_column_double(sqlite3_stmt stmt, int index);
        int sqlite3_column_int(sqlite3_stmt stmt, int index);
        long sqlite3_column_int64(sqlite3_stmt stmt, int index);
#if NET40
        byte[] sqlite3_column_blob(sqlite3_stmt stmt, int index);
#else
        ReadOnlySpan<byte> sqlite3_column_blob(sqlite3_stmt stmt, int index);
#endif
        int sqlite3_column_bytes(sqlite3_stmt stmt, int index);
        int sqlite3_column_type(sqlite3_stmt stmt, int index);
        Utf8z sqlite3_column_decltype(sqlite3_stmt stmt, int index);

        int sqlite3_snapshot_get(sqlite3 db, Utf8z schema, out IntPtr snap);
        int sqlite3_snapshot_cmp(sqlite3_snapshot p1, sqlite3_snapshot p2);
        int sqlite3_snapshot_open(sqlite3 db, Utf8z schema, sqlite3_snapshot snap);
        int sqlite3_snapshot_recover(sqlite3 db, Utf8z name);
        void sqlite3_snapshot_free(IntPtr snap);

        sqlite3_backup sqlite3_backup_init(sqlite3 destDb, Utf8z destName, sqlite3 sourceDb, Utf8z sourceName);
        int sqlite3_backup_step(sqlite3_backup backup, int nPage);
        int sqlite3_backup_remaining(sqlite3_backup backup);
        int sqlite3_backup_pagecount(sqlite3_backup backup);
        int sqlite3_backup_finish(IntPtr backup);

        int sqlite3_blob_open(sqlite3 db, Utf8z db_utf8, Utf8z table_utf8, Utf8z col_utf8, long rowid, int flags, out sqlite3_blob blob);
        int sqlite3_blob_bytes(sqlite3_blob blob);
        int sqlite3_blob_reopen(sqlite3_blob blob, long rowid);
#if NET40
        int sqlite3_blob_write(sqlite3_blob blob, byte[] b, int offset);
        int sqlite3_blob_read(sqlite3_blob blob, byte[] b, int offset);
#else
        int sqlite3_blob_write(sqlite3_blob blob, ReadOnlySpan<byte> b, int offset);
        int sqlite3_blob_read(sqlite3_blob blob, Span<byte> b, int offset);
#endif
        int sqlite3_blob_close(IntPtr blob);

        int sqlite3_config_log(delegate_log func, object v);
        void sqlite3_log(int errcode, Utf8z s);
        void sqlite3_commit_hook(sqlite3 db, delegate_commit func, object v);
        void sqlite3_rollback_hook(sqlite3 db, delegate_rollback func, object v);

        void sqlite3_trace(sqlite3 db, delegate_trace func, object v);
        void sqlite3_profile(sqlite3 db, delegate_profile func, object v);

        void sqlite3_progress_handler(sqlite3 db, int instructions, delegate_progress func, object v);
        void sqlite3_update_hook(sqlite3 db, delegate_update func, object v);
        int sqlite3_create_collation(sqlite3 db, byte[] name, object v, delegate_collation func);
        int sqlite3_create_function(sqlite3 db, byte[] name, int nArg, int flags, object v, delegate_function_scalar func);
        int sqlite3_create_function(sqlite3 db, byte[] name, int nArg, int flags, object v, delegate_function_aggregate_step func_step, delegate_function_aggregate_final func_final);

        int sqlite3_db_status(sqlite3 db, int op, out int current, out int highest, int resetFlg);

#if NET40
        void sqlite3_result_blob(IntPtr context, byte[] val);
#else
        void sqlite3_result_blob(IntPtr context, ReadOnlySpan<byte> val);
#endif
        void sqlite3_result_double(IntPtr context, double val);
#if NET40
        void sqlite3_result_error(IntPtr context, byte[] strErr);
#else
        void sqlite3_result_error(IntPtr context, ReadOnlySpan<byte> strErr);
#endif
        void sqlite3_result_error(IntPtr context, Utf8z strErr);
        void sqlite3_result_int(IntPtr context, int val);
        void sqlite3_result_int64(IntPtr context, long val);
        void sqlite3_result_null(IntPtr context);
#if NET40
        void sqlite3_result_text(IntPtr context, byte[] val);
#else
        void sqlite3_result_text(IntPtr context, ReadOnlySpan<byte> val);
#endif
        void sqlite3_result_text(IntPtr context, Utf8z val);
        void sqlite3_result_zeroblob(IntPtr context, int n);
        // TODO sqlite3_result_value
        void sqlite3_result_error_toobig(IntPtr context);
        void sqlite3_result_error_nomem(IntPtr context);
        void sqlite3_result_error_code(IntPtr context, int code);
#if NET40
        byte[] sqlite3_value_blob(IntPtr p);
#else
        ReadOnlySpan<byte> sqlite3_value_blob(IntPtr p);
#endif
        int sqlite3_value_bytes(IntPtr p);
        double sqlite3_value_double(IntPtr p);
        int sqlite3_value_int(IntPtr p);
        long sqlite3_value_int64(IntPtr p);
        int sqlite3_value_type(IntPtr p);
        Utf8z sqlite3_value_text(IntPtr p);

        int sqlite3_stmt_isexplain(sqlite3_stmt stmt);
        int sqlite3_stmt_busy(sqlite3_stmt stmt);
        int sqlite3_stmt_readonly(sqlite3_stmt stmt);

        // this function returns the errMsg as an IntPtr because it needs to be freed.
        int sqlite3_exec(sqlite3 db, Utf8z sql, delegate_exec callback, object user_data, out IntPtr errMsg);

        int sqlite3_complete(Utf8z sql);

        int sqlite3_compileoption_used(Utf8z sql);
        Utf8z sqlite3_compileoption_get(int n);

        int sqlite3_wal_autocheckpoint(sqlite3 db, int n);
        int sqlite3_wal_checkpoint(sqlite3 db, Utf8z dbName);
        int sqlite3_wal_checkpoint_v2(sqlite3 db, Utf8z dbName, int eMode, out int logSize, out int framesCheckPointed);

        int sqlite3_table_column_metadata(sqlite3 db, Utf8z dbName, Utf8z tblName, Utf8z colName, out Utf8z dataType, out Utf8z collSeq, out int notNull, out int primaryKey, out int autoInc);

        int sqlite3_set_authorizer(sqlite3 db, delegate_authorizer authorizer, object user_data);

        // TODO the following two calls wish the args were spans
        int sqlite3_stricmp(IntPtr p, IntPtr q);
        int sqlite3_strnicmp(IntPtr p, IntPtr q, int n);

        void sqlite3_free(IntPtr p);
#if NET40
        int sqlite3_key(sqlite3 db, byte[] key);
        int sqlite3_key_v2(sqlite3 db, Utf8z dbname, byte[] key);
        int sqlite3_rekey(sqlite3 db, byte[] key);
        int sqlite3_rekey_v2(sqlite3 db, Utf8z dbname, byte[] key);
#else
        int sqlite3_key(sqlite3 db, ReadOnlySpan<byte> key);
        int sqlite3_key_v2(sqlite3 db, Utf8z dbname, ReadOnlySpan<byte> key);
        int sqlite3_rekey(sqlite3 db, ReadOnlySpan<byte> key);
        int sqlite3_rekey_v2(sqlite3 db, Utf8z dbname, ReadOnlySpan<byte> key);
#endif
#if not // TODO consider
        int sqlite3_load_extension(sqlite3 db, utf8z fileName, utf8z procName, ref IntPtr pError);
#endif

        int sqlite3_initialize();
        int sqlite3_shutdown();

        int sqlite3_limit(sqlite3 db, int id, int newVal);

        // sqlite3_config() takes a variable argument list
        int sqlite3_config(int op);
        int sqlite3_config(int op, int val);

        // sqlite3_db_config() takes a variable argument list
        int sqlite3_db_config(sqlite3 db, int op, Utf8z val);
        int sqlite3_db_config(sqlite3 db, int op, int val, out int result);
        int sqlite3_db_config(sqlite3 db, int op, IntPtr ptr, int int0, int int1);

        int sqlite3_enable_load_extension(sqlite3 db, int enable);


        int sqlite3_win32_set_directory(int typ, Utf8z path);

		int sqlite3_keyword_count();
		int sqlite3_keyword_name(int i, out string name);
    }
}

