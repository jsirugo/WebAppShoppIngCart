using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Pages
{
    public class OrderDetailsModel : PageModel
    {
        private readonly AppDbContext _database;
        private readonly AccessControl _accessControl;

        public Product Product { get; set; }

        public OrderDetailsModel(AppDbContext database, AccessControl accessControl)
        {
            _database = database;
            _accessControl = accessControl;
        }
        public void OnGet()
        {
        }
    }
}
