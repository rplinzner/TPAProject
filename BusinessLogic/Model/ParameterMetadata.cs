using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract]
    public class ParameterMetadata
    {
        [DataMember(IsReference = true)]
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