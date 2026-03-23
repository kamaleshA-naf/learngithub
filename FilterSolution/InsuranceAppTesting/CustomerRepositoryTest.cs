using ExceptionFilterAPI.Contexts;
using ExceptionFilterAPI.Interfaces;
using ExceptionFilterAPI.Models;
using ExceptionFilterAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppTesting
{
    internal class CustomerRepositoryTest
    {
        IRepository<int, Customer> _repository;
        [SetUp]
        public void Setup()
        {
            DbContextOptions<InsuranceContext> options = new DbContextOptionsBuilder<InsuranceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            InsuranceContext context = new InsuranceContext(options);
            _repository = new Repository<int, Customer>(context);
        }

        [Test]
        public async Task AddCustomerTest()
        {
            //Arrange
            Customer customer = new Customer
            { Name = "Jane Doe", DateOfBirth = new DateTime(1992, 2, 2), Email = "Jane@gmail.com", PhoneNumber = "9876543210" };
            //Action
            customer = await _repository.AddAsync(customer);
            //Assert
            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.Id, Is.GreaterThan(0));
        }
        [Test]
        public async Task UpdateCustomerTest()
        {
            //Arrange
            Customer customer = new Customer
            { Name = "Jane Doe", DateOfBirth = new DateTime(1992, 2, 2), Email = "Jane@gmail.com", PhoneNumber = "9876543210" };
            customer = await _repository.AddAsync(customer);
            //Action
            customer.Name = "Jane Smith";
            var updatedCustomer = await _repository.UpdateAsync(customer.Id, customer);
            //Assert
            Assert.That(updatedCustomer, Is.Not.Null);
            Assert.That(updatedCustomer.Name, Is.EqualTo("Jane Smith"));
        }
        [Test]
        public async Task UpdateCustomerExceptionTest()
        {
            //Arrange
            Customer customer = new Customer
            {  Name = "Jane Doe", DateOfBirth = new DateTime(1992, 2, 2), Email = "Jane@gmail.com", PhoneNumber = "9876543210" };
            customer = await _repository.AddAsync(customer);
            Customer changeCustomer = new Customer
            { Id = customer.Id+10 ,Name = "Jane Doe", DateOfBirth = new DateTime(1992, 2, 2), Email = "Jane@gmail.com", PhoneNumber = "9876543210" };
            //Action + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.UpdateAsync(changeCustomer.Id, changeCustomer));

        }
    }
}
                
    
