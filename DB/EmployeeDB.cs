using Microsoft.EntityFrameworkCore;

namespace APIExample.DB
{
    public class EmployeeDB : DbContext
    {
        public EmployeeDB(DbContextOptions<EmployeeDB> options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Employee> Employees => Set<Employee>();
    }
}