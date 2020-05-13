
using DataObjects;
using LogicLayer;
using MVCPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        
        IActivityManager _activityManager;
        IUserManager _userManager;
        IScheduleManager _scheduleManager;

        /// <summary>
        /// no argument constructor
        /// </summary>
        public ActivityController()
        {
            _activityManager = new ActivityManager();
            _userManager = new UserManager();
            _scheduleManager = new ScheduleManager();
        }

        /// <summary>
        /// Displays a list of all the Activities that are planned in the future
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ActivityList(bool active = true, string message = "")
        {
            ViewBag.Message = message;
            ViewBag.Title = active ? "Active Activities" : "Inactive Activities";
            ViewBag.IsActive = active;
            var activities = _activityManager.RetrieveAllActivitySchedulesByActive(active);
            
            return View(activities);
        }

        /// <summary>
        /// Displays a detail list of a chosen activity
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ActivityDetails(int activityID)
        {
            ActivityDetailsViewModel activity = new ActivityDetailsViewModel(); 
            activity.Activity = _activityManager.RetrieveActivityByID(activityID);
            activity.Attendees = _userManager.SelectUsersByActivityID(activityID);
            ViewBag.Title = activity.Activity.ActivityName + "Details";
            return View(activity);
        }

        /// <summary>
        /// Loads the edit activity view
        /// </summary>
        /// <param name="activityID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Administrator, Pastor")]
        public ActionResult EditActivity(int activityID)
        {
            var activity = _activityManager.RetrieveActivityByID(activityID);
            ViewBag.ActivityTypeList = _activityManager.RetrieveAllActivityTypes();
            ViewBag.Title = "Edit Activity";
            return View(activity);
        }

        /// <summary>
        /// updates an activity
        /// </summary>
        /// <param name="newActivity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditActivity(ActivityVM newActivity)
        {
            try
            {
                var oldActivity = _activityManager.RetrieveActivityByID(newActivity.ActivityID);
                _activityManager.EditActivity(oldActivity, newActivity);
                _scheduleManager.EditActivitySchedule(oldActivity, newActivity);
                return RedirectToAction("ActivityList", new { message = "Activity Updated."});
            }
            catch (Exception)
            {
                ViewBag.ActivityTypeList = _activityManager.RetrieveAllActivityTypes();
                return View();
            }
        }

        /// <summary>
        /// deactivates or reactivates an activity
        /// </summary>
        /// <param name="activityID"></param>
        /// <returns></returns>
        public ActionResult DeactivateReactivateActivity(int activityID)
        {
            try
            {
                if (_activityManager.RetrieveActivityByID(activityID).ActivityActive)
                {
                    _activityManager.DeactivateActivity(activityID);
                    return RedirectToAction("ActivityList", new { active = true });
                }
                else
                {
                    _activityManager.ActivateActivity(activityID);
                    return RedirectToAction("ActivityList", new { active = false });
                }
                
            }
            catch (Exception)
            {

                
            }
            return RedirectToAction("ActivityList");
        }

        /// <summary>
        /// loads the create view
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateActivity()
        {
            ViewBag.ActivityTypeList = _activityManager.RetrieveAllActivityTypes();
            return View();
        }

        /// <summary>
        /// creates an activity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateActivity(ActivityVM activity)
        {
            try
            {
                int activityID = _activityManager.AddActivity(activity);

                _activityManager.AddActivitySchedule(activityID, activity.Start, activity.End);

                return RedirectToAction("ActivityList", new { message = "Activity Created."});
            }
            catch (Exception)
            {
                ViewBag.ActivityTypeList = _activityManager.RetrieveAllActivityTypes();
                return View();
            }
            
        }

        /// <summary>
        /// displays a persons activities or the activities that are available for them to sign up for
        /// </summary>
        /// <param name="isPersonActivities"></param>
        /// <param name="personID"></param>
        /// <returns></returns>
        public ActionResult PersonActivities(string email, bool isPersonActivities = true, string errorMessage = "")
        {
            email = User.Identity.Name;
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.PersonID = email;
            ViewBag.IsPersonActivities = isPersonActivities;
            List<ActivityVM> activities;
            if (isPersonActivities)
            {
                var person = _userManager.RetrieveUserByID(_userManager.RetrieveUserIDFromEmail(email));
                activities = _activityManager.RetrieveActivitiesByPersonID(_userManager.RetrieveUserIDFromEmail(email));
                ViewBag.Title = person.FirstName + " " + person.LastName + "'s Activities";
            }
            else
            {
                ViewBag.Title = "Available Activities";
                activities = _activityManager.RetrieveAllActivitySchedulesByActive();
                var personActivities = _activityManager.RetrieveActivitiesByPersonID(_userManager.RetrieveUserIDFromEmail(email));
                foreach (var pa in personActivities)
                {
                    for (int i = 0; i < activities.Count; i++)
                    {
                        if (pa.ActivityID == activities[i].ActivityID)
                        {
                            activities.Remove(activities[i]);
                        }
                    }
                }
            }
            return View(activities);
        }

        /// <summary>
        /// allows a user to sign up for or cancel being signed up for an activity
        /// </summary>
        /// <param name="signUp"></param>
        /// <param name="activityID"></param>
        /// <param name="personID"></param>
        /// <returns></returns>
        public ActionResult ActivitySignUpCancel(bool signUp, int activityID, string email)
        {
            string errorMessage = "";
            bool isPersonActivity = false;
            if (signUp)
            {
                try
                {
                    _activityManager.AddPersonActivity(_userManager.RetrieveUserIDFromEmail(email), activityID);
                    isPersonActivity = false;
                }
                catch (Exception)
                {
                    errorMessage = "Unable to add activity";
                }
            }
            else
            {
                try
                {
                    _activityManager.DeletePersonActivity(_userManager.RetrieveUserIDFromEmail(email), activityID);
                    isPersonActivity = true;
                }
                catch (Exception)
                {
                    errorMessage = "Unable to delete activity";
                }
            }
            return RedirectToAction("PersonActivities", new { email = email, isPersonActivities = isPersonActivity , errorMessage = errorMessage });
        }
    }
}