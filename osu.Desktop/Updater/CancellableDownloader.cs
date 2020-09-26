using System;
using System.Threading;
using System.Threading.Tasks;
using Splat;
using Squirrel;

namespace osu.Desktop.Updater
{
    public class CancellableDownloader : IFileDownloader, IEnableLogger
    {
        private CancellationToken cancellationToken;

        public void SetCancellationToken(CancellationToken ctn) => cancellationToken = ctn;

        public async Task DownloadFile(string url, string targetFile, Action<int> progress)
        {
            var failedUrl = default(string);

            var lastSignalled = DateTime.MinValue;

            void downloadProgressChanged(long c, long f)
            {
                var now = DateTime.Now;
                var val = ((double)c / f) * 100;
                if (now - lastSignalled > TimeSpan.FromMilliseconds(200))
                {
                    lastSignalled = now;
                    progress((int)val);
                }
            }

        retry:
            try
            {
                this.Log().Info("Downloading file: " + (failedUrl ?? url));

                await this.WarnIfThrows(
                    async () =>
                    {
                        using (var fileStream = System.IO.File.Create(targetFile))
                            await Utility.DownloadFileAsync(failedUrl ?? url, fileStream, downloadProgressChanged, cancellationToken); //wc.DownloadFileTaskAsync(failedUrl ?? url, targetFile), cancellationToken);

                        progress(100);
                    },
                    "Failed downloading URL: " + (failedUrl ?? url));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                // NB: Some super brain-dead services are case-sensitive yet 
                // corrupt case on upload. I can't even.
                if (failedUrl != null) throw;

                failedUrl = url.ToLower();
                progress(0);
                goto retry;
            }
        }

        public async Task<byte[]> DownloadUrl(string url)
        {
            using (var wc = Utility.CreateWebClient())
            {
                var failedUrl = default(string);

            retry:
                try
                {
                    this.Log().Info("Downloading url: " + (failedUrl ?? url));

                    return await this.WarnIfThrows(() => wc.DownloadDataTaskAsync(failedUrl ?? url),
                        "Failed to download url: " + (failedUrl ?? url));
                }
                catch (Exception)
                {
                    // NB: Some super brain-dead services are case-sensitive yet 
                    // corrupt case on upload. I can't even.
                    if (failedUrl != null) throw;

                    failedUrl = url.ToLower();
                    goto retry;
                }
            }
        }
    }

}
