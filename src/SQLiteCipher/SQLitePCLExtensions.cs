// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
using SQLitePCL;

namespace System.Data.SQLiteCipher
{
    internal static class SQLitePCLExtensions
    {
#if !NET40 && !NET45
        public static bool EncryptionNotSupported()
            => raw.GetNativeLibraryName() == "e_sqlite3";
#endif
    }
}
