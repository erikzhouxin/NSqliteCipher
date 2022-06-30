using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 内部调用类
    /// </summary>
    static class InnerCaller
    {
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

        public static string FromUtf8z(IntPtr nativeString)
        {
            return FromUtf8(nativeString, MyStrLen(nativeString));
        }

        public static string FromUtf8(IntPtr nativeString, int size)
        {
            string result = null;
            if (nativeString != IntPtr.Zero)
            {
                unsafe
                {
                    result = Encoding.UTF8.GetString((byte*)nativeString.ToPointer(), size);
                }
            }
            return result;
        }

    }

}

