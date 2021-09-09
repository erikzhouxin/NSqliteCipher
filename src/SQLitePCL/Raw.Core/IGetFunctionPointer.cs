using System;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// used by the dynamic providers
    /// </summary>
    public interface IGetFunctionPointer
    {
        /// <summary>
        /// »ñÈ¡º¯Êý¾ä±ú
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IntPtr GetFunctionPointer(string name);
    }

}
