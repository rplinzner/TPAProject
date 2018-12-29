using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.TreeViewItems;
using ViewModel.BaseItems;
using BusinessLogic.ReflectionItems;
using Interfaces;

namespace ViewModel.Windows
{
    public class MainWindowVM : BaseVM
    {
        #region Props and fields
        public ISerializer Serializer = new XMLSerializer();
        public string PathForSerialization { get; set; }

        private string _pathVariable;
        public ICommand ClickOpen { get; }
        public ICommand ClickSave { get; }
        public IPathFinder PathFinder { get; set; }
        public ILogFactory LogFactory { get; set; }


        private Reflector _reflector;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private TreeViewAssembly _treeViewAssembly;
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
            ClickSave = new RelayCommand(Save);

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

            if (PathVariable.EndsWith(".dll"))
            {
                try
                {
                    _reflector = new Reflector(PathVariable);
                    LogFactory.Log(new MessageStructure("Reflection has started"));
                }
                catch (Exception e)
                {
                    LogFactory.Log(new MessageStructure("Reflection Error: " + e.Message), LogLevelEnum.Error);
                }
                _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
                LogFactory.Log(new MessageStructure("Showing tree view"));
                ShowTreeView();
            }

            else if (PathVariable.EndsWith(".xml"))
            {
                try
                {
                    LogFactory.Log(new MessageStructure("Deserialization has started"));
                    _reflector = new Reflector(Serializer.Deserialize<AssemblyMetadata>(PathVariable));
                }
                catch (Exception e)
                {
                    LogFactory.Log(new MessageStructure("Deserialization error:" + e.Message), LogLevelEnum.Error);
                }

                LogFactory.Log(new MessageStructure("Deserialization success"), LogLevelEnum.Success);

                _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
                LogFactory.Log(new MessageStructure("Showing tree view"));
                ShowTreeView();
            }
        }

        private void Save()
        {
            LogFactory.Log(new MessageStructure("Serialization has started"));
            PathForSerialization = PathFinder.SaveToPath();
            if (PathForSerialization == null)
            {
                LogFactory.Log(new MessageStructure("Serialization failed - Path is null"), LogLevelEnum.Error);
            }
            else
            {
                try
                {
                    Serializer.Serialize(PathForSerialization, _reflector.AssemblyModel);
                    LogFactory.Log(new MessageStructure("Serializization completed"), LogLevelEnum.Success);
                }
                catch (Exception e)
                {
                    LogFactory.Log(new MessageStructure("Serialization error:" + e.Message), LogLevelEnum.Error);
                }


            }
        }

        private void ShowTreeView()
        {
            TreeViewItem rootItem = _treeViewAssembly;
            HierarchicalAreas.Add(rootItem);

        }
    }
}