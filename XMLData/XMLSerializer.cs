using System.Runtime.Serialization;
using System.IO;
using Data;
using Data.DataModel;

namespace XMLData
{
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssemblyMetadata obj)
        {
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(BaseAssemblyMetadata));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, obj);
            }
        }

        public BaseAssemblyMetadata Deserialize (string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(BaseAssemblyMetadata));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (BaseAssemblyMetadata)dataContractSerializer.ReadObject(fileStream);
            }
        }

    }
}
