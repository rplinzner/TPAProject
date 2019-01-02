using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [DataContract(IsReference = true)]
    [Export(typeof(BaseAssemblyMetadata))]
    public class XMLAssemblyMetadata : BaseAssemblyMetadata
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new List<XMLNamespaceMetadata> Namespaces { get; set; }

    }
}