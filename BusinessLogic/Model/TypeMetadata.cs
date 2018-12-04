using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class TypeMetadata
    {
        #region Properties

        private bool isAnalyzed;
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }
        [DataMember]
        public TypeMetadata BaseType { get; set; }
        [DataMember]
        public List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public TypeEnum Type { get; set; }
        [DataMember]
        public List<TypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public List<PropertyMetadata> Properties { get; set; }
        [DataMember]
        public TypeMetadata DeclaringType { get; set; }
        [DataMember]
        public List<MethodMetadata> Methods { get; set; }
        [DataMember]
        public List<MethodMetadata> Constructors { get; set; }
        [DataMember]
        public List<ParameterMetadata> Fields { get; set; }

        #endregion

        #region Constructors

        public TypeMetadata(Type type)
        {
            Name = type.Name;
            Modifiers = EmitModifiers(type);
        }

        private void Analyze(Type type)
        {
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodMetadata.EmitConstructors(type);
            Methods = MethodMetadata.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);      
            Properties = PropertyMetadata.EmitProperties(type);
            BaseType = EmitExtends(type.BaseType);
            Fields = EmitFields(type);
            Type = GetTypeEnum(type);
            isAnalyzed = true;
        }

        #endregion

        #region Methods

        public static TypeMetadata EmitType(Type type)
        {
            if (!DictionarySingleton.Instance.ContainsKey(type.Name))
            {
                DictionarySingleton.Instance.Add(type.Name, new TypeMetadata(type));
            }

            if (!DictionarySingleton.Instance.Get(type.Name).isAnalyzed)
            {
                DictionarySingleton.Instance.Get(type.Name).Analyze(type);
            }

            return DictionarySingleton.Instance.Get(type.Name);
        }
        public static TypeMetadata EmitReference(Type type)
        {
            if (!DictionarySingleton.Instance.ContainsKey(type.Name))
            {
                DictionarySingleton.Instance.Add(type.Name, new TypeMetadata(type));
            }

            return DictionarySingleton.Instance.Get(type.Name);
        }
        private TypeEnum GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeEnum.Enum :
                type.IsValueType ? TypeEnum.Struct :
                type.IsInterface ? TypeEnum.Interface :
                TypeEnum.Class;
        }

        public static List<TypeMetadata> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();
            return arguments.Select(EmitReference).ToList();
        }

        private List<ParameterMetadata> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
                                                       BindingFlags.Public |
                                                       BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach (FieldInfo field in fieldInfo)
            {
                parameters.Add(new ParameterMetadata(field.Name, EmitReference(field.FieldType)));
            }

            return parameters;
        }

        private TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }

        private Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }




        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }

        private List<TypeMetadata> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();

            return nestedTypes.Select(t => new TypeMetadata(t)).ToList();
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);

            #endregion
        }
    }
}