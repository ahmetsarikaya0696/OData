﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OData.API.Models;

namespace OData.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(AppDbContext context) : base(context)
        {
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_context.Categories.AsQueryable());
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetCategory([FromODataUri] int key)
        {
            return Ok(_context.Categories.Where(c => c.Id == key));
        }

        [ODataAttributeRouting]
        [EnableQuery]
        [HttpGet("OData/Categories({categoryId})/Products({productId})")]
        public IActionResult ProductById([FromODataUri] int categoryId, [FromODataUri] int productId)
        {
            return Ok(_context.Products.Where(p => p.CategoryId == categoryId && p.Id == productId));
        }
    }
}
