namespace OData.API.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Color { get; set; }

        public Product Product { get; set; }
    }
}
