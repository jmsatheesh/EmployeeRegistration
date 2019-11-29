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
        private readonly IOptions<ApplicatonSettings> _config;
        public EmployeesController(IOptions<ApplicatonSettings> config)
        {
            this._config = config;
        }

        private HttpResponseMessage GetApi(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.Value.ApiUrl);
                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait();

                return responseTask.Result;

            }
        }

        private Employee GetEmployeeById(int? id)
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


        private async Task<bool> SaveRegistration(string apiUri, Employee employee, bool isCreate)
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
        public async Task<IActionResult> Index()
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



        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
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

        //// GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Number,Position,Address,PhoneNumber,Email")] Employee employee)
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

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Employees/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Number,Position,Address,PhoneNumber,Email")] Employee employee)
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

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

    }
}
