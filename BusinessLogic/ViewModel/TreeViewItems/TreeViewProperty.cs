using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TreeViewProperty :TreeViewItem
    {
        private PropertyMetadata PropertyData { get; set; }
        public TreeViewProperty(PropertyMetadata data) : base(data.Name)
        {
            PropertyData = data;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (PropertyData.Type != null)
            {
                //children.Add(new TreeViewType(TypeMetadata.TypeDictionary[PropertyData.Type.Name]));

            }
        }
    }
}