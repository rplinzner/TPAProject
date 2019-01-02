using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using System.IO;
using Data;
using Data.DataModel;
using Newtonsoft.Json;
using XMLData.XMLModel;

namespace XMLData
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssemblyMetadata obj)
        {
            XMLAssemblyMetadata xmlMetadata = (XMLAssemblyMetadata)obj;
            string name = JsonConvert.SerializeObject(xmlMetadata, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.Write(name);
            }
        }

        public BaseAssemblyMetadata Deserialize (string path)
        {
            using (StreamReader file = new StreamReader(path, true))
            {
                string reader = file.ReadToEnd();
                return JsonConvert.DeserializeObject<XMLAssemblyMetadata>(reader,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
        }

    }
}
