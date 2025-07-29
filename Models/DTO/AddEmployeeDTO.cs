namespace API_Tutorial.Models.DTO
{
    public class AddEmployeeDTO
    {
        public required string name { get; set; }
        public int age { get; set; }
        public string? description { get; set; }

        public string? email { get; set; } = null;

        public string? phone { get; set; }
    }
}
