using System.Net.Http.Headers;

namespace Week4.Downloader
{
    public class Chunk
    {
        internal int ChunkId { get; set; }
        internal long Start { get; set; }
        internal long End { get; set; }
    }


    public class LargeFileDownloader
    {
        public async Task Downloader(string url, string targetFolder, int parallelNum)
        {
            #region File extensions
            Dictionary<string, string> extensions = new Dictionary<string, string>();
            extensions.Add(".txt", "text/plain");
            extensions.Add(".jpg", "image/jpeg");
            extensions.Add(".png", "image/png");
            extensions.Add(".mp3", "audio/mpeg");
            extensions.Add(".mp4", "video/mp4");

            #endregion

            var start = DateTime.Now;

            #region Get file size from header
            HttpResponseMessage? response = null;
            long fileSize = 0L;
            string MIME = "";
            using (HttpClient client = new HttpClient())
            {
                response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                MIME = response.Content.Headers.ContentType.MediaType.ToString();
                fileSize = response.Content.Headers.ContentLength ?? 0;
            }
            #endregion

            #region Calculation size of chunks
            var chunks = new List<Chunk>();

            for (int i = 0; i < parallelNum; i++)
            {
                chunks.Add(new Chunk
                {
                    ChunkId = i,
                    Start = i * (fileSize / parallelNum),
                    End = (i + 1) * (fileSize / parallelNum) - 1
                });
            };

            if (chunks.Last().End < fileSize)
            {
                chunks.Add(new Chunk()
                {
                    ChunkId = chunks.Count + 1,
                    Start = chunks.Any() ? chunks.Last().End + 1 : 0,
                    End = chunks.Last().End + 1
                });
            };
            #endregion

            #region Parallel downloads of chunks
            Uri uri = new Uri(url);
            string folder = Path.Combine(targetFolder, "temp");
            Console.WriteLine("Downloaded: ");
            var progress = 0d;
            var count = (double)100 / chunks.Count;
            var tasks = chunks.Select(async chunk =>
            {
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Get, url);

                req.Headers.Range = new RangeHeaderValue(chunk.Start, chunk.End);
                var res = await client.SendAsync(req);

                using (var stream = await res.Content.ReadAsStreamAsync())
                {
                    using (var fileStream = new FileStream(folder + chunk.ChunkId, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                        progress += count;
                        Console.WriteLine($"[{Math.Floor(progress)}%] in 100%");
                    }
                }
            });

            await Task.WhenAll(tasks);
            #endregion

            #region Merge all chunk files in a single file
            using var output = File.Create(Path.Combine(targetFolder, "MergedFile" + extensions
                                                                                .Where(x => x.Value.Equals(MIME))
                                                                                .Select(x => x.Key)
                                                                                .First()));
            foreach (var chunk in chunks)
            {
                var chunkFileBytes = File.ReadAllBytes(folder + chunk.ChunkId);
                output.Write(chunkFileBytes, 0, chunkFileBytes.Length);
            }
            #endregion

            #region Delete temp files
            foreach (var chunk in chunks)
            {
                string filepath = folder + chunk.ChunkId;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            #endregion

            var end = DateTime.Now;
            var timeTaken = (end - start).Milliseconds;
            Console.WriteLine("File was downloaded");
            Console.WriteLine($"Time taken: {timeTaken} ms");
        }
    }
}
