using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace APIExample.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDB _db;

        public EmployeesController(EmployeeDB db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllActive()
        {
            var employees = await _db.Employees.Where(t => t.IsActive).ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee is null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employee.Id },
                employee
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee inputEmployee)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee is null) return NotFound();

            employee.Name = inputEmployee.Name;
            employee.Age = inputEmployee.Age;
            employee.IsActive = inputEmployee.IsActive;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (await _db.Employees.FindAsync(id) is not Employee employee)
                return NotFound();

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
