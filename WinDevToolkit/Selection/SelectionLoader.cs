using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;

namespace WinDevToolkit.Selection
{
    public abstract class SelectionLoader<T> : AsyncLoader
    {
        public ObservableCollection<SelectionItem<T>> SelectionItems { get; private set; }
        private SelectionItem<T> _currentSelection;
        private bool _isSelectionShown;

        protected SelectionLoader(IEnumerable<SelectionItem<T>> selectionItems)
        {
            SelectionItems = new ObservableCollection<SelectionItem<T>>(selectionItems);
            // default option set to 0
            Select(SelectionItems[0]);

            HardwareButtons.BackPressed += (sender, args) =>
            {
                if (IsSelectionShown)
                {
                    IsSelectionShown = false;
                    args.Handled = true;
                }
            };
        }

        protected void Select(SelectionItem<T> selectedItem)
        {
            foreach (var si in SelectionItems)
            {
                si.IsSelected = false;
            }
            selectedItem.IsSelected = true;
            CurrentSelection = selectedItem;
        }

        public async Task SelectAsync(SelectionItem<T> selectedItem)
        {
            if (selectedItem.Equals(_currentSelection))
            {
                return;
            }
            IsSelectionShown = false;
            // if selection available
            if (SelectionItems.Any(i => i.Key.Equals(selectedItem.Key)))
            {
                Select(selectedItem);

                // reload
                await ReloadAsync();
            }
        }

        public SelectionItem<T> CurrentSelection
        {
            get { return _currentSelection; }
            private set
            {
                if (_currentSelection != value)
                {
                    _currentSelection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsSelectionShown
        {
            get { return _isSelectionShown; }
            set
            {
                if (_isSelectionShown != value)
                {
                    _isSelectionShown = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
