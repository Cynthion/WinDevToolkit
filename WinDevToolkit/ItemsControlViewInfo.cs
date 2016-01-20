using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinDevToolkit
{
    /// <summary>
    /// Contains the necessary information for an <see cref="ItemsControl"/> that changes view information during runtime.
    /// </summary>
    public class ItemsControlViewInfo : NotifyPropertyChangedBase
    {
        private int _id;
        private Style _style;
        private ItemsPanelTemplate _itemsPanelTemplate;
        private DataTemplate _itemTemplate;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Style Style
        {
            get { return _style; }
            set
            {
                if (_style != value)
                {
                    _style = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ItemsPanelTemplate ItemsPanelTemplate
        {
            get { return _itemsPanelTemplate; }
            set
            {
                if (_itemsPanelTemplate != value)
                {
                    _itemsPanelTemplate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DataTemplate ItemTemplate
        {
            get { return _itemTemplate; }
            set
            {
                if (_itemTemplate != value)
                {
                    _itemTemplate = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
