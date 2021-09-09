using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SQLitePCL.Raw.Core
{
    /// <summary>
    /// �û���̬�ṩ��
    /// </summary>
    public sealed class EntryPointAttribute : Attribute
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="name"></param>
        public EntryPointAttribute(string name)
        {
            Name = name;
        }
    }
}
