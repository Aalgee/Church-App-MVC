using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MVCPresentationLayer.Controllers
{
    [Authorize(Roles = "Aministrator, Pastor, Manager")]
    public class DelegateController : Controller
    {
        IDelegateManager _delegateManager;
        
        /// <summary>
        /// no argument constructor
        /// </summary>
        public DelegateController()
        {
            _delegateManager = new DelegateManager();
        }

        /// <summary>
        /// displays all delegates
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public ActionResult Index(bool active = true)
        {
            ViewBag.Title = active ? "Active Delegates" : "Inactive Delegates";

            var electionDelegates = _delegateManager.RetrieveDelegatesByActive(active);

            ViewBag.Active = active;

            return View(electionDelegates);
        }

        /// <summary>
        /// displays a delegates details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Delegate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// creates a new delegate
        /// </summary>
        /// <returns></returns>
        // GET: Delegate/Create
        public ActionResult CreateDelegate()
        {
            return View();
        }

        /// <summary>
        /// creates a new delegate
        /// </summary>
        /// <param name="electionDelegate"></param>
        /// <returns></returns>
        // POST: Delegate/Create
        [HttpPost]
        public ActionResult CreateDelegate(ElectionDelegate electionDelegate)
        {
            try
            {
                _delegateManager.AddDelegate(electionDelegate);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// edits a delegate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Delegate/Edit/5
        public ActionResult EditDelegate(int id)
        {
            ViewBag.Title = "Delegate Editor";
            var theDelegate = _delegateManager.RetrieveDelegateByID(id);
            
            return View(theDelegate);
        }

        /// <summary>
        /// edits a delegate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDelegate"></param>
        /// <returns></returns>
        // POST: Delegate/Edit/5
        [HttpPost]
        public ActionResult EditDelegate(int id, ElectionDelegate newDelegate)
        {
            try
            {
                var oldDelegate = _delegateManager.RetrieveDelegateByID(id);
                _delegateManager.EditDelegate(oldDelegate, newDelegate);
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// updates a delegates pin
        /// </summary>
        /// <param name="delegateID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public ActionResult UpdateDelegatePin(int delegateID, string errorMessage = "")
        {
            ViewBag.Title = "Update Pin";
            ViewBag.ErrorMessage = errorMessage;
            var theDelegate = _delegateManager.RetrieveDelegateByID(delegateID);

            return View(theDelegate);
        }

        /// <summary>
        /// updates a delegates pin
        /// </summary>
        /// <param name="electionDelegate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDelegatePin(ElectionDelegate electionDelegate)
        {
            try
            {
                if(_delegateManager.RetrieveDelegateByPin(electionDelegate.Pin).DelegateID != 0)
                {

                    string errorMessage = "The Pin have chosen is already taken";
                    return RedirectToAction("UpdateDelegatePin", new { delegateID = electionDelegate.DelegateID, errorMessage = errorMessage });
                } else
                {
                    _delegateManager.EditDelegatePin(electionDelegate.DelegateID, electionDelegate.Pin);

                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception)
            {

                return View(_delegateManager.RetrieveDelegateByID(electionDelegate.DelegateID));
            }
        }

        /// <summary>
        /// deactivates or reactivates a delegate
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public ActionResult DeactivateReactivateDelegate(int delegateID)
        {
            bool isActive = true;
            if (_delegateManager.RetrieveDelegateByID(delegateID).Active)
            {
                _delegateManager.DeactivateDelegate(delegateID);
                isActive = true;
            }
            else
            {
                _delegateManager.ActivateDelegate(delegateID);
                isActive = false;
            }
            return RedirectToAction("Index", new { active = isActive });
        }
    }
}
