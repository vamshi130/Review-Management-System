using OrganisationEditing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using OrganisationEditing.Extensions;
using System.Net;
using System.Web.Security;
namespace OrganisationEditing.Controllers
{
    public class LogincController : Controller
    {
        reviewEntities ReviewEntities = new reviewEntities();
        public object Id { get; private set; }      
      
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user obj)
        {
            FormsAuthentication.SetAuthCookie(obj.UserName,false);
            var adminid = ReviewEntities.Admins.Where(m => m.UserName== obj.UserName && m.Password == obj.Password).Select(x => x.id).FirstOrDefault(); ;
            if (adminid != 0)
            {
                return RedirectToAction("Index", "Organization");
            }

            var user = ReviewEntities.Organizations.Where(m => m.UserId == obj.UserName && m.Password == obj.Password).Select(x => x.Id).FirstOrDefault();           
            Session["Id"] = user;
            if (user != 0)
            {
                return RedirectToAction("Emplist","Employees", new { user });
            }

            var empl = ReviewEntities.Employees.Where(m => m.UserName == obj.UserName && m.Password == obj.Password).Select(x => x.EmployeeId).FirstOrDefault();
            Session["EmployeeId"] = empl;
            if (empl != 0)
            {

                return RedirectToAction("ReviewerDetails", "Reviewer",new { empl });
            }
            else
            {
                ModelState.AddModelError("Password", "invalid username or password");
                TempData["error"] = "username is invalid";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReviewEntities.Dispose();
            }
            base.Dispose(disposing);
        }
    }  
}





























//public ActionResult LoginEmp()
//{
//    return View();
//}
//[HttpPost]
//public ActionResult LoginEmp(Employee obj)
//{
//    var empl = ReviewEntities.Employees.Where(m => m.UserName == obj.UserName && m.Password == obj.Password).Select(x => x.EmployeeId).FirstOrDefault();
//    Session["EmployeeId"] = empl;
//    Session["reviewerName"] = ReviewEntities.ReviewAssignments.Where(m => m.Reviewer == obj.EmployeeId).Select(m => m.Employee1.FirstName).FirstOrDefault();
//    if (empl != 0)
//    {
//        return RedirectToAction("ReviewerDetails", new { empl });
//    }
//    else
//        return View();
//}