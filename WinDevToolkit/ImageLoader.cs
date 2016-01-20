using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using WinDevToolkit.Services;

namespace WinDevToolkit
{
    public static class ImageLoader
    {
        /// <summary>
        /// Loads an image from the local assets folder.
        /// </summary>
        /// <param name="relativePath">E.g., "/Assets/Icons/image.png"</param>
        /// <returns></returns>
        public static BitmapImage LoadFromAssets(string relativePath)
        {
            // image loading for Windows Runtime
            // see https://msdn.microsoft.com/en-us/library/hh763341.aspx
            var absolutePath = string.Format("ms-appx://{0}", relativePath);
            return new BitmapImage(new Uri(absolutePath, UriKind.Absolute));
        }

        private static BitmapImage _unknownCover;
        private static BitmapImage GetUnknowImage()
        {
            if (_unknownCover == null)
            {
                _unknownCover = new BitmapImage(new Uri("ms-appx:///Assets/cardBack.png", UriKind.Absolute));
            }
            return _unknownCover;
        }

        public static async Task<ImageSource> LoadImageAsync(string imageUrl)
        {
            // check if url is given
            if (imageUrl == null)
            {
                return GetUnknowImage();
            }
            // TODO create options needed?
            ImageSource src = new BitmapImage(new Uri(imageUrl));
            return src;
        }

        private static async Task<ImageSource> LoadAndStoreImageAsync(string imageUrl)
        {
            // check if url is given
            if (imageUrl == null)
            {
                return GetUnknowImage();
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
            var wrtBitmap = await StorageBase.LoadWritableBitmapFromRasr(rasr);
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
