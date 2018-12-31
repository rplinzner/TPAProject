using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModel;

namespace Data
{
    public interface ISerializer
    {
        void Serialize(string path, BaseAssemblyMetadata obj);
        BaseAssemblyMetadata Deserialize(string path);
    }
}
