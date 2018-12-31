using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLNamespaceMetadata : BaseNamespaceMetadata
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new List<XMLTypeMetadata> Types { get; set; }
    }
}