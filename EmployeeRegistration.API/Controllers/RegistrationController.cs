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
            try
            {
                return _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                //Log exception
                return null;

            }

        }

        /// <summary>
        /// Retrives all employee by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetEmployeeById/{id}")]
        public Task<Employee> GetById(int id)
        {
            try
            {
                return _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                //Log exception
                return null;
            }

        }

        /// <summary>
        /// Create a new employee registration
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("CreateRegistration")]
        public async Task<Employee> CreateRegistration([FromBody] Employee objEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return null;

                objEmployee.ModifiedDate = DateTime.Now;
                _repository.Add(objEmployee);
                _repository.Save();
                return objEmployee;
            }
            catch (Exception ex)
            {
                //Log exception
                return null;
            }
        }



        /// <summary>
        /// Updates an existing registration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("UpdateRegistration/{id}")]
        public async Task<Employee> UpdateRegistration(int id, [FromBody] Employee objEmployee)
        {
            try
            {

                _repository.Update(objEmployee);
                _repository.Save();
                return objEmployee;


            }
            catch (Exception ex)
            {
                //Log exception
                return null;
            }

        }

        /// <summary>
        /// Delete an existing registration
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteRegistration/{id}")]
        public async Task<int> DeleteRegistration(int id)
        {
            try
            {
                _repository.Remove(id);
                _repository.Save();
                return id;

            }
            catch (Exception ex)
            {
                //Log exception
                return 0;
            }
        }
    }
}
