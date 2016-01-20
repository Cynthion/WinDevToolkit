using System;
using System.Windows.Input;
using WinDevToolkit.Interfaces;

namespace WinDevToolkit
{
    public class ChangeViewInfoCommand : ICommand
    {
        private readonly IViewInfoProvider _viewInfoProvider;

        public ChangeViewInfoCommand(IViewInfoProvider viewInfoProvider)
        {
            _viewInfoProvider = viewInfoProvider;
        }

        public bool CanExecute(object parameter)
        {
            var icvi = parameter as ItemsControlViewInfo;
            return _viewInfoProvider != null && icvi != null;
        }

        public void Execute(object parameter)
        {
            var icvi = parameter as ItemsControlViewInfo;
            if (_viewInfoProvider != null && icvi != null)
            {
                _viewInfoProvider.ItemsControlViewInfo = icvi;
            }
        }

        // TODO implement usage
        public event EventHandler CanExecuteChanged;
    }
}
