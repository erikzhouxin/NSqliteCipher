using System;
using System.Collections.Generic;
using System.IO;
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
            var res = new byte[len];
            for (int i = start; i < len; i++)
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
    }
}
