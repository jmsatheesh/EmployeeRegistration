using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeRegistration.API.Core;
using EmployeeRegistration.API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistration.API.Controllers
{
    [Route("/api/Registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _repository;


        public RegistrationController(IGenericRepository<Employee> repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Retrives all employee details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllEmployee")]
        public Task<IEnumerable<Employee>> GetAll()
        {
            return _repository.GetAllAsync();
        }

        /// <summary>
        /// Retrives all employee by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetEmployeeById/{id}")]
        public Task<Employee> GetById(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a new employee registration
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("CreateRegistration")]
        public async void Post([FromBody] Employee objEmployee)
        {
            objEmployee.ModifiedDate = DateTime.Now;
            _repository.Add(objEmployee);
            _repository.Save();
        }

        /// <summary>
        /// Updates an existing registration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("UpdateRegistration/{id}")]
        public void Put(int id, [FromBody] Employee objEmployee)
        {
            _repository.Update(objEmployee);
            _repository.Save();
        }

        /// <summary>
        /// Delete an existing registration
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteRegistration/{id}")]
        public async void Delete(int id)
        {
            _repository.Remove(id);
            _repository.Save();
            //await _unitOfWork.CompleteAsync();
        }
    }
}
