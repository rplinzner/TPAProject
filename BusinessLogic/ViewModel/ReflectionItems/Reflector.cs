using System.Reflection;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.ReflectionItems
{
    public class Reflector
    {
        public AssemblyMetadata AssemblyModel { get; private set; }
        public Reflector(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            AssemblyModel = new AssemblyMetadata(assembly);
        }
        public Reflector(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }
    }
}