using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebsiteApp.Models;

namespace WebsiteApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
