namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// �����»���ʱ��ʹ�õĻ���ģʽ
    /// </summary>
    /// <see cref="SqliteConnection" />
    /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
    public enum SqliteCacheMode
    {
        /// <summary>
        /// Ĭ��ģʽ
        /// </summary>
        Default,
        /// <summary>
        /// ˽��ģʽ,ÿ��������һ��˽�л���
        /// </summary>
        Private,
        /// <summary>
        /// ����ģʽ,���ӹ�����,����ģʽ���Ըı�����ͱ���������Ϊ
        /// </summary>
        Shared
    }
}
