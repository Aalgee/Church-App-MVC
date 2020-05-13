using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class CompletedVoteManager : ICompletedVoteManager
    {
        ICompletedVoteAccessor _completedVoteAccessor;

        /// <summary>
        /// full constructor
        /// </summary>
        /// <param name="completedVoteAccessor"></param>
        public CompletedVoteManager(ICompletedVoteAccessor completedVoteAccessor)
        {
            _completedVoteAccessor = completedVoteAccessor;
        }

        /// <summary>
        /// no argument constructor
        /// </summary>
        public CompletedVoteManager()
        {
            _completedVoteAccessor = new CompletedVoteAccessor();
        }

        /// <summary>
        /// adds a completed vote
        /// </summary>
        /// <param name="completedVote"></param>
        /// <returns></returns>
        public int AddCompletedVote(CompletedVote completedVote)
        {
            int result;
            try
            {
                result = _completedVoteAccessor.InsertCompletedVote(completedVote);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Completed Vote not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// retrieves all completed votes
        /// </summary>
        /// <returns></returns>
        public List<CompletedVoteVM> RetrieveAllCompletedVotes()
        {
            try
            {
                return _completedVoteAccessor.SelectAllCompletedVotes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// retrieves a completed vote by id
        /// </summary>
        /// <param name="completedVoteID"></param>
        /// <returns></returns>
        public CompletedVoteVM RetrieveCompletedVoteByCompletedVoteID(int completedVoteID)
        {
            try
            {
                return _completedVoteAccessor.SelectCompletedVoteByCompletedVoteID(completedVoteID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// retrieves completed voted by election id
        /// </summary>
        /// <param name="electionID"></param>
        /// <returns></returns>
        public List<CompletedVoteVM> RetrieveCompletedVotesByElectionID(int electionID)
        {
            try
            {
                return _completedVoteAccessor.SelectCompletedVotesByElectionID(electionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// edits a completed vote
        /// </summary>
        /// <param name="oldCompletedVote"></param>
        /// <param name="newCompletedVote"></param>
        /// <returns></returns>
        public bool EditCompletedVote(CompletedVote oldCompletedVote, CompletedVote newCompletedVote)
        {
            bool result;
            try
            {
                result = 1 == _completedVoteAccessor.UdateCompletedVote(oldCompletedVote, newCompletedVote);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Candidate not Edited", ex);
            }
            return result;
        }

        /// <summary>
        /// deletes a completed vote
        /// </summary>
        /// <param name="completedVoteID"></param>
        /// <returns></returns>
        public bool DeleteCompletedVote(int completedVoteID)
        {
            bool result;
            try
            {
                result = 1 == _completedVoteAccessor.DeleteCompletedVote(completedVoteID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Completed Vote not Deleted", ex);
            }
            return result;
        }
    }
}
