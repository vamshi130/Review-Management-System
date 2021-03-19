using OrganisationEditing.Extensions;
using OrganisationEditing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganisationEditing.Controllers
{
    public class ReviewController : Controller
    {
        reviewEntities ReviewEntities = new reviewEntities();
        // GET: Review
        [Authorize(Roles = "Admin")]
        public ActionResult ReviewList(int? orgid)                    //List of Reviews
        {
            if (orgid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<ReviewCreation> reviews = ReviewEntities.ReviewCreations.Where(m => m.Id == orgid);
            return View(reviews);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateR()                                //Adding Review Agenda
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateR(ReviewCreation data)
        {
            try
            {
                data.Created_by = (string)Session["Name"];
                data.Created_On = DateTime.Now;
                ReviewEntities.ReviewCreations.Add(data);
                ReviewEntities.SaveChanges();
                TempData["MessageR"] = "Review Assigned Successfully";
                return RedirectToAction("CreateR", "Review");
            }
            catch
            {
                ModelState.AddModelError("", "invalid data");
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ReviewAssign(int? rev)                    //List of Review Assignments
        {
            if (rev == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = (int)Session["Id"];
            IEnumerable<ReviewAssignment> reviewAssignments = ReviewEntities.ReviewAssignments.Where(m => m.OrganisationId == id && m.ReviewId == rev);
            return View(reviewAssignments);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AssigningReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["ReviewId"] = id;
            int Orgid = (int)Session["Id"];
            ViewBag.AgendaName = ReviewEntities.ReviewCreations.Where(m => m.ReviewId == id).Select(m => m.Agenda).FirstOrDefault();
            ViewBag.ReviewIdnum = ReviewEntities.ReviewCreations.Where(m => m.ReviewId == id).Select(m => m.ReviewId).FirstOrDefault();
            ViewBag.EmployeeId = ReviewEntities.Employees.Where(m => m.Id == Orgid);
            return View();
        }                //Assinging Reviews with multiple selection
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AssigningReview(MultiAssignments data)
        {
            foreach (var item in data.EmployeeId)
            {
                ReviewAssignment arrayData = new ReviewAssignment();
                arrayData.EmployeeId = Convert.ToInt32(item);
                arrayData.Reviewer = data.Reviewer;
                arrayData.OrganisationId = data.OrganisationId;
                arrayData.ReviewId = data.ReviewId;
                arrayData.Created_On = DateTime.Now;
                arrayData.Status = data.Status;
                arrayData.Created_by = (string)Session["Name"];
                var user = ReviewEntities.ReviewAssignments.Where(m => m.EmployeeId == arrayData.EmployeeId && m.Reviewer == arrayData.Reviewer && m.ReviewId == arrayData.ReviewId).Select(m => m.id).FirstOrDefault();
                if ((user != 0) || ((arrayData.Reviewer == arrayData.EmployeeId)))
                {
                    this.AddNotification("Reviewer and employee not be the same or the The record is already submitted", NotificationType.ERROR);
                    return RedirectToAction("AssigningReview", "Review", new { id = Session["ReviewId"] });
                }
                else
                {
                    try
                    {
                        ReviewEntities.ReviewAssignments.Add(arrayData);
                        ReviewEntities.SaveChanges();
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Invalid operation");
                        return View();
                    }
                    //return RedirectToAction("AssigningReview", "Loginc", new { id = Session["ReviewId"] });
                }
            }
            TempData["MessageA"] = "Review Assigned Successfully";
            return RedirectToAction("AssigningReview", "Review", new { id = Session["ReviewId"] });
        }
        public ActionResult SubmissionDetails(int? sub)
        {
            if (sub == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<ReviewSubmission> reviewSubmissions = ReviewEntities.ReviewSubmissions.Where(m => m.ReviewId == sub);
            return View(reviewSubmissions);
        }
    }
}