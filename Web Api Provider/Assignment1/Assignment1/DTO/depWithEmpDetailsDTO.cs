namespace Assignment1.DTO
{
    public class depWithEmpDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> empNames { get; set; } = new List<string>();
    }
}
