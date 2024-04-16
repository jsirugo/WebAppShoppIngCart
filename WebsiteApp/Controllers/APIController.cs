using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Controllers
{
    [Route("/api/products")]
    [ApiController]
    [AllowAnonymous]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        [HttpGet]

        public ActionResult<IEnumerable<Product>> GetProducts(
            int currentPage = 1,
            string? searchTerm = null,
            string? category = null,
            int pageSize = 10)
        {
            IQueryable<Product> query = database.Products;

           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            var totalProductsCount = query.Count();

            var products = query
                .OrderBy(p => p.Name)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalProductsCount / (double)pageSize);

            return Ok(new
            {
                Products = products,
                TotalPages = totalPages,
                PageNumber = currentPage
            });
        }
    }
}
    

