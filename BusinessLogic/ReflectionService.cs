using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLogic.Mapper;
using BusinessLogic.Model;
using Data;
using Data.DataModel;

namespace BusinessLogic
{
    public class ReflectionService
    {
        public IEnumerable<BaseAssemblyMetadata> container =
            Composition.Compose.Instance._container.GetExportedValues<BaseAssemblyMetadata>();

        public IEnumerable<ISerializer> containerser =
            Composition.Compose.Instance._container.GetExportedValues<ISerializer>();

        public ISerializer Serializer;

        public ReflectionService()
        {
            Serializer = containerser.First();
        }

        public void Save(string path, AssemblyMetadata metadata)
        {
            
            BaseAssemblyMetadata Assembly = container.First();
            Serializer.Serialize(path, AssemblyModelMapper.MapDown(metadata, Assembly));
            //Serializer.Serialize(path, AssemblyModelMapper.MapDown(metadata, Assembly.GetType()));
        }

        public AssemblyMetadata Load(string path)
        {
            //Serializer = containerser.First();
            return AssemblyModelMapper.MapUp(Serializer.Deserialize(path));
        }


    }
}