using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public abstract class TreeViewItem
    {
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        private bool _wasBuilt;
        private bool _isExpanded;

        public TreeViewItem(string name)
        {
            Name = name;
            Children = new ObservableCollection<TreeViewItem>() { null };
            _wasBuilt = false;
        }
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();   //clear null elem
                Build(Children);
                _wasBuilt = true;
            }
        }
        public abstract void Build(ObservableCollection<TreeViewItem> children);
    }
}