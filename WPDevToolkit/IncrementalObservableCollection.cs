using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace WPDevToolkit
{
    // inspired by https://marcominerva.wordpress.com/2013/05/22/implementing-the-isupportincrementalloading-interface-in-a-window-store-app/

    public interface IIncrementalSource<T>
    {
        Task<IEnumerable<T>> GetPagedItems(int pageIndex, int pageSize);

        bool IsIncrementalLoading { get; }
    }

    public sealed class IncrementalObservableCollection<TSource, T> : ObservableRangeCollection<T>, ISupportIncrementalLoading
        where TSource : IIncrementalSource<T>, new()
    {
        private readonly TSource _source;
        private readonly int _itemsPerPage;
        private int _currentPage; // = 1; // start index 1

        public IncrementalObservableCollection(TSource source, int itemsPerPage = 10)
        {
            _source = source;
            _itemsPerPage = itemsPerPage;

            CollectionChanged += (sender, args) =>
            {
                if (Count == 0)
                {
                    Reset();
                }
            };
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count = 0)
        {
            HasMoreItems = true; // enable the first time
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

            return Task.Run(async () =>
            {
                uint resCount = 0;
                var result = await _source.GetPagedItems(_currentPage++, _itemsPerPage);
                var resultArray = result as T[] ?? result.ToArray();
                if (result == null || !resultArray.Any())
                {
                    HasMoreItems = false;
                }
                else
                {
                    resCount = (uint)resultArray.Length;

                    // update UI
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        AddRange(resultArray);
                    });
                }
                return new LoadMoreItemsResult { Count = resCount };
            }).AsAsyncOperation();
        }

        public void Reset()
        {
            _currentPage = 0;
        }

        public bool HasMoreItems { get; private set; }
    }
}
