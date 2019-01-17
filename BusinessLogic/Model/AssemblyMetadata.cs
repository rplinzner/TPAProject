using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{

    public class AssemblyMetadata 
    {
        public string Name { get; set; }
        public List<NamespaceMetadata> Namespaces { get; set; }

        public AssemblyMetadata() { }

        public AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            Namespaces = types.GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceMetadata(t.Key, t.ToList())).ToList();
        }

    }
}