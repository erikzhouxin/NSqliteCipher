using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.SQLiteEFCore
{
    internal static class MethodInfoExtensions
    {
        public static bool IsContainsMethod(this MethodInfo method)
            => method.Name == nameof(IList.Contains)
                && method.DeclaringType.GetInterfaces().Append(method.DeclaringType).Any(
                    t => t == typeof(IList)
                        || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>)));
    }
}
