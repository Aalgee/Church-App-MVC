using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Group table
    /// </summary>
    public interface IGroupManager
    {
        List<Group> RetrieveGroupsByPersonID(int personID);
        List<string> RetrieveGroupsByActivityID(int activityID);
        List<GroupActivityVM> RetrieveActivitiesByGroupID(string groupID);
        bool AddUnapprovedPersonGroup(int personID, string groupID);
        List<string> RetriveUnapprovedPersonGroups(int personID);
        bool EditPersonGroupAsApproved(int personID, string groupID);
        List<Group> RetrieveAllGroups();
        bool AddGroup(Group group);
        bool DeleteGroup(string groupID);
        Group RetrieveGroupByID(string groupID);
    }
}
