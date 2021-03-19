using OrganisationEditing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace OrganisationEditing.Controllers
{
    [Authorize(Roles = "SuperUser")]
    public class OrganizationController : Controller
    {
        reviewEntities reviewEntities = new reviewEntities();

        //Organisation Crud Operations

        public ActionResult Index()                           //List of Organisations
        {
            IEnumerable<Organization> Organisation1 = reviewEntities.Organizations;
            return View(Organisation1);
        }
        public ActionResult Create()                          //Adding Organisaion
        {
            List<Country> countryList = reviewEntities.Countries.ToList();
            ViewBag.CountryList = new SelectList(countryList, "CountryId", "CountryName");
            return View();
        }
        public JsonResult GetStateList(int CountryId)
        {
            reviewEntities.Configuration.ProxyCreationEnabled = false;
            List<State> StateList = reviewEntities.States.Where(x => x.CountryId == CountryId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityList(int StateId)
        {
            reviewEntities.Configuration.ProxyCreationEnabled = false;
            List<City> CityList = reviewEntities.Cities.Where(x => x.StateId == StateId).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUserIdExist(String UserId)
        {
            return Json(!reviewEntities.Organizations.Any(m => m.UserId == UserId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(Organization model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Created_On = DateTime.Now;
                    reviewEntities.Organizations.Add(model);
                    model.Role = "Admin";
                    reviewEntities.SaveChanges();
                    TempData["Message1"] = "Your  " + model.Name + "  has been created Successfully ";
                    return RedirectToAction("Create");
                }
                catch
                {
                    ModelState.AddModelError("", "Invalid operation");

                    return View();
                }

            }
            else
                ModelState.AddModelError("", "Invalid data");

            return View();
        }
        public ActionResult Edit(int? id)                       //Editing Organisation
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = reviewEntities.Organizations.Find(id);
            ViewBag.CountryList = reviewEntities.Countries;
            ViewBag.StateList = reviewEntities.States;
            ViewBag.CityList = reviewEntities.Cities;
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(Organization model)
        {
            try
            {
                model.Role = "Admin";
                reviewEntities.Entry<Organization>(model).State = EntityState.Modified;
                reviewEntities.SaveChanges();
                TempData["Message1"] = "Your Order " + model.Name + "has been created Successfully ";
                int id = model.Id;
                return RedirectToAction("Edit", "Employee", new { id });
            }
            catch
            {

                ModelState.AddModelError("", "Invalid operation");

                return View();
            }
        }
        public ActionResult Delete(int id)                     //Deleting Organisation
        {
            try
            {
                var obj = reviewEntities.Organizations.Find(id);
                obj.Status = false;
                reviewEntities.Entry<Organization>(obj).State = EntityState.Modified;
                reviewEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                ModelState.AddModelError("", "Invalid operation");

                return View();
            }


        }


    }
}































//public ActionResult LoginAdmin()
//{
//    return View();
//}
//[HttpPost]
//public ActionResult LoginAdmin(Admin obj)
//{
//    var adminid = reviewEntities.Admins.Any(m => m.UserName == obj.UserName && m.Password == obj.Password);
//    if (adminid)
//    {
//        return RedirectToAction("Index", "Employee");
//    }
//    else
//    {
//        ModelState.AddModelError("Password", "Invalid username or password");
//        return View();
//    }
//}