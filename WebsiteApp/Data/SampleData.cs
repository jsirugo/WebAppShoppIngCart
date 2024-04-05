using WebsiteApp.Models;

namespace WebsiteApp.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Brad"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will"
                });
              
            }
            if (!database.Products.Any())
            {
                database.Products.Add(new Product
                {
                    Name = "Chad Volvo 240",
                    Description = "Finaste bilen i Sverige",
                    Price = 300,
                    Category = "240 blazera däck",
                    ImagePath = "volvo240.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Virgin SAAB 900",
                    Description = "Vänd effektivt ditt mående på ända när du ser volvon susa förbi",
                    Price = 10,
                    Category = "900 bränn humör",
                    ImagePath = "saab900.jpg"
                });
            }

                database.SaveChanges();
        }
    }
}

