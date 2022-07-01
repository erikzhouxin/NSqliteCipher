using System;

namespace SQLitePCL.Raw.Core
{
#if NET40
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int delegate_collation(object user_data, byte[] s1, byte[] s2);
#else
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int delegate_collation(object user_data, ReadOnlySpan<byte> s1, ReadOnlySpan<byte> s2);
#endif
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="type"></param>
    /// <param name="database"></param>
    /// <param name="table"></param>
    /// <param name="rowid"></param>
    public delegate void delegate_update(object user_data, int type, Utf8z database, Utf8z table, long rowid);
    /// <summary>
    /// ��־ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="errorCode"></param>
    /// <param name="msg"></param>
    public delegate void delegate_log(object user_data, int errorCode, Utf8z msg);
    /// <summary>
    /// ��Ȩί��
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
    /// ִ��ί��
    /// this delegate returns strings as IntPtrs because they are in an array,
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="values"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public delegate int delegate_exec(object user_data, IntPtr[] values, IntPtr[] names);
    /// <summary>
    /// �ύί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <returns></returns>
    public delegate int delegate_commit(object user_data);
    /// <summary>
    /// �ع�ί��
    /// </summary>
    /// <param name="user_data"></param>
    public delegate void delegate_rollback(object user_data);
    /// <summary>
    /// ׷��ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="statement"></param>
    public delegate void delegate_trace(object user_data, Utf8z statement);
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="statement"></param>
    /// <param name="ns"></param>
    public delegate void delegate_profile(object user_data, Utf8z statement, long ns);
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="user_data"></param>
    /// <returns></returns>
    public delegate int delegate_progress(object user_data);
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    /// <param name="args"></param>
    public delegate void delegate_function_scalar(sqlite3_context ctx, object user_data, sqlite3_value[] args);
    /// <summary>
    /// �ۺ�ί��(����)
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    /// <param name="args"></param>
    public delegate void delegate_function_aggregate_step(sqlite3_context ctx, object user_data, sqlite3_value[] args);
    /// <summary>
    /// �ۺ�ί��(���)
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="user_data"></param>
    public delegate void delegate_function_aggregate_final(sqlite3_context ctx, object user_data);

