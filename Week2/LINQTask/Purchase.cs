namespace Week2.LINQTask
{
    public class Purchase
    {
        public int ConsumerCode { get; set; }
        public string ArticelNumber { get; set; }
        public string StoreName { get; set; }

        public Purchase(int consumerCode, string articleNumber, string storeName)
        {
            ConsumerCode = consumerCode;
            ArticelNumber = articleNumber;
            StoreName = storeName;
        }
    }
}
