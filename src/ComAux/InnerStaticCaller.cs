using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Data.SQLiteCipher
{
    internal static class InnerStaticCaller
    {
        public static string utf8_to_string(this string value)
        {
            return value ?? string.Empty;
        }
#if NET40
        public static Object GetValue(this PropertyInfo prop,object model)
        {
            return prop.GetValue(null, null);
        }
        public static TypeInfo GetTypeInfo(this Type type)
        {
            return new TypeInfo(type);
        }
#endif
#if NET40 || NET45
        public static PropertyInfo GetRuntimeProperty(this Type type, string name)
        {
            return type.GetProperty(name);
        }
        public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public |
                                      BindingFlags.Static | BindingFlags.Instance);
        }
        public static T[] AsSpan<T>(this T[] arr,int start,int length)
        {
            return arr.Skip(start).Take(length).ToArray();
        }
        public static T[] ToArray<T>(this T[] arr)
        {
            return arr;
        }
#endif
        /// <summary>
        /// 获取对象的Json字符串
        /// Newtonsoft.Json.JsonConvert
        /// </summary>
        public static string GetJsonString<T>(this T value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
