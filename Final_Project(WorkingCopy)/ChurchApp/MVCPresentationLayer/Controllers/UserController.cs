using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    [Authorize(Roles = "Adminstrator, Pastor, Administrator")]
    public class UserController : Controller
    {
        private IUserManager _userManager;

        /// <summary>
        /// no argument constructor
        /// </summary>
        public UserController()
        {
            _userManager = new UserManager();
        }

        /// <summary>
        /// the user index
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        // GET: User
        public ActionResult Index(bool active = true)
        {
            var users = new List<User>();
            ViewBag.Active = active;
            if (active)
            {
                ViewBag.Title = "Active Users";
                users = _userManager.RetrieveUserListByActive(true);
            }
            else
            {
                ViewBag.Title = "Inactive Users";
                users = _userManager.RetrieveUserListByActive(false);
            }
            
            return View(users);
        }

        /// <summary>
        /// shows a users details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "User Details";
            var user = _userManager.RetrieveUserByID(id);
            return View(user);
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <returns></returns>
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// creates a new user POST method
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                _userManager.AddUser(user);
                
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// edits a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: User/Edit/5
        public ActionResult EditUser(int id)
        {
            var user = _userManager.RetrieveUserByID(id);

            return View(user);
        }

        /// <summary>
        /// Edits a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newUser"></param>
        /// <returns></returns>
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditUser(int id, User newUser)
        {
            try
            {
                var oldUser = _userManager.RetrieveUserByID(id);
                _userManager.EditUser(oldUser, newUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// deactivates a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: User/Delete/5
        public ActionResult DeactivateUser(int id)
        {
            _userManager.SetUserActiveStatus(false, id);
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// deacticates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeactivateUser(int id, User user)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// reactivates a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReactivateUser(int id)
        {
            _userManager.SetUserActiveStatus(true, id);
            return RedirectToAction("Index", new { active = false });
        }
    }
}
