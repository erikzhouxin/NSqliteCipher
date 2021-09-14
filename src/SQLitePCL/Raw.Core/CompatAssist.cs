using System;
using System.Collections.Generic;
using System.Text;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 兼容助手
    /// </summary>
    internal static class CompatAssist
    {
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
