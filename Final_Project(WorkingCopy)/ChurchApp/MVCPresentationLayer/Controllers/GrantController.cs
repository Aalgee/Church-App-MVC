using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    public class GrantController : Controller
    {
        IGrantManager _grantManager;
        IGrantFundsManager _grantFundsManager;
        IDelegateManager _delegateManager;

        /// <summary>
        /// no argument constructor
        /// </summary>
        public GrantController()
        {
            _grantManager = new GrantManager();
            _grantFundsManager = new GrantFundsManager();
            _delegateManager = new DelegateManager();
        }


        /// <summary>
        /// displays grants
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        // GET: Grant
        public ActionResult Index(bool active = true)
        {
            ViewBag.Title = active ? "Active Grants" : "Inactive Grants";
            var grants = _grantManager.RetrieveGrantsByActive(active);
            var grantFunds = _grantFundsManager.RetrieveGrantFunds().Amount;
            ViewBag.Active = active;
            
            //ViewBag.Title = "Grants";
            ViewBag.GrantFunds = "Total Grant Funds $" + grantFunds.ToString("0.##");

            return View(grants);
        }

        /// <summary>
        /// allows a dlegate to login using only the predetermined Pin. This system was designed for people who are not
        /// tech savvy therefore it is ultra simplified
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public ActionResult DelegateLogin(string errorMessage = "")
        {
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.Title = "Delegate Login for Grant";
            ViewBag.SubTitle = "Grant Vote";
            ViewBag.LoginMessage = "Please Enter Pin Below.";
            

            return View();
        }

        /// <summary>
        /// allows a dlegate to login using only the predetermined Pin. This system was designed for people who are not
        /// tech savvy therefore it is ultra simplified
        /// </summary>
        /// <param name="electionDelegate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelegateLogin(ElectionDelegate electionDelegate)
        {
            string errorMessage = "";

            try
            {
                electionDelegate = _delegateManager.RetrieveDelegateByPin(electionDelegate.Pin);
                if (electionDelegate.DelegateID == 0)
                {
                    errorMessage = "Could Not Recognize Pin.";
                    return RedirectToAction("DelegateLogin", new { errorMessage = errorMessage });

                }
                
                else if (!electionDelegate.HasVotedForGrants)
                {
                    return RedirectToAction("Vote", new { delegateID = electionDelegate.DelegateID.ToString() });
                }
                else
                {
                    errorMessage = "You have already voted for grants";
                    return RedirectToAction("DelegateLogin", new { errorMessage = errorMessage});
                }
            }
            catch (Exception)
            {
                errorMessage = "Unable to Access Grant Voting.";
                return RedirectToAction("DelegateLogin", new { errorMessage = errorMessage });
            }
        }

        /// <summary>
        /// displays the actual voting page for any number of grants
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public ActionResult Vote(int delegateID)
        {
            ViewBag.Title = "Vote for Grants";
            ViewBag.ChosenAmount1 = "You have Chosen ";
            ViewBag.ChosenAmount2 = " Out of 8 total grants.";
            ViewBag.DelegateID = delegateID;
            var grants = _grantManager.RetrieveGrantsByActive(true);

            return View(grants);
        }

        /// <summary>
        /// displays the actual voting page for any number of grants
        /// </summary>
        /// <param name="grantVotes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Vote(FormCollection grantVotes)
        {
            int delegateID = Int32.Parse(grantVotes[0]);


            try
            {


                if (grantVotes.Count == 9)
                {
                    for (int i = 1; i < grantVotes.Count; i++)
                    {
                        var grant = _grantManager.RetrieveGrantByGrantID(Int32.Parse(grantVotes[i]));
                        grant.Points += 1;
                        _grantManager.EditGrantPointsByID(grant.GrantID, grant.Points);
                    }
                    var oldDelegate = _delegateManager.RetrieveDelegateByID(delegateID);
                    var newDelegate = new ElectionDelegate
                    {
                        DelegateID = oldDelegate.DelegateID,
                        FirstName = oldDelegate.FirstName,
                        LastName = oldDelegate.LastName,
                        Active = oldDelegate.Active,
                        HasVotedForElections = oldDelegate.HasVotedForElections,
                        HasVotedForGrants = true,
                        Pin = oldDelegate.Pin
                    };
                    _delegateManager.EditDelegate(oldDelegate, newDelegate);
                    
                    return RedirectToAction("GrantVoteConfirmation");
                }
                else
                {
                    return RedirectToAction("Vote", new { delegateID = delegateID });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("DelegateLogin");
            }
            
        }

        /// <summary>
        /// a confirmation after voting is completed successfully
        /// </summary>
        /// <returns></returns>
        public ActionResult GrantVoteConfirmation()
        {
            ViewBag.Title = "Thank You for Voting!";
            ViewBag.Message = "Click button bellow to proceed to next voter.";
            return View();
        }

        /// <summary>
        /// displays the details of the grant chosen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Grant/Details/5
        public ActionResult Details(int id)
        {
            var grants = _grantManager.RetrieveGrantByGrantID(id);
            return View(grants);
        }

        /// <summary>
        /// adds a grant
        /// </summary>
        /// <returns></returns>
        // GET: Grant/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a Grant";

            return View();
        }

        /// <summary>
        /// adds a grant
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        // POST: Grant/Create
        [HttpPost]
        public ActionResult Create(Grant grant)
        {
            try
            {
                _grantManager.AddGrant(grant);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// edits a grant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Grant/Edit/5
        public ActionResult Edit(int id)
        {
            var grant = _grantManager.RetrieveGrantByGrantID(id);
            return View(grant);
        }

        /// <summary>
        /// edits a grant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="grant"></param>
        /// <returns></returns>
        // POST: Grant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Grant grant)
        {
            try
            {
                var newGrant = new Grant
                {
                    Active = grant.Active,
                    AmountAskedFor = grant.AmountAskedFor,
                    AmountRecieved = grant.AmountRecieved,
                    Description = grant.Description,
                    GrantID = grant.GrantID,
                    GrantName = grant.GrantName,
                    Points = grant.Points
                };
                _grantManager.EditGrant(_grantManager.RetrieveGrantByGrantID(grant.GrantID), newGrant);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// deletes a grant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Grant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        /// <summary>
        /// deletes a grant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Grant/Delete/5
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

        /// <summary>
        /// deactivates or reactivates a grant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeactivateReactivateGrant(int id)
        {
            bool isActive = true;
            if (_grantManager.RetrieveGrantByGrantID(id).Active)
            {
                _grantManager.DeactivateGrant(id);
                isActive = true;
            }
            else
            {
                _grantManager.ReactivateGrant(id);
                isActive = false;
            }
            return RedirectToAction("Index", new { active = isActive});
        }
    }
}
