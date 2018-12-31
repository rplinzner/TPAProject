using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Data.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BasePropertyMetadata
    {
        #region Properties

        [DataMember]
        public virtual string Name { get; set; }

        public virtual BaseTypeMetadata Type { get; set; }
        #endregion

    }
}