using System.Data.Common;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    ///     Creates instances of various System.Data.SQLiteCipher classes.
    /// </summary>
    public class SqliteFactory : DbProviderFactory
    {
        private SqliteFactory()
        {
        }

        /// <summary>
        ///     The singleton instance.
        /// </summary>
        public static readonly SqliteFactory Instance = new SqliteFactory();

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <returns>The new command.</returns>
        public override DbCommand CreateCommand()
            => new SqliteCommand();

        /// <summary>
        ///     Creates a new connection.
        /// </summary>
        /// <returns>The new connection.</returns>
        public override DbConnection CreateConnection()
            => new SqliteConnection();

        /// <summary>
        ///     Creates a new connection string builder.
        /// </summary>
        /// <returns>The new connection string builder.</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
            => new SqliteConnectionStringBuilder();

        /// <summary>
        ///     Creates a new parameter.
        /// </summary>
        /// <returns>The new parameter.</returns>
        public override DbParameter CreateParameter()
            => new SqliteParameter();
    }
}
