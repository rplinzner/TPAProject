using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    public class XMLTypeMetadata : BaseTypeMetadata
    {
        #region Properties

        public override string Name { get; set; }
        public override string NamespaceName { get; set; }
        public new XMLTypeMetadata BaseType { get; set; }
        public new List<XMLTypeMetadata> GenericArguments { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public override TypeEnum Type { get; set; }
        public new List<XMLTypeMetadata> ImplementedInterfaces { get; set; }
        public new List<XMLTypeMetadata> NestedTypes { get; set; }
        public new List<XMLPropertyMetadata> Properties { get; set; }
        public new XMLTypeMetadata DeclaringType { get; set; }
        public new List<XMLMethodMetadata> Methods { get; set; }
        public new List<XMLMethodMetadata> Constructors { get; set; }
        public new List<XMLParameterMetadata> Fields { get; set; }

        #endregion

    }
}