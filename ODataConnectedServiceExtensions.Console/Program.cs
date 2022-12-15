using Default;

var serviceRoot = "https://localhost:44323/OData/";
var context = new Container(new Uri(serviceRoot));

var products = context.Products.ExecuteAsync().Result;

products.ToList().ForEach(p =>
{
    Console.WriteLine($"{p.Id} {p.Name}");
});

Console.ReadLine();