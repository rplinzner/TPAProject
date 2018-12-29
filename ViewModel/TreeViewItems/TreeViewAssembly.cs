using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace ViewModel.TreeViewItems
{
    public class TreeViewAssembly : TreeViewItem
    {
        public List<NamespaceMetadata> Namespaces { get; private set; }

        public TreeViewAssembly(AssemblyMetadata Assembly) : base(Assembly.Name)
        {
            Namespaces = Assembly.Namespaces;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Namespaces == null) return;
            foreach (NamespaceMetadata metadata in Namespaces)
            {
                children.Add(new TreeViewNamespace(metadata));
            }
        }
    }
}