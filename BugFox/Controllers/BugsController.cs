using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugFox.Models;
using BugFox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugFox.Controllers
{
    public class BugsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private BugsViewModel _bvm = new BugsViewModel();

        public BugsController(ApplicationDbContext db)
        {
            _db = db;
        }

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
                _bvm.SessionUser = sessionVerify;

            }

            _bvm.Users = _db.Users.ToList();
            _bvm.Bugs = _db.Bugs.ToList();
            return View(_bvm);
        }

        /// <summary>
        /// Recieves a Bug Item from the View and adds it to the database
        /// </summary>
        /// <param name="bug"></param>
        /// <returns>Index View</returns>
        [HttpPost]
        public IActionResult Index(Bug bug)
        {
            bug.CreatedOn = DateTime.Now;

            string sessionHash = HttpContext.Session.GetString("_UserSession");
            var sessionVerify = _db.Users.Where(u => u.SessionHash == sessionHash).FirstOrDefault();
            _bvm.SessionUser = sessionVerify;

            bug.SubmittedBy = _bvm.SessionUser.FirstName + " " + _bvm.SessionUser.LastName;
            bug.isResolved = false;
            _db.Bugs.Add(bug);
            _db.SaveChanges();

            _bvm.Users = _db.Users.ToList();
            _bvm.Bugs = _db.Bugs.ToList();
            return View(_bvm);
        }

        /// <summary>
        /// Marks a Bug Item as Resolved
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var bug = _db.Bugs.Where(x => x.Id == id).FirstOrDefault();
            bug.isResolved = true;
            _db.SaveChanges();

            _bvm.Users = _db.Users.ToList();
            _bvm.Bugs = _db.Bugs.ToList();
            return View("Index", _bvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
