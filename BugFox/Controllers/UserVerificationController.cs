using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugFox.Models;
using BugFox.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugFox.Controllers
{
    public class UserVerificationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private DashboardViewModel _dvm;

        public UserVerificationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Attempts to sign in a user, generate a new session hash, and send a new cookie
        /// </summary>
        /// <param name="user">Posted UserModel item from the Index View</param>
        /// <returns>Index View if Login failed or an error has occured. Dashboard/Index View if Login Successful</returns>
        [HttpPost]
        public IActionResult Index(User user)
        {
            // Check if Username already exists
            var userVerify = _db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
            if (userVerify != null)
            {
                if(user.Password == userVerify.Password)
                {
                    _dvm = new DashboardViewModel();
                    _dvm.Users = _db.Users.ToList();
                    _dvm.Bugs = _db.Bugs.ToList();

                    //Login Successful, Generate Session Hash for User
                    string sessionHash = System.Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("_UserSession", sessionHash);
                    userVerify.SessionHash = sessionHash;
                    _db.SaveChanges();

                    return Redirect("Home/Index");
                }
                // Return Error "Username or password is incorrect"
                return View();
            }

            // Return Error "Username or password is incorrect"
            return View();
        }

    /// <summary>
    /// Posts a UserModel item from the Signup View and provides verification logic to add new user to the database.
    /// </summary>
    /// <param name="user">Posted UserModel item from the Signup View</param>
    /// <returns>Signup View if Username is taken or an error has occured. Index View if Username is available </returns>
    [HttpPost]
        public IActionResult Signup(User user)
        {
            //Check if user exists and login
            Debug.WriteLine(user.Username);
            Debug.WriteLine(user.Password);

            // Check if Username already exists
            var userVerify = _db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
            if (userVerify != null)
            {
                // Return Error "Username already in use, try another"
                return View();
            }

            _db.Users.Add(user);
            _db.SaveChanges();
            return View("Index");
        }
    }
}
