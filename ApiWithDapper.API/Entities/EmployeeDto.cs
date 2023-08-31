namespace ApiWithDapper.API.Entities
{
    public class EmployeeDto
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int Age { get; set; }
    }
}
