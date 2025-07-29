namespace API_Tutorial.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required String Email { get; set; }
        public required String Password { get; set; }
    }
}