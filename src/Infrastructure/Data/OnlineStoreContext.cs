using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class OnlineStoreContext : DbContext
{
    public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tool> Tool { get; set; }
}
