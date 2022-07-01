using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// Sqlite回调
    /// </summary>
    public class sqlite3_backup : SafeHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        sqlite3_backup() : base(IntPtr.Zero, true)
        {
        }
        /// <summary>
        /// 来自
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static sqlite3_backup From(IntPtr p)
        {
            var h = new sqlite3_backup();
            h.SetHandle(p);
            return h;
        }
        /// <summary>
        /// 是非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放处理
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            int rc = RawCore.internal_sqlite3_backup_finish(handle);
            // TODO check rc?
            return true;
        }
        /// <summary>
        /// 手动关闭
        /// </summary>
        /// <returns></returns>
        public int manual_close()
        {
            int rc = RawCore.internal_sqlite3_backup_finish(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
            return rc;
        }
    }
    /// <summary>
    /// typed wrapper for an IntPtr.  still opaque.  the upper layers can't
    /// do anything with this except hand it back to us on calls to
    /// raw.sqlite3_result_*.
    /// except for the 'state' property below, which the upper layers can 
    /// use to store state between calls to
    /// xStep/xFinal for an aggregate function.
    /// </summary>
    public class sqlite3_context
    {
        private IntPtr _p;
        private object _user_data;

        /// <summary>
        /// must be called by one of the two subclass (scalar or agg)
        /// </summary>
        /// <param name="user_data"></param>
        protected sqlite3_context(object user_data)
        {
            _user_data = user_data;
        }

        /// <summary>
        /// used by raw.sqlite3_user_data (which is internal to the PCL assembly)
        /// </summary>
        internal object user_data
        {
            get
            {
                return _user_data;
            }
        }
        /// <summary>
        /// used by raw.sqlite3_result_* (which is internal to the PCL assembly)
        /// to fetch the actual context pointer to pass back to sqlite.
        /// </summary>
        internal IntPtr ptr => _p;
        /// <summary>
        /// used by either the scalar or agg subclass, located
        /// in util.cs, compiled into the platform assembly.  each
        /// call to xFunc, xStep, or xFinal actually gives us a
        /// different context pointer.  however, for an aggregate
        /// function, we want this sqlite3_context object to be the
        /// same throughout all the calls to xStep or xFinal.  so
        /// we fix the pointer on each call.  and we want this to be
        /// invisible to the upper layers, so make this protected and do 
        /// the fixup in a subclass.
        /// </summary>
        /// <param name="p"></param>
        protected void set_context_ptr(IntPtr p)
        {
            _p = p;
        }
        /// <summary>
        /// this is available to the upper layers, to store state during 
        /// the run of an aggregate function.  not needed for scalar functions.
        /// </summary>
        public object state;
    }
    /// <summary>
    /// typed wrapper for an IntPtr.  still opaque.  the upper layers can't
    /// do anything with this except hand it back to us on calls to
    /// raw.sqlite3_value_*
    /// </summary>
    public class sqlite3_value
    {
        private IntPtr _p;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="p"></param>
        public sqlite3_value(IntPtr p)
        {
            _p = p;
        }
        /// <summary>
        /// 句柄
        /// </summary>
        internal IntPtr ptr => _p;
    }
    /// <summary>
    /// 大对象
    /// </summary>
    public class sqlite3_blob : SafeHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        sqlite3_blob() : base(IntPtr.Zero, true)
        {
        }
        /// <summary>
        /// 来源
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static sqlite3_blob From(IntPtr p)
        {
            var h = new sqlite3_blob();
            h.SetHandle(p);
            return h;
        }
        /// <summary>
        /// 是非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放指针
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            int rc = RawCore.internal_sqlite3_blob_close(handle);
            // TODO check rc?
            return true;
        }
        /// <summary>
        /// 手动释放
        /// </summary>
        /// <returns></returns>
        public int manual_close()
        {
            int rc = RawCore.internal_sqlite3_blob_close(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
            return rc;
        }
    }
    /// <summary>
    /// 快照
    /// </summary>
    public class sqlite3_snapshot : SafeHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        sqlite3_snapshot() : base(IntPtr.Zero, true)
        {
        }
        /// <summary>
        /// 来自
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static sqlite3_snapshot From(IntPtr p)
        {
            var h = new sqlite3_snapshot();
            h.SetHandle(p);
            return h;
        }
        /// <summary>
        /// 是非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放句柄
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            RawCore.internal_sqlite3_snapshot_free(handle);
            return true;
        }
        /// <summary>
        /// 手动关闭
        /// </summary>
        public void manual_close()
        {
            RawCore.internal_sqlite3_snapshot_free(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
        }
    }
    /// <summary>
    /// 批量
    /// </summary>
    public class sqlite3_stmt : SafeHandle
    {
        private sqlite3 _db;
        /// <summary>
        /// 构造
        /// </summary>
        sqlite3_stmt() : base(IntPtr.Zero, true)
        {
        }
        /// <summary>
        /// 来自
        /// </summary>
        /// <param name="p"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        internal static sqlite3_stmt From(IntPtr p, sqlite3 db)
        {
            var h = new sqlite3_stmt();
            h.SetHandle(p);
            db.add_stmt(h);
            h._db = db;
            return h;
        }
        /// <summary>
        /// 是非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放句柄
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            int rc = RawCore.internal_sqlite3_finalize(handle);
            // TODO check rc?
            _db.remove_stmt(this);
            return true;
        }
        /// <summary>
        /// 手动关闭
        /// </summary>
        /// <returns></returns>
        public int manual_close()
        {
            int rc = RawCore.internal_sqlite3_finalize(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
            _db.remove_stmt(this);
            return rc;
        }
        /// <summary>
        /// 句柄
        /// </summary>
        // TODO rm?  used by the next_stmt code.
        internal IntPtr ptr => handle;
        /// <summary>
        /// We keep track of the db connection handle for this stmt, even though
        /// the underlying sqlite C library keeps track of it as well.  On a call
        /// to sqlite3_db_handle(), if we called the C function and get a pointer
        /// and then wrap it in a new instance of our sqlite3 class, we would end
        /// up with two instances of that class having the same wrapped IntPtr.
        /// This seems bad.  So we implement it here at this layer as well.
        /// </summary>
        internal sqlite3 db
        {
            get
            {
                return _db;
            }
        }
    }
    /// <summary>
    /// sqlite3
    /// </summary>
    public class sqlite3 : SafeHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        sqlite3() : base(IntPtr.Zero, true)
        {
        }
        /// <summary>
        /// 是非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放句柄
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            int rc = RawCore.internal_sqlite3_close_v2(handle);
            // TODO check rc?
            dispose_extra();
            return true;
        }
        /// <summary>
        /// 手动关闭2.0
        /// </summary>
        /// <returns></returns>
        public int manual_close_v2()
        {
            int rc = RawCore.internal_sqlite3_close_v2(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
            dispose_extra();
            return rc;
        }
        /// <summary>
        /// 手动关闭
        /// </summary>
        /// <returns></returns>
        public int manual_close()
        {
            int rc = RawCore.internal_sqlite3_close(handle);
            // TODO review.  should handle always be nulled here?
            // TODO maybe called SetHandleAsInvalid instead?
            handle = IntPtr.Zero;
            dispose_extra();
            return rc;
        }
        /// <summary>
        /// 创建新的sqlite3
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static sqlite3 New(IntPtr p)
        {
            var h = new sqlite3();
            h.SetHandle(p);
#if not // changing this to default OFF for v2
            h.enable_sqlite3_next_stmt(true);
#endif
            return h;
        }

        // this dictionary is used only for the purpose of supporting sqlite3_next_stmt.
        private ConcurrentDictionary<IntPtr, sqlite3_stmt> _stmts = null;
        /// <summary>
        /// 启动stmt
        /// </summary>
        /// <param name="enabled"></param>
        public void enable_sqlite3_next_stmt(bool enabled)
        {
            if (enabled)
            {
                if (_stmts == null)
                {
                    _stmts = new ConcurrentDictionary<IntPtr, sqlite3_stmt>();
                }
            }
            else
            {
                _stmts = null;
            }
        }
        /// <summary>
        /// 添加stmt
        /// </summary>
        /// <param name="stmt"></param>
        internal void add_stmt(sqlite3_stmt stmt)
        {
            if (_stmts != null)
            {
                _stmts[stmt.ptr] = stmt;
            }
        }
        /// <summary>
        /// 发现stmt
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal sqlite3_stmt find_stmt(IntPtr p)
        {
            if (_stmts != null)
            {
                return _stmts[p];
            }
            else
            {
                // any change to the wording of this error message might break a test case
                throw new Exception("The sqlite3_next_stmt() function is disabled.  To enable it, call sqlite3.enable_sqlite3_next_stmt(true) immediately after opening the sqlite3 connection.");
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="s"></param>
        internal void remove_stmt(sqlite3_stmt s)
        {
            if (_stmts != null)
            {
                _stmts.TryRemove(s.ptr, out var stmt);
            }
        }

        IDisposable extra;
        /// <summary>
        /// 获取或创建附加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public T GetOrCreateExtra<T>(Func<T> f)
            where T : class, IDisposable
        {
            if (extra != null)
            {
                return (T)extra;
            }
            else
            {
                var q = f();
                extra = q;
                return q;
            }
        }
        /// <summary>
        /// 释放附加
        /// </summary>
        private void dispose_extra()
        {
            if (extra != null)
            {
                extra.Dispose();
                extra = null;
            }
        }

    }
}

