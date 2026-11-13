using System.Collections.Generic;
using System.Web.Mvc;
using eedbackApp.Models;

namespace eedbackApp.Controllers
{
    public class FeedbackController : Controller
    {
        // Yahan hum temporarily application ki memory mein data save karenge
        private static List<Feedback> feedbackList = new List<Feedback>();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new Feedback());
        }

        [HttpPost]
        public ActionResult Submit(Feedback model)
        {
            if (ModelState.IsValid)
            {
                // Form submit hote hi data list mein save ho jayega
                feedbackList.Add(model);

                TempData["SuccessMessage"] = "Thank you! Your feedback has been submitted.";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        // Naya Action: Yahan se hum sabhi feedbacks dekh payenge
        [HttpGet]
        public ActionResult List()
        {
            // View ko saved list bhej rahe hain
            return View(feedbackList);
        }
    }
}