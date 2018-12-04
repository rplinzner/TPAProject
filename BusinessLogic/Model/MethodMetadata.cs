using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        [DataMember]
        public TypeMetadata ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public List<ParameterMetadata> Parameters { get; set; }
        #endregion

        public MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
        }

        private List<TypeMetadata> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new TypeMetadata(t)).ToList();
        }

        private bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = method.IsPublic ? AccessLevel.Public :
                method.IsFamily ? AccessLevel.Protected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.Private;

            AbstractEnum _abstract = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum _static = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum _virtual = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, _abstract, _static, _virtual);
        }
        public static List<MethodMetadata> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodMetadata(t)).ToList();
        }

        private List<ParameterMetadata> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterMetadata(t.Name, TypeMetadata.EmitReference(t.ParameterType))).ToList();
        }

        private TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }

        public static List<MethodMetadata> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodMetadata(t)).ToList();
        }
    }
}