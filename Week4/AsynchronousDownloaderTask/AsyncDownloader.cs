namespace Week4.JustDownloader
{
    public class AsyncDownloader
    {
        public async Task AsyncDowloader(string url, string targetFolder)
        {
            using (HttpClient hc = new HttpClient())
            { 
                var start = DateTime.Now;
                var content = await hc.GetAsync(url);

                byte[] file = await content.Content.ReadAsByteArrayAsync();
                
                Uri uri = new Uri(url);
                var path = Path.Combine(targetFolder, uri.Segments.Last());

                await File.WriteAllBytesAsync(path, file);
                
                var end = DateTime.Now;
                var timeTaken = (end - start).Milliseconds;
                Console.WriteLine("File was downloaded");
                Console.WriteLine($"Time taken: {timeTaken} ms");
            }
        }
    }
}
