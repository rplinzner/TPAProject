﻿using BusinessLogic.Logger;
using BusinessLogic.Logger.Enum;
using BusinessLogic.Logger.Interface;
using BusinessLogic.ViewModel;
using System.Collections.Generic;
using System.Windows;
using WpfApplication.WpfHelper;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM()
            {
                //TODO: Create Pathfinder for WPF
                PathFinder = new WpfPathFinder(),
                LogFactory = new BaseLogFactory(new List<ILogger>
                {
                    new FileLogger("log.txt")
                }, LogOutputLevelEnum.Debug)
            };
        }
    }
}
