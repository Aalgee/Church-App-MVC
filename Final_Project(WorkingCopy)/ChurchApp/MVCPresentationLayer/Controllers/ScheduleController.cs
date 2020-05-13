using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    public class ScheduleController : Controller
    {
        IScheduleManager _scheduleManager;
        
        /// <summary>
        /// no argument constructor
        /// </summary>
        public ScheduleController()
        {
            _scheduleManager = new ScheduleManager();
        }

        /// <summary>
        /// displays a persons schedule
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        // GET: Schedule
        public ActionResult PersonScheduleList(int personID = 1000000)
        {
            var schedule = _scheduleManager.RetrieveSchedule(personID);
            ViewBag.Title = "Your Schedule";
            return View(schedule);
        }

        /// <summary>
        /// displays schedule item details
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public ActionResult PersonScheduleDetails(int scheduleID)
        {
            var schedule = _scheduleManager.RetrieveScheduleByID(scheduleID);
            ViewBag.Title = "Details";
            return View(schedule);
        }
    }
}