using OrganisationEditing.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganisationEditing.Controllers
{
    public class EmployeesController : Controller
    {

        reviewEntities ReviewEntities = new reviewEntities();
        // GET: Employees
        [Authorize(Roles = "Admin")]
        public ActionResult EmpList(int? user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["Name"] = ReviewEntities.Organizations.Where(m => m.Id == user).Select(n => n.Name).SingleOrDefault();
            IEnumerable<Employee> employees1 = ReviewEntities.Employees.Where(m => m.Id == user);
            return View(employees1);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateE()
        {
            ViewBag.Id = Session["Id"];
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateE(Employee data)                   //Adding employeess
        {
            data.Created_by = (string)Session["Name"];
            data.Created_On = DateTime.Now;

            try
            {
                data.Role = "user";
                ReviewEntities.Employees.Add(data);
                ReviewEntities.SaveChanges();
                TempData["MessageE"] = "Employee" + data.FirstName + "Added Succesfully";
                return RedirectToAction("CreateE", "Employees");
            }
            catch
            {
                ModelState.AddModelError("", "Invalid operation");

                return View();
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditE(int? id1)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = ReviewEntities.Employees.Find(id1);
            ViewBag.Id = Session["Id"];
            return View("CreateE", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditE(Employee model)                    //Edit employee
        {
            try
            {
                model.Role = "user";
                model.Created_by = (string)Session["Name"];
                model.Created_On = DateTime.Now;
                ReviewEntities.Entry<Employee>(model).State = EntityState.Modified;
                ReviewEntities.SaveChanges();
                TempData["MessageE"] = "Employee" + model.FirstName + "Added Succesfully";
                return RedirectToAction("EditE", "Employees", new { id1 = model.EmployeeId });
            }
            catch
            {
                ModelState.AddModelError("", "Invalid operation");
                return View();

            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteE(int? id1)                         //deleting employee
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var del = ReviewEntities.Employees.Find(id1);
                del.Status = "InActive";
                ReviewEntities.Entry<Employee>(del).State = EntityState.Modified;
                ReviewEntities.SaveChanges();
                return RedirectToAction("Emplist", "Employees", new { user = Session["Id"] });
            }
            catch
            {
                ModelState.AddModelError("", "Invalid operation");
                return View();
            }
        }
    }
}