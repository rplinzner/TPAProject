using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLParameterMetadata : BaseParameterMetadata
    {
        [DataMember]
        public override string Name { get; set; }
        [DataMember]
        public new XMLTypeMetadata Type { get; set; }

    }
}