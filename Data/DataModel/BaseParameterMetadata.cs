using System.Runtime.Serialization;

namespace Data.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseParameterMetadata
    {
        [DataMember]
        public virtual string Name { get; set; }
        public virtual BaseTypeMetadata Type { get; set; }

    }
}