using System;
using System.Threading.Tasks;
using WinDevToolkit.Interfaces;

namespace WinDevToolkit
{
    public struct LoadResult
    {
        public static LoadResult Success = new LoadResult(null);

        private readonly string _message;

        public LoadResult(string message = null) : this()
        {
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }
    }

    public abstract class AsyncLoader : NotifyPropertyChangedBase, IAsyncLoader
    {
        private string _statusText;
        private bool _hasStatusText;
        private bool _isLoading;
        private bool _isLoaded;

        public async Task LoadAsync()
        {
            if (!_isLoaded)
            {
                try
                {
                    IsLoading = true;
                    StatusText = null;

                    var res = await TaskHelper.HandleTaskObservedAsync(DoLoadAsync());
                    StatusText = res.GetMessage();
                    IsLoaded = true;
                }
                catch (Exception)
                {
                    StatusText = "Oops, something went wrong...";
                    IsLoaded = false;
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        public async Task ReloadAsync()
        {
            _isLoaded = false;
            await LoadAsync();
        }

        protected abstract Task<LoadResult> DoLoadAsync();

        public string StatusText
        {
            get { return _statusText; }
            private set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    HasStatusText = _statusText != null;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool HasStatusText
        {
            get { return _hasStatusText; }
            private set
            {
                if (_hasStatusText != value)
                {
                    _hasStatusText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
