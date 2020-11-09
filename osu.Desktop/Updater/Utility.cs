// I take some codes from ppy squirrel repository, because here using not official squirrel
// but I need to implement cancellable downloader.
// Im sorry if u mad
// Source: https://github.com/ppy/Squirrel.Windows/blob/master/src/Squirrel/Utility.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Splat;

namespace osu.Desktop.Updater
{
    /// <summary>
    /// Some handly codes for cancellable downloader
    /// </summary>
    public static class Utility
    {
        public static WebClient CreateWebClient()
        {
            // Tls11 and Tls are obsoleted, so we can use Tls12 only now :(
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var ret = new WebClient();
            var wp = WebRequest.DefaultWebProxy;
            if (wp != null)
            {
                wp.Credentials = CredentialCache.DefaultCredentials;
                ret.Proxy = wp;
            }

            return ret;
        }

        // Next method taken from source: https://github.com/dotnet/runtime/issues/31479#issuecomment-578436466
        // And I want to say again, why you f***in guys implemented an useless CancelAsync function in WebClient

        public static async Task DownloadFileAsync(string target, Stream toStream, Action<long, long> progressCallback = null, CancellationToken cancellationToken = default)
            => await DownloadFileAsync(new Uri(target), toStream, progressCallback, cancellationToken);

        /// <summary>
        /// Downloads a file from the specified Uri into the specified stream.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="toStream"></param> 
        /// <param name="progressCallback">If not null, will be called as the download progress. The first parameter will be the number of bytes downloaded so far, and the second the total size of the expected file after download.</param>
        /// <param name="cancellationToken">An optional CancellationToken that can be used to cancel the in-progress download.</param>
        /// <returns>A task that is completed once the download is complete.</returns>
        public static async Task DownloadFileAsync(Uri uri, Stream toStream, Action<long, long> progressCallback = null, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));

            if (uri.IsFile)
            {
                await using Stream file = File.OpenRead(uri.LocalPath);

                if (progressCallback != null)
                {
                    long length = file.Length;
                    byte[] buffer = new byte[4096];
                    int read;
                    int totalRead = 0;
                    while ((read = await file.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        await toStream.WriteAsync(buffer.AsMemory(0, read), cancellationToken).ConfigureAwait(false);
                        totalRead += read;
                        progressCallback(totalRead, length);
                    }
                    Debug.Assert(totalRead == length || length == -1);
                }
                else
                {
                    await file.CopyToAsync(toStream, cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                if (progressCallback != null)
                {
                    long length = response.Content.Headers.ContentLength ?? -1;
                    await using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    byte[] buffer = new byte[4096];
                    int read;
                    int totalRead = 0;
                    while ((read = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        await toStream.WriteAsync(buffer.AsMemory(0, read), cancellationToken).ConfigureAwait(false);
                        totalRead += read;
                        progressCallback(totalRead, length);
                    }
                    Debug.Assert(totalRead == length || length == -1);
                }
                else
                {
                    await response.Content.CopyToAsync(toStream).ConfigureAwait(false);
                }
            }
        }

        public static void WarnIfThrows(this IEnableLogger This, Action block, string message = null)
        {
            This.Log().LogIfThrows(LogLevel.Warn, message, block);
        }

        public static Task WarnIfThrows(this IEnableLogger This, Func<Task> block, string message = null)
        {
            return This.Log().LogIfThrows(LogLevel.Warn, message, block);
        }

        public static Task<T> WarnIfThrows<T>(this IEnableLogger This, Func<Task<T>> block, string message = null)
        {
            return This.Log().LogIfThrows(LogLevel.Warn, message, block);
        }

        public static void LogIfThrows(this IFullLogger This, LogLevel level, string message, Action block)
        {
            try
            {
                block();
            }
            catch (Exception ex)
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        This.DebugException(message ?? "", ex);
                        break;
                    case LogLevel.Info:
                        This.InfoException(message ?? "", ex);
                        break;
                    case LogLevel.Warn:
                        This.WarnException(message ?? "", ex);
                        break;
                    case LogLevel.Error:
                        This.ErrorException(message ?? "", ex);
                        break;
                }

                throw;
            }
        }

        public static async Task LogIfThrows(this IFullLogger This, LogLevel level, string message, Func<Task> block)
        {
            try
            {
                await block();
            }
            catch (Exception ex)
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        This.DebugException(message ?? "", ex);
                        break;
                    case LogLevel.Info:
                        This.InfoException(message ?? "", ex);
                        break;
                    case LogLevel.Warn:
                        This.WarnException(message ?? "", ex);
                        break;
                    case LogLevel.Error:
                        This.ErrorException(message ?? "", ex);
                        break;
                }
                throw;
            }
        }

        public static async Task<T> LogIfThrows<T>(this IFullLogger This, LogLevel level, string message, Func<Task<T>> block)
        {
            try
            {
                return await block();
            }
            catch (Exception ex)
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        This.DebugException(message ?? "", ex);
                        break;
                    case LogLevel.Info:
                        This.InfoException(message ?? "", ex);
                        break;
                    case LogLevel.Warn:
                        This.WarnException(message ?? "", ex);
                        break;
                    case LogLevel.Error:
                        This.ErrorException(message ?? "", ex);
                        break;
                }
                throw;
            }
        }

    }
}
