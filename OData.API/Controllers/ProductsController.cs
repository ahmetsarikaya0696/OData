using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpGet]
        public IActionResult PutProduct([FromODataUri] int key, [FromBody] Product product)
        {
            product.Id = key;
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromODataUri]int key)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == key);
            if (product == null) return NotFound();

            _context.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
