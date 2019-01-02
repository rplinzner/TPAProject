using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    public class XMLNamespaceMetadata : BaseNamespaceMetadata
    {
        public override string Name { get; set; }
        public new List<XMLTypeMetadata> Types { get; set; }
    }
}