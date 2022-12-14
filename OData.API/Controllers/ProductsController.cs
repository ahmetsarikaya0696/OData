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

        //[EnableQuery(PageSize = 2)]
        [EnableQuery]
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
        public IActionResult DeleteProduct([FromODataUri] int key)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == key);
            if (product == null) return NotFound();

            _context.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Login(ODataActionParameters parameters)
        {
            Login login = parameters["Login"] as Login;
            return Ok($"Email : {login.Email}-Password : {login.Password}");
        }


        /// <summary>
        /// .../OData/Products/MultiplyFunction(a1=3,a2=4,a3=5)
        /// </summary>
        /// <param sayi1="a1"></param>
        /// <param sayi2="a2"></param>
        /// <param sayi3="a3"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MultiplyFunction([FromODataUri] int a1, [FromODataUri] int a2, [FromODataUri] int a3)
        {
            return Ok(a1 * a2 * a3);
        }

        [HttpGet]
        public IActionResult KdvHesapla([FromODataUri] int key, [FromODataUri] double kdv)
        {
            var product = _context.Products.Find(key);
            return Ok($"{product.Price + product.Price * kdv}");
        }
    }
}
