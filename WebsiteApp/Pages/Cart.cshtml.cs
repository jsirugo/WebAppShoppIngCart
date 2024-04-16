using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly AppDbContext _database;
        private readonly AccessControl _accessControl;
        public List<CartItem> CartItems { get; set; }
        public double TotalPrice { get; set; }

        public CartModel(AppDbContext database, AccessControl accessControl)
        {
            _database = database;
            _accessControl = accessControl;
        }

        private void Setup()
        {
            var accountId = _accessControl.LoggedInAccountID;
            CartItems = _database.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.Cart.AccountId == accountId)
                .ToList();

            TotalPrice = CartItems.Sum(ci => ci.Product.Price * ci.Quantity);
        }
        public IActionResult OnPostRemoveItemFromCart(int productId)
        {
            var accountId = _accessControl.LoggedInAccountID;
            var cart = _database.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == accountId);

            if (cart != null)
            {
                var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                    _database.SaveChanges();
                }
            }

            return RedirectToPage("/Cart");
        }

        public void OnGet()
        {
            Setup();
        }
    }
}
