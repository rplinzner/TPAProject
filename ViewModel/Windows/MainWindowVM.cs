using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using BusinessLogic;
using BusinessLogic.ReflectionItems;
using Interfaces;
using Logging;
using ViewModel.BaseItems;
using ViewModel.TreeViewItems;

namespace ViewModel.Windows
{
    public class MainWindowVM : BaseVM
    {
        #region MEF
        [Import(typeof(IPathFinder))]
        public IPathFinder PathFinder { get; set; }
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        [Import(typeof(IShowInfo))]
        public IShowInfo ShowInfo { get; set; }
        public ReflectionService Service { get; set; } = new ReflectionService();
        #endregion

        public string PathForSerialization { get; set; }

        private string _pathVariable;
        private bool _isButtonJsonActive = true;
        private bool _isButtonDbActive = true;

        #region Commands

        public ICommand ClickOpen { get; }
        public ICommand ClickSave { get; }
        public ICommand ClickOpenDB { get; }
        public ICommand ClickSaveDB { get; }


        #endregion

        private Reflector _reflector;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private TreeViewAssembly _treeViewAssembly;

        public bool IsButtonJsonActive
        {
            get => _isButtonJsonActive;
            set
            {
                _isButtonJsonActive = value;
                OnPropertyChanged(nameof(IsButtonJsonActive));
            }
        }

        public string PathVariable
        {
            get => _pathVariable;
            set
            {
                _pathVariable = value;
                OnPropertyChanged(nameof(PathVariable));
            }
        }

        public bool IsButtonDbActive
        {
            get => _isButtonDbActive;
            set
            {
                _isButtonDbActive = value;
                OnPropertyChanged(nameof(IsButtonDbActive));
            }
        }


        #region ctor
        public MainWindowVM()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            ClickOpen = new RelayCommand(Open);
            ClickSave = new RelayCommand(Save);
            ClickOpenDB = new RelayCommand(OpenDB);
            ClickSaveDB = new RelayCommand(SaveDB);
            if (Service.Serializer.ToString().Contains("DB"))
            {
                IsButtonJsonActive = false;
            }
            else
            {
                IsButtonDbActive = false;
            }
        }




        #endregion
        private void OpenDB()
        {

            ShowInfo.Show("Deserialization from DB started. Press OK and wait for end result");
            Logger.Log(new MessageStructure("Deserialization from DB started..."));
            try
            {
                _reflector = new Reflector(Service.Load(""));
                Logger.Log(new MessageStructure("Reflection from DB has started"));
            }
            catch (Exception e)
            {
                Logger.Log(new MessageStructure("Reflection from DB Error: " + e.Message), LogLevelEnum.Error);
                ShowInfo.Show("Deserialization error, check log for more info");
            }
            _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
            Logger.Log(new MessageStructure("Showing tree view"));
            ShowTreeView();
        }


        private void Open()
        {
            Logger.Log(new MessageStructure("Loading Path"));
            PathVariable = PathFinder.FindPath();
            if (PathVariable == null)
            {
                Logger.Log(new MessageStructure("Path Loading Failed"), LogLevelEnum.Error);
                return;
            }
            Logger.Log(new MessageStructure("Path Loading Succeeded"), LogLevelEnum.Success);

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
                    ShowInfo.Show("Deserialization error, check log for more info");
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
                    _reflector = new Reflector(Service.Load(PathVariable));
                }
                catch (Exception e)
                {
                    Logger.Log(new MessageStructure("Deserialization error:" + e.Message), LogLevelEnum.Error);
                    ShowInfo.Show("Deserialization error, check log for more info");
                }

                Logger.Log(new MessageStructure("Deserialization success"), LogLevelEnum.Success);

                _treeViewAssembly = new TreeViewAssembly(_reflector.AssemblyModel);
                Logger.Log(new MessageStructure("Showing tree view"));
                ShowTreeView();
            }
        }

        private void SaveDB()
        {

            ShowInfo.Show("Serialization to DB has started. Press OK and wait for end result");
            Logger.Log(new MessageStructure("Serialization to DB has started"));
            try
            {
                Service.Save("", _reflector.AssemblyModel);
                Logger.Log(new MessageStructure("Serializization to DB completed"), LogLevelEnum.Success);
                ShowInfo.Show("Serialization to DB completed");
            }
            catch (Exception e)
            {
                Logger.Log(new MessageStructure("Serialization to DB error: " + e.Message), LogLevelEnum.Error);
                ShowInfo.Show("Serialization error, check log for more info");
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
                    Service.Save(PathForSerialization, _reflector.AssemblyModel);
                    Logger.Log(new MessageStructure("Serializization completed"), LogLevelEnum.Success);
                    ShowInfo.Show("Serialization completed");
                }
                catch (Exception e)
                {
                    Logger.Log(new MessageStructure("Serialization error: " + e.Message), LogLevelEnum.Error);
                    ShowInfo.Show("Serialization error, check log for more info");
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