using Default;

var serviceRoot = "https://localhost:44323/OData/";
var context = new Container(new Uri(serviceRoot));

//var products = context.Products.AddQueryOption("$filter","categoryId eq 2")
//                               .AddQueryOption("$select", "Id,Name")
//                               .AddQueryOption("$filter", "Id eq 2")
//                               .Execute();

var products = context.Products.Expand(p => p.Category)
                               .Execute();

products.ToList().ForEach(p =>
{
    Console.WriteLine($"ProductId : {p.Id}\r\nProductName : {p.Name}\r\nCategoryName : {p.Category.Name}\r\nCategoryId : {p.Category.Id}");
    Console.WriteLine("****************************************");
});

Console.ReadLine();