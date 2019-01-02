using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Runtime.CompilerServices;

namespace Composition
{
    public sealed class Compose
    {
        private static Compose _instance = new Compose();

        public static Compose Instance
        {
            get { return _instance; }
        }

        private List<DirectoryCatalog> _extensionCatalogs = new List<DirectoryCatalog>();
        private AggregateCatalog _catalog;
        public CompositionContainer _container;

        public void AddCatalog(DirectoryCatalog obj)
        {
            _extensionCatalogs.Add(obj);
        }

        public void CreateCompositionContainer()
        {
            _catalog = new AggregateCatalog(_extensionCatalogs);
            _container = new CompositionContainer(_catalog);
        }

        public void ComposeParts(object obj)
        {
            _container.ComposeParts(obj);
        }



    }
}
