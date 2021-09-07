using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SQLitePCL
{

    // used by the dynamic providers

    public sealed class EntryPointAttribute : Attribute
    {
        public string Name { get; private set; }
        public EntryPointAttribute(string name)
        {
            Name = name;
        }
    }
}

