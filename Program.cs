using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeDB>(opt => opt.UseInMemoryDatabase("EmployeeList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5163")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// SignalR
builder.Services.AddSignalR();

// Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<EmployeeHub>("/employeeHub");

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