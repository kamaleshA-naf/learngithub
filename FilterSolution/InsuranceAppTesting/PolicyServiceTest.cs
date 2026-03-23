using AutoMapper;
using Castle.Core.Logging;
using ExceptionFilterAPI.Contexts;
using ExceptionFilterAPI.Interfaces;
using ExceptionFilterAPI.Models;
using ExceptionFilterAPI.Models.DTOs;
using ExceptionFilterAPI.Repositories;
using ExceptionFilterAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppTesting
{
    internal class PolicyServiceTest
    {
        IRepository<int, Customer> _customerRepository;
        IRepository<int, Policy> _policyRepository;

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<InsuranceContext> options = new DbContextOptionsBuilder<InsuranceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            InsuranceContext context = new InsuranceContext(options);
            _customerRepository = new Repository<int, Customer>(context);
            _policyRepository = new Repository<int, Policy>(context);
        }

        [Test]
        public async Task AddPolicySuccessTest()
        {
            //Arrange
            Insurance insurance = new Insurance
            {
                InsuranceNumber = 1,
                Name = "Test",
                PayingDuration = 120,
                ReturnDuration = 180
            };
            Mock<IRepository<int, Insurance>> _insuranceRepositoryMock =
               new Mock<IRepository<int, Insurance>>();
            _insuranceRepositoryMock.Setup(ir=>ir.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(insurance);

            Mock<ILogger<PolicyService>> loggerMock = new Mock<ILogger<PolicyService>>();

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Customer>(It.IsAny<AddCustomerDto>()))
                .Returns(new Customer
                {
                    Name = "Test",
                    PhoneNumber = "23444442234",
                    Email = "test@test.com",
                    DateOfBirth = new DateTime(2000, 3, 3)
                });

            IPolicyService policyService = new  PolicyService(
                _customerRepository, _policyRepository, 
                _insuranceRepositoryMock.Object,loggerMock.Object,mapperMock.Object);
            AddPolicyRequestDto newPolicy = new AddPolicyRequestDto
            {
                Customer = new AddCustomerDto
                {
                    Name = "Test",
                    Phone = "23444442234",
                    Email = "test@test.com",
                    DateOfBirth = new DateTime(2000, 3, 3)
                },
                InsuranceNumber = 1
            };
            //Action
            var result = await policyService.AddPolicyAsync(newPolicy);
            //Assert
            Assert.NotNull(result);
            Assert.That(result.MarurityDate.Year, Is.EqualTo(DateTime.Now.AddMonths(insurance.ReturnDuration).Year));

        }

        [TearDown] 
        public void TearDown() 
        { 
        }
    }
}
