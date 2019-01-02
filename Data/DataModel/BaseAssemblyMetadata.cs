using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Data.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseAssemblyMetadata 
    {
        [DataMember]
        public virtual string Name { get; set; }
        public virtual List<BaseNamespaceMetadata> Namespaces { get; set; }

    }
}