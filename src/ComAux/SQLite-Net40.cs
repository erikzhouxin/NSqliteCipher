//
// Copyright (c) 2009-2019 Krueger Systems, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
#if NET40
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// ί��
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
    /// ������Ϣ
    /// </summary>
    public class TypeInfo : Type
    {
        private readonly Type innerType;
        /// <summary>
        /// �Զ�������
        /// </summary>
        public Attribute[] CustomAttributes { get; }
        /// <summary>
        /// ����
        /// </summary>
        public Type[] GenericTypeArguments { get; }
        /// <summary>
        /// ��������
        /// </summary>
        public PropertyInfo[] DeclaredProperties { get; }
        /// <summary>
        /// ����
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
        /// Ψһ��ʶ
        /// </summary>
        public override Guid GUID { get; }
        /// <summary>
        /// ģ��
        /// </summary>
        public override Module Module => innerType.Module;
        /// <summary>
        /// ����
        /// </summary>
        public override Assembly Assembly => innerType.Assembly;
        /// <summary>
        /// ȫ��
        /// </summary>
        public override string FullName => innerType.FullName;
        /// <summary>
        /// �����ռ�
        /// </summary>
        public override string Namespace => innerType.Namespace;
        /// <summary>
        /// ��������
        /// </summary>
        public override string AssemblyQualifiedName => innerType.AssemblyQualifiedName;
        /// <summary>
        /// ������
        /// </summary>
        public override Type BaseType => innerType.BaseType;
        /// <summary>
        /// ϵͳ����
        /// </summary>
        public override Type UnderlyingSystemType => innerType.UnderlyingSystemType;
        /// <summary>
        /// ����
        /// </summary>
        public override string Name => innerType.Name;
        /// <summary>
        /// ��ȡ���췽��
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return innerType.GetConstructors(bindingAttr);
        }
        /// <summary>
        /// ��ȡ�Զ���ע��
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override object[] GetCustomAttributes(bool inherit)
        {
            return innerType.GetCustomAttributes(inherit);
        }
        /// <summary>
        /// ��ȡ�Զ���ע��
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return innerType.GetCustomAttributes(attributeType, inherit);
        }
        /// <summary>
        /// ��ȡԪ������
        /// </summary>
        /// <returns></returns>
        public override Type GetElementType()
        {
            return innerType.GetElementType();
        }
        /// <summary>
        /// ��ȡ�¼�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return innerType.GetEvent(name, bindingAttr);
        }
        /// <summary>
        /// ��ȡ�¼�
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return innerType.GetEvents(bindingAttr);
        }
        /// <summary>
        /// ��ȡ�ֶ�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return innerType.GetField(name, bindingAttr);
        }
        /// <summary>
        /// ��ȡ�ֶ�
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return innerType.GetFields(bindingAttr);
        }
        /// <summary>
        /// ��ȡ�ӿ�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public override Type GetInterface(string name, bool ignoreCase)
        {
            return innerType.GetInterface(name, ignoreCase);
        }
        /// <summary>
        /// ��ȡ�ӿ�
        /// </summary>
        /// <returns></returns>
        public override Type[] GetInterfaces()
        {
            return innerType.GetInterfaces();
        }
        /// <summary>
        /// ��ȡ��Ա
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return innerType.GetMembers(bindingAttr);
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return innerType.GetMethods(bindingAttr);
        }
        /// <summary>
        /// ��ȡǶ������
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return innerType.GetNestedType(name, bindingAttr);
        }
        /// <summary>
        /// ��ȡǶ������
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return innerType.GetNestedTypes(bindingAttr);
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return innerType.GetProperties(bindingAttr);
        }
        /// <summary>
        /// ���ó�Ա
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
        /// �Ѷ���
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return innerType.IsDefined(attributeType, inherit);
        }
        /// <summary>
        /// ��ȡע����ʵ��
        /// </summary>
        /// <returns></returns>
        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            return innerType.Attributes;
        }
        /// <summary>
        /// ��ȡ���췽��ʵ��
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
        /// ��ȡ����ʵ��
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
        /// ��ȡ����ʵ��
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
        /// ��Ԫ������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool HasElementTypeImpl()
        {
            return innerType.HasElementType;
        }
        /// <summary>
        /// ������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool IsArrayImpl()
        {
            return innerType.IsArray;
        }
        /// <summary>
        /// ������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool IsByRefImpl()
        {
            return innerType.IsByRef;
        }
        /// <summary>
        /// ��COM����ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool IsCOMObjectImpl()
        {
            return innerType.IsCOMObject;
        }
        /// <summary>
        /// ��ָ��ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool IsPointerImpl()
        {
            return innerType.IsPointer;
        }
        /// <summary>
        /// ��ԭʼ��ʵ��
        /// </summary>
        /// <returns></returns>
        protected override bool IsPrimitiveImpl()
        {
            return innerType.IsPrimitive;
        }
        /// <summary>
        /// ��ȡ���巽��
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal MethodInfo GetDeclaredMethod(string name)
        {
            return innerType.GetMethod(name);
        }
    }
}
#endif