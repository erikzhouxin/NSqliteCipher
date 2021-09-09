using static SQLitePCL.Raw.Core.RawCore;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    ///     Represents the type affinities used by columns in SQLite tables.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/dotnet/standard/data/sqlite/types">Data Types</seealso>
    public enum SqliteType
    {
        /// <summary>
        ///     A signed integer.
        /// </summary>
        Integer = SQLITE_INTEGER,

        /// <summary>
        ///     A floating point value.
        /// </summary>
        Real = SQLITE_FLOAT,

        /// <summary>
        ///     A text string.
        /// </summary>
        Text = SQLITE_TEXT,

        /// <summary>
        ///     A blob of data.
        /// </summary>
        Blob = SQLITE_BLOB
    }
}
