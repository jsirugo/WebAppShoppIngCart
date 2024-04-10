using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Product> AllProducts { get; set; }
        public string SearchTerm { get; set; }
        public string Category { get; set; }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default page size

        public IndexModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
            Products = new List<Product>();
        }

        public void ShowProducts()
        {
            AllProducts = database.Products.ToList();
        }

        public void OnGet(string searchTerm, string category, int pageNumber = 1)
        {
            SearchTerm = searchTerm;
            Category = category;

            ShowProducts();

            IQueryable<Product> productsQuery = database.Products;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
            }
            else if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }
          
            int totalItems = productsQuery.Count();

        
            int skip = (pageNumber - 1) * PageSize;
            Products = productsQuery.Skip(skip).Take(PageSize).ToList();

           /* if (!string.IsNullOrEmpty(searchTerm))
            {
                Products = Products.Where(p => p.Name.Contains(searchTerm)).ToList();
            }
            else if (!string.IsNullOrEmpty(category))
            {
                Products = Products.Where(p => p.Category == category).ToList();
            }
            else
            {
                Products = AllProducts;
            }

            int totalItems = Products.Count();

            int skip = (pageNumber - 1) * PageSize;
            Products = Products.Skip(skip).Take(PageSize).ToList();*/

        }

        public IActionResult OnPostChangePage(string searchTerm, string category, int pageNumber)
        {
            if (Request.Form.ContainsKey("previousButton"))
            {
                if (pageNumber > 1)  
                {
                    pageNumber--;
                }
            }
            else if (Request.Form.ContainsKey("nextButton"))
            {
                pageNumber++;
            }

            return RedirectToPage("/Index", new { searchTerm, category, pageNumber });
        }

        public IActionResult OnPostAddItemToCart(int productId, string searchTerm, string category, int? page)
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

            return RedirectToPage("/Index", new { searchTerm, category, page });
        }

        public bool ShowPreviousButton => PageNumber > 1;
        public bool ShowNextButton => Products.Count == PageSize;
    }
}
