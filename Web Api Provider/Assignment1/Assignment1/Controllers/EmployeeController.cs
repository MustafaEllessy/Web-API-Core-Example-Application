using Assignment1.DTO;
using Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        CourseEntitiesForAPI course;
        public EmployeeController(CourseEntitiesForAPI _course)
        { 
            course= _course;
        }
        [HttpGet("{id:int}",Name ="GetByIDName")]
        public IActionResult GetByID(int id) 
        {
            Employee employee= course.Employees.Include(x=>x.department.Name).FirstOrDefault(x => x.Id == id);
   
            if (employee != null) 
            {
                employeeWithDepartmentNameDTO dt = new employeeWithDepartmentNameDTO();
                dt.Id = employee.Id;
                dt.Name = employee.Name;
                dt.Address = employee.Address;
                dt.salary = employee.salary;
                dt.DepName = employee.department.Name;
                return Ok(dt);
            }
            return BadRequest("the object not found");
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Employee employee = course.Employees.FirstOrDefault(x => x.Name == name);
            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest("the object not found");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee>employees=course.Employees.ToList();
            if(employees.Count > 0)
            {
            return Ok(employees);
            }
            return BadRequest("No Employees Found");
        }
        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            if(ModelState.IsValid)
            {
                Employee employee=course.Employees.FirstOrDefault(x => x.Id==emp.Id);
                if(employee != null)
                {
                    employee.Address = emp.Address;
                    employee.Name = emp.Name;
                    employee.salary = emp.salary;
                    course.SaveChanges();
                    return StatusCode(204,"data saved");

                }
                return BadRequest("id not found");
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult Insert(Employee emp)
        {
            if(ModelState.IsValid) 
            {
                course.Add(emp);
                course.SaveChanges();
                return Created(Url.Link("GetByIDName",new { id=emp.Id}),emp);

            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
         Employee emp=course.Employees.FirstOrDefault(x=>x.Id==id);
            if(emp!= null)
            {
                try
                {
                    course.Remove(emp);
                    course.SaveChanges();
                    return StatusCode(204, "removed");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
           
            }
            return BadRequest("id not found");
            
        }


    }
}
