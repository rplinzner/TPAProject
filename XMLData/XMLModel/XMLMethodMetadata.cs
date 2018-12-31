using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLMethodMetadata : BaseMethodMetadata
    {
        #region Properties

        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new List<XMLTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        [DataMember]
        public new XMLTypeMetadata ReturnType { get; set; }
        [DataMember]
        public override bool Extension { get; set; }
        [DataMember]
        public new List<XMLParameterMetadata> Parameters { get; set; }
        #endregion
    }
}