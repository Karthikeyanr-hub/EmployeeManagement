using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementWebAPI.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];
        public async Task<ActionResult> Index()
        {
            IEnumerable<Employee> employees = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var result = await client.GetAsync("employees/get");

                    if (result.IsSuccessStatusCode)
                    {
                        employees = await result.Content.ReadAsAsync<IList<Employee>>();
                    }
                    else
                    {
                        employees = new List<Employee>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        throw new Exception(result.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAPIException(ex);
            }
            return View(employees);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var result = await client.GetAsync($"employees/details/{id}");

                    if (result.IsSuccessStatusCode)
                    {
                        employee = await result.Content.ReadAsAsync<Employee>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        throw new Exception(result.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAPIException(ex);
            }

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Address,Gender,Company,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseAddress);

                        var response = await client.PostAsJsonAsync("employees/Create", employee);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LogAPIException(ex);
                }
            }
            return View(employee);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var result = await client.GetAsync($"employees/details/{id}");

                    if (result.IsSuccessStatusCode)
                    {
                        employee = await result.Content.ReadAsAsync<Employee>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        throw new Exception(result.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAPIException(ex);
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Gender,Company,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseAddress);
                        var response = await client.PutAsJsonAsync("employees/edit", employee);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LogAPIException(ex);
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var result = await client.GetAsync($"employees/details/{id}");

                    if (result.IsSuccessStatusCode)
                    {
                        employee = await result.Content.ReadAsAsync<Employee>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        throw new Exception(result.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAPIException(ex);
            }

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var response = await client.DeleteAsync($"employees/delete/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAPIException(ex);
            }
            return View();
        }

        public async Task LogAPIException(Exception ex)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                await client.PostAsJsonAsync("employees/LogException", ex);
            }
        }


    }
}
