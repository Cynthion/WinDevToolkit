using System;
using System.IO;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace WinDevToolkit.Services
{
    public abstract class ApiBase
    {
        protected async Task<string> ApiGetRequestAsync(string url, string accessToken = null)
        {
            if (!PhoneInteraction.IsInternetAvailable())
            {
                await Messaging.ShowInternetUnavailableAsync();
                return null;
            }

            ExceptionDispatchInfo exception = null;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "GET";
                    SetHeaders(request, accessToken);

                    using (var response = await request.GetResponseAsync() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                return await reader.ReadToEndAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                exception = ExceptionDispatchInfo.Capture(e);
            }

            if (exception != null)
            {
                // TODO make user-friendly
                await Messaging.ShowMessageDialogErrorAsync(exception.SourceException);
                //exception.Throw();
            }
            return null;
        }

        protected async Task<string> ApiPostRequestAsync(string url, string postJson, string accessToken = null)
        {
            if (!PhoneInteraction.IsInternetAvailable())
            {
                await Messaging.ShowInternetUnavailableAsync();
                return null;
            }

            ExceptionDispatchInfo exception = null;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    SetHeaders(request, accessToken);

                    // bytes
                    var byteArray = Encoding.UTF8.GetBytes(postJson);

                    using (var writer = await request.GetRequestStreamAsync())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }
                    using (var response = await request.GetResponseAsync() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                return await reader.ReadToEndAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                exception = ExceptionDispatchInfo.Capture(e);
            }

            if (exception != null)
            {
                await Messaging.ShowMessageDialogErrorAsync(exception.SourceException);
            }
            return null;
        }

        private void SetHeaders(HttpWebRequest request, string accessToken = null)
        {
            // set headers
            request.Accept = "application/json";
            DoSetHeaders(request);

            //// set OAuth header
            //if (accessToken != null)
            //{
            //    request.Headers[HttpRequestHeader.Authorization] = string.Format("Bearer {0}", accessToken);
            //}
        }

        protected virtual void DoSetHeaders(HttpWebRequest request)
        {
            // no headers set by default
        }
    }
}
