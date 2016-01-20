using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;

namespace WinDevToolkit.Services
{
    // TODO implement ClearLocalAll (Q42 as example)
    public abstract class StorageBase
    {
        #region Folder Accessors

        protected static StorageFolder GetRootFolder()
        {
            return ApplicationData.Current.LocalFolder;
        }

        protected static Task<StorageFolder> GetFolderAsync(string folderName)
        {
            return GetRootFolder().CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists).AsTask();
        }

        #endregion

        #region Object Save/Load

        protected static async Task SaveAsync<T>(T obj, IStorageFolder storageFolder, string fileName)
        {
            if (obj != null)
            {
                // create file
                var file = await storageFolder.CreateFileAsync(fileName + ".json", CreationCollisionOption.ReplaceExisting);

                // serialize object with JSON serializer
                var storageString = JsonConvert.SerializeObject(obj);

                // write content to file
                await FileIO.WriteTextAsync(file, storageString);
            }
        }

        protected static async Task<T> LoadAsync<T>(IStorageFolder storageFolder, string fileName)
        {
            fileName += ".json";
            try
            {
                var file = await storageFolder.GetFileAsync(fileName);

                // deserialize to object with JSON deserializer
                var data = await FileIO.ReadTextAsync(file);
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
            catch (FileNotFoundException)
            {
                return default(T);
            }
        }

        #endregion

        #region Image Save/Load

        protected static async Task SaveImageAsync(WriteableBitmap image, IStorageFolder storageFolder, string fileName)
        {
            try
            {
                if (image != null)
                {
                    // create file
                    var file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    // serialize image
                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                        encoder.SetPixelData(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Premultiplied,
                            (uint)image.PixelWidth,
                            (uint)image.PixelHeight,
                            96, //dpiX
                            96, //dpiY
                            image.PixelBuffer.ToArray());
                        await encoder.FlushAsync();
                        await stream.FlushAsync();
                    }

                    /*
                    using (var stream = image.PixelBuffer.AsStream())
                    {
                        var pixels = new byte[(uint) stream.Length];
                        await stream.ReadAsync(pixels, 0, pixels.Length);

                        using (var writeStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                            encoder.SetPixelData(
                                BitmapPixelFormat.Bgra8,
                                BitmapAlphaMode.Premultiplied,
                                (uint)image.PixelWidth,
                                (uint)image.PixelHeight,
                                96, //dpiX
                                96, //dpiY
                                pixels);
                            await encoder.FlushAsync();

                            using (var outputStream = writeStream.GetOutputStreamAt(0))
                            {
                                await outputStream.FlushAsync();
                            }
                        }
                    }*/
                }
            }
            catch (Exception)
            {
                // ignore
                // mostly due to concurrency issues
            }
        }

        protected static async Task<BitmapImage> LoadImageAsync(IStorageFolder storageFolder, string fileName) // TODO make generic for images
        {
            try
            {
                var file = await storageFolder.GetFileAsync(fileName);

                /*using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    var decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, stream);
                    var pixels = await decoder.GetPixelDataAsync();
                    var image = new WriteableBitmap((int) decoder.PixelWidth, (int) decoder.PixelHeight);
                    image.SetSourceAsync()
                }*/

                // deserialize image
                var bitmap = new BitmapImage();
                using (var inputStream = await file.OpenAsync(FileAccessMode.Read)) //.OpenReadAsync())
                {
                    await bitmap.SetSourceAsync(inputStream);
                }
                return bitmap;
            }
            catch (FileNotFoundException)
            {
                return null; // possible, if image not stored yet
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected static async Task DeleteImageAsync(IStorageFolder storageFolder, string fileName)
        {
            try
            {
                var file = await storageFolder.GetFileAsync(fileName);

                // delete image
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (FileNotFoundException)
            { }
        }

        // TODO methods below needed? --> clean up!

        public static async Task<WriteableBitmap> LoadWriteableBitmapFromFileAsync(StorageFile file)
        {
            // create RandomAccessStream reference from the file
            var rasr = RandomAccessStreamReference.CreateFromFile(file);
            return await LoadWritableBitmapFromRasr(rasr);
        }

        public static async Task<WriteableBitmap> LoadWritableBitmapFromRasr(RandomAccessStreamReference rasr)
        {
            // read the image file into a RandomAccessStream
            WriteableBitmap wrtBitmap;
            using (IRandomAccessStreamWithContentType fileStream = await rasr.OpenReadAsync())
            {
                // now that you have the raw bytes, create an image decoder
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                // get the first frame from the decoder because we are picking an image
                BitmapFrame frame = await decoder.GetFrameAsync(0);

                // convert the frame into pixels
                // I know the parameterless version of GetPixelDataAsync works for this image [Petzold]
                PixelDataProvider pixelProvider = await frame.GetPixelDataAsync();

                // convert pixels into byte array
                byte[] srcPixels = pixelProvider.DetachPixelData();

                // create an in-memory WriteableBitmap of the same size
                wrtBitmap = new WriteableBitmap((int)frame.PixelWidth, (int)frame.PixelHeight);

                // push the pixels from the source file into the in-memory bitmap
                using (Stream pixelStream = wrtBitmap.PixelBuffer.AsStream())
                {
                    pixelStream.Seek(0, SeekOrigin.Begin);
                    pixelStream.Write(srcPixels, 0, srcPixels.Length);
                }
            }
            return wrtBitmap;
        }

        #endregion
    }
}
