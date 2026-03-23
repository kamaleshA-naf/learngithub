namespace ExceptionFilterAPI.Models
{
    public class Insurance
    {
        public int InsuranceNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PayingDuration { get; set; }
        public int ReturnDuration { get; set; }
        public float PayoutPerMonth { get; set; }
        public float Balance { get; set; }
        public ICollection<Policy>? Policies { get; set; }
    }
}
