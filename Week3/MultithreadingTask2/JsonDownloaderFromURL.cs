using Newtonsoft.Json.Linq;
using System.Net;

namespace Week3.MultithreadingTask2
{
    public class JsonDownloaderFromURL
    {
        private WebClient wc;

        public void DownloaderJson(string url, int start, int end, WebClient wc)
        {
            string json = "";
            string target = "";
            int id = 1;

            json = wc.DownloadString(url);

            JArray array = JArray.Parse(json);
            
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
    }
}
