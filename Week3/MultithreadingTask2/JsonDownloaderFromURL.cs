using Newtonsoft.Json.Linq;
using System.Net;

namespace Week3.MultithreadingTask2
{
    public class JsonDownloaderFromURL
    {
        public void DownloaderJson(string url, int start, int end, WebClient wc)
        {
            string json = "";
            string target = "";
            int id = 1;

            json = wc.DownloadString(url);

            JArray array = JArray.Parse(json);

            if (start < end)
            {
                if (start < 100)
                {
                    foreach (var urls in array)
                    {
                        if (id >= end) break;
                        target = urls["thumbnailUrl"].ToString();
                        id = Convert.ToInt32(urls["id"].ToString());
                        wc.DownloadFile(target, @$"C:\Users\azizk\Downloads\JSON\{id}.jpg");
                        Console.WriteLine($"{id}.jpg loaded + {id}");
                    }
                }
                else
                {
                    foreach (var urls in array)
                    {
                        if (id >= end) break;
                        target = urls["thumbnailUrl"].ToString();
                        id = Convert.ToInt32(urls["id"].ToString());
                        if (id < start) continue;
                        wc.DownloadFile(target, @$"C:\Users\azizk\Downloads\JSON\{id}.jpg");
                        Console.WriteLine($"{id}.jpg loaded + {id}");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Please enter the correct value for the START and END variables. " +
                                            "The values for the START variable must be lower than for the END variable.");
            }
        }

        static void Main(string[] args)
        {
            JsonDownloaderFromURL downloader = new JsonDownloaderFromURL();
            List<WebClient> webClients = new List<WebClient>();

            for (int i = 0; i < 10; i++)
            {
                webClients.Add(new WebClient());
            }

            Console.WriteLine("Download started...");
            Console.WriteLine(DateTime.Now);

            int from = 0;
            int end = 500;

            foreach (WebClient client in webClients)
            {
                Thread loader = new Thread(() => downloader.DownloaderJson("https://jsonplaceholder.typicode.com/photos", from, end, client));
                loader.Start();
                Thread.Sleep(50);
                from += 500;
                end += 500;
            }

            Thread.Sleep(180000);
            Console.WriteLine("All images are uploaded.");
            Console.WriteLine(DateTime.Now);
        }
    }
}
