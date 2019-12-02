using EmployeeRegistration.API.Controllers;
using EmployeeRegistration.API.Core;
using EmployeeRegistration.API.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace EmployeeRegistration.API.Test
{
    [TestClass]
    public class TestRegistrationController
    {
        private Mock<IGenericRepository<Employee>> _employeeMock;
        RegistrationController objController;
        List<Employee> listEmployee;
        Employee emp = new Employee();

        [TestInitialize]
        public void Initialize()
        {

            _employeeMock = new Mock<IGenericRepository<Employee>>();
            objController = new RegistrationController(_employeeMock.Object);
            listEmployee = new List<Employee>()
            {
                new Employee() {Id=1, Address="India", Email="TestUser1@gmail.com",ModifiedDate=DateTime.Now,
                    Name ="UserTest1", Number=1, PhoneNumber="001", Position="Developer" },
                new Employee() {Id=2, Address="India", Email="TestUser2@gmail.com",ModifiedDate=DateTime.Now,
                    Name ="UserTest2", Number=2, PhoneNumber="002", Position="Developer" } 
            };
            emp = new Employee()
            {
                Id = 1,
                Address = "India",
                Email = "TestUser1@gmail.com",
                ModifiedDate = DateTime.Now,
                Name = "UserTest1",
                Number = 1,
                PhoneNumber = "001",
                Position = "Developer"
            };
        }
         

        [TestMethod]
        public async Task Valid_Employee_Create()
        { 
            //Act
            var result = (Employee)await objController.CreateRegistration(emp);

            ////Assert 
            _employeeMock.Verify(m => m.Add(emp), Times.Once);
            Assert.AreEqual(emp, result); 

        }
        [TestMethod]
        public async Task Invalid_Employee_Create()
        {
            objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (Employee)await objController.CreateRegistration(emp);

            ////Assert
            Assert.AreEqual(null, result);  

        }

        [TestMethod]
        public async Task Valid_Employee_Update()
        {
            //Act
            var result = (Employee)await objController.UpdateRegistration(1, emp);

            ////Assert 
            _employeeMock.Verify(m => m.Update(emp), Times.Once);
            Assert.AreEqual(emp, result); 

        }
        

        [TestMethod]
        public async Task Valid_Employee_Delete()
        {
            //Act
            var result = (int)await objController.DeleteRegistration(1);

            ////Assert 
            _employeeMock.Verify(m => m.Remove(1), Times.Once);
            Assert.AreEqual(1, result); 

        }
        [TestMethod]
        public async Task Invalid_Employee_Delete()
        {
            objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (int)await objController.DeleteRegistration(-1000);

            ////Assert
            _employeeMock.Verify(m => m.Remove(-1000), Times.Once);
            Assert.AreEqual(-1000, result);

        }
    }
}
