using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static Object GetValue(this PropertyInfo prop, object model)
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
        public static T[] AsSpan<T>(this T[] arr, int start, int length)
        {
            return arr.Skip(start).Take(length).ToArray();
        }
        public static T[] ToArray<T>(this T[] arr)
        {
            return arr;
        }
#endif
    }
#if NET40
    /// <summary>
    /// 委托
    /// </summary>
    /// <param name="user_data"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate int strdelegate_collation(object user_data, string s1, string s2);
    class WeakReference<T> : WeakReference
    {
        public WeakReference(T target) : base(target)
        { }
        public WeakReference(object target) : base(target)
        { }

        public bool TryGetTarget(out T c)
        {
            c = (T)base.Target;
            return base.Target is T;
        }
    }
    /// <summary>
    /// 类型信息
    /// </summary>
    public class TypeInfo : Type
    {
        private readonly Type innerType;
        /// <summary>
        /// 自定义属性
        /// </summary>
        public Attribute[] CustomAttributes { get; }
        /// <summary>
        /// 参数
        /// </summary>
        public Type[] GenericTypeArguments { get; }
        /// <summary>
        /// 定义属性
        /// </summary>
        public PropertyInfo[] DeclaredProperties { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="type"></param>
        public TypeInfo(Type type)
        {
            innerType = type;
            CustomAttributes = type.GetCustomAttributes(false) as Attribute[];
            GenericTypeArguments = type.GetGenericArguments();
            DeclaredProperties = type.GetProperties();
            GUID = Guid.NewGuid();
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public override Guid GUID { get; }
        /// <summary>
        /// 模型
        /// </summary>
        public override Module Module => innerType.Module;
        /// <summary>
        /// 程序集
        /// </summary>
        public override Assembly Assembly => innerType.Assembly;
        /// <summary>
        /// 全名
        /// </summary>
        public override string FullName => innerType.FullName;
        /// <summary>
        /// 命名空间
        /// </summary>
        public override string Namespace => innerType.Namespace;
        /// <summary>
        /// 程序集名称
        /// </summary>
        public override string AssemblyQualifiedName => innerType.AssemblyQualifiedName;
        /// <summary>
        /// 基类型
        /// </summary>
        public override Type BaseType => innerType.BaseType;
        /// <summary>
        /// 系统类型
        /// </summary>
        public override Type UnderlyingSystemType => innerType.UnderlyingSystemType;
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => innerType.Name;
        /// <summary>
        /// 获取构造方法
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return innerType.GetConstructors(bindingAttr);
        }
        /// <summary>
        /// 获取自定义注解
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override object[] GetCustomAttributes(bool inherit)
        {
            return innerType.GetCustomAttributes(inherit);
        }
        /// <summary>
        /// 获取自定义注解
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return innerType.GetCustomAttributes(attributeType, inherit);
        }
        /// <summary>
        /// 获取元素类型
        /// </summary>
        /// <returns></returns>
        public override Type GetElementType()
        {
            return innerType.GetElementType();
        }
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return innerType.GetEvent(name, bindingAttr);
        }
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return innerType.GetEvents(bindingAttr);
        }
        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return innerType.GetField(name, bindingAttr);
        }
        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return innerType.GetFields(bindingAttr);
        }
        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public override Type GetInterface(string name, bool ignoreCase)
        {
            return innerType.GetInterface(name, ignoreCase);
        }
        /// <summary>
        /// 获取接口
        /// </summary>
        /// <returns></returns>
        public override Type[] GetInterfaces()
        {
            return innerType.GetInterfaces();
        }
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return innerType.GetMembers(bindingAttr);
        }
        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return innerType.GetMethods(bindingAttr);
        }
        /// <summary>
        /// 获取嵌套类型
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return innerType.GetNestedType(name, bindingAttr);
        }
        /// <summary>
        /// 获取嵌套类型
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return innerType.GetNestedTypes(bindingAttr);
        }
        /// <summary>
        /// 获取属性数组
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return innerType.GetProperties(bindingAttr);
        }
        /// <summary>
        /// 调用成员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="invokeAttr"></param>
        /// <param name="binder"></param>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <param name="modifiers"></param>
        /// <param name="culture"></param>
        /// <param name="namedParameters"></param>
        /// <returns></returns>
        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
        {
            return innerType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
        }
        /// <summary>
        /// 已定义
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return innerType.IsDefined(attributeType, inherit);
        }
        /// <summary>
        /// 获取注解标记实现
        /// </summary>
        /// <returns></returns>
        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            return innerType.Attributes;
        }
        /// <summary>
        /// 获取构造方法实现
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="callConvention"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            return innerType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
        }
        /// <summary>
        /// 获取方法实现
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="callConvention"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            return innerType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
        }
        /// <summary>
        /// 获取属性实现
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            return innerType.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
        }
        /// <summary>
        /// 有元素类型实现
        /// </summary>
        /// <returns></returns>
        protected override bool HasElementTypeImpl()
        {
            return innerType.HasElementType;
        }
        /// <summary>
        /// 是数组实现
        /// </summary>
        /// <returns></returns>
        protected override bool IsArrayImpl()
        {
            return innerType.IsArray;
        }
        /// <summary>
        /// 是引用实现
        /// </summary>
        /// <returns></returns>
        protected override bool IsByRefImpl()
        {
            return innerType.IsByRef;
        }
        /// <summary>
        /// 是COM对象实现
        /// </summary>
        /// <returns></returns>
        protected override bool IsCOMObjectImpl()
        {
            return innerType.IsCOMObject;
        }
        /// <summary>
        /// 是指向实现
        /// </summary>
        /// <returns></returns>
        protected override bool IsPointerImpl()
        {
            return innerType.IsPointer;
        }
        /// <summary>
        /// 非原始的实现
        /// </summary>
        /// <returns></returns>
        protected override bool IsPrimitiveImpl()
        {
            return innerType.IsPrimitive;
        }
        /// <summary>
        /// 获取定义方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal MethodInfo GetDeclaredMethod(string name)
        {
            return innerType.GetMethod(name);
        }
    }
#endif
#if NET45
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="user_data"></param>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public delegate int strdelegate_collation(object user_data, string s1, string s2);
#endif
}
