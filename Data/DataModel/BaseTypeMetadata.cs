using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Data.DataModel
{
   
    public abstract class BaseTypeMetadata
    {
        #region Properties

        
        public virtual string Name { get; set; }
        
        public virtual string NamespaceName { get; set; }

        public virtual BaseTypeMetadata BaseType { get; set; }

        public virtual List<BaseTypeMetadata> GenericArguments { get; set; }

        public virtual TypeModifiers Modifiers { get; set; }
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