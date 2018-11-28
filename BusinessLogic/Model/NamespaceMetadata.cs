using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata() { }

        public NamespaceMetadata(string name, List<Type> types)
        {
            Name = name;
            this.Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
        }
    }
}