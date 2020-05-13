using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    /// <summary>
    /// Interface methods for interacting with the user table in the database
    /// </summary>
    public interface IUserAccessor
    {
        int InsertNewlyRegisteredUser(User user, string passwordHash);
        User SelectUserByID(int userID);
        User AuthenticateUser(string username, string passwordHash);
        bool UpdatePasswordHash(int PersonID, string oldPasswordHash, string NewPasswordHash);
        List<string> SelectRolesByPersonID(int personID);
        List<User> SelectUsersByActive(bool Active = true);
        List<string> SelectGroupsByPersonID(int personID);

        List<string> SelectAllRoles();
        List<string> SelectAllGroups();
        int UpdatePerson(User oldUser, User newUser);
        int InsertPerson(User user);
        int ActivatePerson(int personID);
        int DeactivatePerson(int personID);
        int InsertOrDeletePersonRole(int personID, string role, bool delete = false);
        int InsertOrDeletePersonGroup(int personID, string group, bool delete = false);
        List<User> SelectUsersByActivity(int activityID);
        List<User> SelectUsersByGroupID(string groupID);
        List<User> SelectUnapprovedUsersByGroupID(string groupID);
        int UpdatePersonRoleAsApporved(int personID, string roleID);
        int InsertUnapprovedPersonRole(int personID, string roleID);
        List<string> SelectUnapprovedPersonRoles(int personID);
        List<User> SelectUnapprovedUsersByRoleID(string roleID);
        List<User> SelectUsersByRoleID(string roleID, bool isApproved = true, bool active = true);
        User getUserByEmail(string email);
    }
}
