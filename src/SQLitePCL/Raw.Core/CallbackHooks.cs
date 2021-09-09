using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 保存属性
    /// </summary>
    public sealed class PreserveAttribute : System.Attribute
    {
        /// <summary>
        /// 所有成员
        /// </summary>
        public bool AllMembers;
        /// <summary>
        /// 条件
        /// </summary>
        public bool Conditional;
    }
    /// <summary>
    /// 回调属性
    /// </summary>
    public sealed class MonoPInvokeCallbackAttribute : Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="t"></param>
        public MonoPInvokeCallbackAttribute(Type t)
        {
            Type = t;
        }
    }
    /// <summary>
    /// 安全的GC处理
    /// </summary>
    public class SafeGCHandle : SafeHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public SafeGCHandle(object value, GCHandleType type) : base(IntPtr.Zero, true)
        {
            if (value == null) { return; }
            SetHandle(GCHandle.ToIntPtr(GCHandle.Alloc(value, type)));
        }
        /// <summary>
        /// 是否非法
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        /// <summary>
        /// 释放
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            GCHandle.FromIntPtr(handle).Free();
            return true;
        }

    }
    /// <summary>
    /// 钩子处理
    /// </summary>
    public class HookHandle : SafeGCHandle
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="target"></param>
        public HookHandle(object target) : base(target, GCHandleType.Normal)
        {
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <returns></returns>
        public IDisposable ForDispose()
        {
            return IsInvalid ? null : this;
        }
    }
    /// <summary>
    /// 比较缓冲
    /// </summary>
    class CompareBuf : EqualityComparer<byte[]>
    {
        readonly Func<IntPtr, IntPtr, int, bool> Func;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="f"></param>
        public CompareBuf(Func<IntPtr, IntPtr, int, bool> f)
        {
            Func = f;
        }
        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public override bool Equals(byte[] p1, byte[] p2)
        {
            if (p1.Length != p2.Length) { return false; }
            var h1 = GCHandle.Alloc(p1, GCHandleType.Pinned);
            var h2 = GCHandle.Alloc(p2, GCHandleType.Pinned);
            var result = Func(h1.AddrOfPinnedObject(), h2.AddrOfPinnedObject(), p1.Length);
            h1.Free();
            h2.Free();
            return result;
        }
        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int GetHashCode(byte[] p)
        {
            return p.Length; // TODO: 可优化
        }
    }
    /// <summary>
    /// 函数名称
    /// </summary>
    class FuncName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public byte[] Name { get; }
        /// <summary>
        /// 序号
        /// </summary>
        public int N { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="n"></param>
        public FuncName(byte[] name, int n)
        {
            Name = name;
            N = n;
        }
    }
    /// <summary>
    /// 比较函数名称类
    /// </summary>
    class CompareFuncName : EqualityComparer<FuncName>
    {
        IEqualityComparer<byte[]> _ptrlencmp;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ptrlencmp"></param>
        public CompareFuncName(IEqualityComparer<byte[]> ptrlencmp)
        {
            _ptrlencmp = ptrlencmp;
        }
        /// <summary>
        /// 相等判断
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public override bool Equals(FuncName p1, FuncName p2)
        {
            if (p1.N != p2.N) { return false; }
            return _ptrlencmp.Equals(p1.Name, p2.Name);
        }
        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int GetHashCode(FuncName p)
        {
            return p.N + p.Name.Length; // TODO:可优化
        }
    }
    /// <summary>
    /// 钩子处理
    /// </summary>
    public class HookHandles : IDisposable
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Func"></param>
        public HookHandles(Func<IntPtr, IntPtr, int, bool> Func)
        {
            var cmp = new CompareBuf(Func);
            collation = new ConcurrentDictionary<byte[], IDisposable>(cmp);
            scalar = new ConcurrentDictionary<FuncName, IDisposable>(new CompareFuncName(cmp));
            agg = new ConcurrentDictionary<FuncName, IDisposable>(new CompareFuncName(cmp));
        }

        readonly ConcurrentDictionary<byte[], IDisposable> collation;
        readonly ConcurrentDictionary<FuncName, IDisposable> scalar;
        readonly ConcurrentDictionary<FuncName, IDisposable> agg;
        /// <summary>
        /// 更新
        /// </summary>
        public IDisposable update;
        /// <summary>
        /// 回滚
        /// </summary>
        public IDisposable rollback;
        /// <summary>
        /// 提交
        /// </summary>
        public IDisposable commit;
        /// <summary>
        /// 跟踪
        /// </summary>
        public IDisposable trace;
        /// <summary>
        /// 存档
        /// </summary>
        public IDisposable profile;
        /// <summary>
        /// 处理
        /// </summary>
        public IDisposable progress;
        /// <summary>
        /// 授权
        /// </summary>
        public IDisposable authorizer;
        /// <summary>
        /// 移除函数名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nargs"></param>
        /// <returns></returns>
        public bool RemoveScalarFunction(byte[] name, int nargs)
        {
            var k = new FuncName(name, nargs);
            if (scalar.TryRemove(k, out var h_old))
            {
                h_old.Dispose();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加函数名称类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nargs"></param>
        /// <param name="d"></param>
        public void AddScalarFunction(byte[] name, int nargs, IDisposable d)
        {
            var k = new FuncName(name, nargs);
            scalar[k] = d;
        }
        /// <summary>
        /// 移除参数名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nargs"></param>
        /// <returns></returns>
        public bool RemoveAggFunction(byte[] name, int nargs)
        {
            var k = new FuncName(name, nargs);
            if (agg.TryRemove(k, out var h_old))
            {
                h_old.Dispose();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加函数名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nargs"></param>
        /// <param name="d"></param>
        public void AddAggFunction(byte[] name, int nargs, IDisposable d)
        {
            var k = new FuncName(name, nargs);
            agg[k] = d;
        }
        /// <summary>
        /// 移除序列
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveCollation(byte[] name)
        {
            if (collation.TryRemove(name, out var h_old))
            {
                h_old.Dispose();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加序列
        /// </summary>
        /// <param name="name"></param>
        /// <param name="d"></param>
        public void AddCollation(byte[] name, IDisposable d)
        {
            collation[name] = d;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            foreach (var h in collation.Values) h.Dispose();
            foreach (var h in scalar.Values) h.Dispose();
            foreach (var h in agg.Values) h.Dispose();
            if (update != null) update.Dispose();
            if (rollback != null) rollback.Dispose();
            if (commit != null) commit.Dispose();
            if (trace != null) trace.Dispose();
            if (profile != null) profile.Dispose();
            if (progress != null) progress.Dispose();
            if (authorizer != null) authorizer.Dispose();
        }
    }
    /// <summary>
    /// 日志钩子信息
    /// </summary>
    public class LogHookInfo
    {
        private delegate_log _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public LogHookInfo(delegate_log func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static LogHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as LogHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="rc"></param>
        /// <param name="msg"></param>
        public void Call(int rc, Utf8z msg)
        {
            _func(_user_data, rc, msg);
        }
    }
    /// <summary>
    /// 提交钩子信息
    /// </summary>
    public class CommitHookInfo
    {
        private delegate_commit _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public CommitHookInfo(delegate_commit func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <returns></returns>
        public int Call()
        {
            return _func(_user_data);
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static CommitHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as CommitHookInfo;
        }
    }
    /// <summary>
    /// 回滚钩子信息
    /// </summary>
    public class RollbackHookInfo
    {
        private delegate_rollback _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public RollbackHookInfo(delegate_rollback func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static RollbackHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as RollbackHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        public void Call()
        {
            _func(_user_data);
        }
    }
    /// <summary>
    /// 追踪钩子信息
    /// </summary>
    public class TraceHookInfo
    {
        private delegate_trace _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public TraceHookInfo(delegate_trace func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static TraceHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as TraceHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="s"></param>
        public void Call(Utf8z s)
        {
            _func(_user_data, s);
        }
    }
    /// <summary>
    /// 存档钩子信息
    /// </summary>
    public class ProfileHookInfo
    {
        private delegate_profile _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public ProfileHookInfo(delegate_profile func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ProfileHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as ProfileHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="s"></param>
        /// <param name="elapsed"></param>
        public void Call(Utf8z s, long elapsed)
        {
            _func(_user_data, s, elapsed);
        }
    }
    /// <summary>
    /// 处理回调信息
    /// </summary>
    public class ProgressHookInfo
    {
        private delegate_progress _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public ProgressHookInfo(delegate_progress func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ProgressHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as ProgressHookInfo;
        }
        /// <summary>
        /// 回调
        /// </summary>
        /// <returns></returns>
        public int Call()
        {
            return _func(_user_data);
        }
    }
    /// <summary>
    /// 更新钩子信息
    /// </summary>
    public class UpdateHookInfo
    {
        private delegate_update _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public UpdateHookInfo(delegate_update func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static UpdateHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as UpdateHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="db"></param>
        /// <param name="tbl"></param>
        /// <param name="rowid"></param>
        public void Call(int typ, Utf8z db, Utf8z tbl, long rowid)
        {
            _func(_user_data, typ, db, tbl, rowid);
        }
    }
    /// <summary>
    /// 列表构造信息
    /// </summary>
    public class CollationHookInfo
    {
        private delegate_collation _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public CollationHookInfo(delegate_collation func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static CollationHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as CollationHookInfo;
        }

#if NET40
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int Call(byte[] s1, byte[] s2)
        {
            return _func(_user_data, s1, s2);
        }
#else
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int Call(ReadOnlySpan<byte> s1, ReadOnlySpan<byte> s2)
        {
            return _func(_user_data, s1, s2);
        }
#endif
    }
    /// <summary>
    /// 执行钩子信息
    /// </summary>
    public class ExecHookInfo
    {
        private delegate_exec _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public ExecHookInfo(delegate_exec func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ExecHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as ExecHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="n"></param>
        /// <param name="values_ptr"></param>
        /// <param name="names_ptr"></param>
        /// <returns></returns>
        public int Call(int n, IntPtr values_ptr, IntPtr names_ptr)
        {
            var values = new IntPtr[n];
            var names = new IntPtr[n];
            // TODO warning on the following line.  SizeOf(Type) replaced in .NET 4.5.1 with SizeOf<T>()
            int ptr_size = Marshal.SizeOf(typeof(IntPtr));
            for (int i = 0; i < n; i++)
            {
                IntPtr vp;

                vp = Marshal.ReadIntPtr(values_ptr, i * ptr_size);
                values[i] = vp;

                vp = Marshal.ReadIntPtr(names_ptr, i * ptr_size);
                names[i] = vp;
            }

            return _func(_user_data, values, names);
        }
    }
    /// <summary>
    /// 函数钩子信息
    /// </summary>
    public class FunctionHookInfo
    {
        private delegate_function_scalar _func_scalar;
        private delegate_function_aggregate_step _func_step;
        private delegate_function_aggregate_final _func_final;
        private object _user_data;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func_scalar"></param>
        /// <param name="user_data"></param>
        public FunctionHookInfo(delegate_function_scalar func_scalar, object user_data)
        {
            _func_scalar = func_scalar;
            _user_data = user_data;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func_step"></param>
        /// <param name="func_final"></param>
        /// <param name="user_data"></param>
        public FunctionHookInfo(delegate_function_aggregate_step func_step, delegate_function_aggregate_final func_final, object user_data)
        {
            _func_step = func_step;
            _func_final = func_final;
            _user_data = user_data;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static FunctionHookInfo FromPtr(IntPtr p)
        {
            return ((GCHandle)p).Target as FunctionHookInfo; ;
        }

        /// <summary>
        /// 回调标量
        /// </summary>
        /// <param name="context"></param>
        /// <param name="num_args"></param>
        /// <param name="argsptr"></param>
        public void CallScalar(IntPtr context, int num_args, IntPtr argsptr)
        {
            scalar_sqlite3_context ctx = new scalar_sqlite3_context(context, _user_data);

            sqlite3_value[] a = new sqlite3_value[num_args];
            // TODO warning on the following line.  SizeOf(Type) replaced in .NET 4.5.1 with SizeOf<T>()
            int ptr_size = Marshal.SizeOf(typeof(IntPtr));
            for (int i = 0; i < num_args; i++)
            {
                IntPtr vp = Marshal.ReadIntPtr(argsptr, i * ptr_size);
                a[i] = new sqlite3_value(vp);
            }

            _func_scalar(ctx, _user_data, a);
        }
        /// <summary>
        /// 回调步骤
        /// </summary>
        /// <param name="context"></param>
        /// <param name="agg_context"></param>
        /// <param name="num_args"></param>
        /// <param name="argsptr"></param>
        public void CallStep(IntPtr context, IntPtr agg_context, int num_args, IntPtr argsptr)
        {
            sqlite3_context ctx = GetContext(context, agg_context);

            sqlite3_value[] a = new sqlite3_value[num_args];
            // TODO warning on the following line.  SizeOf(Type) replaced in .NET 4.5.1 with SizeOf<T>()
            int ptr_size = Marshal.SizeOf(typeof(IntPtr));
            for (int i = 0; i < num_args; i++)
            {
                IntPtr vp = Marshal.ReadIntPtr(argsptr, i * ptr_size);
                a[i] = new sqlite3_value(vp);
            }

            _func_step(ctx, _user_data, a);
        }
        /// <summary>
        /// 回调结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="agg_context"></param>
        public void CallFinal(IntPtr context, IntPtr agg_context)
        {
            sqlite3_context ctx = GetContext(context, agg_context);

            _func_final(ctx, _user_data);

            IntPtr c = Marshal.ReadIntPtr(agg_context);
            GCHandle h = (GCHandle)c;
            h.Free();
        }
        private sqlite3_context GetContext(IntPtr context, IntPtr agg_context)
        {
            // agg_context is a pointer to 8 bytes of storage, obtained from
            // sqlite3_aggregate_context().  we will use it to store the
            // sqlite3_context object so we can use the same object for all
            // calls to xStep and xFinal for this instance/invocation of the
            // aggregate function.

            agg_sqlite3_context ctx;
            IntPtr c = Marshal.ReadIntPtr(agg_context);
            if (c == IntPtr.Zero)
            {
                // this is the first call to xStep or xFinal.  we need a new
                // sqlite3_context object to pass to the user's callback.
                ctx = new agg_sqlite3_context(_user_data);

                // and store a handle in the agg_context storage area so we
                // can get this back next time.
                GCHandle h = GCHandle.Alloc(ctx);
                Marshal.WriteIntPtr(agg_context, (IntPtr)h);
            }
            else
            {
                // we've been through here before.  retrieve the sqlite3_context
                // object from the agg_context storage area.
                GCHandle h = (GCHandle)c;
                ctx = h.Target as agg_sqlite3_context;
            }

            // we are reusing the same sqlite3_context object for each call
            // to xStep/xFinal within the same instance/invocation of the
            // user's agg function.  but SQLite actually gives us a different
            // context pointer on each call.  so we need to fix it up.
            ctx.fix_ptr(context);

            return ctx;
        }

        #region // 内部类
        private class agg_sqlite3_context : sqlite3_context
        {
            public agg_sqlite3_context(object v) : base(v)
            {
            }

            public void fix_ptr(IntPtr p)
            {
                set_context_ptr(p);
            }
        }
        private class scalar_sqlite3_context : sqlite3_context
        {
            public scalar_sqlite3_context(IntPtr p, object v) : base(v)
            {
                set_context_ptr(p);
            }
        }
        #endregion
    }
    /// <summary>
    /// 授权回调
    /// </summary>
    public class AuthorizerHookInfo
    {
        private delegate_authorizer _func;
        private object _user_data;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func"></param>
        /// <param name="v"></param>
        public AuthorizerHookInfo(delegate_authorizer func, object v)
        {
            _func = func;
            _user_data = v;
        }
        /// <summary>
        /// 来源句柄
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AuthorizerHookInfo from_ptr(IntPtr p)
        {
            return ((GCHandle)p).Target as AuthorizerHookInfo;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="action_code"></param>
        /// <param name="param0"></param>
        /// <param name="param1"></param>
        /// <param name="dbName"></param>
        /// <param name="inner_most_trigger_or_view"></param>
        /// <returns></returns>
        public int Call(int action_code, Utf8z param0, Utf8z param1, Utf8z dbName, Utf8z inner_most_trigger_or_view)
        {
            return _func(_user_data, action_code, param0, param1, dbName, inner_most_trigger_or_view);
        }
    }

}


