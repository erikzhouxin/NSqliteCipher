using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     SQLite specific extension methods for <see cref="DbContext.Database" />.
    /// </summary>
    public static class SqliteDatabaseFacadeExtensions
    {
        /// <summary>
        ///     <para>
        ///         Returns <see langword="true" /> if the database provider currently in use is the SQLite provider.
        ///     </para>
        ///     <para>
        ///         This method can only be used after the <see cref="DbContext" /> has been configured because
        ///         it is only then that the provider is known. This means that this method cannot be used
        ///         in <see cref="DbContext.OnConfiguring" /> because this is where application code sets the
        ///         provider to use as part of configuring the context.
        ///     </para>
        /// </summary>
        /// <param name="database"> The facade from <see cref="DbContext.Database" />. </param>
        /// <returns> <see langword="true" /> if SQLite is being used; <see langword="false" /> otherwise. </returns>
        public static bool IsSqlite([NotNull] this DatabaseFacade database)
            => database.ProviderName.Equals(
                typeof(SqliteOptionsExtension).Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}
