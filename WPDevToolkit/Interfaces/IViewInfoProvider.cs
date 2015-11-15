using System.ComponentModel;

namespace WPDevToolkit.Interfaces
{
    public interface IViewInfoProvider : INotifyPropertyChanged
    {
        ItemsControlViewInfo ItemsControlViewInfo { get; set; }
    }
}
