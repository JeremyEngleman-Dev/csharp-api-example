using Microsoft.EntityFrameworkCore;

public class EmployeeDB : DbContext
{
    public EmployeeDB(DbContextOptions<EmployeeDB> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
}
