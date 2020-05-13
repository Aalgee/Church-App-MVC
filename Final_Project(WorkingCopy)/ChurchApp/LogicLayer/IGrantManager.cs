using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Interface for grant manager methods
    /// </summary>
    public interface IGrantManager
    {
        /// <summary>
        /// Adds a grant to the data access layer
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        int AddGrant(Grant grant);

        /// <summary>
        /// Retrieves all grants from the data access layer
        /// </summary>
        /// <returns></returns>
        List<Grant> RetrieveAllGrants();

        /// <summary>
        /// Retrieves all active grants from the data access layer
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        List<Grant> RetrieveGrantsByActive(bool active);

        /// <summary>
        /// Retrieves a Grant by grant id from the data access layer
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        Grant RetrieveGrantByGrantID(int grantID);

        /// <summary>
        /// Edits a grant
        /// </summary>
        /// <param name="oldGrant"></param>
        /// <param name="newGrant"></param>
        /// <returns></returns>
        bool EditGrant(Grant oldGrant, Grant newGrant);

        /// <summary>
        /// edit a grants points
        /// </summary>
        /// <param name="grantID"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        bool EditGrantPointsByID(int grantID, int points);

        /// <summary>
        /// Deactivate a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        bool DeactivateGrant(int grantID);

        /// <summary>
        /// reactivate a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        bool ReactivateGrant(int grantID);
    }
}
