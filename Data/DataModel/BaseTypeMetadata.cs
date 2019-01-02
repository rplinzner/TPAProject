using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Data.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseTypeMetadata
    {
        #region Properties

        [DataMember]
        public virtual bool IsAnalyzed { get; set; } //???
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string NamespaceName { get; set; }

        public virtual BaseTypeMetadata BaseType { get; set; }

        public virtual List<BaseTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public virtual TypeEnum Type { get; set; }

        public virtual List<BaseTypeMetadata> ImplementedInterfaces { get; set; }

        public virtual List<BaseTypeMetadata> NestedTypes { get; set; }

        public virtual List<BasePropertyMetadata> Properties { get; set; }

        public virtual BaseTypeMetadata DeclaringType { get; set; }

        public virtual List<BaseMethodMetadata> Methods { get; set; }

        public virtual List<BaseMethodMetadata> Constructors { get; set; }

        public virtual List<BaseParameterMetadata> Fields { get; set; }

        #endregion

    }
}