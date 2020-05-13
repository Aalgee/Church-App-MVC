using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
    public class ElectionManager : IElectionManager
    {
        IElectionAccessor _electionAccessor;

        /// <summary>
        /// full constructor
        /// </summary>
        /// <param name="electionAccessor"></param>
        public ElectionManager(IElectionAccessor electionAccessor)
        {
            _electionAccessor = electionAccessor;
        }

        /// <summary>
        /// no argumrny constructor
        /// </summary>
        public ElectionManager()
        {
            _electionAccessor = new ElectionAccessor();
        }

        /// <summary>
        /// Adds an election
        /// </summary>
        /// <param name="election"></param>
        /// <returns></returns>
        public int AddElection(Election election)
        {
            int result;
            try
            {
                result = _electionAccessor.InsertElection(election);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Election not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// Deativates an election
        /// </summary>
        /// <param name="electionID"></param>
        /// <returns></returns>
        public bool DeactivateElection(int electionID)
        {
            bool result;
            try
            {
                result = 1 == _electionAccessor.DeactivateElection(electionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Election not Deactivated", ex);
            }
            return result;
        }

        /// <summary>
        /// retrieves an election by id
        /// </summary>
        /// <param name="electionID"></param>
        /// <returns></returns>
        public ElectionVM RetrieveElectionByElectionID(int electionID)
        {
            try
            {
                return _electionAccessor.SelectElectionByElectionID(electionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// retrieves a list of elections by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ElectionVM> RetrieveElectionsByActive(bool active)
        {
            try
            {
                return _electionAccessor.SelectElectionsByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// edits an election
        /// </summary>
        /// <param name="oldElection"></param>
        /// <param name="newElection"></param>
        /// <returns></returns>
        public bool EditElection(Election oldElection, Election newElection)
        {
            bool result;
            try
            {
                result = 1 == _electionAccessor.UpdateElection(oldElection, newElection);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Election not Edited", ex);
            }
            return result;
        }
    }
}
