using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeDB>(opt => opt.UseInMemoryDatabase("EmployeeList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "API is running");

/*
app.MapGet("/employees", async (EmployeeDB db) =>
    await db.Employees.ToListAsync());

app.MapGet("/employees/active", async (EmployeeDB db) =>
    await db.Employees.Where(t => t.IsActive).ToListAsync());

app.MapGet("/employees/{id}", async (int id, EmployeeDB db) =>
    await db.Employees.FindAsync(id)
        is Employee employee
            ? Results.Ok(employee)
            : Results.NotFound());


app.MapPost("/employees", async (Employee employee, EmployeeDB db) =>
{
    db.Employees.Add(employee);
    await db.SaveChangesAsync();
    return Results.Created($"/employees/{employee.Id}", employee);
});

app.MapPut("/employees/{id}", async (int id, Employee inputEmployee, EmployeeDB db) =>
{
    var todo = await db.Employees.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputEmployee.Name;
    todo.Age = inputEmployee.Age;
    todo.IsActive = inputEmployee.IsActive;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/employees/{id}", async (int id, EmployeeDB db) =>
{
    if (await db.Employees.FindAsync(id) is Employee employee)
    {
        db.Employees.Remove(employee);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
*/

app.Run();