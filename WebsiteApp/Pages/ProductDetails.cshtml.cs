using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteApp.Data;
using WebsiteApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebsiteApp.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly AppDbContext _database;
        private readonly AccessControl _accessControl;

        public Product Product { get; set; }

        public ProductDetailsModel(AppDbContext database, AccessControl accessControl)
        {
            _database = database;
            _accessControl = accessControl;
        }

        public IActionResult OnGet(int id)
        {
            Product = _database.Products.FirstOrDefault(p => p.ID == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAddItemToCart(int productId)
        {
            Product = _database.Products.FirstOrDefault(p => p.ID == productId);

            if (Product == null)
            {
                return NotFound();
            }

            int loggedInAccountId = _accessControl.LoggedInAccountID;

            var cart = _database.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == loggedInAccountId);
            if (cart == null)
            {
                cart = new Cart { AccountId = loggedInAccountId };
                _database.Carts.Add(cart);
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1 });
            }
            _database.SaveChanges();

            return RedirectToPage("/ProductDetails", new { id = productId });
        }
    }
}
