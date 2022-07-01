using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 原生类库
    /// </summary>
    public static partial class NativeLibrary
    {
        /// <summary>
        /// where内容
        /// </summary>
        public const int WHERE_PLAIN = 0x01;
        /// <summary>
        /// where运行环境行标识
        /// </summary>
        public const int WHERE_RUNTIME_RID = 0x02;
        /// <summary>
        /// where架构
        /// </summary>
        public const int WHERE_ARCH = 0x04;
#if NET50 || NET60 || NETCore
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="libraryName"></param>
        /// <param name="assy"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static IntPtr Load(string libraryName, System.Reflection.Assembly assy, int flags)
        {
            // TODO convert flags
            return System.Runtime.InteropServices.NativeLibrary.Load(libraryName, assy, null);
        }
        /// <summary>
        /// 尝试加载程序集
        /// </summary>
        /// <param name="libraryName"></param>
        /// <param name="assy"></param>
        /// <param name="flags"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static bool TryLoad(string libraryName, System.Reflection.Assembly assy, int flags, out IntPtr handle)
        {
            // TODO convert flags
            return System.Runtime.InteropServices.NativeLibrary.TryLoad(libraryName, assy, null, out handle);
        }
        /// <summary>
        /// 获取导出句柄
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IntPtr GetExport(IntPtr handle, string name)
        {
            return System.Runtime.InteropServices.NativeLibrary.GetExport(handle, name);
        }
        /// <summary>
        /// 尝试获取导出句柄
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool TryGetExport(IntPtr handle, string name, out IntPtr address)
        {
            return System.Runtime.InteropServices.NativeLibrary.TryGetExport(handle, name, out address);
        }
        /// <summary>
        /// 释放句柄
        /// </summary>
        /// <param name="handle"></param>
        public static void Free(IntPtr handle)
        {
            System.Runtime.InteropServices.NativeLibrary.Free(handle);
        }
#else
        static class NativeLib_dlopen
        {
            const string SO = "dl";

            public const int RTLD_NOW = 2; // for dlopen's flags 

            [DllImport(SO)]
            public static extern IntPtr dlopen(string dllToLoad, int flags);

            [DllImport(SO)]
            public static extern IntPtr dlsym(IntPtr hModule, string procedureName);

            [DllImport(SO)]
            public static extern int dlclose(IntPtr hModule);

        }

        static class NativeLib_Win
        {
            [DllImport("kernel32", SetLastError = true)]
            public static extern IntPtr LoadLibrary(string lpFileName);

            public const uint LOAD_WITH_ALTERED_SEARCH_PATH = 8;

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FreeLibrary(IntPtr hModule);

        }

        enum LibSuffix
        {
            DLL,
            DYLIB,
            SO,
        }

        enum Loader
        {
            win,
            dlopen,
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="libraryName"></param>
        /// <param name="assy"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IntPtr Load(string libraryName, System.Reflection.Assembly assy, int flags)
        {
            var logWriter = new StringWriter();
            logWriter.WriteLine($"Library {libraryName} not found");
            var h = MyLoad(libraryName, assy, flags, s => logWriter.WriteLine(s));
            if (h == IntPtr.Zero)
            {
                throw new Exception(logWriter.ToString());
            }
            return h;
        }
        static IntPtr MyGetExport(IntPtr handle, string name)
        {
            var plat = WhichLoader();
            if (plat == Loader.win)
            {
                return NativeLib_Win.GetProcAddress(handle, name);
            }
            else if (plat == Loader.dlopen)
            {
                return NativeLib_dlopen.dlsym(handle, name);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 获取导出
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IntPtr GetExport(IntPtr handle, string name)
        {
            var h = MyGetExport(handle, name);
            if (h == IntPtr.Zero)
            {
                throw new Exception($"Symbol {name} not found");
            }
            return h;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="handle"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void Free(IntPtr handle)
        {
            var plat = WhichLoader();
            if (plat == Loader.win)
            {
                NativeLib_Win.FreeLibrary(handle);
            }
            else if (plat == Loader.dlopen)
            {
                NativeLib_dlopen.dlclose(handle);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 尝试加载
        /// </summary>
        /// <param name="libraryName"></param>
        /// <param name="assy"></param>
        /// <param name="flags"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static bool TryLoad(string libraryName, System.Reflection.Assembly assy, int flags, out IntPtr handle)
        {
            var h = MyLoad(libraryName, assy, flags, s => { });
            handle = h;
            return h != IntPtr.Zero;
        }
        /// <summary>
        /// 尝试导出
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool TryGetExport(IntPtr handle, string name, out IntPtr address)
        {
            var h = MyGetExport(handle, name);
            address = h;
            return h != IntPtr.Zero;
        }
        /// <summary>
        /// 基础名称
        /// </summary>
        /// <param name="basename"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static string basename_to_libname(string basename, LibSuffix suffix)
        {
            switch (suffix)
            {
                case LibSuffix.DLL:
                    return string.Format("{0}.dll", basename);
                case LibSuffix.DYLIB:
                    return string.Format("lib{0}.dylib", basename);
                case LibSuffix.SO:
                    return string.Format("lib{0}.so", basename);
                default:
                    throw new NotImplementedException();
            }
        }

        static bool TryLoad(string name, Loader plat, Action<string> log, out IntPtr h)
        {
            try
            {
                if (plat == Loader.win)
                {
                    log($"win TryLoad: {name}");
                    var ptr = NativeLib_Win.LoadLibrary(name);
                    if (ptr != IntPtr.Zero)
                    {
                        log($"LoadLibrary gave: {ptr}");
                        h = ptr;
                        return true;
                    }
                    else
                    {
                        var err = Marshal.GetLastWin32Error();
                        // NOT HERE: log($"error code: {err}");
                        throw new System.ComponentModel.Win32Exception();
                    }
                }
                else if (plat == Loader.dlopen)
                {
                    log($"dlopen TryLoad: {name}");
                    var ptr = NativeLib_dlopen.dlopen(name, NativeLib_dlopen.RTLD_NOW);
                    log($"dlopen gave: {ptr}");
                    if (ptr != IntPtr.Zero)
                    {
                        h = ptr;
                        return true;
                    }
                    else
                    {
                        // TODO log errno?
                        h = IntPtr.Zero;
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (NotImplementedException)
            {
                throw;
            }
            catch (Exception e)
            {
                log($"thrown: {e}");
                h = IntPtr.Zero;
                return false;
            }
        }

        static Loader WhichLoader()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Loader.win;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Loader.dlopen;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Loader.dlopen;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static LibSuffix WhichLibSuffix()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return LibSuffix.DLL;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LibSuffix.SO;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return LibSuffix.DYLIB;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static string get_rid_front()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "win";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "osx";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static string get_rid_back()
        {
            switch (RuntimeInformation.OSArchitecture)
            {
                case Architecture.Arm:
                    return "arm";
                case Architecture.Arm64:
                    return "arm64";
                case Architecture.X64:
                    return (IntPtr.Size == 8) ? "x64" : "x86";
                case Architecture.X86:
                    return "x86";
                default:
                    throw new NotImplementedException();
            }
        }

        static string get_rid()
        {
            var front = get_rid_front();
            var back = get_rid_back();
            return $"{front}-{back}";
        }

        static bool Search(IList<string> a, Loader plat, Action<string> log, out string name, out IntPtr h)
        {
            foreach (var s in a)
            {
                if (TryLoad(s, plat, log, out var api))
                {
                    name = s;
                    h = api;
                    return true;
                }
            }
            name = null;
            h = IntPtr.Zero;
            return false;
        }

        static List<string> MakePossibilitiesFor(string basename, System.Reflection.Assembly assy, int flags, LibSuffix suffix)
        {
            var a = new List<string>();
#if not
			a.Add(basename);
#endif
            var libname = basename_to_libname(basename, suffix);
            if ((flags & WHERE_PLAIN) != 0)
            {
                a.Add(libname);
            }

#if not // TODO is this ever useful?
			{
				var dir = System.IO.Directory.GetCurrentDirectory();
				a.Add(Path.Combine(dir, "runtimes", rid, "native", libname));
			}
#endif

            if ((flags & WHERE_RUNTIME_RID) != 0)
            {
                var rid = get_rid();
                var dir = System.IO.Path.GetDirectoryName(assy.Location);
                a.Add(Path.Combine(dir, "runtimes", rid, "native", libname));
            }

            if ((flags & WHERE_ARCH) != 0)
            {
                var dir = System.IO.Path.GetDirectoryName(assy.Location);
                var arch = get_rid_back();
                a.Add(Path.Combine(dir, arch, libname));
            }

            return a;
        }

#if not
		static IntPtr Load_ios_internal()
		{
			// TODO err check this
			var dll = NativeLib_dlopen.dlopen(null, NativeLib_dlopen.RTLD_NOW);
            return dll;
		}
#endif

        static IntPtr MyLoad(string basename, System.Reflection.Assembly assy, int flags, Action<string> log)
        {
            // TODO make this code accept a string that already has the suffix?
            // TODO does S.R.I.NativeLibrary do that?

            var plat = WhichLoader();
            log($"plat: {plat}");
            var suffix = WhichLibSuffix();
            log($"suffix: {suffix}");
            var a = MakePossibilitiesFor(basename, assy, flags, suffix);
            log($"possibilities ({a.Count}):");
            for (int i = 0; i < a.Count; i++)
            {
                log($"    {i + 1}) {a[i]}");
            }
            if (Search(a, plat, log, out var lib, out var h))
            {
                log($"found: {lib}");
                return h;
            }
            else
            {
                log("NOT FOUND");
                return IntPtr.Zero;
            }
        }
#endif
#if NET40 || NET45
        /// <summary>
        /// 运行环境信息
        /// </summary>
        public static class RuntimeInformation
        {
            private static string s_osDescription;
            private static volatile int s_osArch = -1;
            private static volatile int s_processArch = -1;
            /// <summary>
            /// 操作系统架构
            /// </summary>
            public static Architecture OSArchitecture
            {
                get
                {
                    int osArch = s_osArch;

                    if (osArch == -1)
                    {
                        SYSTEM_INFO.GetNativeSystemInfo(out SYSTEM_INFO sysInfo);
                        osArch = s_osArch = (int)Map((ProcessorArchitecture)sysInfo.wProcessorArchitecture);
                    }

                    return (Architecture)osArch;
                }
            }

            private static Architecture Map(ProcessorArchitecture processorArchitecture)
            {
                switch (processorArchitecture)
                {
                    case ProcessorArchitecture.Processor_Architecture_ARM64:
                        return Architecture.Arm64;
                    case ProcessorArchitecture.Processor_Architecture_ARM:
                        return Architecture.Arm;
                    case ProcessorArchitecture.Processor_Architecture_AMD64:
                        return Architecture.X64;
                    case ProcessorArchitecture.Processor_Architecture_INTEL:
                    default:
                        return Architecture.X86;
                }
            }
            /// <summary>
            /// 操作系统
            /// </summary>
            /// <param name="osPlatform"></param>
            /// <returns></returns>
            public static bool IsOSPlatform(OSPlatform osPlatform)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                    case PlatformID.Xbox:
                        return osPlatform == OSPlatform.Windows;
                    case PlatformID.Unix:
                        return osPlatform == OSPlatform.Linux;
                    case PlatformID.MacOSX:
                        return osPlatform == OSPlatform.OSX;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// https://blog.csdn.net/ArvinStudy/article/details/7761860
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            internal ushort wProcessorArchitecture;
            internal ushort wReserved;
            internal int dwPageSize;
            internal IntPtr lpMinimumApplicationAddress;
            internal IntPtr lpMaximumApplicationAddress;
            internal IntPtr dwActiveProcessorMask;
            internal int dwNumberOfProcessors;
            internal int dwProcessorType;
            internal int dwAllocationGranularity;
            internal short wProcessorLevel;
            internal short wProcessorRevision;

            [DllImport("kernel32", SetLastError = true)]
            internal static extern void GetNativeSystemInfo(out SYSTEM_INFO lpSystemInfo);
        }

        internal enum ProcessorArchitecture : ushort
        {
            Processor_Architecture_INTEL = 0,
            Processor_Architecture_ARM = 5,
            Processor_Architecture_IA64 = 6,
            Processor_Architecture_AMD64 = 9,
            Processor_Architecture_ARM64 = 12,
            Processor_Architecture_UNKNOWN = 0xFFFF
        }
        /// <summary>
        /// 架构
        /// </summary>
        public enum Architecture
        {
            /// <summary>
            /// 32位
            /// </summary>
            X86 = 0,
            /// <summary>
            /// 64位
            /// </summary>
            X64 = 1,
            /// <summary>
            /// Arm32位
            /// </summary>
            Arm = 2,
            /// <summary>
            /// Arm64位
            /// </summary>
            Arm64 = 3
        }
        /// <summary>
        /// 操作系统平台
        /// </summary>
        public readonly struct OSPlatform : IEquatable<OSPlatform>
        {
            /// <summary>
            /// Linux
            /// </summary>
            public static OSPlatform Linux { get; } = new OSPlatform("LINUX");
            /// <summary>
            /// OSX
            /// </summary>
            public static OSPlatform OSX { get; } = new OSPlatform("OSX");
            /// <summary>
            /// Windows
            /// </summary>
            public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                if (obj is OSPlatform os)
                {
                    return Name == os.Name;
                }
                if (obj is string oss)
                {
                    return Name == oss;
                }
                return false;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(OSPlatform other)
            {
                return Name == other.Name;
            }
            /// <summary>
            /// 哈希
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Name;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(OSPlatform left, OSPlatform right)
            {
                return left.Name == right.Name;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(OSPlatform left, OSPlatform right)
            {
                return left.Name != right.Name;
            }
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; }
            private OSPlatform(string name)
            {
                Name = name;
            }
        }
#endif
    }
}
