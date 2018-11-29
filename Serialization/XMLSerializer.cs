using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.IO;

namespace Serialization
{
    public class XMLSerializer : ISerializer
    {
        public void Serialize<T>(string path, T obj)
        {
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(T));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, obj);
            }
        }

        public T Deserialize<T>(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (T)dataContractSerializer.ReadObject(fileStream);
            }
        }

    }
}
