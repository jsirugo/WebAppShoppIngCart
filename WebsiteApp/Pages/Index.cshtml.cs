using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public List<Product> Products { get; set; }
        public IndexModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public void ShowProducts()
        {
            Products = database.Products.ToList();
        }
        public void OnGet()
        {
            ShowProducts();
        }

        public void OnPostAddItemToCart(int productId)
        {
            int loggedInAccountId = accessControl.LoggedInAccountID;

            var cart = database.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == loggedInAccountId);
            if (cart == null)
            {
                cart = new Cart { AccountId = loggedInAccountId };
                database.Carts.Add(cart);
            }

            var product = database.Products.FirstOrDefault(p => p.ID == productId);

            if (product != null)
            {
                var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1 });
                }

                database.SaveChanges();
            }

            Response.Redirect("/");
        }
    }
    
}