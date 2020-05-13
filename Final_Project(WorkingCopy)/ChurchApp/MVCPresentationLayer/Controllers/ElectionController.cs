using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MVCPresentationLayer.Controllers
{
    public class ElectionController : Controller
    {
        IElectionManager _electionManager;
        ICandidateManager _candidateManager;
        IUserManager _userManager;
        IDelegateManager _delegateManager;

        /// <summary>
        /// no argument constructor
        /// </summary>
        public ElectionController()
        {
            _electionManager = new ElectionManager();
            _candidateManager = new CandidateManager();
            _userManager = new UserManager();
            _delegateManager = new DelegateManager();
        }

        /// <summary>
        /// Shows the view associated with delegate login
        /// </summary>
        /// <returns></returns>
        public ActionResult DelegateLogIn(string errorMessage = "")
        {
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.Title = "Delegate Login for Election Vote";
            ViewBag.SubTitle = "Election Vote";
            ViewBag.LoginMessage = "Please Enter Pin Below.";

            return View();
        }

        /// <summary>
        /// Allows a delegate to start an election vote by entering their pin. This is needed
        /// for simple sign in for people who may not be tech savy
        /// </summary>
        /// <param name="electionDelegate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelegateLogIn(ElectionDelegate electionDelegate)
        {
            string errorMessage = "";

            try
            {
                electionDelegate = _delegateManager.RetrieveDelegateByPin(electionDelegate.Pin);
                if (electionDelegate.DelegateID == 0)
                {
                    errorMessage = "Could not recognize Pin.";
                    return RedirectToAction("DelegateLogIn", new { errorMessage = errorMessage });
                }
                else if (electionDelegate.HasVotedForElections)
                {
                    errorMessage = "You have already voted for grants";
                    return RedirectToAction("DelegateLogIn", new { errorMessage = errorMessage });
                }
                else 
                {
                    return RedirectToAction("Vote", new { delegateID = electionDelegate.DelegateID.ToString() });
                }
                
                
            }
            catch (Exception)
            {

                return View();
            }
        }

        /// <summary>
        /// The view used by delegates to vote for various elections
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public ActionResult Vote(string delegateID, string errorMessage = "")
        {
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.ElectionDelegateID = delegateID;
            var elections = _electionManager.RetrieveElectionsByActive(true);
            foreach (var e in elections)
            {
                e.Candidates = _candidateManager.RetrieveCandidateByElectionID(e.ElectionID);
            }
            ViewBag.Title = "Vote";
            return View(elections);
        }

        /// <summary>
        /// the post method used for election vote collection
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Vote(FormCollection formCollection)
        {
            string errorMessage = "";
            try
            {
                var elections = _electionManager.RetrieveElectionsByActive(true);
                var delegateID = formCollection[1].ToString();
                if (formCollection.Count != elections.Count + 2)
                {
                    errorMessage = "Please vote for all elections";
                    return RedirectToAction("Vote", new { delegateID = delegateID, errorMessage = errorMessage});
                }
                else
                {
                    
                    var oldDelegate = _delegateManager.RetrieveDelegateByID(Int32.Parse(delegateID));

                    var newDelegate = new ElectionDelegate
                    {
                        DelegateID = oldDelegate.DelegateID,
                        Active = oldDelegate.Active,
                        FirstName = oldDelegate.FirstName,
                        LastName = oldDelegate.LastName,
                        Pin = oldDelegate.Pin,
                        HasVotedForGrants = oldDelegate.HasVotedForGrants,
                        HasVotedForElections = true // mark that this delegate has voted for elections
                    };

                    // gives a vote to the designated candidate
                    for (int i = 2; i < formCollection.Count; i++)
                    {
                        var candidate = _candidateManager.RetrieveCandidateByCandidateID(Int32.Parse(formCollection[i]));
                        candidate.Votes += 1;
                        _candidateManager.EditCandidateVotes(candidate);
                    }

                    _delegateManager.EditDelegate(oldDelegate, newDelegate);
                    return RedirectToAction("VotingConfirmation");
                }

            }
            catch (Exception)
            {

                return View();
            }
        }

        /// <summary>
        /// Activates or deactivates an election
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ActivateDeactivateElection(int id)
        {
            var oldElection = _electionManager.RetrieveElectionByElectionID(id);
            var newElection = new Election()

            {
                Name = oldElection.Name,
                Description = oldElection.Description,
                Active = oldElection.Active ? false : true
            };
            _electionManager.EditElection(oldElection, newElection);
            return RedirectToAction("Index", new { active = oldElection.Active });
        }

        /// <summary>
        /// Displays all elections
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        // GET: Election
        public ActionResult Index(bool active = true)
        {
            ViewBag.Title = "Elections";
            ViewBag.Active = true;
            var elections = _electionManager.RetrieveElectionsByActive(active);
            return View(elections);
        }

        /// <summary>
        /// displays election details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Election/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Election Details";
            var election = _electionManager.RetrieveElectionByElectionID(id);
            
            election.Candidates = _candidateManager.RetrieveCandidateByElectionID(id);
            return View(election);
        }

        /// <summary>
        /// adds a candidate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: Candidate/Add
        public ActionResult AddCandidate(int id)
        {
            var candidates = _candidateManager.RetrieveCandidateByElectionID(id);
            ViewBag.Election = _electionManager.RetrieveElectionByElectionID(id);
            var users = _userManager.RetrieveUserListByActive();
            var candidateUsers = new List<User>();
            //var users = new List<User>();
            foreach(var c in candidates)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (c.PersonID == users[i].PersonID)
                    {
                        users.Remove(users[i]);
                    }
                }
            }
            return View(users);
        }

        /// <summary>
        /// Adds a candidate to a particular election
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="electionID"></param>
        /// <returns></returns>
        [ActionName("AddCandidateToElection")]
        public ActionResult AddCandidate(int personID, int electionID)
        {
            var candidate = new Candidate()
            {

                PersonID = personID,
                ElectionID = electionID,
                Active = true
            };
            _candidateManager.AddCandidate(candidate);

            return RedirectToAction("Details", new { id = electionID });
        }

        /// <summary>
        /// The delete candidate view
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public ActionResult DeleteCandidate(int candidateID)
        {
            var candidate = _candidateManager.RetrieveCandidateByCandidateID(candidateID);
            
            return View(candidate);
        }

        /// <summary>
        /// Delete candidate post method
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCandidate(CandidateVM candidate)
        {
            try
            {
                candidate = _candidateManager.RetrieveCandidateByCandidateID(candidate.CandidateID);
                

                _candidateManager.DeleteCandidate(candidate.CandidateID);
                
                return RedirectToAction("Details", new { id = candidate.ElectionID });
            }
            catch (Exception)
            {

                return View();
            }
        }

        /// <summary>
        /// displays a voting confirmation page
        /// </summary>
        /// <returns></returns>
        public ActionResult VotingConfirmation()
        {
            ViewBag.Title = "Thank You for Voting!";
            ViewBag.Message = "Click button bellow to proceed to next voter.";

            return View();
        }

        /// <summary>
        /// creates a new election
        /// </summary>
        /// <returns></returns>
        // GET: Election/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// creates a new election
        /// </summary>
        /// <param name="election"></param>
        /// <returns></returns>
        // POST: Election/Create
        [HttpPost]
        public ActionResult Create(Election election)
        {
            try
            {
                _electionManager.AddElection(election);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// edits an election
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Election/Edit/5
        public ActionResult Edit(int id)
        {
            var election = _electionManager.RetrieveElectionByElectionID(id);
            return View(election);
        }

        /// <summary>
        /// edits an election
        /// </summary>
        /// <param name="id"></param>
        /// <param name="election"></param>
        /// <returns></returns>
        // POST: Election/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Election election)
        {
            try
            {
                _electionManager.EditElection(_electionManager.RetrieveElectionByElectionID(id),election);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// deletes an election
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Election/Delete/5
        public ActionResult Delete(int id)
        {
            var election = _electionManager.RetrieveElectionByElectionID(id);
            return View(election);
        }

        /// <summary>
        /// deletes an election
        /// </summary>
        /// <param name="id"></param>
        /// <param name="election"></param>
        /// <returns></returns>
        // POST: Election/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Election election)
        {
            try
            {
                _electionManager.DeactivateElection(id);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
