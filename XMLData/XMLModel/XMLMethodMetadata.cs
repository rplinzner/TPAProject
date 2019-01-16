using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    public class XMLMethodMetadata : BaseMethodMetadata
    {
        #region Properties

        
        public override string Name { get; set; }
        
        public new List<XMLTypeMetadata> GenericArguments { get; set; }
        public override MethodModifiers Modifiers { get; set; }
        public new XMLTypeMetadata ReturnType { get; set; }
        public override bool Extension { get; set; }
        public new List<XMLParameterMetadata> Parameters { get; set; }
        #endregion
    }
}