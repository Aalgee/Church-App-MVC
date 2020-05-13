using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface foe interacting with the Grant Table
    /// </summary>
    public interface IGrantAccessor
    {
        int InsertGrant(Grant grant);
        List<Grant> SelectAllGrants();
        List<Grant> SelectGrantsByActive(bool active);
        Grant SelectGrantByGrantID(int grantID);
        int UpdateGrant(Grant oldGrant, Grant newGrant);
        int UpdateGrantPointsByID(int grantID, int points);
        int DeactivateGrant(int grantID);
        int ReactivateGrant(int grantID);
    }
}
