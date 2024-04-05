using System.ComponentModel.DataAnnotations;

namespace WebsiteApp.Models
{
    public class CartItem
    {
        public int Id { get; set; } // PK cart item
        
        public int CartId { get; set; } // FK cart bord
        public Cart Cart { get; set; } // Navigation property

        public int ProductId { get; set; } // FK produkt bord
        public Product Product { get; set; } // Navigation property 

        public int Quantity { get; set; } 
    }
}
