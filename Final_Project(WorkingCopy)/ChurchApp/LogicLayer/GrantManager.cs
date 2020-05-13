using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// This class holds methods that directly interact with the data access classes
    /// </summary>
    public class GrantManager : IGrantManager
    {
        IGrantAccessor _grantAccessor;

        /// <summary>
        /// Constructor used for testing purposes
        /// </summary>
        /// <param name="grantAccessor"></param>
        public GrantManager(IGrantAccessor grantAccessor)
        {
            _grantAccessor = grantAccessor;
        }

        /// <summary>
        /// the default constructor
        /// </summary>
        public GrantManager()
        {
            _grantAccessor = new GrantAccessor();
        }

        /// <summary>
        /// Adds a Grant to the database
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        public int AddGrant(Grant grant)
        {
            int result;
            try
            {
                result = _grantAccessor.InsertGrant(grant);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// Deactivates a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public bool DeactivateGrant(int grantID)
        {
            bool result;
            try
            {
                result = 1 == _grantAccessor.DeactivateGrant(grantID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant not Deactivated", ex);
            }
            return result;
        }

        /// <summary>
        /// Edits a grant
        /// </summary>
        /// <param name="oldGrant"></param>
        /// <param name="newGrant"></param>
        /// <returns></returns>
        public bool EditGrant(Grant oldGrant, Grant newGrant)
        {
            bool result;
            try
            {
                result = 1 == _grantAccessor.UpdateGrant(oldGrant, newGrant);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant not Edited", ex);
            }
            return result;
        }

        /// <summary>
        /// Retrieve all grants from the data access layer
        /// </summary>
        /// <returns></returns>
        public List<Grant> RetrieveAllGrants()
        {
            try
            {
                return _grantAccessor.SelectAllGrants();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Retrieve a grant by id from the data access layer
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public Grant RetrieveGrantByGrantID(int grantID)
        {
            try
            {
                return _grantAccessor.SelectGrantByGrantID(grantID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Retrieves active grants from data access layer
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Grant> RetrieveGrantsByActive(bool active)
        {
            try
            {
                return _grantAccessor.SelectGrantsByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Reactivates a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public bool ReactivateGrant(int grantID)
        {
            try
            {
                return 1 == _grantAccessor.ReactivateGrant(grantID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant not Reactivated", ex);
            }
        }

        /// <summary>
        /// Edits a grants points
        /// </summary>
        /// <param name="grantID"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool EditGrantPointsByID(int grantID, int points)
        {
            try
            {
                return 1 == _grantAccessor.UpdateGrantPointsByID(grantID, points);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Grant points not updated", ex);
            }
        }
    }
}
