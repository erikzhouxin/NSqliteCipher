using System;
using System.Runtime.InteropServices;

namespace SQLitePCL
{

    // used by the dynamic providers

    public interface IGetFunctionPointer
    {
        IntPtr GetFunctionPointer(string name);
    }

}
