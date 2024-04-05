using System.ComponentModel.DataAnnotations;

namespace WebsiteApp.Models
{
    public class Cart
    {
        public int Id { get; set; } // PK cart
        
        public int AccountId { get; set; } // FK account bord
        public Account Account { get; set; } // Navigation property 
        public List<CartItem> CartItems { get; set; } = new List<CartItem>(); // lista cartitems
    }
}