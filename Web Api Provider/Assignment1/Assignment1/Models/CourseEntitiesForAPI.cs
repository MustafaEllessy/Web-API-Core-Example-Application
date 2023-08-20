using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Assignment1.Models
{
    public class CourseEntitiesForAPI:IdentityDbContext<ApplicationUser>
    {
        public CourseEntitiesForAPI()
        {

        }
        public CourseEntitiesForAPI(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
