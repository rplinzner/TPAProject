using System.Runtime.Serialization;
using Data.DataModel;

namespace XMLData.XMLModel
{
    public class XMLParameterMetadata : BaseParameterMetadata
    {
        public override string Name { get; set; }
        public new XMLTypeMetadata Type { get; set; }

    }
}