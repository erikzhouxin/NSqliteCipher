using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace SQLitePCL
{
    static class util
    {
        public static utf8z to_utf8z(this string s)
        {
            return utf8z.FromString(s);
        }

        public static byte[] to_utf8_with_z(this string sourceText)
        {
            if (sourceText == null)
            {
                return null;
            }

            int nlen = Encoding.UTF8.GetByteCount(sourceText);

            var byteArray = new byte[nlen + 1];
            var wrote = Encoding.UTF8.GetBytes(sourceText, 0, sourceText.Length, byteArray, 0);
            byteArray[wrote] = 0;

            return byteArray;
        }

        static int my_strlen(System.IntPtr nativeString)
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

        public static string from_utf8_z(IntPtr nativeString)
        {
            return from_utf8(nativeString, my_strlen(nativeString));
        }

        public static string from_utf8(IntPtr nativeString, int size)
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

