using System;
using System.Reflection;

using static SQLitePCL.raw;

namespace System.Data.SQLiteCipher
{
    internal static class BundleInitializer
    {
        private const int SQLITE_WIN32_DATA_DIRECTORY_TYPE = 1;
        private const int SQLITE_WIN32_TEMP_DIRECTORY_TYPE = 2;

        public static void Initialize()
        {
            try
            {
#if NET40 || NET45
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlcipher());
#else
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlcipher());
#endif
                Assembly assembly = Assembly.Load(new AssemblyName("SQLitePCLRaw.batteries_v2"));
                if (assembly != null)
                {
                    assembly.GetType("SQLitePCL.Batteries_V2").GetTypeInfo().GetDeclaredMethod("Init")
                        .Invoke(null, null);
                }
            }
            catch
            {
            }

#if !NET40 && !NET45
            if ((!AppContext.TryGetSwitch("Microsoft.EntityFrameworkCore.Issue19754", out var isEnabled) || !isEnabled)
                && ApplicationDataHelper.CurrentApplicationData != null)
            {
                var rc = sqlite3_win32_set_directory(
                    SQLITE_WIN32_DATA_DIRECTORY_TYPE,
                    ApplicationDataHelper.LocalFolderPath);
                SqliteException.ThrowExceptionForRC(rc, db: null);

                rc = sqlite3_win32_set_directory(
                    SQLITE_WIN32_TEMP_DIRECTORY_TYPE,
                    ApplicationDataHelper.TemporaryFolderPath);
                SqliteException.ThrowExceptionForRC(rc, db: null);
            }
#endif
        }
    }
}
