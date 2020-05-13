using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        IActivityManager _activityManager;
        IUserManager _userManager;

        /// <summary>
        /// no argument constructor
        /// </summary>
        public ProfileController()
        {
            _activityManager = new ActivityManager();
            _userManager = new UserManager();
        }

        /// <summary>
        /// displays a persons profile page
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ActionResult PersonProfile(string email)
        {
            email = User.Identity.Name;
            ViewBag.Title = "Your Profile";
            var profile = new ProfileObject
            {
                Activities = _activityManager.RetrieveActivitiesByPersonID(_userManager.RetrieveUserIDFromEmail(email)),
                Groups = _userManager.RetrievePersonGroups(_userManager.RetrieveUserIDFromEmail(email)),
                Person = _userManager.RetrieveUserByID(_userManager.RetrieveUserIDFromEmail(email))
            };
            profile.Person.Roles = _userManager.RetrievePersonRoles(_userManager.RetrieveUserIDFromEmail(email));
            
            return View(profile);
        }

    }
}