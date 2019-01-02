using System.Collections.ObjectModel;
using ViewModel.TreeViewItems;

namespace ConsoleApplication.View
{
    public class TreeViewItemCmd
    {
        public TreeViewItem TreeItem { get; set; }
        public bool IsExpanded { get; set; }
        public int Id { get; set; }

        public TreeViewItemCmd(TreeViewItem treeItem, int id)
        {
            TreeItem = treeItem;
            IsExpanded = false;
            Id = id;
        }

        public ObservableCollection<TreeViewItem> Expand()
        {
            IsExpanded = true;
            TreeItem.IsExpanded = true;
            return TreeItem.Children;
        }
    }
}
