using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Timer = System.Timers.Timer;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// 连接池
    /// </summary>
    public class SqliteConnectionPool
    {
        private static ConcurrentDictionary<string, SqliteConnectionPool> _poolDic = new();
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static SqliteConnection GetConnection(string connString)
        {
            return GetPool(connString).GetInstance();
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static SqliteConnectionPool GetPool(string connString)
        {
            // 多线程并发的时候会产生多个实例,不知为何
            //return _poolDic.GetOrAdd(connString, (k) => { return new SqliteConnectionPool(connString); });
            SqliteConnectionPool pool;
            if (Monitor.TryEnter(_poolDic, TimeSpan.FromSeconds(5)))
            {
                pool = _poolDic.GetOrAdd(connString, (k) => new SqliteConnectionPool(connString));
                Monitor.Exit(_poolDic);
            }
            else
            {
                pool = new SqliteConnectionPool(connString);
            }
            return pool;
        }
        #region // 内部实现
        /// <summary>
        /// 连接池队列
        /// </summary>
        private ConcurrentQueue<SQLiteConnection> _connPool = new();
        private Timer _clearTimer = new Timer
        {
            Enabled = true, // 是否执行System.Timers.Timer.Elapsed事件
            Interval = 1800000, // 执行间隔时间,单位为毫秒;此时时间间隔单位为分钟
            AutoReset = true, // 设置是执行一次（false）还是一直执行(true)
        };
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnString { get; }
        /// <summary>
        /// 私有构造
        /// </summary>
        /// <param name="connString"></param>
        private SqliteConnectionPool(string connString)
        {
            ConnString = connString;
            // 定时清理连接池
            _clearTimer.Elapsed += (s, e) =>
            {
                if(_connPool.Count > 1)
                {
                    if (_connPool.TryDequeue(out SQLiteConnection curr))
                    {
                        curr.Release();
                    }
                }
            };
            _clearTimer.Start();
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public SqliteConnection GetInstance()
        {
            if (_connPool.IsEmpty || !_connPool.TryDequeue(out SQLiteConnection result))
            {
                result = new SQLiteConnection(ConnString);
            }
            return result;
        }
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="instance"></param>
        public void Recycle(SqliteConnection instance)
        {
            if (instance is SQLiteConnection conn)
            {
                _connPool.Enqueue(conn);
            }
        }
        #endregion
        #region // 内部类
        /// <summary>
        /// 带有连接池的连接
        /// </summary>
        private class SQLiteConnection : SqliteConnection
        {
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="connString"></param>
            public SQLiteConnection(string connString) : base(connString)
            {
            }
            /// <summary>
            /// 不释放资源
            /// </summary>
            /// <param name="disposing"></param>
            protected override void Dispose(bool disposing)
            {
                GetPool(ConnectionString).Recycle(this);
            }
            /// <summary>
            /// 释放资源
            /// </summary>
            public virtual void Release()
            {
                base.Dispose();
            }
        }
        #endregion
    }
}
