namespace WebsiteApp.Models
{
    public class CartItem
    {
        public int Id { get; set; } // Primary key for the cart item

        public int CartId { get; set; } // Foreign key referencing the Cart table
        public Cart Cart { get; set; } // Navigation property for Cart

        public int ProductId { get; set; } // Foreign key referencing the Product table
        public Product Product { get; set; } // Navigation property for Product

        public int Quantity { get; set; } // Number of units of the product in the cart
    }
}
