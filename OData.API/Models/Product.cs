namespace OData.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }


        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Feature Feature { get; set; }
    }
}
