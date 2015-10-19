using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace WPDevToolkit
{
    public static class ImageLoader
    {
        private static BitmapImage _unknownCover;
        private static BitmapImage GetUnknowCover()
        {
            if (_unknownCover == null)
            {
                // TODO correct path
                _unknownCover = new BitmapImage(new Uri("ms-appx:///Assets/Pictures/UnknownCover150.png", UriKind.Absolute));
            }
            return _unknownCover;
        }

        // TODO store images elsewhere, according to type -> now all in ShowCover
        // TODO only store covers and fanarts
        private static async Task<ImageSource> LoadAndStoreImageAsync(string imageUrl)
        {
            // check if url is given
            if (imageUrl == null)
            {
                return GetUnknowCover();
            }

            var fileName = imageUrl.XxHash();

            // try to load image from the storage
            /*var bitmap = await BaseStorage.LoadShowCoverAsync(fileName);
            if (bitmap != null)
            {
                return bitmap;
            }*/

            // else, load from web
            var rasr = RandomAccessStreamReference.CreateFromUri(new Uri(imageUrl));
            var wrtBitmap = await BaseStorage.LoadWritableBitmapFromRasr(rasr);
            return wrtBitmap;

            // store
            /*if (wrtBitmap != null)
            {
                await BaseStorage.StoreShowCoverAsync(wrtBitmap, fileName);
                return wrtBitmap;
            }*/
        }

        //private static Task RemoveImageAsync(string imageUrl)
        //{
        //    var fileName = imageUrl.XxHash();
        //    return BaseStorage.DeleteShowCoverAsync(fileName);
        //}
    }
}
