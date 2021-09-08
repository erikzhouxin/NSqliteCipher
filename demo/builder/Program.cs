using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.SQLiteCipher.Builder
{
    class Program
    {
        static String NUSPEC_VERSION { get; set; }
        static String ASSEMBLY_VERSION { get; set; }
        static String Src { get; set; }
        static void Main(string[] args)
        {
            NUSPEC_VERSION = string.Format("{0:yyyy.M.d}", DateTime.Now);
            ASSEMBLY_VERSION = string.Format("{0:yyyy.M.d}.{1}", DateTime.Now, (int)(DateTime.Now - new DateTime(2020, 1, 1)).TotalDays);
            var current = Directory.GetCurrentDirectory();
            Src = Path.GetFullPath(Path.Combine(current, "..", "..", "..", "..", "..", "src"));
            var projs = new string[]
            {
                Path.Combine(Src, "SQLiteCipher", "System.Data.SQLiteCipher.csproj"),
                Path.Combine(Src, "Entity", "System.Data.SQLiteCipherEntity.csproj")
            };
            foreach (var proj in projs)
            {
                ReplaceVersion(proj);
            }
        }

        private static void ReplaceVersion(string projFile)
        {
            var coreProj = Path.GetFullPath(projFile);
            var content = File.ReadAllText(coreProj, Encoding.UTF8);
            // 忽略大小写
            Regex versionReg = new Regex(@"(<Version>)\d+[\.\d+]+(</Version>)", RegexOptions.IgnoreCase);
            Regex fileReg = new Regex(@"(<FileVersion>)\d+[\.\d+]+(</FileVersion>)", RegexOptions.IgnoreCase);
            Regex assemblyReg = new Regex(@"(<AssemblyVersion>)\d+[\.\d+]+(</AssemblyVersion>)", RegexOptions.IgnoreCase);
            var targetContent = versionReg.Replace(content, "<Version>" + NUSPEC_VERSION + "</Version>");
            targetContent = fileReg.Replace(targetContent, "<FileVersion>" + ASSEMBLY_VERSION + "</FileVersion>");
            targetContent = assemblyReg.Replace(targetContent, "<AssemblyVersion>" + ASSEMBLY_VERSION + "</AssemblyVersion>");
            File.WriteAllText(coreProj, targetContent, Encoding.UTF8);
        }
    }
}
