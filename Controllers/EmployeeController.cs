using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace APIExample.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDB _db;
        private readonly IHubContext<EmployeeHub> _hubContext;

        public EmployeesController(EmployeeDB db, IHubContext<EmployeeHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllActive()
        {
            var employees = await _db.Employees.Where(t => t.IsActive).ToListAsync();
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee is null) return NotFound();

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("EmployeeAdd", employee);

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employee.Id },
                employee
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee inputEmployee)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee is null) return NotFound();

            employee.Name = inputEmployee.Name;
            employee.Age = inputEmployee.Age;
            employee.IsActive = inputEmployee.IsActive;

            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("EmployeeUpdate", employee);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (await _db.Employees.FindAsync(id) is not Employee employee)
                return NotFound();

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("EmployeeDelete", id);

            return NoContent();
        }
    }
}
