using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using System.ComponentModel.Design;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    public class GroupController : Controller
    {
        IGroupManager _groupManager;
        IPersonGroupManager _personGroupManager;
        IGroupActivityManager _groupActivityManager;
        IUserManager _userManager;

        /// <summary>
        /// no-argument constructor
        /// </summary>
        public GroupController()
        {
            _groupManager = new GroupManager();
            _personGroupManager = new PersonGroupManager();
            _groupActivityManager = new GroupActivityManager();
            _userManager = new UserManager();
        }

        /// <summary>
        /// shows a list of groups
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult GroupsList(string message = "")
        {
            var groups = _groupManager.RetrieveAllGroups();
            ViewBag.Message = message;
            ViewBag.Title = "Groups";
            return View(groups);
        }

        /// <summary>
        /// creates a new group
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGroup()
        {
            ViewBag.Title = "Create New Group";
            return View();
        }

        /// <summary>
        /// creates a new group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            ViewBag.Title = "Create New Group";
            try
            {
                if (_groupManager.AddGroup(group))
                {
                    return RedirectToAction("GroupsList", new { message = "Group Added Successfully"});
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View();
        }

        /// <summary>
        /// deletes a group
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult DeleteGroup(string groupID)
        {
            ViewBag.Title = "Delete Group";
            ViewBag.SubTitle = "Are you sure you want to delete this group?";
            var group = _groupManager.RetrieveGroupByID(groupID);
            return View(group);
        }

        /// <summary>
        /// deletes a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteGroup(Group group)
        {
            ViewBag.Title = "Delete Group";
            ViewBag.SubTitle = "Are you sure you want to delete this group?";
            try
            {
                _groupActivityManager.DeleteGroupActivitiesByGroupID(group.GroupID);
                _personGroupManager.DeletePersonGroupsByGroupID(group.GroupID);
                _groupManager.DeleteGroup(group.GroupID);
                

                return RedirectToAction("GroupsList", new { message = "Group deleted" });

                
            }
            catch (Exception)
            {

                return View(group);
            }
            return View(group);
        }

        /// <summary>
        /// displays a persons goups and the groups they have applied to join but have npt been approved for yet
        /// </summary>
        /// <param name="active"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult PersonGroups(bool active = true, string message = "")
        {
            ViewBag.Active = active;
            var email = User.Identity.Name;
            var groups = new List<Group>();
            ViewBag.Message = message;
            var pendingGroups = _groupManager.RetriveUnapprovedPersonGroups(_userManager.RetrieveUserIDFromEmail(email));
            ViewBag.PendingGroups = pendingGroups;
            if (active == true)
            {
                ViewBag.Title = "Your Ministries";
                ViewBag.Subtitle = "Ministries you are signed up for";
                groups = _groupManager.RetrieveGroupsByPersonID(_userManager.RetrieveUserIDFromEmail(email));
                
            }
            else
            {
                ViewBag.Title = "Available Ministries";
                ViewBag.Subtitle = "Ministries you can sign up for";
                groups = _groupManager.RetrieveAllGroups();
                var personGroups = _groupManager.RetrieveGroupsByPersonID(_userManager.RetrieveUserIDFromEmail(email));
                foreach (var pg in personGroups)
                {
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (pg.GroupID == groups[i].GroupID)
                        {
                            groups.Remove(groups[i]);
                        }
                    }
                }
                foreach (var pendg in pendingGroups)
                {
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if(pendg == groups[i].GroupID)
                        {
                            groups.Remove(groups[i]);
                        }
                    }
                }
            }

            return View(groups);
        }

        /// <summary>
        /// allows a user to sign up for or cancel membership to a group
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public ActionResult SignUpCancelGroup(string groupID, bool active)
        {
            string message = "";
            if (active)
            {
                _userManager.DeleteUserGroup(_userManager.RetrieveUserIDFromEmail(User.Identity.Name), groupID);
                message = groupID + " Ministry Canceled";
            }
            else
            {
                _groupManager.AddUnapprovedPersonGroup(_userManager.RetrieveUserIDFromEmail(User.Identity.Name), groupID);
                message = groupID + " Ministry Applied for";
            }

            return RedirectToAction("PersonGroups", new { message = message });
        }

        /// <summary>
        /// displays the people that are in a group and that are awaiting approval for the group
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="active"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult GroupPeople(string groupID, bool active = true, string message = "")
        {
            ViewBag.Title = groupID + "'s Personnel Page";
            ViewBag.Message = message;

            var groupPeople = new GroupMemberApplicantViewModel();

            ViewBag.MemberTitle = groupID + "'s Members";
            groupPeople.GroupMembers = _userManager.RetrieveUsersByGroupID(groupID);
            
            
            ViewBag.ApplicantTitle = groupID + "'s Applicants";
            groupPeople.GroupApplicants = _userManager.RetrieveUnapprovedUsersByGroupID(groupID);

            groupPeople.GroupID = groupID;

            return View(groupPeople);
        }

        /// <summary>
        /// allows user to approve a group applicant
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult ApproveApplicant(int personID, string groupID)
        {
            _groupManager.EditPersonGroupAsApproved(personID, groupID);
            return RedirectToAction("GroupPeople", new { groupID = groupID });
        }

        /// <summary>
        /// allows a user to deny an applicant
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult DenyApplicant(int personID, string groupID)
        {
            _userManager.DeleteUserGroup(personID, groupID);
            return RedirectToAction("GroupPeople", new { groupID = groupID });
        }
    }
}