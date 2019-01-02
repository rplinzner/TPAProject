using System.Collections.ObjectModel;
using ViewModel.TreeViewItems;

namespace ConsoleApplication.View
{
    class TreeViewCmd
    {
        public ObservableCollection<TreeViewItemCmd> Data { get; set; }

        public TreeViewCmd(ObservableCollection<TreeViewItemCmd> data)
        {
            Data = data;
        }

        public void Expand(int index)
        {
            TreeViewItemCmd item = Data[index];
            if (!item.IsExpanded)
            {
                int i = 1;
                ObservableCollection<TreeViewItem> items = item.Expand();
                foreach (TreeViewItem treeViewItem in items)
                {
                    Data.Insert(index + i, new TreeViewItemCmd(treeViewItem, item.Id + 1));
                    i++;
                }
            }
            else
            {
                for (int i = item.TreeItem.Children.Count; i > 0; i--)
                {
                    if (Data[index + i].IsExpanded)
                    {
                        Expand(index + i);
                        Data.RemoveAt(index + i);
                    }
                    else
                    {
                        Data.RemoveAt(index + i);
                    }
                }
                item.IsExpanded = false;
            }
        }
    }
}
