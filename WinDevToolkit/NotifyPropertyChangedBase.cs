using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinDevToolkit
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChangedImpl(string propertyName)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            NotifyPropertyChangedImpl(propertyName);
        }
    }
}
