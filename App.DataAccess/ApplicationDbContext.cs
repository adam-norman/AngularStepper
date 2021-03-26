using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}