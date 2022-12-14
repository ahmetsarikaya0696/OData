using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OData.API.Models;

namespace OData.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(AppDbContext context) : base(context)
        {
        }

        [EnableQuery(PageSize = 2)]
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.AsQueryable());
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProduct([FromODataUri] int key)
        {
            return Ok(_context.Products.Where(p => p.Id == key));
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
