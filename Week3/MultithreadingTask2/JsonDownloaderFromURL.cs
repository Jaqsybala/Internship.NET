using Newtonsoft.Json.Linq;

namespace Week3.MultithreadingTask2
{
    public class JsonDownloaderFromURL
    {
        public static async Task DownloaderJson(string url, int start, int end, HttpClient hc)
        {
            string target = "";
            int id = 1;

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string targetFolder = filePath + "\\JSON";
            bool exist = Directory.Exists(targetFolder);

            if (!exist) Directory.CreateDirectory(targetFolder);
            

            var hrm = await hc.GetAsync(url);
            var json = await hrm.Content.ReadAsStringAsync();
            
            JArray array = JArray.Parse(json);

            if (start > end)
            {
                throw new ArgumentException("Please enter the correct value for the START and TO variables. " +
                                            "The values for the START variable must be lower than for the TO variable.");
            }

            foreach (var urls in array)
            {
                if (!urls.HasValues)
                {
                    throw new ArgumentException("No value to download");
                }
                if (id >= end) break;
                target = urls["thumbnailUrl"].ToString();
                id = Convert.ToInt32(urls["id"].ToString());
                if (id <= start) continue;
                using (var stream = await hc.GetStreamAsync(target))
                {
                    using (var fileStream = new FileStream(targetFolder + "\\{id}.jpg", FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                        Console.WriteLine($"{id}.jpg loaded + {id}");

                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Download started...");
            var startTime = DateTime.Now;

            int start = 0;
            string fileToDownload = "https://jsonplaceholder.typicode.com/photos";

            List<ThreadItem> httpClients = new List<ThreadItem>();
            for (int i = 0; i < 10; i++)
            {
                httpClients.Add(new ThreadItem
                {
                    Start = start,
                    End = start + 500,
                });

                start += 500;

            }

            List<Task> tasks = new List<Task>();

            foreach (var item in httpClients)
            {
                Task task = Task.Run(() => DownloaderJson(fileToDownload, item.Start, item.End, item.Client));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            var endTime = DateTime.Now;
            var duration = (endTime - startTime).TotalSeconds;
            Console.WriteLine("All images are uploaded.");
            Console.WriteLine($"Time taken: {Math.Round(duration)} s");
        }
    }
}
