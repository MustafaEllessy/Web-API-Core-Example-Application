using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int salary { get; set; }
        [ForeignKey("department")]
        public int depID { get; set; }
        public virtual Department department { get; set; }
    }
}
