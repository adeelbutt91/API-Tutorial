using API_Tutorial.Data;
using API_Tutorial.Models.DTO;
using API_Tutorial.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Tutorial.Controllers
{
  
    [Route("api/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApplicationDBContext DBContext { get; }
        public EmployeesController(ApplicationDBContext dBContext)
        {
            this.DBContext = dBContext;
        }

      
        [HttpGet("GetAllEmployees")]
        [Authorize]
        public IActionResult GetAllEmployees()
        {
            var Allemployees = DBContext.Employees.ToList();
            return Ok(Allemployees);
        }


        [HttpGet]
        [Route("GetEmployeeByID/{id:guid}")]
        public IActionResult GetEmployeeByID(Guid id)
        {
            var emp = DBContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }


        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(AddEmployeeDTO objEmp)
        {
            var objemployee = new Employee()
            {
                name = objEmp.name,
                age = objEmp.age,
                description = objEmp.description,
                email = objEmp.email,
                phone = objEmp.phone
            };

            DBContext.Employees.Add(objemployee);
            DBContext.SaveChanges();
            return Ok(objemployee);
        }

        [HttpPut]
        [Route("UpdateEmployee/{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, AddEmployeeDTO obj)
        {
            var Emp = DBContext.Employees.Find(id); if (Emp == null)
            {
                return NotFound();
            }

            Emp.name = obj.name;
            Emp.age = obj.age;
            Emp.description = obj.description;
            Emp.email = obj.email;
            Emp.phone = obj.phone;

            DBContext.Employees.Update(Emp);
            DBContext.SaveChanges();
            return Ok(Emp);
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var emp = DBContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound(id);
            }
            DBContext.Employees.Remove(emp);
            DBContext.SaveChanges();
            return Ok(emp);
        }
    }
}
