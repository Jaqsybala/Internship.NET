using Week4.BinaryTreeTask;
using Week4.Downloader;
using Week4.JustDownloader;

namespace Week4
{
    public class Program
    {
        static void Main(string[] args)
        {
            //BinaryTreeTask
            Tree<int> binaryTree = new Tree<int>()
            {
                Left = new Tree<int>()
                {
                    Left = new Tree<int>()
                    {
                        Data = 4
                    },
                    Right = new Tree<int>()
                    {
                        Data = 5
                    },
                    Data = 2
                },
                Right = new Tree<int>()
                {
                    Left = new Tree<int>()
                    {
                        Data = 6
                    },
                    Right = new Tree<int>()
                    {
                        Right = new Tree<int>()
                        {
                            Data = 8
                        },
                        Data = 7
                    },
                    Data = 3
                },
                Data = 1
            };

            DoTree(binaryTree, x => Console.Write($"{x} "));

            //LargeFileDownloaderTask
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string largeFileToDownload = "http://212.183.159.230/100MB.zip";
            LargeFileDownloader lfd = new LargeFileDownloader();
            Task task = Task.Run(() => lfd.Downloader(largeFileToDownload, filePath, 8));
            Task.WaitAll(task);

            //AsynchronousDownloaderTask
            string fileToDownloadAsync = "https://newengland.com/wp-content/uploads/maine-coon-cat-trivia.jpg";
            AsyncDownloader ad = new AsyncDownloader();
            Task task1 = Task.Run(() => ad.AsyncDowloader(fileToDownloadAsync, filePath));
            Task.WaitAll(task1);
        }

        public static void DoTree<T>(Tree<T> tree, Action<T> action)
        {
            if (tree == null) return;
            var left = Task.Factory.StartNew(() => DoTree(tree.Left, action));
            var right = Task.Factory.StartNew(() => DoTree(tree.Right, action));
            action(tree.Data);

            try
            {
                Task.WaitAll(left, right);
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }
    }
}
