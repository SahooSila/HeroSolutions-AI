using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AI_Series_Startup_Kit.Controllers
{
    public class HomeController : Controller
    {
        // Home Page
        public ActionResult Index()
        {
            return View();
        }

        // Admin Index
        public ActionResult admin_index()
        {
            return View();
        }

        // Admin - Image Validation Page
        public ActionResult image_validation()
        {
            return View();
        }

        // Admin - Gesture Management Page
        public ActionResult gesture_management()
        {
            return View();
        }

        // Admin - Audit Log Page
        public ActionResult audit_log()
        {
            return View();
        }


        // User Index
        public ActionResult user_index()
        {
            return View();
        }

        // User - Register Page
        public ActionResult register()
        {
            return View();
        }

        // User - Verify Page
        public ActionResult verify()
        {
            return View();
        }


        // Legal Document Verification
        public ActionResult document_verification()
        {
            return View();
        }

        //Quality Control Check
        public ActionResult quality_control_check()
        {
            return View();
        }

    }
}