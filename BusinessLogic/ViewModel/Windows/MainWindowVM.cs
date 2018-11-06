using System;
using System.Collections.ObjectModel;
using BusinessLogic.ViewModel.ReflectionItems;
using System.Windows.Input;
using BusinessLogic.Logger;
using BusinessLogic.Logger.Enum;
using BusinessLogic.Logger.Interface;
using BusinessLogic.ViewModel.TreeViewItems;

namespace BusinessLogic.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        #region Props and fields
        private string _pathVariable;
        public ICommand ClickOpen { get; }
        public IPathFinder PathFinder { get; set; }
        public ILogFactory LogFactory { get; set; }
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
            LogFactory.Log(new MessageStructure("Loading Path"));
            PathVariable = PathFinder.FindPath();
            if (PathVariable == null)
            {
                LogFactory.Log(new MessageStructure("Path Loading Failed"), LogLevelEnum.Error);
                return;
            }
                LogFactory.Log(new MessageStructure("Path Loading Succeeded"), LogLevelEnum.Success);
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