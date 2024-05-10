using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Entity.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

       public DbSet<Employee> Employees { get; set; } = null!;
    }
}
