using System.ComponentModel;

namespace WinDevToolkit.Interfaces
{
    public interface IViewInfoProvider : INotifyPropertyChanged
    {
        ItemsControlViewInfo ItemsControlViewInfo { get; set; }
    }
}
