﻿using System;
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
        public List<Product> AllProducts { get; set; } // då vi ej har ett eget bord för kategorier används alla produkter för att få fram kategorierna. Omständigt, kommer göras annorlunda nästa gång.
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

        public void FetchProducts()
        {
            AllProducts = database.Products.ToList(); //för att kunna visa kategorierna
        }
        public async Task OnGetAsync(int currentPage = 1, string searchTerm = null, string category = null)
        {
            
            FetchProducts();
            SearchTerm = searchTerm?.Trim();
            Category = category;

          
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:5000/api");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var productsFromApi = JsonSerializer.Deserialize<List<Product>>(jsonString);

                if (productsFromApi != null && productsFromApi.Any())
                {  
                    if (!string.IsNullOrEmpty(SearchTerm))
                    {
                        productsFromApi = productsFromApi.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(Category))
                    {
                        productsFromApi = productsFromApi.Where(p => p.Category == Category).ToList();
                    }

                    Products = productsFromApi
                        .OrderBy(p => p.Name)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    var totalProductsCount = productsFromApi.Count;
                    TotalPages = (int)Math.Ceiling(totalProductsCount / (double)PageSize);
                    PageNumber = currentPage;
                    pageNumbers = Enumerable.Range(1, TotalPages).ToList();
                }
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

        public bool ShowPreviousButton => PageNumber > 1;
        public bool ShowNextButton => Products.Count == PageSize;
    }
}