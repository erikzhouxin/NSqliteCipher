using System;
using System.Collections.Generic;
using System.Text;

namespace SQLitePCL
{
    internal static class CompatAssist
    {
#if NET45 || NET40
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
