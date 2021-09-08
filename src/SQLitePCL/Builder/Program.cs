using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace SQLitePCL.Raw.Builder
{
    static class Program
    {
        public static String Src { get; private set; }

        static void Main(string[] args)
        {
            var current = Directory.GetCurrentDirectory();
            Src = Path.GetFullPath(Path.Combine(current, "..", "..", "..", ".."));
            // 生成信息
            var nupkgs_dir_name = "nupkgs";
            var dir_nupkgs = Path.Combine(Src, "nupkgs");
            Directory.CreateDirectory(dir_nupkgs);
            foreach (var s in Directory.GetFiles(dir_nupkgs, "*.nupkg"))
            {
                File.Delete(s);
            }
            gen_directory_build_props(Src, nupkgs_dir_name);
            // 生成包
            gen_nuspec_lib_e_sqlcipher(Src);
            // 编译发布
            var pack_dirs = new Dictionary<string, string> {
                { "Raw.Core", "SQLitePCL.Raw.Core" },
            };
            foreach (var s in pack_dirs)
            {
                Exec("dotnet", "pack -c Release", Path.Combine(Program.Src, s.Key));
            }
            var msbuild_pack_dirs = new Dictionary<string, string>{
                { "Lib", "SQLitePCL.Raw.Liber" },
            };
            foreach (var s in msbuild_pack_dirs)
            {
                var dir = Path.Combine(Program.Src, s.Key);
                Exec("dotnet", "restore", dir);
                Exec(@"C:\Program Files\Microsoft Visual Studio 2019\MSBuild\Current\Bin\msbuild.exe", "/p:Configuration=Release /t:pack", dir);
            }
            var nuspecs = new Dictionary<string, string> {
                { "Lib", "SQLitePCL.Raw.Liber" },
            };
            foreach (var s in nuspecs)
            {
                var dir_proj = Path.Combine(Program.Src, s.Key);
                var path_empty = Path.Combine(dir_proj, "_._");
                if (!File.Exists(path_empty))
                {
                    File.WriteAllText(path_empty, "");
                }
                Exec("dotnet", "pack", dir_proj);
            }
        }

        #region // MSBuilder
        public static string get_build_prop(string p)
        {
            var path_xml = Path.Combine(Src, "Directory.Build.props");
            var xml = XElement.Load(path_xml);
            var props = xml.Elements(XName.Get("PropertyGroup")).First();
            var ver = props.Elements(XName.Get(p)).First();
            return ver.Value;
        }
        public static void Exec(string fileName, string args, string startDir)
        {
            var wd = System.IO.Path.GetFullPath(startDir);
            var procStartInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                FileName = fileName,
                Arguments = args,
                WorkingDirectory = wd
            };

            var p = new Process()
            {
                StartInfo = procStartInfo
            };
            p.OutputDataReceived += P_OutputDataReceived;
            p.ErrorDataReceived += P_ErrorDataReceived;

            var desc = $"{fileName} {args} in {wd}";
            //printfn "-------- %s" desc
            p.Start(); //|> ignore
                       //printfn "Started %s with pid %i" p.ProcessName p.Id
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();

            //printfn "Finished %s after %A milliseconds" desc timer.ElapsedMilliseconds
            var rc = p.ExitCode;
            //if (rc != 0)
            //throw new Exception();
        }

        private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Console.Error.WriteLine(e.Data);
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Console.WriteLine(e.Data);
        }
        #endregion
        #region // Version
        static string NUSPEC_VERSION { get; } = string.Format("{0:yyyy.M.d}", DateTime.Now);
        static string ASSEMBLY_VERSION { get; } = string.Format("{0:yyyy.M.d}.{1}", DateTime.Now, (int)(DateTime.Now - new DateTime(2020, 1, 1)).TotalDays);
        static string COPYRIGHT { get; } = $"Copyright 2020-{DateTime.Now.Year}";
        static string AUTHORS { get; } = "ErikZhouXin EricSink";
        static string SUMMARY { get; } = "SQLitePCLRaw is a Portable Class Library (PCL) for low-level (raw) access to SQLite";

        private static void gen_directory_build_props(string root, string nupkgs_dir_name)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (XmlWriter f = XmlWriter.Create(Path.Combine(root, "Directory.Build.props"), settings))
            {
                f.WriteStartDocument();

                f.WriteComment("This file is automatically generated");
                f.WriteStartElement("Project");
                f.WriteStartElement("PropertyGroup");

                f.WriteElementString("Copyright", COPYRIGHT);
                f.WriteElementString("Company", "SourceGear");
                f.WriteElementString("Authors", AUTHORS);
                f.WriteElementString("Version", NUSPEC_VERSION);
                f.WriteElementString("AssemblyVersion", ASSEMBLY_VERSION);
                f.WriteElementString("FileVersion", ASSEMBLY_VERSION);
                f.WriteElementString("Description", SUMMARY);
                f.WriteElementString("ProviderLangVersion", "7.3");
                f.WriteElementString("GenerateAssemblyProductAttribute", "false");
                f.WriteElementString("PackageLicenseExpression", "Apache-2.0");
                f.WriteElementString("PackageRequireLicenseAcceptance", "false");
                f.WriteElementString("PackageTags", PACKAGE_TAGS);
                f.WriteElementString("RepositoryUrl", "https://github.com/ericsink/SQLitePCL.raw");
                f.WriteElementString("RepositoryType", "git");
                f.WriteElementString("PackageOutputPath", string.Format("$([System.IO.Path]::Combine($(MSBuildThisFileDirectory), '{0}'))", nupkgs_dir_name));

                f.WriteElementString("cb_bin_path", "$([System.IO.Path]::Combine($(MSBuildThisFileDirectory), 'Beans'))");
                f.WriteElementString("src_path", "$([System.IO.Path]::Combine($(MSBuildThisFileDirectory), '..', 'src'))");

                f.WriteEndElement(); // PropertyGroup
                f.WriteEndElement(); // project

                f.WriteEndDocument();
            }
        }
        #endregion
        #region // Generator Creater

        enum TFM
        {
            NONE,
            IOS,
            TVOS,
            ANDROID,
            UWP,
            NETSTANDARD20,
            NET461,
            XAMARIN_MAC,
            NETCOREAPP31,
            NET50,
            NET40,
            NET45,
        }

        enum LibSuffix
        {
            DLL,
            DYLIB,
            SO,
        }

        // in cb, the sqlcipher builds do not have the e_ prefix.
        // we do this so we can continue to build v1.
        // but in v2, we want things to be called e_sqlcipher.

        static string AsString_basename_in_cb(this WhichLib e)
        {
            switch (e)
            {
                case WhichLib.E_SQLITE3: return "e_sqlite3";
                case WhichLib.E_SQLCIPHER: return "e_sqlcipher"; // TODO no e_ prefix in cb yet
                default:
                    throw new NotImplementedException(string.Format("WhichLib.AsString for {0}", e));
            }
        }

        static string AsString_basename_in_nupkg(this WhichLib e)
        {
            switch (e)
            {
                case WhichLib.E_SQLITE3: return "e_sqlite3";
                case WhichLib.E_SQLCIPHER: return "e_sqlcipher";
                default:
                    throw new NotImplementedException(string.Format("WhichLib.AsString for {0}", e));
            }
        }

        static string basename_to_libname(string basename, LibSuffix suffix)
        {
            switch (suffix)
            {
                case LibSuffix.DLL:
                    return $"{basename}.dll";
                case LibSuffix.DYLIB:
                    return $"lib{basename}.dylib";
                case LibSuffix.SO:
                    return $"lib{basename}.so";
                default:
                    throw new NotImplementedException();
            }
        }

        static string AsString_libname_in_nupkg(this WhichLib e, LibSuffix suffix)
        {
            var basename = e.AsString_basename_in_nupkg();
            return basename_to_libname(basename, suffix);
        }

        static string AsString_libname_in_cb(this WhichLib e, LibSuffix suffix)
        {
            var basename = e.AsString_basename_in_cb();
            return basename_to_libname(basename, suffix);
        }

        static string AsString(this TFM e)
        {
            switch (e)
            {
                case TFM.NONE: throw new Exception("TFM.NONE.AsString()");
                case TFM.IOS: return "Xamarin.iOS10";
                case TFM.TVOS: return "Xamarin.tvOS10";
                case TFM.ANDROID: return "MonoAndroid80";
                case TFM.UWP: return "uap10.0";
                case TFM.NETSTANDARD20: return "netstandard2.0";
                case TFM.XAMARIN_MAC: return "Xamarin.Mac20";
                case TFM.NET461: return "net461";
                case TFM.NETCOREAPP31: return "netcoreapp3.1";
                case TFM.NET50: return "net5.0";
                case TFM.NET40: return "net40";
                case TFM.NET45: return "net45";
                default:
                    throw new NotImplementedException(string.Format("TFM.AsString for {0}", e));
            }
        }

        private static void write_nuspec_file_entry(string src, string target, XmlWriter f)
        {
            f.WriteStartElement("file");
            f.WriteAttributeString("src", src);
            f.WriteAttributeString("target", target);
            f.WriteEndElement(); // file
        }

        private static void write_nuspec_file_entry_native(string src, string rid, string filename, XmlWriter f)
        {
            write_nuspec_file_entry(
                src,
                string.Format("runtimes\\{0}\\native\\{1}", rid, filename),
                f
                );
        }

        private static void write_nuspec_file_entry_nativeassets(string src, string rid, TFM tfm, string filename, XmlWriter f)
        {
            write_nuspec_file_entry(
                src,
                string.Format("runtimes\\{0}\\nativeassets\\{1}\\{2}", rid, tfm.AsString(), filename),
                f
                );
        }

        private static void write_empty(XmlWriter f, TFM tfm)
        {
            f.WriteComment("empty directory in lib to avoid nuget adding a reference");

            f.WriteStartElement("file");
            f.WriteAttributeString("src", "_._");
            f.WriteAttributeString("target", string.Format("lib/{0}/_._", tfm.AsString()));
            f.WriteEndElement(); // file
        }

        const string PACKAGE_TAGS = "sqlite;xamarin";

        private static void write_nuspec_common_metadata(
            string id,
            XmlWriter f
            )
        {
            f.WriteAttributeString("minClientVersion", "2.12"); // TODO not sure this is right

            f.WriteElementString("id", id);
            f.WriteElementString("title", id);
            f.WriteElementString("version", "$version$");
            f.WriteElementString("authors", "$authors$");
            f.WriteElementString("copyright", "$copyright$");
            f.WriteElementString("requireLicenseAcceptance", "false");
            write_license(f);
            f.WriteStartElement("repository");
            f.WriteAttributeString("type", "git");
            f.WriteAttributeString("url", "https://github.com/ericsink/SQLitePCL.raw");
            f.WriteEndElement(); // repository
            f.WriteElementString("summary", "$summary$");
            f.WriteElementString("tags", PACKAGE_TAGS);
        }

        static string make_cb_path_win(
            WhichLib lib,
            string toolset,
            string flavor,
            string arch
            )
        {
            var dir_name = lib.AsString_basename_in_cb();
            var lib_name = lib.AsString_libname_in_cb(LibSuffix.DLL);
            return Path.Combine("$cb_bin_path$", dir_name, "win", toolset, flavor, arch, lib_name);
        }

        static string make_cb_path_linux(
            WhichLib lib,
            string cpu
            )
        {
            var dir_name = lib.AsString_basename_in_cb();
            var lib_name = lib.AsString_libname_in_cb(LibSuffix.SO);
            return Path.Combine("$cb_bin_path$", dir_name, "linux", cpu, lib_name);
        }

        static string make_cb_path_mac(
            WhichLib lib,
            string cpu
            )
        {
            var dir_name = lib.AsString_basename_in_cb();
            var lib_name = lib.AsString_libname_in_cb(LibSuffix.DYLIB);
            return Path.Combine("$cb_bin_path$", dir_name, "mac", cpu, lib_name);
        }

        static void write_nuspec_file_entry_native_linux(
            WhichLib lib,
            string cpu_in_cb,
            string rid,
            XmlWriter f
            )
        {
            var filename = lib.AsString_libname_in_nupkg(LibSuffix.SO);
            write_nuspec_file_entry_native(
                make_cb_path_linux(lib, cpu_in_cb),
                rid,
                filename,
                f
                );
        }

        static void write_nuspec_file_entry_native_mac(
            WhichLib lib,
            string cpu_in_cb,
            string rid,
            XmlWriter f
            )
        {
            var filename = lib.AsString_libname_in_nupkg(LibSuffix.DYLIB);
            write_nuspec_file_entry_native(
                make_cb_path_mac(lib, cpu_in_cb),
                rid,
                filename,
                f
                );
        }

        static void write_nuspec_file_entry_native_win(
            WhichLib lib,
            string toolset,
            string flavor,
            string cpu,
            string rid,
            XmlWriter f
            )
        {
            var filename = lib.AsString_libname_in_nupkg(LibSuffix.DLL);
            write_nuspec_file_entry_native(
                make_cb_path_win(lib, toolset, flavor, cpu),
                rid,
                filename,
                f
                );
        }

        static void write_nuspec_file_entry_native_uwp(
            WhichLib lib,
            string toolset,
            string flavor,
            string cpu,
            string rid,
            XmlWriter f
            )
        {
            var filename = lib.AsString_libname_in_nupkg(LibSuffix.DLL);
            write_nuspec_file_entry_nativeassets(
                make_cb_path_win(lib, toolset, flavor, cpu),
                rid,
                TFM.UWP,
                filename,
                f
                );
        }

        static void write_nuspec_file_entries_from_cb(
            WhichLib lib,
            string win_toolset,
            XmlWriter f
            )
        {
            write_nuspec_file_entry_native_win(lib, win_toolset, "plain", "x86", "win-x86", f);
            write_nuspec_file_entry_native_win(lib, win_toolset, "plain", "x64", "win-x64", f);
            write_nuspec_file_entry_native_win(lib, win_toolset, "plain", "arm", "win-arm", f);
            write_nuspec_file_entry_native_win(lib, win_toolset, "plain", "arm64", "win-arm64", f);
            write_nuspec_file_entry_native_uwp(lib, win_toolset, "appcontainer", "arm64", "win10-arm64", f);
            write_nuspec_file_entry_native_uwp(lib, win_toolset, "appcontainer", "arm", "win10-arm", f);
            write_nuspec_file_entry_native_uwp(lib, win_toolset, "appcontainer", "x64", "win10-x64", f);
            write_nuspec_file_entry_native_uwp(lib, win_toolset, "appcontainer", "x86", "win10-x86", f);

            write_nuspec_file_entry_native_mac(lib, "x86_64", "osx-x64", f);
            write_nuspec_file_entry_native_mac(lib, "arm64", "osx-arm64", f);

            write_nuspec_file_entry_native_linux(lib, "x64", "linux-x64", f);
            write_nuspec_file_entry_native_linux(lib, "x86", "linux-x86", f);
            write_nuspec_file_entry_native_linux(lib, "armhf", "linux-arm", f);
            write_nuspec_file_entry_native_linux(lib, "armsf", "linux-armel", f);
            write_nuspec_file_entry_native_linux(lib, "arm64", "linux-arm64", f);
            write_nuspec_file_entry_native_linux(lib, "musl-x64", "linux-musl-x64", f);
            write_nuspec_file_entry_native_linux(lib, "musl-x64", "alpine-x64", f);
            write_nuspec_file_entry_native_linux(lib, "mips64", "linux-mips64", f);
            write_nuspec_file_entry_native_linux(lib, "s390x", "linux-s390x", f);
        }

        private static XmlWriterSettings XmlWriterSettings_default()
        {
            var settings = new XmlWriterSettings();
            settings.NewLineChars = "\n";
            settings.Indent = true;
            return settings;
        }

        private static void gen_dummy_csproj(string dir_proj, string id, string proj)
        {
            var settings = XmlWriterSettings_default();
            settings.OmitXmlDeclaration = true;

            using (XmlWriter f = XmlWriter.Create(Path.Combine(dir_proj, $"{proj}.csproj"), settings))
            {
                f.WriteStartDocument();
                f.WriteComment("Automatically generated");

                f.WriteStartElement("Project");
                f.WriteAttributeString("Sdk", "Microsoft.NET.Sdk");

                f.WriteStartElement("PropertyGroup");

                f.WriteElementString("TargetFrameworks", "netstandard2.0;net40;net45;");
                f.WriteElementString("NoBuild", "true");
                f.WriteElementString("IncludeBuildOutput", "false");
                f.WriteElementString("NuspecFile", $"{id}.nuspec");
                f.WriteElementString("NuspecProperties", "version=$(version);src_path=$(src_path);cb_bin_path=$(cb_bin_path);authors=$(Authors);copyright=$(Copyright);summary=$(Description)");

                f.WriteEndElement(); // PropertyGroup

                f.WriteEndElement(); // Project

                f.WriteEndDocument();
            }
        }

        private static void gen_nuspec_lib_e_sqlcipher(string dir_src)
        {
            string id = "NSQLitePCL.Raw.Liber";
            string proj = "SQLitePCL.Raw.Liber";
            string projDir = "Lib";

            var settings = XmlWriterSettings_default();
            settings.OmitXmlDeclaration = false;

            var dir_proj = Path.Combine(dir_src, projDir);
            Directory.CreateDirectory(dir_proj);
            gen_dummy_csproj(dir_proj, id, proj);

            using (XmlWriter f = XmlWriter.Create(Path.Combine(dir_proj, string.Format("{0}.nuspec", id)), settings))
            {
                f.WriteStartDocument();
                f.WriteComment("Automatically generated");

                f.WriteStartElement("package", "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd");

                f.WriteStartElement("metadata");
                write_nuspec_common_metadata(id, f);
                f.WriteElementString("description", "This package contains platform-specific native code builds of SQLCipher (see sqlcipher/sqlcipher on GitHub) for use with SQLitePCLRaw.  Note that these sqlcipher builds are unofficial and unsupported.  For official sqlcipher builds, contact Zetetic.  To use this package, you need SQLitePCLRaw.core as well as one of the SQLitePCLRaw.provider.* packages.  Convenience packages are named SQLitePCLRaw.bundle_*.");

                f.WriteEndElement(); // metadata

                f.WriteStartElement("files");

                write_nuspec_file_entries_from_cb(WhichLib.E_SQLCIPHER, "v142", f);

                var tname = string.Format("{0}.targets", id);
                var path_targets = Path.Combine(dir_proj, tname);
                var relpath_targets = Path.Combine(".", tname);
                gen_nuget_targets(path_targets, WhichLib.E_SQLCIPHER);
                write_nuspec_file_entry(relpath_targets, string.Format("build\\{0}", TFM.NET461.AsString()), f);
                // TODO need a comment here to explain these
                write_empty(f, TFM.NET461);
                write_empty(f, TFM.NETSTANDARD20);
                write_empty(f, TFM.NET40);
                write_empty(f, TFM.NET45);

                f.WriteEndElement(); // files

                f.WriteEndElement(); // package

                f.WriteEndDocument();
            }

            var coreProj = Path.GetFullPath(Path.Combine(dir_proj, proj + ".csproj"));
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

        private static void write_license(XmlWriter f)
        {
            f.WriteStartElement("license");
            f.WriteAttributeString("type", "expression");
            f.WriteString("Apache-2.0");
            f.WriteEndElement();
        }

        enum WhichProvider
        {
            E_SQLITE3,
            E_SQLCIPHER,
            SQLCIPHER,
            INTERNAL,
            SQLITE3,
            WINSQLITE3,
            DYNAMIC_CDECL,
            DYNAMIC_STDCALL,
        }

        static string AsString(this WhichProvider e)
        {
            switch (e)
            {
                case WhichProvider.E_SQLITE3: return "e_sqlite3";
                case WhichProvider.E_SQLCIPHER: return "e_sqlcipher";
                case WhichProvider.SQLCIPHER: return "sqlcipher";
                case WhichProvider.INTERNAL: return "internal";
                case WhichProvider.SQLITE3: return "sqlite3";
                case WhichProvider.WINSQLITE3: return "winsqlite3";
                case WhichProvider.DYNAMIC_CDECL: return "dynamic_cdecl";
                case WhichProvider.DYNAMIC_STDCALL: return "dynamic_stdcall";
                default:
                    throw new NotImplementedException(string.Format("WhichProvider.AsString for {0}", e));
            }
        }

        enum WhichLib
        {
            NONE,
            E_SQLITE3,
            E_SQLCIPHER,
        }

        static LibSuffix get_lib_suffix_from_rid(string rid)
        {
            var parts = rid.Split('-');
            var front = parts[0].ToLower();
            if (front.StartsWith("win"))
            {
                return LibSuffix.DLL;
            }
            else if (front.StartsWith("osx"))
            {
                return LibSuffix.DYLIB;
            }
            else if (
                front.StartsWith("linux")
                || front.StartsWith("alpine")
                )
            {
                return LibSuffix.SO;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static void write_nuget_target_item(
            string rid,
            WhichLib lib,
            XmlWriter f
            )
        {
            var suffix = get_lib_suffix_from_rid(rid);
            var filename = lib.AsString_libname_in_nupkg(suffix);

            f.WriteStartElement("Content");
            f.WriteAttributeString("Include", string.Format("$(MSBuildThisFileDirectory)..\\..\\runtimes\\{0}\\native\\{1}", rid, filename));
            f.WriteElementString("Link", string.Format("runtimes\\{0}\\native\\{1}", rid, filename));
            f.WriteElementString("CopyToOutputDirectory", "PreserveNewest");
            f.WriteElementString("Pack", "false");
            f.WriteEndElement(); // Content
        }

        private static void gen_nuget_targets(string dest, WhichLib lib)
        {
            var settings = XmlWriterSettings_default();
            settings.OmitXmlDeclaration = false;

            using (XmlWriter f = XmlWriter.Create(dest, settings))
            {
                f.WriteStartDocument();
                f.WriteComment("Automatically generated");

                f.WriteStartElement("Project", "http://schemas.microsoft.com/developer/msbuild/2003");
                f.WriteAttributeString("ToolsVersion", "4.0");

                f.WriteStartElement("ItemGroup");
                f.WriteAttributeString("Condition", " '$(RuntimeIdentifier)' == '' AND '$(OS)' == 'Windows_NT' ");
                write_nuget_target_item("win-x86", lib, f);
                write_nuget_target_item("win-x64", lib, f);
                write_nuget_target_item("win-arm", lib, f);
                f.WriteEndElement(); // ItemGroup

                f.WriteStartElement("ItemGroup");
                f.WriteAttributeString("Condition", " '$(RuntimeIdentifier)' == '' AND '$(OS)' == 'Unix' AND Exists('/Library/Frameworks') ");
                write_nuget_target_item("osx-x64", lib, f);
                f.WriteEndElement(); // ItemGroup

                f.WriteStartElement("ItemGroup");
                f.WriteAttributeString("Condition", " '$(RuntimeIdentifier)' == '' AND '$(OS)' == 'Unix' AND !Exists('/Library/Frameworks') ");
                write_nuget_target_item("linux-x86", lib, f);
                write_nuget_target_item("linux-x64", lib, f);
                write_nuget_target_item("linux-arm", lib, f);
                write_nuget_target_item("linux-armel", lib, f);
                write_nuget_target_item("linux-arm64", lib, f);
                write_nuget_target_item("linux-x64", lib, f);
                write_nuget_target_item("linux-mips64", lib, f);
                write_nuget_target_item("linux-s390x", lib, f);
                f.WriteEndElement(); // ItemGroup

                f.WriteEndElement(); // Project

                f.WriteEndDocument();
            }
        }

        #endregion

    }
}
