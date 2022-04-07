namespace Week2.LINQTask
{
    public class Price
    {
        public string ArticleNumber { get; set; }
        public string StoreName { get; set; }
        public int Prices { get; set; }

        public Price(string articleNumber, string storeName, int prices)
        { 
            ArticleNumber = articleNumber;
            StoreName = storeName;
            Prices = prices;
        }
    }
}
