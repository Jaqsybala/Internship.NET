namespace Week4.JustDownloader
{
    public class AsyncDownloader
    {
        public async Task AsyncDowloader(string url, string targetFolder)
        {
            using (HttpClient hc = new HttpClient())
            {
                //Start of downloading image - 1 step
                var start = DateTime.Now;
                /* This expression sends a request to this URL
                   and will wait a result from URL also releases a worker thread - 2 step */
                var content = await hc.GetAsync(url); /* <- When an expression are gotten a result from this URL,
                                                            then execution of method continues here - 3 step */

                //This expression reads bytes from array of bytes asynchronously - 4 step
                byte[] file = await content.Content.ReadAsByteArrayAsync(); // <- Execution of method continues here - 5 step

                //Here is creating a new object of Uri class - 6 step
                Uri uri = new Uri(url);
                //Using Path.Combine method we combine a target folder with filename - 7 step
                var path = Path.Combine(targetFolder, uri.Segments.Last());
                //This expression writes all bytes from array of bytes asynchronously to the file that locates in target folder - 8 step
                await File.WriteAllBytesAsync(path, file); // <- Execution of method continues here - 9 step

                //End of downloading image - 10 step
                var end = DateTime.Now;
                //Here is calculating duration of downloading image - 11 step
                var timeTaken = (end - start).Milliseconds;
                //Output to the console about download ending - 12 step
                Console.WriteLine("File was downloaded");
                Console.WriteLine($"Time taken: {timeTaken} ms");
            }
        }
    }
}
