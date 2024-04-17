using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        private readonly IHttpClientFactory clientFactory;
        public List<Product> Products { get; set; }
        public List<Product> AllProducts { get; set; } 
        public List<int> pageNumbers { get; set; }
        public string SearchTerm { get; set; }

        public string Category { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; } = 1; // Default to page 1
        public int PageSize { get; set; } = 10; // Default page size

        public IndexModel(AppDbContext database, AccessControl accessControl, IHttpClientFactory clientFactory)
        {
            this.database = database;
            this.accessControl = accessControl;
            this.clientFactory = clientFactory;
            Products = new List<Product>();
        }
        public class ProductsApiResponse
        {
            public List<Product> Products { get; set; }
            public int TotalPages { get; set; }
            public int PageNumber { get; set; }

        }
        public bool ShowPreviousButton => PageNumber > 1;
        public bool ShowNextButton => Products.Count == PageSize;

        public void FetchProducts()
        {
            AllProducts = database.Products.ToList(); //för att kunna visa kategorierna(väl medvetna om att detta inte håller i riktig databas)
        }
        public async Task OnGetAsync(int currentPage = 1, string searchTerm = null, string category = null)
        {
            FetchProducts();
            SearchTerm = searchTerm?.Trim();
            Category = category;

            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:5000/api/products?currentPage={currentPage}&searchTerm={SearchTerm}&category={Category}");  //apí

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync(); // läser av json
                var result = JsonSerializer.Deserialize<ProductsApiResponse>(jsonString);

                Products = result.Products;
                TotalPages = result.TotalPages;
                PageNumber = result.PageNumber;
                pageNumbers = Enumerable.Range(1, TotalPages).ToList();
            }

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

            return RedirectToPage("/Index", new { searchTerm, category, currentPage = page });
        }

        
    }
}