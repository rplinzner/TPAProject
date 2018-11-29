using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public interface ISerializer
    {
        void Serialize<T>(string path, T obj);
        T Deserialize<T>(string path);
    }
}
