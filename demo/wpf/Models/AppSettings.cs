using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestWPFUI.SQLiteCipher.Models
{
    /// <summary>
    /// 配置类
    /// </summary>
    public static class EConfiguration
    {
        /// <summary>
        /// 基目录
        /// </summary>
        public static String BaseDirectory { get; }
        /// <summary>
        /// 资源目录
        /// </summary>
        public static String ResDirectory { get; }
        /// <summary>
        /// 上传目录
        /// </summary>
        public static String UploadDirectory { get; }
        /// <summary>
        /// 缓存目录
        /// </summary>
        public static String TempDirectory { get; }
        /// <summary>
        /// 设置文件
        /// </summary>
        public static string AppSettingFile { get { return ResDirectory + "AppSettings.json"; } }
        /// <summary>
        /// 应用配置
        /// </summary>
        public static AppSettings AppSettings { get; private set; }
        /// <summary>
        /// 静态构造
        /// </summary>
        static EConfiguration()
        {
            BaseDirectory = Environment.CurrentDirectory;
#if DEBUG
            ResDirectory = Path.GetFullPath(BaseDirectory + @"\..\Resources\");
            if (!Directory.Exists(ResDirectory))
            {
                ResDirectory = Path.GetFullPath(BaseDirectory + @"\..\..\Resources\");
                if (!Directory.Exists(ResDirectory))
                {
                    ResDirectory = Path.GetFullPath(BaseDirectory + @"\..\..\..\Resources\");
                    if (!Directory.Exists(ResDirectory))
                    {
                        ResDirectory = Path.GetFullPath(BaseDirectory + @"\Resources\");
                    }
                }
            }
#else
            ResDirectory = Path.GetFullPath(BaseDirectory + @"\Resources\");
#endif
            TempDirectory = Path.GetFullPath(BaseDirectory + @"\Temp\");
            UploadDirectory = Path.GetFullPath(BaseDirectory + @"\Upload\");
            LoadAppSettings();
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private static void LoadAppSettings()
        {
            if (!Directory.Exists(ResDirectory)) { Directory.CreateDirectory(ResDirectory); }
            if (!Directory.Exists(TempDirectory)) { Directory.CreateDirectory(TempDirectory); }
            if (!Directory.Exists(UploadDirectory)) { Directory.CreateDirectory(UploadDirectory); }
            AppSettings settings;
            if (!File.Exists(AppSettingFile))
            {
                settings = new AppSettings();
                File.WriteAllText(AppSettingFile, settings.GetJsonFormatString());
            }
            else
            {
                settings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(AppSettingFile));
            }
            AppSettings = settings;
        }
        /// <summary>
        /// 默认参数
        /// </summary>
        public static List<DbMenuModel> Menus { get { return AppSettings.Menus; } }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public static bool TrySave()
        {
            try
            {
                File.WriteAllText(AppSettingFile, AppSettings.GetJsonFormatString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
        internal static string GetJsonFormatString<T>(this T model)
        {
            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            //格式化json字符串
            new JsonSerializer().Serialize(jsonWriter, model);
            return textWriter.ToString();
        }
    }
    /// <summary>
    /// 应用配置
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        public List<DbMenuModel> Menus { get; private set; } = new List<DbMenuModel>();
    }
}
