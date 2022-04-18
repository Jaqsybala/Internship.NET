namespace Week2.LINQTask
{
    public class ForTest
    {
        public string Country { get; set; }
        public string Store { get; set; }
        public int Code { get; set; }
        public int Birth { get; set; }
        public int Price { get; set; }

        public ForTest(string country, string store, int code, int birth, int price)
        {
            Country = country;
            Store = store;
            Code = code;
            Birth = birth;
            Price = price;
        }
    }
}
