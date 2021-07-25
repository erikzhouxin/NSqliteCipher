namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// 创建新缓存时可使用的缓存模式
    /// </summary>
    /// <see cref="SqliteConnection" />
    /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
    public enum SqliteCacheMode
    {
        /// <summary>
        /// 默认模式
        /// </summary>
        Default,
        /// <summary>
        /// 私有模式,每个连接有一个私有缓存
        /// </summary>
        Private,
        /// <summary>
        /// 共享模式,连接共享缓存,这种模式可以改变事务和表锁定的行为
        /// </summary>
        Shared
    }
}
