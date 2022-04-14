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
                new Consumer(1, 1997, "Almaty"),
                new Consumer(2, 1998, "Astana"),
                new Consumer(3, 1999, "Atyrau"),
            };

            List<Goods> goods = new List<Goods>()
            {
                //Sequence B
                new Goods("FK000-0100", "Food", "Kazakhstan"),
                new Goods("FU000-0101", "Food", "Ukraine"),
                new Goods("FJ000-0102", "Food", "Japan"),
                new Goods("SR000-0200", "Sport", "Russia"),
                new Goods("SU000-0201", "Sport", "USA"),
                new Goods("CT000-0300", "Clothes", "Turkey"),
                new Goods("CI000-0301", "Clothes", "Italy"),
            };

            List<Discount> discounts = new List<Discount>()
            {
                //Sequence C
                new Discount(1, "Magnum", 10),
                new Discount(1, "Gucci", 5),
                new Discount(2, "Sportmaster", 30),
                new Discount(2, "Decathlon", 15),
                new Discount(2, "DeFacto", 50),
                new Discount(3, "DeFacto", 10),
                new Discount(1, "DeFacto", 10),
            };

            List<Price> prices = new List<Price>()
            {
                //Sequence D
                new Price("FK000-0100", "Magnum", 5000),
                new Price("FU000-0101", "Magnum", 12000),
                new Price("FJ000-0102", "Magnum", 80000),
                new Price("SR000-0200", "Sportmaster", 20000),
                new Price("SU000-0201", "Sportmaster", 40000),
                new Price("SR000-0200", "Decathlon", 10000),
                new Price("SU000-0201", "Decathlon", 15000),
                new Price("CT000-0300", "DeFacto", 7500),
                new Price("CI000-0301", "Gucci", 1800000),
            };

            List<Purchase> purchases = new List<Purchase>()
            {
                //Sequence E
                new Purchase(1, "FK000-0100", "Magnum"),
                new Purchase(2, "FU000-0101", "Magnum"),
                new Purchase(3, "FJ000-0102", "Magnum"),
                new Purchase(1, "SU000-0201", "Sportmaster"),
                new Purchase(2, "SU000-0201", "Sportmaster"),
                new Purchase(1, "SR000-0200", "Sportmaster"),
                new Purchase(3, "SR000-0200", "Decathlon"),
                new Purchase(2, "CI000-0301", "Gucci"),
                new Purchase(3, "CT000-0300", "DeFacto"),
                new Purchase(1, "CT000-0300", "DeFacto"),
                new Purchase(1, "CT000-0300", "DeFacto"),
                new Purchase(1, "CT000-0300", "DeFacto"),

            };

            var finalLINQ =
                    from e in purchases
                    join b in goods on e.ArticleNumber equals b.ArticleNumber
                    join a in consumers on e.ConsumerCode equals a.ConsumerCode
                    join c in discounts on new { e.StoreName, e.ConsumerCode } equals new { c.StoreName, c.ConsumerCode }
                    join d in prices on new { e.ArticleNumber, e.StoreName } equals new { d.ArticleNumber, d.StoreName }
                    join fq in (from se in purchases
                                join sb in goods on se.ArticleNumber equals sb.ArticleNumber
                                join sa in consumers on se.ConsumerCode equals sa.ConsumerCode
                                join sd in prices on new { se.ArticleNumber, se.StoreName } equals new { sd.ArticleNumber, sd.StoreName }
                                group new { sb, se, sa } by new { sb.CountryOfOrigin, se.StoreName } into CS
                                select new
                                {
                                    acountry_of_origin = CS.Key.CountryOfOrigin,
                                    astore_name = CS.Key.StoreName,
                                    maxy = CS.Max(x => x.sa.YearOfBirth)
                                })
                    on new
                    {
                        Country = b.CountryOfOrigin,
                        Store = e.StoreName
                    } equals new
                    {
                        Country = fq.acountry_of_origin,
                        Store = fq.astore_name
                    }
                    join sq in (from ce in purchases
                                join cb in goods on ce.ArticleNumber equals cb.ArticleNumber
                                join ca in consumers on ce.ConsumerCode equals ca.ConsumerCode
                                join cc in discounts on new { ce.StoreName, ce.ConsumerCode } equals new { cc.StoreName, cc.ConsumerCode }
                                join cd in prices on new { ce.ArticleNumber, ce.StoreName } equals new { cd.ArticleNumber, cd.StoreName }
                                group new { cb, ce, ca, cd, cc } by new { cb.CountryOfOrigin, ce.StoreName, ce.ConsumerCode, ca.YearOfBirth } into KS
                                select new
                                {
                                    ccountry_of_origin = KS.Key.CountryOfOrigin,
                                    cstore_name = KS.Key.StoreName,
                                    cconsumer_code = KS.Key.ConsumerCode,
                                    cyear_of_birth = KS.Key.YearOfBirth,
                                    cnew_price = KS.Sum(x => x.cd.Prices - (x.cd.Prices * x.cc.Discounts) / 100)
                                })
                    on new
                    {
                        Country = b.CountryOfOrigin,
                        Store = e.StoreName,
                        Code = e.ConsumerCode,
                        Birth = a.YearOfBirth
                    } equals new
                    {
                        Country = sq.ccountry_of_origin,
                        Store = sq.cstore_name,
                        Code = sq.cconsumer_code,
                        Birth = sq.cyear_of_birth
                    }
                    where a.YearOfBirth == fq.maxy
                    group new { b, e, a, fq, sq } by new
                    {
                        b.CountryOfOrigin,
                        e.StoreName,
                        e.ConsumerCode,
                        a.YearOfBirth,
                        fq.acountry_of_origin,
                        fq.astore_name,
                        fq.maxy,
                        sq.ccountry_of_origin,
                        sq.cstore_name,
                        sq.cconsumer_code,
                        sq.cyear_of_birth,
                        sq.cnew_price
                    } into finalGroup
                    select new
                    {
                        Country = finalGroup.Key.CountryOfOrigin,
                        Store = finalGroup.Key.StoreName,
                        Code = finalGroup.Key.ConsumerCode,
                        Birth = finalGroup.Key.YearOfBirth,
                        Price = finalGroup.Key.cnew_price
                    };

            foreach (var item in finalLINQ)
                Console.WriteLine(item);
        }
    }
}