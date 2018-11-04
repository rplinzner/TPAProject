using System;
using System.Collections.ObjectModel;
using BusinessLogic.ViewModel.ReflectionItems;
using System.Windows.Input;
using BusinessLogic.ViewModel.TreeViewItems;

namespace BusinessLogic.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        #region Props and fields
        private string _pathVariable;
        public ICommand ClickOpen { get; }
        public IPathFinder PathFinder { get; set; }
        private Reflector _reflector;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private TreeViewAssembly _treeViewAssembly;
        //TODO: Add assembly tree item
        public string PathVariable
        {
            get => _pathVariable;
            set
            {
                _pathVariable = value;
                OnPropertyChanged(nameof(PathVariable));
            }
        }
        #endregion

        #region ctor
        public MainWindowVM()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            ClickOpen = new RelayCommand(Open);

        }
        #endregion
        private void Open()
        {
            PathVariable = PathFinder.FindPath();
            if (PathVariable == null) return;
            try
            {
                _reflector = new Reflector(PathVariable);
            }
            catch (Exception e)
            {
                //TODO: Implement logger
            }
            _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
            Console.Out.WriteLine("typ");
            Console.Out.WriteLine(_treeViewAssembly.GetType());

            ShowTreeView();
        }

        private void ShowTreeView()
        {
            TreeViewItem rootItem = _treeViewAssembly;
            HierarchicalAreas.Add(rootItem);

        }
    }
}