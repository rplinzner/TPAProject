﻿using System;
using System.Windows.Input;

namespace BusinessLogic.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}