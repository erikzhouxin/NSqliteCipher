using System;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// ��̬���͵��ṩ��
    /// used by the dynamic providers
    /// </summary>
    public interface IGetFunctionPointer
    {
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IntPtr GetFunctionPointer(string name);
    }

}
