namespace employee.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PersonId { get; set; }
        public int ProfessionId { get; set; }
        public string? Profession { get; set; }
    }
}
