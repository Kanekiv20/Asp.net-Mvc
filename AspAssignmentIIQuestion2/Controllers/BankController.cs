using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspAssignmentIIQuestion2.Models;
using Bank1;
using ServiceBank;

namespace AspAssignmentIIQuestion2.Controllers
{
    public class BankController : Controller
    {
        private IBankOperation iBankOperation;
        public BankController(IBankOperation iBankOperation)
        {
            this.iBankOperation=iBankOperation;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(BankUser user)
        {
            TempData["message"] = this.iBankOperation.Signup(user);
            Session["Username"] = user.Username;
            Session["Password"] = user.Password;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(BankUser user)
        {
            string check =this.iBankOperation.Login(user);
            if (check.Equals("LogIn unsuccessful"))
            {
                TempData["message"] = "Log In Not Successful";
                return View();
            }
            else
            {
                Session["Username"] = user.Username;
                TempData["message"] = "Log In Successful";
                return RedirectToAction("Landing");
            }
        }
        [MyFilters]
        public ActionResult Landing()
        {
            return View();
        }
        [MyFilters]
        public ActionResult Logout()
        {
            if (Session["Username"] != null)
            {
                Session.Abandon();
                TempData["message"] = "You have been logged out";
                return RedirectToAction("Welcome");
            }
            else
            {
                TempData["message"] = "You are not logged in";
                return RedirectToAction("Welcome");
            }
        }
        [MyFilters]

        public ActionResult Balance()
        {
           decimal bal = this.iBankOperation.Balence(Session["Username"].ToString());
           TempData["message"] = "Balence is:Rs"+bal;
           return RedirectToAction("Landing");
            
        }
        [MyFilters]
        public ActionResult Transfer()
        {
            return View();
        }
        [MyFilters]
        [HttpPost]
        public ActionResult Transfer(BankUser user)
        {
            TempData["message"] = this.iBankOperation.Transfer(Session["Username"].ToString(), user.Username, user.Balance);
            return View();

        }
    }
}