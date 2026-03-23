namespace ExceptionFilterAPI.Models.DTOs
{
    public class AddPolicyRequestDto
    {
        public int InsuranceNumber { get; set; }
        public int? CustomerId { get; set; }
        public AddCustomerDto? Customer { get; set; }
    }
}
