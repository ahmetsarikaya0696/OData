using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using OData.API.Models;

namespace OData.API.Controllers
{
    public class HelperController : BaseController
    {
        public HelperController(AppDbContext context) : base(context)
        {
        }

        [ODataAttributeRouting]
        [EnableQuery]
        [HttpGet("OData/KdvHesapla")]
        public IActionResult KdvHesapla()
        {
            return Ok(0.18);
        }
    }
}
