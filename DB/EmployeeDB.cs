using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

public class EmployeeDB : DbContext
{
    public EmployeeDB(DbContextOptions<EmployeeDB> options) : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
}