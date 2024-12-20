using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspAssignmentII.Models;
namespace AspAssignmentII.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ShowName();
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
        public ActionResult InsertMethod()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertMethod(People_Details people)
        {
            InsertPeople ob = new InsertPeople();
            TempData["message"] = ob.InsertMethod(people);
            return View();
        }
        [HttpPost]
        public ActionResult SearchMethod(string searchItem)
        {
            SearchPeople ob = new SearchPeople();
            People_Details user = ob.SearchMethod(searchItem);
            if(user==null)
            {
                TempData["message"] = "User not found";
            }
            else
            {
                TempData["message"] = "User found";
                TempData["id"] = user.id;
                TempData["username"] = user.username;
                TempData["name"] = user.name;
                TempData["address"] = user.address;
            }
            return RedirectToAction("Index");
        }
        public void ShowName()
        {
            SearchPeople ob = new SearchPeople();
            DataSet ds = ob.GetName();
            List<SelectListItem> userlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                userlist.Add(new SelectListItem
                {
                    Text = dr["name"].ToString(),
                    Value = dr["id"].ToString()
                });
            }
            ViewBag.User = new SelectList(userlist, "Value", "Text");
        }

    }
}