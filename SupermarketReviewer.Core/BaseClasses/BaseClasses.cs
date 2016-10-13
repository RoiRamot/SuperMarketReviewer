using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SupermarketReviewer.Core.BaseClasses
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
    class WindowService 
    {
        public void ShowWindow(object viewModel)
        {
            var win = new Window {Content = viewModel};
            win.Show();
        }
    }
}
