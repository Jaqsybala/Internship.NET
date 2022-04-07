namespace Week2.LINQTask
{
    public class Goods
    {
        public string ArticleNumber { get; set; }
        public string Category { get; set; }
        public string CountryOfOrigin { get; set; }

        public Goods(string articleNumber, string category, string countryOfOrigin)
        {
            ArticleNumber = articleNumber;
            Category = category;
            CountryOfOrigin = countryOfOrigin;
        }
    }
}
