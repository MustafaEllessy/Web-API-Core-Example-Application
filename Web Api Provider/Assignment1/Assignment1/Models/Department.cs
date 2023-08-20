using System.Text.Json.Serialization;

namespace Assignment1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public virtual List<Employee> Employees { get; set; }   
    }
}