    /// <summary>
    /// �˽ӿ�ΪSQLite3API�ĺ��Ľӿ�
    /// ʵ�ִ˽ӿ�Ϊ����ֲ���ı��
    /// ����һ���ײ�Ķ���
    /// �������Ϊ4��:
    /// 1.SQLite C API����
    /// 2.C API������(PInvoke)
    /// 3.ʵ�ָýӿڵ������(C#����Providerʵ��)
    /// 4.RawCore���࣬������Provider�ľ���ʵ�֣�������IntPtrs�滻Ϊǿ����(����Ȼ��͸��)��API��ת���ַ�����/��utf8..
    /// 
    /// �����Ļ���ԭ���Ǳ�����ײ�����Ƶ�SQLite API����������sqlite3_style_names�ͷ��
    /// ����RawCore�����Ǻܵײ��API��Ϊ�߼����Ѻõ�Ӧ�õ춨����ֲ����
    /// </summary>
    public interface ISQLite3Provider
    {
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        string GetNativeLibraryName();
        /// <summary>
        /// �����ݿ�
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_open(Utf8z filename, out IntPtr db);
        /// <summary>
        /// �����ݿ�
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <param name="vfs"></param>
        /// <returns></returns>
        int sqlite3_open_v2(Utf8z filename, out IntPtr db, int flags, Utf8z vfs);
        /// <summary>
        /// �ر����ݿ�
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_close_v2(IntPtr db); /* 3.7.14+ */
        /// <summary>
        /// �ر����ݿ�
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_close(IntPtr db);
        /// <summary>
        /// ���ù����ڴ�
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        int sqlite3_enable_shared_cache(int enable);
        /// <summary>
        /// �ж�
        /// </summary>
        /// <param name="db"></param>
        void sqlite3_interrupt(sqlite3 db);
        /// <summary>
        /// vfsɾ��
        /// </summary>
        /// <param name="vfs"></param>
        /// <param name="pathname"></param>
        /// <param name="syncDir"></param>
        /// <returns></returns>
        int sqlite3__vfs__delete(Utf8z vfs, Utf8z pathname, int syncDir);
        /// <summary>
        /// �̰߳�ȫ
        /// </summary>
        /// <returns></returns>
        int sqlite3_threadsafe();
        /// <summary>
        /// ���汾
        /// </summary>
        /// <returns></returns>
        Utf8z sqlite3_libversion();
        /// <summary>
        /// ���汾��
        /// </summary>
        /// <returns></returns>
        int sqlite3_libversion_number();
        /// <summary>
        /// Դ��ʶ
        /// </summary>
        /// <returns></returns>
        Utf8z sqlite3_sourceid();
        /// <summary>
        /// �ڴ�ʹ��
        /// </summary>
        /// <returns></returns>
        long sqlite3_memory_used();
        /// <summary>
        /// ��λ
        /// </summary>
        /// <param name="resetFlag"></param>
        /// <returns></returns>
        long sqlite3_memory_highwater(int resetFlag);
        /// <summary>
        /// ��ջ
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        long sqlite3_soft_heap_limit64(long n);
        /// <summary>
        /// ��ջ
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        long sqlite3_hard_heap_limit64(long n);
        /// <summary>
        /// ״̬
        /// </summary>
        /// <param name="op"></param>
        /// <param name="current"></param>
        /// <param name="highwater"></param>
        /// <param name="resetFlag"></param>
        /// <returns></returns>
        int sqlite3_status(int op, out int current, out int highwater, int resetFlag);
        /// <summary>
        /// ֻ��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        int sqlite3_db_readonly(sqlite3 db, Utf8z dbName);
        /// <summary>
        /// �ļ���
        /// </summary>
        /// <param name="db"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        Utf8z sqlite3_db_filename(sqlite3 db, Utf8z att);
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        Utf8z sqlite3_errmsg(sqlite3 db);
        /// <summary>
        /// ������id
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        long sqlite3_last_insert_rowid(sqlite3 db);
        /// <summary>
        /// �ı�
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_changes(sqlite3 db);
        /// <summary>
        /// �ܸı�
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_total_changes(sqlite3 db);
        /// <summary>
        /// �Զ��ύ
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_get_autocommit(sqlite3 db);
        /// <summary>
        /// ��ʱ
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        int sqlite3_busy_timeout(sqlite3 db, int ms);
        /// <summary>
        /// ��չ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        int sqlite3_extended_result_codes(sqlite3 db, int onoff);
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_errcode(sqlite3 db);
        /// <summary>
        /// ��չ�������
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        int sqlite3_extended_errcode(sqlite3 db);
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        Utf8z sqlite3_errstr(int rc); /* 3.7.15+ */

#if NET40
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        int sqlite3_prepare_v2(sqlite3 db, byte[] sql, out IntPtr stmt, out byte[] remain);
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        int sqlite3_prepare_v3(sqlite3 db, byte[] sql, uint flags, out IntPtr stmt, out byte[] remain);
#else
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        int sqlite3_prepare_v2(sqlite3 db, ReadOnlySpan<byte> sql, out IntPtr stmt, out ReadOnlySpan<byte> remain);
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        int sqlite3_prepare_v3(sqlite3 db, ReadOnlySpan<byte> sql, uint flags, out IntPtr stmt, out ReadOnlySpan<byte> remain);
#endif
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        [Obsolete]
        int sqlite3_prepare_v2(sqlite3 db, Utf8z sql, out IntPtr stmt, out Utf8z remain);
        /// <summary>
        /// ׼��
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="flags"></param>
        /// <param name="stmt"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        [Obsolete]
        int sqlite3_prepare_v3(sqlite3 db, Utf8z sql, uint flags, out IntPtr stmt, out Utf8z remain);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_step(sqlite3_stmt stmt);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_finalize(IntPtr stmt);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_reset(sqlite3_stmt stmt);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_clear_bindings(sqlite3_stmt stmt);
        /// <summary>
        /// stmt״̬
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="op"></param>
        /// <param name="resetFlg"></param>
        /// <returns></returns>
        int sqlite3_stmt_status(sqlite3_stmt stmt, int op, int resetFlg);
        /// <summary>
        /// sql���
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        Utf8z sqlite3_sql(sqlite3_stmt stmt);
        /// <summary>
        /// �⴦��
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        IntPtr sqlite3_db_handle(IntPtr stmt);
        /// <summary>
        /// ��һ��stmt
        /// </summary>
        /// <param name="db"></param>
        /// <param name="stmt"></param>
        /// <returns></returns>
        IntPtr sqlite3_next_stmt(sqlite3 db, IntPtr stmt);
        /// <summary>
        /// ��
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        int sqlite3_bind_zeroblob(sqlite3_stmt stmt, int index, int size);
        /// <summary>
        /// �󶨲�������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_bind_parameter_name(sqlite3_stmt stmt, int index);
#if NET40
        /// <summary>
        /// �󶨶���
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        int sqlite3_bind_blob(sqlite3_stmt stmt, int index, byte[] blob);
#else
        /// <summary>
        /// �󶨶���
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        int sqlite3_bind_blob(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> blob);
#endif
        /// <summary>
        /// �󶨸�����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        int sqlite3_bind_double(sqlite3_stmt stmt, int index, double val);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        int sqlite3_bind_int(sqlite3_stmt stmt, int index, int val);
        /// <summary>
        /// �󶨳�����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        int sqlite3_bind_int64(sqlite3_stmt stmt, int index, long val);
        /// <summary>
        /// �󶨿�ֵ
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int sqlite3_bind_null(sqlite3_stmt stmt, int index);
#if NET40
        /// <summary>
        /// ���ֽ�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, byte[] text);
        /// <summary>
        /// ���ı�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        int sqlite3_bind_text16(sqlite3_stmt stmt, int index, string text);
#else
        /// <summary>
        /// ���ֽ�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, ReadOnlySpan<byte> text);
        /// <summary>
        /// ���ı�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        int sqlite3_bind_text16(sqlite3_stmt stmt, int index, ReadOnlySpan<char> text);
#endif
        /// <summary>
        /// ���ı�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        int sqlite3_bind_text(sqlite3_stmt stmt, int index, Utf8z text);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_bind_parameter_count(sqlite3_stmt stmt);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        int sqlite3_bind_parameter_index(sqlite3_stmt stmt, Utf8z strName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_database_name(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_name(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ԭʼ����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_origin_name(sqlite3_stmt stmt, int index);
        /// <summary>
        /// �б���
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_table_name(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ���ı�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_text(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_data_count(sqlite3_stmt stmt);
        /// <summary>
        /// �м���
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_column_count(sqlite3_stmt stmt);
        /// <summary>
        /// �и�����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        double sqlite3_column_double(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int sqlite3_column_int(sqlite3_stmt stmt, int index);
        /// <summary>
        /// �г�����
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        long sqlite3_column_int64(sqlite3_stmt stmt, int index);
#if NET40
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        byte[] sqlite3_column_blob(sqlite3_stmt stmt, int index);
#else
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        ReadOnlySpan<byte> sqlite3_column_blob(sqlite3_stmt stmt, int index);
#endif
        /// <summary>
        /// ���ֽ�
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int sqlite3_column_bytes(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int sqlite3_column_type(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ���ʽ��������
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Utf8z sqlite3_column_decltype(sqlite3_stmt stmt, int index);
        /// <summary>
        /// ���ջ�ȡ
        /// </summary>
        /// <param name="db"></param>
        /// <param name="schema"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        int sqlite3_snapshot_get(sqlite3 db, Utf8z schema, out IntPtr snap);
        /// <summary>
        /// ���յ�оƬ
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        int sqlite3_snapshot_cmp(sqlite3_snapshot p1, sqlite3_snapshot p2);
        /// <summary>
        /// ���մ�
        /// </summary>
        /// <param name="db"></param>
        /// <param name="schema"></param>
        /// <param name="snap"></param>
        /// <returns></returns>
        int sqlite3_snapshot_open(sqlite3 db, Utf8z schema, sqlite3_snapshot snap);
        /// <summary>
        /// ���ո���
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        int sqlite3_snapshot_recover(sqlite3 db, Utf8z name);
        /// <summary>
        /// �����ͷ�
        /// </summary>
        /// <param name="snap"></param>
        void sqlite3_snapshot_free(IntPtr snap);
        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <param name="destDb"></param>
        /// <param name="destName"></param>
        /// <param name="sourceDb"></param>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        sqlite3_backup sqlite3_backup_init(sqlite3 destDb, Utf8z destName, sqlite3 sourceDb, Utf8z sourceName);
        /// <summary>
        /// ����ҳ
        /// </summary>
        /// <param name="backup"></param>
        /// <param name="nPage"></param>
        /// <returns></returns>
        int sqlite3_backup_step(sqlite3_backup backup, int nPage);
        /// <summary>
        /// ʣ��
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        int sqlite3_backup_remaining(sqlite3_backup backup);
        /// <summary>
        /// ҳ����
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        int sqlite3_backup_pagecount(sqlite3_backup backup);
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="backup"></param>
        /// <returns></returns>
        int sqlite3_backup_finish(IntPtr backup);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="db_utf8"></param>
        /// <param name="table_utf8"></param>
        /// <param name="col_utf8"></param>
        /// <param name="rowid"></param>
        /// <param name="flags"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        int sqlite3_blob_open(sqlite3 db, Utf8z db_utf8, Utf8z table_utf8, Utf8z col_utf8, long rowid, int flags, out sqlite3_blob blob);
        /// <summary>
        /// ��ȡ�ֽ�
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        int sqlite3_blob_bytes(sqlite3_blob blob);
        /// <summary>
        /// ���´�
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        int sqlite3_blob_reopen(sqlite3_blob blob, long rowid);
#if NET40
        /// <summary>
        /// д��
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int sqlite3_blob_write(sqlite3_blob blob, byte[] b, int offset);
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int sqlite3_blob_read(sqlite3_blob blob, byte[] b, int offset);
#else
        /// <summary>
        /// д��
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int sqlite3_blob_write(sqlite3_blob blob, ReadOnlySpan<byte> b, int offset);
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int sqlite3_blob_read(sqlite3_blob blob, Span<byte> b, int offset);
#endif
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        int sqlite3_blob_close(IntPtr blob);
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        int sqlite3_config_log(delegate_log func, object v);
        /// <summary>
        /// ��־
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="s"></param>
        void sqlite3_log(int errcode, Utf8z s);
        /// <summary>
        /// �ύ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_commit_hook(sqlite3 db, delegate_commit func, object v);
        /// <summary>
        /// �ع�����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_rollback_hook(sqlite3 db, delegate_rollback func, object v);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_trace(sqlite3 db, delegate_trace func, object v);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_profile(sqlite3 db, delegate_profile func, object v);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="instructions"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_progress_handler(sqlite3 db, int instructions, delegate_progress func, object v);
        /// <summary>
        /// ���»ص�
        /// </summary>
        /// <param name="db"></param>
        /// <param name="func"></param>
        /// <param name="v"></param>
        void sqlite3_update_hook(sqlite3 db, delegate_update func, object v);
        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="v"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        int sqlite3_create_collation(sqlite3 db, byte[] name, object v, delegate_collation func);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="flags"></param>
        /// <param name="v"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        int sqlite3_create_function(sqlite3 db, byte[] name, int nArg, int flags, object v, delegate_function_scalar func);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="nArg"></param>
        /// <param name="flags"></param>
        /// <param name="v"></param>
        /// <param name="func_step"></param>
        /// <param name="func_final"></param>
        /// <returns></returns>
        int sqlite3_create_function(sqlite3 db, byte[] name, int nArg, int flags, object v, delegate_function_aggregate_step func_step, delegate_function_aggregate_final func_final);
        /// <summary>
        /// ���ݿ�״̬
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="current"></param>
        /// <param name="highest"></param>
        /// <param name="resetFlg"></param>
        /// <returns></returns>
        int sqlite3_db_status(sqlite3 db, int op, out int current, out int highest, int resetFlg);
#if NET40
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_blob(IntPtr context, byte[] val);
#else
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_blob(IntPtr context, ReadOnlySpan<byte> val);
#endif
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_double(IntPtr context, double val);
#if NET40
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strErr"></param>
        void sqlite3_result_error(IntPtr context, byte[] strErr);
#else
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strErr"></param>
        void sqlite3_result_error(IntPtr context, ReadOnlySpan<byte> strErr);
#endif
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strErr"></param>
        void sqlite3_result_error(IntPtr context, Utf8z strErr);
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_int(IntPtr context, int val);
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_int64(IntPtr context, long val);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="context"></param>
        void sqlite3_result_null(IntPtr context);
#if NET40
        /// <summary>
        /// ����ı�
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_text(IntPtr context, byte[] val);
#else
        /// <summary>
        /// ����ı�
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_text(IntPtr context, ReadOnlySpan<byte> val);
#endif
        /// <summary>
        /// ����ı�
        /// </summary>
        /// <param name="context"></param>
        /// <param name="val"></param>
        void sqlite3_result_text(IntPtr context, Utf8z val);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="context"></param>
        /// <param name="n"></param>
        void sqlite3_result_zeroblob(IntPtr context, int n);
        // TODO sqlite3_result_value
        /// <summary>
        /// ����̫��
        /// </summary>
        /// <param name="context"></param>
        void sqlite3_result_error_toobig(IntPtr context);
        /// <summary>
        /// �����ڴ����
        /// </summary>
        /// <param name="context"></param>
        void sqlite3_result_error_nomem(IntPtr context);
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="code"></param>
        void sqlite3_result_error_code(IntPtr context, int code);
#if NET40
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        byte[] sqlite3_value_blob(IntPtr p);
#else
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        ReadOnlySpan<byte> sqlite3_value_blob(IntPtr p);
#endif
        /// <summary>
        /// ֵ�ֽ�
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int sqlite3_value_bytes(IntPtr p);
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        double sqlite3_value_double(IntPtr p);
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int sqlite3_value_int(IntPtr p);
        /// <summary>
        /// ������ֵ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        long sqlite3_value_int64(IntPtr p);
        /// <summary>
        /// ֵ����
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        int sqlite3_value_type(IntPtr p);
        /// <summary>
        /// ֵ�ı�
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Utf8z sqlite3_value_text(IntPtr p);
        /// <summary>
        /// stmt������
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_stmt_isexplain(sqlite3_stmt stmt);
        /// <summary>
        /// stmt����æ
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_stmt_busy(sqlite3_stmt stmt);
        /// <summary>
        /// stmt ֻ��
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        int sqlite3_stmt_readonly(sqlite3_stmt stmt);

        /// <summary>
        /// this function returns the errMsg as an IntPtr because it needs to be freed.
        /// ִ�нű�
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="callback"></param>
        /// <param name="user_data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        int sqlite3_exec(sqlite3 db, Utf8z sql, delegate_exec callback, object user_data, out IntPtr errMsg);
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int sqlite3_complete(Utf8z sql);
        /// <summary>
        /// ����ѡ��ʹ��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int sqlite3_compileoption_used(Utf8z sql);
        /// <summary>
        /// ����ѡ���ȡ
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        Utf8z sqlite3_compileoption_get(int n);
        /// <summary>
        /// �Զ�����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        int sqlite3_wal_autocheckpoint(sqlite3 db, int n);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        int sqlite3_wal_checkpoint(sqlite3 db, Utf8z dbName);
        /// <summary>
        /// ����2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbName"></param>
        /// <param name="eMode"></param>
        /// <param name="logSize"></param>
        /// <param name="framesCheckPointed"></param>
        /// <returns></returns>
        int sqlite3_wal_checkpoint_v2(sqlite3 db, Utf8z dbName, int eMode, out int logSize, out int framesCheckPointed);
        /// <summary>
        /// ����ԭ��ǩ����
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
        int sqlite3_table_column_metadata(sqlite3 db, Utf8z dbName, Utf8z tblName, Utf8z colName, out Utf8z dataType, out Utf8z collSeq, out int notNull, out int primaryKey, out int autoInc);
        /// <summary>
        /// ������֤
        /// </summary>
        /// <param name="db"></param>
        /// <param name="authorizer"></param>
        /// <param name="user_data"></param>
        /// <returns></returns>
        int sqlite3_set_authorizer(sqlite3 db, delegate_authorizer authorizer, object user_data);
        /// <summary>
        /// TODO the following two calls wish the args were spans
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        int sqlite3_stricmp(IntPtr p, IntPtr q);
        /// <summary>
        /// TODO the following two calls wish the args were spans
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        int sqlite3_strnicmp(IntPtr p, IntPtr q, int n);
        /// <summary>
        /// �ͷž��
        /// </summary>
        /// <param name="p"></param>
        void sqlite3_free(IntPtr p);
#if NET40
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_key(sqlite3 db, byte[] key);
        /// <summary>
        /// ����2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_key_v2(sqlite3 db, Utf8z dbname, byte[] key);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_rekey(sqlite3 db, byte[] key);
        /// <summary>
        /// ��������2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_rekey_v2(sqlite3 db, Utf8z dbname, byte[] key);
#else
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_key(sqlite3 db, ReadOnlySpan<byte> key);
        /// <summary>
        /// ����2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_key_v2(sqlite3 db, Utf8z dbname, ReadOnlySpan<byte> key);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_rekey(sqlite3 db, ReadOnlySpan<byte> key);
        /// <summary>
        /// ��������2
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int sqlite3_rekey_v2(sqlite3 db, Utf8z dbname, ReadOnlySpan<byte> key);
#endif
#if not // TODO consider
        int sqlite3_load_extension(sqlite3 db, utf8z fileName, utf8z procName, ref IntPtr pError);
#endif
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        int sqlite3_initialize();
        /// <summary>
        /// �ر�
        /// </summary>
        /// <returns></returns>
        int sqlite3_shutdown();
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        int sqlite3_limit(sqlite3 db, int id, int newVal);
        // /// <summary>
        // /// ����
        // /// </summary>
        // sqlite3_config() takes a variable argument list
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        int sqlite3_config(int op);
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        int sqlite3_config(int op, int val);
        // /// <summary>
        // /// ����
        // /// </summary>
        // sqlite3_db_config() takes a variable argument list
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        int sqlite3_db_config(sqlite3 db, int op, Utf8z val);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="val"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        int sqlite3_db_config(sqlite3 db, int op, int val, out int result);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="op"></param>
        /// <param name="ptr"></param>
        /// <param name="int0"></param>
        /// <param name="int1"></param>
        /// <returns></returns>
        int sqlite3_db_config(sqlite3 db, int op, IntPtr ptr, int int0, int int1);
        /// <summary>
        /// ���ü�����չ
        /// </summary>
        /// <param name="db"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        int sqlite3_enable_load_extension(sqlite3 db, int enable);
        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        int sqlite3_win32_set_directory(int typ, Utf8z path);
        /// <summary>
        /// �ؼ��ּ���
        /// </summary>
        /// <returns></returns>
        int sqlite3_keyword_count();
        /// <summary>
        /// �ؼ�������
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        int sqlite3_keyword_name(int i, out string name);
    }
}

