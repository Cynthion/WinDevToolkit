using System.Threading.Tasks;

namespace WinDevToolkit.Interfaces
{
    public interface IAsyncLoader
    {
        Task LoadAsync();

        Task ReloadAsync();

        bool IsLoading { get; }

        bool IsLoaded { get; }

        bool HasStatusText { get; }

        string StatusText { get; }
    }
}
