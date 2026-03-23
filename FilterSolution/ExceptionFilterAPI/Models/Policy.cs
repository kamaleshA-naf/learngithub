namespace ExceptionFilterAPI.Models
{
    public class Policy
    {
        public int PolicyNumber { get; set; }
        public int CustomerId { get; set; }
        public int InsuranceNumber { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public Customer? Customer { get; set; }

        public Insurance? Insurance { get; set; }
    }
}
