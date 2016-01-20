using System.Globalization;

namespace WinDevToolkit.WindowsStore
{
    public class PurchaseItem : NotifyPropertyChangedBase
    {
        public string Id { get; private set; }
        public double Price { get; private set; }

        private bool _isPurchased;

        public PurchaseItem(string id, double price)
        {
            Id = id;
            Price = price;
        }

        public bool IsPurchased
        {
            get { return _isPurchased; }
            set
            {
                if (_isPurchased != value)
                {
                    _isPurchased = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return Price.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
