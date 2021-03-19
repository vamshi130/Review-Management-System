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
    public class ReviewerController : Controller
    {
        reviewEntities ReviewEntities = new reviewEntities();
        // GET: Reviewer
        [Authorize(Roles = "user")]
        public ActionResult ReviewerDetails(int? empl)                //Review Assignments of a Reviewer
        {
            if (empl == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.List = ReviewEntities.ReviewAssignments.Where(m => m.Reviewer == empl && m.Status == true);
            Session["reviewerName"] = ReviewEntities.ReviewAssignments.Where(m => m.Reviewer == empl).Select(m => m.Employee1.FirstName).FirstOrDefault();
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult SubmissionScreen(int? empid, int? revid)    //Giving reviews to employees
        {
            if (empid == null || revid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SubRID = ReviewEntities.ReviewAssignments.Where(m => m.EmployeeId == empid && m.Reviewer == revid).Select(m => m.ReviewId).FirstOrDefault();
            ViewBag.ReviewerId = ReviewEntities.ReviewAssignments.Where(m => m.EmployeeId == empid && m.Reviewer == revid).Select(m => m.EmployeeId).FirstOrDefault();
            int num = ViewBag.SubRID;
            ViewBag.Agenda = ReviewEntities.ReviewCreations.Where(m => m.ReviewId == num).Select(m => m.Agenda).FirstOrDefault();
            int org = (int)Session["EmployeeId"];
            ViewBag.OrganisationIdF = ReviewEntities.Employees.Where(m => m.EmployeeId == org).Select(m => m.Id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult SubmissionScreen(ReviewSubmission obj)
        {
            try
            {
                obj.Created_On = DateTime.Now;
                obj.Created_by = (String)Session["reviewerName"];
                ReviewEntities.ReviewSubmissions.Add(obj);
                ReviewEntities.SaveChanges();
                var assignobj = ReviewEntities.ReviewAssignments.Single(m => m.EmployeeId == obj.EmployeeId && m.ReviewId == obj.ReviewId);
                assignobj.Status = false;
                ReviewEntities.Entry<ReviewAssignment>(assignobj).State = EntityState.Modified;
                obj.Created_On = DateTime.Now;
                ReviewEntities.SaveChanges();
                TempData["MessageS"] = "Review Saved Successfully";
                return RedirectToAction("ReviewerDetails", new { empl = Session["EmployeeId"] });
            }
            catch
            {
                ModelState.AddModelError("", "Invalid data");
                return View();
            }
        }

        [Authorize(Roles = "user,Admin")]

        //public ActionResult SubmissionDetails(int? sub)
        //{
        //    if (sub == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    IEnumerable<ReviewSubmission> reviewSubmissions = ReviewEntities.ReviewSubmissions.Where(m => m.ReviewId == sub);
        //    return View(reviewSubmissions);
        //}

        [Authorize(Roles = "user")]
        public ActionResult ReviewerSubmits()                        //List of reviewer review submissions
        {
            int rev = (int)Session["EmployeeId"];
            IEnumerable<ReviewSubmission> reviewerSubmissions = ReviewEntities.ReviewSubmissions.Where(m => m.ReviewerId == rev);
            return View(reviewerSubmissions);
        }
    }
}