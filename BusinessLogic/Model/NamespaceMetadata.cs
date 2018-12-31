using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    public class NamespaceMetadata
    {
        public string Name { get; set; }
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata() { }

        public NamespaceMetadata(string name, List<Type> types)
        {
            Name = name;
            this.Types = (from type in types orderby type.Name select TypeMetadata.EmitType(type)).ToList();
        }
    }
}