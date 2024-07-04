using employee.Models;
using employee.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {

        IPKG_EMPLOYEE package;
        public employeeController(IPKG_EMPLOYEE package)
        {
            this.package = package;
        }
        [HttpGet]
        public IActionResult get_employees()
        {
            return Ok(package.get_employees());

        }

        [HttpPost("list")]

        public IActionResult add_employees(List<Employee> employee)
        {
            package.add_employees_list(employee);
            return Ok();

        }
        [HttpPost("contacts")]

        public IActionResult add_empadd_employees_with_contactsloyees(employeeWithContacts employeeWithContacts)
        {
            package.add_employees_with_contacts(employeeWithContacts);
            return Ok();

        }

        [HttpPost]

        public IActionResult add_employees(Employee employee)
        {
            package.add_employees(employee);
            return Ok();

        }
        [HttpDelete]

        public IActionResult delete_employees(int id)
        {
            package.delete_employees(id);
            return Ok();

        }
        [HttpPut]

        public IActionResult update_employees(Employee employee)
        {
            package.update_employees(employee);
            return Ok();

        }
        [HttpGet("profession")]
        public IActionResult get_profession()
        {
            return Ok(package.get_professions());

        }



    }
}
