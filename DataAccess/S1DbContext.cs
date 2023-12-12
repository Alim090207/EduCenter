using Microsoft.EntityFrameworkCore;
using Service1.Entities;

namespace Service1.DataAccess
{
    public class S1DbContext : DbContext
    {
        public S1DbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
