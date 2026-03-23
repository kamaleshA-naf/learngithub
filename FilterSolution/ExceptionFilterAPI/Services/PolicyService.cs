using AutoMapper;
using ExceptionFilterAPI.Interfaces;
using ExceptionFilterAPI.Models;
using ExceptionFilterAPI.Models.DTOs;

namespace ExceptionFilterAPI.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Policy> _policyRepository;
        private readonly IRepository<int, Insurance> _insuranceRepository;
        private readonly ILogger<PolicyService> _logger;
        private readonly IMapper _mapper;

        public PolicyService(IRepository<int,Customer> customerRepository,
            IRepository<int, Policy> policyRepository,
            IRepository<int, Insurance> insuranceRepository,
            ILogger<PolicyService> logger,
            IMapper mapper
            ) 
        {
            _customerRepository = customerRepository;
            _policyRepository = policyRepository;
            _insuranceRepository = insuranceRepository;
            _logger = logger;
            _mapper = mapper;

        }
        public async Task<AddPolicyResponseDto> AddPolicyAsync(AddPolicyRequestDto policy)
        {
            if (policy == null)
                throw new ArgumentNullException("Policy object is null");
            var insurance = await _insuranceRepository.GetByIdAsync(policy.InsuranceNumber);
            if(insurance == null)
                throw new ArgumentNullException("Insurance is not present");
            if (policy.CustomerId != null)
            {
                var customer = await _customerRepository.GetByIdAsync((int)policy.CustomerId);
                if (customer == null)
                    throw new Exception("No such customer");
            }
            if (policy.Customer != null)
            {
                //var customer = new Customer
                //{
                //    Name = policy.Customer.Name,
                //    DateOfBirth = policy.Customer.DateOfBirth,
                //    PhoneNumber = policy.Customer.Phone,
                //    Email = policy.Customer.Email,

                //};
                var customer = _mapper.Map<Customer>(policy.Customer);
                customer = await _customerRepository.AddAsync(customer);
                if (customer == null)
                    throw new Exception("Unable to add customer so unable to add policy");
                _logger.LogInformation("New customer added with Id " + customer.Id);
                policy.CustomerId = customer.Id;
            }
            var addedPolicy = await _policyRepository.AddAsync(new Policy
            {
                InsuranceNumber = policy.InsuranceNumber,
                CustomerId = (int)policy.CustomerId,
                StartDate = DateTime.Now,
            });
            if (addedPolicy == null)
                throw new Exception("Unable to add policy");
            _logger.LogInformation($"{policy.CustomerId}");
            return new AddPolicyResponseDto
            {
                PolicyNumber = addedPolicy.PolicyNumber,
                MarurityDate = DateTime.Now.AddMonths(insurance.ReturnDuration),
            };

        }

    }
}
