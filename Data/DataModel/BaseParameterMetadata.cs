using System.Runtime.Serialization;

namespace Data.DataModel
{
    
    public abstract class BaseParameterMetadata
    {
        
        public virtual string Name { get; set; }
        public virtual BaseTypeMetadata Type { get; set; }

    }
}