using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> empNames { get; set; } = new List<string>() { };
    }
}