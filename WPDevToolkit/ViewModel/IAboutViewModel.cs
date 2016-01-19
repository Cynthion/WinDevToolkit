using System.Threading.Tasks;
using WPDevToolkit.Interfaces;

namespace WPDevToolkit.ViewModel
{
    public interface IAboutViewModel : IAsyncLoader
    {
        Task BuyDonationAsync(PurchaseItem purchaseItem);
    }
}