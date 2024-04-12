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
        public List<int> pageNumbers { get; set; }
        public string SearchTerm { get; set; }

        public string Category { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default page size

        public IndexModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
            Products = new List<Product>();
        }

        public void FetchProducts()
        {
            AllProducts = database.Products.ToList();
        }
        public async Task OnGetAsync(int currentPage = 1, string searchTerm = null, string category = null)
        {
            FetchProducts();
            SearchTerm = searchTerm?.Trim(); // Trim leading/trailing whitespaces from searchTerm
            Category = category;

            IQueryable<Product> productsQuery = database.Products;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(Category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }


            var totalProductsCount = await productsQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(totalProductsCount / (double)PageSize);
            PageNumber = currentPage;
            pageNumbers = Enumerable.Range(1, TotalPages).ToList();

            Products = await productsQuery
                .OrderBy(p => p.Name)
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
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