using Assignment1.DTO;
using Assignment1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        CourseEntitiesForAPI course;
        public DepartmentController(CourseEntitiesForAPI _course)
        {
            course = _course;
        }
        [HttpGet("{id:int}", Name = "GetByIDName2")]
        public IActionResult GetByID(int id)
        {
            Department department = course.Departments.Include(x => x.Employees).FirstOrDefault(x => x.Id==id);
            if (department != null)
            {
                depWithEmpDetailsDTO dp=new depWithEmpDetailsDTO();
                dp.Id = department.Id;
                dp.Name = department.Name;
                dp.Description = department.Description;
                foreach (var item in department.Employees)
                {
                    dp.empNames.Add(item.Name);
                }
                return Ok(dp);
            }
            return BadRequest("the object not found");
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Department department = course.Departments.Include(x => x.Employees)
                .FirstOrDefault(x => x.Name == name);
            if (department != null)
            {
                depWithEmpDetailsDTO dp = new depWithEmpDetailsDTO();
                dp.Id = department.Id;
                dp.Name = department.Name;
                dp.Description = department.Description;
                foreach (var item in department.Employees)
                {
                    dp.empNames.Add(item.Name);
                }
                return Ok(dp);
            }
            return BadRequest("the object not found");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Department> departments = course.Departments.Include(x => x.Employees).ToList();

            if (departments.Count > 0)
            {
                List<depWithEmpDetailsDTO> deps = new List<depWithEmpDetailsDTO>();
                foreach (var item in departments)   
                {
                    List<string> localEmpNames = new List<string>();
                    foreach (var it in item.Employees)
                    {
                        localEmpNames.Add(it.Name);
                    }
                    depWithEmpDetailsDTO dp = new depWithEmpDetailsDTO()
                    { Id = item.Id, Name = item.Name, Description = item.Description,empNames= localEmpNames };
                    deps.Add(dp);
                }
                return Ok(deps);
            }
            return BadRequest("No department Found");
        }
        [HttpPut]
        public IActionResult Update(depWithEmpDetailsDTO depG)
        {
            if (ModelState.IsValid)
            {
                Department dep = course.Departments.FirstOrDefault(x => x.Id == depG.Id);
                if (dep != null)
                {
                    dep.Name = depG.Name;
                    dep.Description = depG.Description;
                    course.SaveChanges();
                    return StatusCode(204, "data saved");
                }
                return BadRequest("id not found");
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult Insert(depWithEmpDetailsDTO depG)
        {
            if (ModelState.IsValid)
            {
                Department dp = new Department();
                dp.Name=depG.Name;
                dp.Description=depG.Description;
                
                course.Add(dp);
                course.SaveChanges();
                return Created(Url.Link("GetByIDName2", new { id = depG.Id }), depG);

            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Department dep = course.Departments.FirstOrDefault(x => x.Id == id);
            if (dep != null)
            {
                try
                {
                    course.Remove(dep);
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
