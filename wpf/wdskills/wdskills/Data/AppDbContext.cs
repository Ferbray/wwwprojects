using Microsoft.EntityFrameworkCore;
using wdskills.Model;

namespace wdskills.Data
{
    public class AppDbContext : DbContext
    {
        internal DbSet<User> User => Set<User>();
        internal DbSet<Role> Role => Set<Role>();
        internal DbSet<Product> Product => Set<Product>();
        internal DbSet<Point> Roint => Set<Point>();
        internal DbSet<OrderProduct> OrderProduct => Set<OrderProduct>();
        internal DbSet<Order> Order => Set<Order>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
