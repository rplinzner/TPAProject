using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLTypeMetadata : BaseTypeMetadata
    {
        #region Properties

        [DataMember]
        public override bool IsAnalyzed { get; set; } //???
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public override string NamespaceName { get; set; }
        [DataMember]
        public new XMLTypeMetadata BaseType { get; set; }
        [DataMember]
        public new List<XMLTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public override Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public override TypeEnum Type { get; set; }
        [DataMember]
        public new List<XMLTypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public new List<XMLTypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public new List<XMLPropertyMetadata> Properties { get; set; }
        [DataMember]
        public new XMLTypeMetadata DeclaringType { get; set; }
        [DataMember]
        public new List<XMLMethodMetadata> Methods { get; set; }
        [DataMember]
        public new List<XMLMethodMetadata> Constructors { get; set; }
        [DataMember]
        public new List<XMLParameterMetadata> Fields { get; set; }

        #endregion

    }
}