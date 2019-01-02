using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    public class XMLPropertyMetadata : BasePropertyMetadata
    {
        #region Properties
        public override string Name { get; set; }
        public new XMLTypeMetadata Type { get; set; }
        #endregion

    }
}