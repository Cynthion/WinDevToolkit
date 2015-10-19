using System.Threading.Tasks;

namespace WPDevToolkit.Interfaces
{
    interface ILoadAsync
    {
        Task LoadAsync();

        bool IsLoading { get; }
    }
}
