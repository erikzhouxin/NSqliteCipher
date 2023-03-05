using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     SQLite specific extension methods for <see cref="MigrationBuilder" />.
    /// </summary>
    public static class SqliteMigrationBuilderExtensions
    {
        /// <summary>
        ///     <para>
        ///         Returns <see langword="true" /> if the database provider currently in use is the SQLite provider.
        ///     </para>
        /// </summary>
        /// <param name="migrationBuilder">
        ///     The migrationBuilder from the parameters on <see cref="Migration.Up(MigrationBuilder)" /> or
        ///     <see cref="Migration.Down(MigrationBuilder)" />.
        /// </param>
        /// <returns> <see langword="true" /> if SQLite is being used; <see langword="false" /> otherwise. </returns>
        public static bool IsSqlite([NotNull] this MigrationBuilder migrationBuilder)
            => string.Equals(
                migrationBuilder.ActiveProvider,
                typeof(SqliteOptionsExtension).Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}
