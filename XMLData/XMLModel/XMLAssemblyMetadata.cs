using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [Export(typeof(BaseAssemblyMetadata))]
    public class XMLAssemblyMetadata : BaseAssemblyMetadata
    {
        
        public override string Name { get; set; }
        
        public new List<XMLNamespaceMetadata> Namespaces { get; set; }

    }
}