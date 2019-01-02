using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Data.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseNamespaceMetadata
    {
        [DataMember]
        public virtual string Name { get; set; }

        public virtual List<BaseTypeMetadata> Types { get; set; }
    }
}