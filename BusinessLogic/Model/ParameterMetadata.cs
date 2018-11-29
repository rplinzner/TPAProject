using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata Type { get; set; }

        public ParameterMetadata(string name, TypeMetadata type)
        {
            Name = name;
            Type = type;
        }
    }
}