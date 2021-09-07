using SQLitePCL;

namespace System.Data.SQLiteCipher
{
    internal static class SQLitePCLExtensions
    {
#if !NET40 && !NET45
        public static bool EncryptionNotSupported()
            => raw.GetNativeLibraryName() == "e_sqlite3";
#endif
    }
}
