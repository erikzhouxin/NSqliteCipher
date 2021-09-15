using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Data.SQLiteEFCore
{
    internal sealed class LegacyReferenceEqualityComparer : IEqualityComparer<object>, IEqualityComparer
    {
        private LegacyReferenceEqualityComparer()
        {
        }

        public static LegacyReferenceEqualityComparer Instance { get; } = new LegacyReferenceEqualityComparer();

        public new bool Equals([CanBeNull] object x, [CanBeNull] object y)
            => ReferenceEquals(x, y);

        public int GetHashCode([NotNull] object obj)
            => RuntimeHelpers.GetHashCode(obj);

        bool IEqualityComparer<object>.Equals(object x, object y)
            => ReferenceEquals(x, y);

        int IEqualityComparer.GetHashCode(object obj)
            => RuntimeHelpers.GetHashCode(obj);

        bool IEqualityComparer.Equals(object x, object y)
            => ReferenceEquals(x, y);

        int IEqualityComparer<object>.GetHashCode(object obj)
            => RuntimeHelpers.GetHashCode(obj);
    }
}
