using System.ComponentModel.Composition;
using BusinessLogic.Mapper;
using BusinessLogic.Model;
using Data;
using Data.DataModel;

namespace BusinessLogic
{
    [Export(typeof(ReflectionService))]
    public class ReflectionService
    {
        public BaseAssemblyMetadata Assembly = Composition.Compose.Instance._container.GetExportedValue<BaseAssemblyMetadata>();
        public ISerializer Serializer = Composition.Compose.Instance._container.GetExportedValue<ISerializer>();

        public void Save(string path, AssemblyMetadata metadata)
        {
            Serializer.Serialize(path, AssemblyModelMapper.MapDown(metadata, Assembly.GetType()));
        }

        public AssemblyMetadata Load(string path)
        {
            return AssemblyModelMapper.MapUp(Serializer.Deserialize(path));
        }


    }
}