using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Controllers
{
    [Route("/api")]
    [ApiController]
    [AllowAnonymous]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }
     

        [HttpGet]
        public List<Product> Products()
        {
            List<Product> products = database.Products.ToList();

            
            products = products.OrderBy(p => p.Name)
                .ToList();

            return products;
        }
    }
}
