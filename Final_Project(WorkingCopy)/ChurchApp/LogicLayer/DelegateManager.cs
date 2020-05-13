using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// delegate manager class
    /// </summary>
    public class DelegateManager : IDelegateManager
    {
        IDelegateAccessor _delegateAccessor;

        /// <summary>
        /// standar constructor
        /// </summary>
        public DelegateManager()
        {
            _delegateAccessor = new DelegateAccessor();
        }

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="delegateAccessor"></param>
        public DelegateManager(IDelegateAccessor delegateAccessor)
        {
            _delegateAccessor = delegateAccessor;
        }

        /// <summary>
        /// Activates a deactivated delegate
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public bool ActivateDelegate(int delegateID)
        {
            try
            {
                return 1 == _delegateAccessor.ActivateDelegate(delegateID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not activated", ex);
            }
        }

        /// <summary>
        /// Adds delegates to the data access layer
        /// </summary>
        /// <param name="electionDelegate"></param>
        /// <returns></returns>
        public bool AddDelegate(ElectionDelegate electionDelegate)
        {
            try
            {
                return 1 == _delegateAccessor.InsertDelegate(electionDelegate);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not added", ex);
            }
        }

        /// <summary>
        /// deactivates a delegate
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public bool DeactivateDelegate(int delegateID)
        {
            try
            {
                return 1 == _delegateAccessor.DeactivateDelegate(delegateID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not de-activated", ex);
            }
        }

        /// <summary>
        /// retrieves a delegate by ID
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public ElectionDelegate RetrieveDelegateByID(int delegateID)
        {
            try
            {
                return _delegateAccessor.SelectDelegateByID(delegateID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not Retrieved", ex);
            }
        }

        /// <summary>
        /// Retrieves a delegate by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ElectionDelegate> RetrieveDelegatesByActive(bool active)
        {
            try
            {
                return _delegateAccessor.SelectDelegatesByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not found", ex);
            }
        }

        /// <summary>
        /// Edits a delegate
        /// </summary>
        /// <param name="oldDelegate"></param>
        /// <param name="newDelegate"></param>
        /// <returns></returns>
        public bool EditDelegate(ElectionDelegate oldDelegate, ElectionDelegate newDelegate)
        {
            try
            {
                return 1 == _delegateAccessor.UpdateDelegate(oldDelegate, newDelegate);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not activated", ex);
            }
        }

        /// <summary>
        /// Retrieves a delegate by their pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public ElectionDelegate RetrieveDelegateByPin(string pin)
        {
            try
            {
                return _delegateAccessor.SelectDelegateByPin(pin);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not Retrieved", ex);
            }
        }

        /// <summary>
        /// Edits a delegates pin
        /// </summary>
        /// <param name="delegateID"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public bool EditDelegatePin(int delegateID, string pin)
        {
            try
            {
                return 1 == _delegateAccessor.UpdateDelegatePin(delegateID, pin);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delegate not updated", ex);
            }
        }
    }
}
