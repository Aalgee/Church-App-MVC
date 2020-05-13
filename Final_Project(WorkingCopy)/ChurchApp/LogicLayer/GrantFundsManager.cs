using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
    public class GrantFundsManager : IGrantFundsManager
    {
        IGrantFundsAccessor _grantFundsAccessor;

        /// <summary>
        /// full constructor
        /// </summary>
        /// <param name="grantFundsAccessor"></param>
        public GrantFundsManager(IGrantFundsAccessor grantFundsAccessor)
        {
            _grantFundsAccessor = grantFundsAccessor;
        }

        /// <summary>
        /// no argument constructor
        /// </summary>
        public GrantFundsManager()
        {
            _grantFundsAccessor = new GrantFundsAccessor();
        }

        /// <summary>
        /// Adds grant funds
        /// </summary>
        /// <param name="grantFunds"></param>
        /// <returns></returns>
        public int AddGrantFunds(GrantFunds grantFunds)
        {
            int result;
            try
            {
                result = _grantFundsAccessor.InsertGrantFunds(grantFunds);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant Funds not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// Edits grant funds
        /// </summary>
        /// <param name="oldGrantFunds"></param>
        /// <param name="newGrantFunds"></param>
        /// <returns></returns>
        public bool EditGrantFunds(GrantFunds oldGrantFunds, GrantFunds newGrantFunds)
        {
            bool result;
            try
            {
                result = 1 == _grantFundsAccessor.UpdateGrantFunds(oldGrantFunds, newGrantFunds);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Grant Funds not Edited", ex);
            }
            return result;
        }

        /// <summary>
        /// Retrieves all grant funds
        /// </summary>
        /// <returns></returns>
        public GrantFunds RetrieveGrantFunds()
        {
            try
            {
                return _grantFundsAccessor.SelectGrantFunds();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }
    }
}
