using OData.API.Models;

namespace OData.API.Seeds
{
    public class DataSeed
    {
        public static void SeedDatabase(AppDbContext context)
        {
            if (context.Products.Any() || context.Categories.Any()) return;

            Product p1 = new()
            {
                Name = "Kurşun Kalem",
                Stock = 100,
                Price = 125,
                CreatedDate = DateTime.Now,
            };

            Product p2 = new()
            {
                Name = "Tükenmez Kalem",
                Stock = 222,
                Price = 444,
                CreatedDate = DateTime.Now,
            };

            Product p3 = new()
            {
                Name = "Dolma Kalem",
                Stock = 334,
                Price = 600,
                CreatedDate = DateTime.Now,
            };

            Product p4 = new()
            {
                Name = "Büyük Boy Defter",
                Stock = 300,
                Price = 838,
                CreatedDate = DateTime.Now,
            };

            Product p5 = new()
            {
                Name = "Orta Boy Defter",
                Stock = 200,
                Price = 100,
                CreatedDate = DateTime.Now,
            };

            Product p6 = new()
            {
                Name = "Küçük Boy Defter",
                Stock = 200,
                Price = 300,
                CreatedDate = DateTime.Now,
            };

            Product p7 = new()
            {
                Name = "Tarih Kitabı",
                Stock = 100,
                Price = 900,
                CreatedDate = DateTime.Now,
            };

            Product p8 = new()
            {
                Name = "Edebiyat Kitabı",
                Stock = 200,
                Price = 555,
                CreatedDate = DateTime.Now,
            };

            Product p9 = new()
            {
                Name = "Coğrafya Kitabı",
                Stock = 133,
                Price = 333,
                CreatedDate = DateTime.Now,
            };

            var productsForC1 = new List<Product> { p1, p2, p3 };
            var productsForC2 = new List<Product> { p4, p5, p6 };
            var productsForC3 = new List<Product> { p7, p8, p9 };

            Category c1 = new() { Name = "Kalemler" };

            Category c2 = new() { Name = "Defterler" };

            Category c3 = new() { Name = "Kitaplar" };


            productsForC1.ForEach(p => c1.Products.Add(p));
            productsForC2.ForEach(p => c2.Products.Add(p));
            productsForC3.ForEach(p => c3.Products.Add(p));


            var categories = new List<Category> { c1, c2, c3 };
            categories.ForEach(c => context.Add(c));

            context.SaveChanges();
        }
    }
}
