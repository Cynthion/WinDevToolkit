//using System.Threading.Tasks;
//using WPDevToolkit.Interfaces;

//namespace WPDevToolkit.ViewModel
//{
//    public class AsyncLoaderBaseViewModel : BaseViewModel, IAsyncLoader
//    {
//        private readonly AsyncLoader _asyncLoader;

//        public AsyncLoaderBaseViewModel(AsyncLoader asyncLoader)
//        {
//            _asyncLoader = asyncLoader;
//        }

//        // use AsyncLoader as a mixin class
//        public Task LoadAsync()
//        {
//            return _asyncLoader.LoadAsync();
//        }

//        public bool IsLoading { get; private set; }
//        public bool IsLoaded { get; private set; }
//        public bool HasStatusText { get; private set; }
//        public string StatusText { get; private set; }
//    }
//}
