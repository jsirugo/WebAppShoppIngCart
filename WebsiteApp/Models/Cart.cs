namespace WebsiteApp.Models
{
    public class Cart
    {
        public int Id { get; set; } // Primary key for the cart

        public int AccountId { get; set; } // Foreign key referencing the Account table
        public Account Account { get; set; } // Navigation property for Account

        public List<CartItem> CartItems { get; set; } // Collection of CartItem objects
    }
}