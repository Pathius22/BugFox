using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BugFox.Models;
using BugFox.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BugFox.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardViewModel viewModel = new DashboardViewModel();

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
            viewModel.Bugs = _db.Bugs.ToList<Bug>();
            viewModel.Users = _db.Users.ToList<User>();
        }

        /// <summary>
        /// Get Request Validates User Session
        /// </summary>
        /// <returns>Home Dashboard if Valid Session.  UserVerification Index if Invalid Session</returns>
        public IActionResult Index()
        {
            string sessionHash = HttpContext.Session.GetString("_UserSession");

            if (sessionHash == null)
            {
                return Redirect("UserVerification/Index");
            }
            else
            {
                var sessionVerify = _db.Users.Where(u => u.SessionHash == sessionHash).FirstOrDefault();
                viewModel.SessionUser = sessionVerify;
            }

            viewModel.Bugs = _db.Bugs.ToList<Bug>();
            viewModel.Users = _db.Users.ToList<User>();
            return View(viewModel);
        }
    }
}
