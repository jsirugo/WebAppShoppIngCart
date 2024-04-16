using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Pages
{
    public class OrderDetailsModel : PageModel
    {
        private readonly AppDbContext _database;
        private readonly AccessControl _accessControl;

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<CartItem> PurchasedItems { get; set; } = new List<CartItem>();
        public double TotalPrice { get; set; }

        public OrderDetailsModel(AppDbContext database, AccessControl accessControl)
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
            PurchasedItems = CartItems;
        }


        public void OnGet()
        {
            Setup();
        }
        public IActionResult OnGetRemoveItems(List<CartItem> CartItems)
        {
            var accountId = _accessControl.LoggedInAccountID;
            var cart = _database.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == accountId);

            if (cart != null)
            {

                cart.CartItems.Clear();
                _database.SaveChanges();
            }

            return RedirectToPage("/OrderDetails");
        }
    }
}
