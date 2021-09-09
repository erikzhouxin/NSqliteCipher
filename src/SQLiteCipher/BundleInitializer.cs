using System.Reflection;
using static SQLitePCL.Raw.Core.RawCore;

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
                var assembly = Assembly.Load(new AssemblyName("SQLitePCLRaw.batteries_v2"));
                if (assembly != null)
                {
                    assembly.GetType("SQLitePCL.Batteries_V2").GetTypeInfo().GetDeclaredMethod("Init").Invoke(null, null);
                }
            }
            catch { }
            // 当没有Provider的时候进行查找
            SetProviderIfNull();
            if (ApplicationDataHelper.CurrentApplicationData != null)
            {
                var rc = sqlite3_win32_set_directory(SQLITE_WIN32_DATA_DIRECTORY_TYPE, ApplicationDataHelper.LocalFolderPath);
                SqliteException.ThrowExceptionForRC(rc, db: null);
                rc = sqlite3_win32_set_directory(SQLITE_WIN32_TEMP_DIRECTORY_TYPE, ApplicationDataHelper.TemporaryFolderPath);
                SqliteException.ThrowExceptionForRC(rc, db: null);
            }
        }
    }
}
