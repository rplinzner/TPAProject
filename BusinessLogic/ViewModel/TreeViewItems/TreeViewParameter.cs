using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TreeViewParameter : TreeViewItem
    {
        public ParameterMetadata ParameterData { get; set; }

        public TreeViewParameter(ParameterMetadata parameterMetadata) :base(parameterMetadata.Name)
        {
            ParameterData = parameterMetadata;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterData.Type == null) return;
            children.Add(new TreeViewType(TypeMetadata.TypeDictionary[ParameterData.Type.Name]));
        }
    }
}