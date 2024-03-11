using CaliberMVC.DBContext;
using CaliberMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaliberMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDBConetxt _employee = new EmployeeDBConetxt();
        // GET: Employee
        public ActionResult Index(int page = 1, string search = null)
        {
            int pageSize = 5; // Number of records per page

            List<Employee> employeeList = _employee.GetAllEmployees(); // Assuming this returns a List<Employee>

            // Filter by search keyword if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                employeeList = employeeList
                    .Where(e =>
                        e.Name.Contains(search) ||
                        e.Email.Contains(search) ||
                        e.Address.Contains(search) ||
                        e.ExperienceLevel.Contains(search) ||
                        e.Gender.Contains(search) ||
                        e.DateOfBirth.ToString().Contains(search))
                    .ToList();
            }

            int totalRecords = employeeList.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            IEnumerable<Employee> employees = employeeList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(employees);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (employee != null)
            {
                var inserted = _employee.InsertEmployee(employee);
                if (inserted)
                {
                    return RedirectToAction("Index"); // Redirect to the "Index" action method
                }
                else
                {
                    return View(employee);
                }
                
            }
            else
            {
                return View(employee);
            }
        }

        public ActionResult Edit(int ID)
        {
            var employee = _employee.GetEmployee(ID).FirstOrDefault();
            if(employee != null)
            {
                return View(employee);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var inserted = _employee.InsertEmployee(employee);
                if (inserted)
                {
                    return RedirectToAction("Index"); // Redirect to the "Index" action method
                }
            }
            return RedirectToAction("Index");

        }


        public ActionResult Delete(int ID)
        {
            var employee = _employee.GetEmployee(ID).FirstOrDefault();
            return View(employee);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.IsDeleted = true;
                var inserted = _employee.InsertEmployee(employee);
                if (inserted)
                {
                    return RedirectToAction("Index"); // Redirect to the "Index" action method
                }
            }
            return RedirectToAction("Index");
        }



    }
}