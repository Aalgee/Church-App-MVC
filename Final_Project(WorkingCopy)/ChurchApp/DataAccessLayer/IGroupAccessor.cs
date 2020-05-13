using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// interface for interacting with the Group table
    /// </summary>
    public interface IGroupAccessor
    {
        List<Group> SelectGroupsByPersonID(int personID);
        List<string> SelectGroupsByActivityID(int activityID);
        List<GroupActivityVM> SelectActivitiesByGroupID(string groupID);
        int InsertUnapprovedPersonGroup(int personID, string groupID);
        List<string> SelectUnapprovedPersonGroups(int personID);
        int UpdatePersonGroupAsApproved(int personID, string groupID);
        List<Group> SelectAllGroups();
        int InsertGroup(Group group);
        int DeleteGroup(string groupID);
        Group SelectGroupByID(string groupID);
    }
}
