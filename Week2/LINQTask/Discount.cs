namespace Week2.LINQTask
{
    public class Discount
    {
        public int ConsumerCode { get; set; }
        public string StoreName { get; set; }
        public int Discounts { get; set; }

        public Discount(int consumerCode, string storeName, int discounts)
        { 
            ConsumerCode = consumerCode;
            StoreName = storeName;
            Discounts = discounts;
        }
    }
}
