namespace Week2.LINQTask
{
    public class Purchase
    {
        public int ConsumerCode { get; set; }
        public string ArticleNumber { get; set; }
        public string StoreName { get; set; }

        public Purchase(int consumerCode, string articleNumber, string storeName)
        {
            ConsumerCode = consumerCode;
            ArticleNumber = articleNumber;
            StoreName = storeName;
        }
    }
}
