using System.Collections.Generic;
using SQLitePCL;

namespace System.Data.SQLiteCipher
{
    internal static class SQLitePCLExtensions
    {
#if !NET40 && !NET45
        public static bool EncryptionNotSupported()
            => raw.GetNativeLibraryName() == "e_sqlite3";
#endif
        private static readonly Dictionary<string, bool> _knownLibraries = new Dictionary<string, bool>
        {
            { "e_sqlcipher", true },
            { "e_sqlite3", false },
            { "sqlcipher", true },
            { "winsqlite3", false }
        };

        public static bool? EncryptionSupported()
            => EncryptionSupported(out _);

        public static bool? EncryptionSupported(out string libraryName)
        {
            libraryName = raw.GetNativeLibraryName();

            return _knownLibraries.TryGetValue(libraryName, out var supported)
                ? supported
                : default(bool?);
        }
    }
}
