using Week2.LINQTask;

namespace Week2
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Consumer> consumers = new List<Consumer>()
            { 
                //Sequence A
                new Consumer(123, 1997, "Almaty"),
                new Consumer(124, 2001, "Almaty"),
                new Consumer(125, 1991, "Astana"),
                new Consumer(126, 1993, "Atyrau"),
                new Consumer(127, 1995, "Aktau"),
                new Consumer(128, 1989, "Almaty"),
            };

            List<Goods> goods = new List<Goods>()
            {
                //Sequence B
                new Goods("FK200-5400", "Food", "Kazakhstan"),
                new Goods("FK201-5401", "Food", "Kazakhstan"),
                new Goods("FK202-5402", "Food", "Kazakhstan"),

                new Goods("FR300-5300", "Food", "Russia"),
                new Goods("FR301-5301", "Food", "Russia"),

                new Goods("FC100-5200", "Food", "China"),
                new Goods("FC101-5201", "Food", "China"),

                new Goods("FG500-5500", "Food", "Germany"),

                new Goods("FF400-5700", "Food", "France"),
                new Goods("FF401-5701", "Food", "France"),
            };

            List<Discount> discounts = new List<Discount>()
            {
                //Sequence C
                new Discount(123, "Magnum", 5),
                new Discount(124, "Magnum", 15),

                new Discount(125, "Marko", 3),
                new Discount(126, "Marko", 11),

                new Discount(127, "Small", 20),
                new Discount(123, "Small", 5),
            };

            List<Price> prices = new List<Price>()
            {
                //Sequence D
                new Price("FK200-5400", "Magnum", 500),
                new Price("FK201-5401", "Magnum", 1000),
                new Price("FK202-5402", "Magnum", 1500),
                new Price("FR300-5300", "Magnum", 1200),
                new Price("FR301-5301", "Magnum", 850),
                new Price("FC100-5200", "Magnum", 900),
                new Price("FC101-5201", "Magnum", 450),
                new Price("FG500-5500", "Magnum", 2500),
                new Price("FF400-5700", "Magnum", 3000),

                new Price("FK200-5400", "Marko", 450),
                new Price("FK201-5401", "Marko", 950),
                new Price("FK202-5402", "Marko", 1450),
                new Price("FR300-5300", "Marko", 1100),
                new Price("FR301-5301", "Marko", 750),
                new Price("FC100-5200", "Marko", 800),
                new Price("FC101-5201", "Marko", 350),
                new Price("FG500-5500", "Marko", 1500),
                new Price("FF400-5700", "Marko", 2000),

                new Price("FK200-5400", "Small", 600),
                new Price("FK201-5401", "Small", 1100),
                new Price("FK202-5402", "Small", 1600),
                new Price("FR300-5300", "Small", 1300),
                new Price("FR301-5301", "Small", 950),
                new Price("FC100-5200", "Small", 1000),
                new Price("FC101-5201", "Small", 550),
                new Price("FG500-5500", "Small", 2600),
                new Price("FF400-5700", "Small", 3100),
            };

            List<Purchase> purchases = new List<Purchase>()
            {
                //Sequence E
                new Purchase(123, "FK200-5400", "Magnum"),
                new Purchase(123, "FK201-5401", "Magnum"),
                new Purchase(124, "FK202-5402", "Magnum"),
                new Purchase(124, "FR300-5300", "Marko"),
                new Purchase(123, "FR301-5301", "Marko"),
                new Purchase(125, "FC100-5200", "Small"),
                new Purchase(126, "FC101-5201", "Small"),
                new Purchase(127, "FG500-5500", "Small"),
                new Purchase(127, "FF400-5700", "Marko"),

            };

            var oneLINQ = purchases
                .Join(goods,
                p => p.ArticelNumber,
                g => g.ArticleNumber,
                (p, g) => new { p, g })
                .Join(consumers,
                p => p.p.ConsumerCode,
                c => c.ConsumerCode,
                (p, c) => new { p, c })
                .Select(x => new
                {
                    Country = x.p.g.CountryOfOrigin,
                    Store = x.p.p.StoreName,
                    Consumer_code = x.c.ConsumerCode,
                    Year_Of_Birth = x.c.YearOfBirth,
                    Total_costs = prices
                        .Where(p => p.ArticleNumber == x.p.p.ArticelNumber)
                        .Select(p => p.Prices)
                })
                //.Where(x => x.Year_Of_Birth == consumers.Select(x => x.YearOfBirth).Max())
                .OrderByDescending(x => x.Country)
                .ThenByDescending(x => x.Store)
                .ThenByDescending(x => x.Consumer_code);


            foreach (var item in oneLINQ)
                Console.WriteLine(item);
            //Console.WriteLine(oneLINQ);
        }
    }
}