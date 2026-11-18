using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCampusPortal.Controllers
{
    public class ServiceRequestController : Controller
    {
       


        ServiceRequestDAL dal = new ServiceRequestDAL();

        // GET: ServiceRequest
        public ActionResult Index()
        {
            return View(dal.GetAll());
        }

        // GET: ServiceRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                dal.Insert(request);

                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: ServiceRequest/UpdateStatus/1
        public ActionResult UpdateStatus(int id)
        {
            ViewBag.RequestId = id;

            return View();
        }

        // POST: ServiceRequest/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int id, string status)
        {
            dal.UpdateStatus(id, status);

            return RedirectToAction("Index");
        }
    }
}