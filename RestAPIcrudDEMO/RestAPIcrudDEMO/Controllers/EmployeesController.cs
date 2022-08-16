using Microsoft.AspNetCore.Mvc;
using RestAPIcrudDEMO.EmployeeData;
using RestAPIcrudDEMO.Models;
using System;

namespace RestAPIcrudDEMO.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Dependancy injection with constructor
        private IEmployeeData _employeeData; // Dependancy Injection

        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }
        
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);

            if(employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with ID: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult add (Employee employee)
        {
             _employeeData.AddEmployee(employee );
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);  
        }




        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);

            if(employee != null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok();
            }
            return NotFound($"Employee with ID: {id} was not found");
        }

        [HttpPatch]//edit
        [Route("api/[controller]/{id}")]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if(existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
                return Ok();
            }
            return Ok(employee);
        }

    }
}
