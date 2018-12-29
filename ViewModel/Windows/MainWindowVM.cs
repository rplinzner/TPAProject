using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using ViewModel.TreeViewItems;
using ViewModel.BaseItems;
using BusinessLogic.ReflectionItems;
using BusinessLogic.Model;
using Composition;
using Interfaces;
using Logging;
using Serialization;

namespace ViewModel.Windows
{
    public class MainWindowVM : BaseVM
    {

        //TODO: zrobić osobny serwis
        public ISerializer Serializer = new XMLSerializer();
        
        
        #region MEF
        [Import(typeof(IPathFinder))]
        public IPathFinder PathFinder { get; set; }
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        [Import(typeof(IShowInfo))]
        public IShowInfo ShowInfo { get; set; }
        #endregion

        public string PathForSerialization { get; set; }

        private string _pathVariable;

        #region Commands

        public ICommand ClickOpen { get; }
        public ICommand ClickSave { get; }
        

        #endregion
        
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
            //Logger.Log(new MessageStructure("Loading Path"));
            PathVariable = PathFinder.FindPath();
            if (PathVariable == null)
            {
                Logger.Log(new MessageStructure("Path Loading Failed"), LogLevelEnum.Error);
                return;
            }
            //Logger.Log(new MessageStructure("Path Loading Succeeded"), LogLevelEnum.Success);

            if (PathVariable.EndsWith(".dll"))
            {
                try
                {
                    _reflector = new Reflector(PathVariable);
                    Logger.Log(new MessageStructure("Reflection has started"));
                    ShowInfo.Show("SUCCESS");
                }
                catch (Exception e)
                {
                    Logger.Log(new MessageStructure("Reflection Error: " + e.Message), LogLevelEnum.Error);
                    ShowInfo.Show("FAIL");
                }
                _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
                Logger.Log(new MessageStructure("Showing tree view"));
                ShowTreeView();
            }

            else if (PathVariable.EndsWith(".xml"))
            {
                try
                {
                    Logger.Log(new MessageStructure("Deserialization has started"));
                    _reflector = new Reflector(Serializer.Deserialize<AssemblyMetadata>(PathVariable));
                }
                catch (Exception e)
                {
                    Logger.Log(new MessageStructure("Deserialization error:" + e.Message), LogLevelEnum.Error);
                }

                Logger.Log(new MessageStructure("Deserialization success"), LogLevelEnum.Success);

                _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
                Logger.Log(new MessageStructure("Showing tree view"));
                ShowTreeView();
            }
        }

        private void Save()
        {
            Logger.Log(new MessageStructure("Serialization has started"));
            PathForSerialization = PathFinder.SaveToPath();
            if (PathForSerialization == null)
            {
                Logger.Log(new MessageStructure("Serialization failed - Path is null"), LogLevelEnum.Error);
            }
            else
            {
                try
                {
                    Serializer.Serialize(PathForSerialization, _reflector.AssemblyModel);
                    Logger.Log(new MessageStructure("Serializization completed"), LogLevelEnum.Success);
                }
                catch (Exception e)
                {
                    Logger.Log(new MessageStructure("Serialization error:" + e.Message), LogLevelEnum.Error);
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