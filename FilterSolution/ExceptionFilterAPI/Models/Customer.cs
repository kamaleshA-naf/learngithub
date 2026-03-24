namespace ExceptionFilterAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string emailnumber { get; set; } = string.Empty;

        public ICollection<Policy>? Policies { get; set; }
    }
}
