using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPDevToolkit.Services
{
    public interface IStorageService
    {
        Task StorePurchasesAsync(IList<PurchaseItem> purchaseItems);

        Task<IList<PurchaseItem>> LoadPurchasesAsync();
    }
}