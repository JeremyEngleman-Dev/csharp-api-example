using APIExample.Authentication;

namespace APIExample.DB
{
    public static class DbSeeder
    {
        public static void Seed(EmployeeDB context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Id = 1, Name = "Admin" },
                    new Role { Id = 2, Name = "User" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Username = "John Smith",
                        Email = "jsmith@company.com",
                        PasswordHash = PasswordHasher.Hash("adminSecret"),
                        RoleId = 1
                    },
                    new User
                    {
                        Id = 2,
                        Username = "Ron Smith",
                        Email = "rsmith@company.com",
                        PasswordHash = PasswordHasher.Hash("userSecret"),
                        RoleId = 2
                    }
                );
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    new Employee { Id = 1, Name = "John Smith", Age = 45, IsActive = true },
                    new Employee { Id = 2, Name = "Ron Smith", Age = 36, IsActive = true },
                    new Employee { Id = 3, Name = "Lisa James", Age = 32, IsActive = false },
                    new Employee { Id = 4, Name = "Tina Johnson", Age = 27, IsActive = true }
                );
                context.SaveChanges();
            }
        }
    }
}