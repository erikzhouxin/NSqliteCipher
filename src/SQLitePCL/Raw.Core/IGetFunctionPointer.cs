using System;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 动态类型的提供者
    /// used by the dynamic providers
    /// </summary>
    public interface IGetFunctionPointer
    {
        /// <summary>
        /// 获取函数句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IntPtr GetFunctionPointer(string name);
    }

}
