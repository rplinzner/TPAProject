using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Data.DataModel
{
    public abstract class BaseMethodMetadata
    {
        #region Properties

        [DataMember]
        public virtual string Name { get; set; }

        public virtual List<BaseTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual MethodModifiers Modifiers { get; set; }

        public virtual BaseTypeMetadata ReturnType { get; set; }
        [DataMember]
        public virtual bool Extension { get; set; }

        public virtual List<BaseParameterMetadata> Parameters { get; set; }
        #endregion
    }
}