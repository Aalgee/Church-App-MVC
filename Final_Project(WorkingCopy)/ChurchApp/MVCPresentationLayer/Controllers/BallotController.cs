using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    public class BallotController : Controller
    {
        IElectionManager _electionManager;
        ICandidateManager _candidateManager;
        Delegate _delegate;
        
        /// <summary>
        /// no argument constructor
        /// </summary>
        public BallotController()
        {
            _electionManager = new ElectionManager();
            _candidateManager = new CandidateManager();
        }


        public ActionResult Vote()
        {
            var elections = _electionManager.RetrieveElectionsByActive(true);
            foreach(var e in elections)
            {
                e.Candidates = _candidateManager.RetrieveCandidateByElectionID(e.ElectionID);
            }
            var ballot = new Ballot();
            ballot.Elections = new List<ElectionVM>();
            foreach(var e in elections)
            {
                ballot.Elections.Add(e);
            }
            ViewBag.Title = "Vote";
            return View(elections);
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ballot/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ballot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ballot/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ballot/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ballot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ballot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ballot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
