using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestLog.Models;

namespace TestLog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
       // public static List<User> users = new List<User>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChatHub()
        {
            ViewBag.Message = "Chat Page";

            return View();
        }

        //public ActionResult ChatHub()
        //{
        //    string name = User.Identity.Name;
        //    ViewBag.Message = "Chat Page";
        //    //var users = new List<User>
        //    //{
        //    //    new User {Name = name}
        //    //};

        //    users.Add(new User()
        //    {
        //        Name = name
        //    });

        //    return View(users);
        //}
    }
}