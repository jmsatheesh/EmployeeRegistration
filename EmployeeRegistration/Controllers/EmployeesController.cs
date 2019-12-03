using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeRegistration.Models;
using System.Net.Http;
using Microsoft.Extensions.Options;
using EmployeeRegistration.ApiClient;

namespace EmployeeRegistration.Controllers
{
    public class EmployeesController : Controller
    {
        /// <summary>
        /// Read the config values
        /// </summary>
        private readonly IOptions<ApplicatonSettings> _config;
        public EmployeesController(IOptions<ApplicatonSettings> config)
        {
            this._config = config;
        }

        /// <summary>
        /// Used to talk to the web api for HTTPGet
        /// </summary>
        /// <param name="apiUrl">Api Url to communicate</param>
        /// <returns></returns>
        private HttpResponseMessage GetApi(string apiUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.Value.ApiUrl);
                    var responseTask = client.GetAsync(apiUrl);
                    responseTask.Wait();

                    return responseTask.Result;

                }
            }
            catch (Exception)
            {
                //log exception
                throw; 
            }
            
        }

        /// <summary>
        /// To get the employee details for the given Id
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <returns></returns>
        private Employee GetEmployeeById(int? id)
        {
            try
            {
                var result = GetApi($"api/Registration/GetEmployeeById/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();
                    return readTask.Result;
                }
                return null;
            }
            catch (Exception)
            {
                //log exception
                throw;
            }



        }

        /// <summary>
        /// Common method save the details used for HTTPPut and HTTPost
        /// </summary>
        /// <param name="apiUri">Api Url to communicate</param>
        /// <param name="employee">Employee entity for create/update</param>
        /// <param name="isCreate">Flag to determine for new record or not</param>
        /// <returns></returns>
        private async Task<bool> SaveRegistration(string apiUri, Employee employee, bool isCreate)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.Value.ApiUrl);
                    HttpResponseMessage response = null;
                    if (isCreate)
                        response = await client.PostAsJsonAsync(apiUri, employee);
                    else
                        response = await client.PutAsJsonAsync(apiUri, employee);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }

        /// <summary>
        /// Fetch all the employee details
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Employee> employeeList = Enumerable.Empty<Employee>();
                var result = GetApi("api/Registration/GetAllEmployee");

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                    readTask.Wait();
                    employeeList = readTask.Result;
                }

                return View(employeeList);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }


        /// <summary>
        /// Get a particular employee record based on the id
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <returns></returns>
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }


        /// <summary>
        /// To create view for a new Employee registration 
        /// </summary>
        /// <returns></returns>
        //// GET: Employees/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }

        /// <summary>
        /// To create a new record for employee registration
        /// </summary>
        /// <param name="employee">Employee modile to create a record </param>
        /// <returns></returns>
        // POST: Employees/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Number,Position,Address,PhoneNumber,Email")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await SaveRegistration("/api/Registration/CreateRegistration", employee, true))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }


        /// <summary>
        /// Employee registration edit view
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <returns></returns>
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }

        /// <summary>
        /// To update an existing Employee registration 
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <param name="employee">Employee model for updation</param>
        /// <returns></returns>
        // POST: Employees/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Number,Position,Address,PhoneNumber,Email")] Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    if (await SaveRegistration($"/api/Registration/UpdateRegistration/{id}", employee, false))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }

        /// <summary>
        /// Employee registration  Delete view
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
            catch (Exception)
            {
                //log exception
                throw;
            }

        }

        /// <summary>
        /// To delete a Employee registration 
        /// </summary>
        /// <param name="id">Employee registration Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.Value.ApiUrl);
                    var response = await client.DeleteAsync($"/api/Registration/DeleteRegistration/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
            

        }

    }
}
