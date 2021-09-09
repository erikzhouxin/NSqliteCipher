using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// 用户动态提供者
    /// </summary>
    public sealed class EntryPointAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        public EntryPointAttribute(string name)
        {
            Name = name;
        }
    }
}
