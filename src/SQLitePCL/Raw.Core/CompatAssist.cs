using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 兼容助手
    /// </summary>
    internal static class CompatAssist
    {
#if NET40
        public static byte[] Slice(this byte[] data, int start, int len)
        {
            if (data == null || data.Length < (start + len)) { return new byte[0]; }
            var res = new byte[len];
            for (int i = start; i < start + len; i++)
            {
                res[i - start] = data[i];
            }
            return res;
        }

        public static unsafe byte[] GetPointerArray(IntPtr ptr, int length)
        {
            var ptrX = ptr.ToPointer();
            var res = new byte[length];
            using (UnmanagedMemoryStream ms = new UnmanagedMemoryStream((byte*)ptrX, length))
            {
                ms.Read(res, 0, res.Length);
            }
            return res;

        }
#endif
#if NET45 || NET40
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="bytes"></param>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static unsafe string GetString(this Encoding encoding, byte* bytes, int byteCount)
        {
            var byteArray = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                byteArray[i] = bytes[i];
            }
            return encoding.GetString(byteArray);
        }
#endif
        public static Utf8z ToUtf8z(this string s)
        {
            return Utf8z.FromString(s);
        }

        public static byte[] ToUtf8WithZ(this string sourceText)
        {
            if (sourceText == null) { return new byte[] { }; }
            int nlen = Encoding.UTF8.GetByteCount(sourceText);
            var byteArray = new byte[nlen + 1];
            var wrote = Encoding.UTF8.GetBytes(sourceText, 0, sourceText.Length, byteArray, 0);
            byteArray[wrote] = 0;
            return byteArray;
        }

        static int MyStrLen(System.IntPtr nativeString)
        {
            var offset = 0;
            if (nativeString != IntPtr.Zero)
            {
                // TODO would this be faster if it used unsafe code with a pointer?
                while (Marshal.ReadByte(nativeString, offset) > 0)
                {
                    offset++;
                }
            }
            return offset;
        }
        /// <summary>
        /// 来自Utf8z
        /// </summary>
        /// <param name="nativeString"></param>
        /// <returns></returns>
        public static string FromUtf8z(IntPtr nativeString)
        {
            return FromUtf8(nativeString, MyStrLen(nativeString));
        }
        /// <summary>
        /// 来自UTF8
        /// </summary>
        /// <param name="nativeString"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static unsafe string FromUtf8(IntPtr nativeString, int size)
        {
            return nativeString == IntPtr.Zero ? null : Encoding.UTF8.GetString((byte*)nativeString.ToPointer(), size);
        }
    }
}
