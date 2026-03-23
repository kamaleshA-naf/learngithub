using AutoMapper;

namespace ExceptionFilterAPI.Misc
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Models.DTOs.AddCustomerDto, Models.Customer>();
        }
    }
}
