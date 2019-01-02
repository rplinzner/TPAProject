using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using System.IO;
using Data;
using Data.DataModel;
using XMLData.XMLModel;

namespace XMLData
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssemblyMetadata obj)
        {
            XMLAssemblyMetadata xmlMetadata = (XMLAssemblyMetadata)obj;
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(XMLAssemblyMetadata));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, xmlMetadata);
            }
        }

        public BaseAssemblyMetadata Deserialize (string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssemblyMetadata));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (XMLAssemblyMetadata)dataContractSerializer.ReadObject(fileStream);
            }
        }

    }
}
