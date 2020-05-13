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
    /// candidate manager methods used to interact with the data access layer
    /// </summary>
    public class CandidateManager : ICandidateManager
    {
        ICandidateAccessor _candidateAccessor;

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="candidateAccessor"></param>
        public CandidateManager(ICandidateAccessor candidateAccessor)
        {
            _candidateAccessor = candidateAccessor;
        }

        /// <summary>
        /// standard constructor
        /// </summary>
        public CandidateManager()
        {
            _candidateAccessor = new CandidateAccessor();
        }

        /// <summary>
        /// Adds a candidate to the data access layer
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public int AddCandidate(Candidate candidate)
        {
            int result;
            try
            {
                result = _candidateAccessor.InsertCandidate(candidate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Candidate not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// Deactivates a candidate
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public bool DeactivateCandidate(int candidateID)
        {
            bool result;
            try
            {
                result = 1 == _candidateAccessor.DeactivateCandidate(candidateID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Candidate not Deactivated", ex);
            }
            return result;
        }

        /// <summary>
        /// edits a candidate
        /// </summary>
        /// <param name="oldCandidate"></param>
        /// <param name="newCandidate"></param>
        /// <returns></returns>
        public bool EditCanadidate(Candidate oldCandidate, Candidate newCandidate)
        {
            bool result;
            try
            {
                result = 1 == _candidateAccessor.UpdateCandidate(oldCandidate, newCandidate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Candidate not Edited", ex);
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of candidates from the data access layer
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<CandidateVM> RetrieveCandidateByActive(bool active)
        {
            try
            {
                return _candidateAccessor.SelectCandidatesByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Retrieves a candidate from the data access layer by Candidate ID
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public CandidateVM RetrieveCandidateByCandidateID(int candidateID)
        {
            try
            {
                return _candidateAccessor.SelectCandidateByCandidateID(candidateID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Retrieves a candidate from the data access layer by Election ID
        /// </summary>
        /// <param name="electionID"></param>
        /// <returns></returns>
        public List<CandidateVM> RetrieveCandidateByElectionID(int electionID)
        {
            try
            {
                return _candidateAccessor.SelectCandidatesByElectionID(electionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// Deletes a candidate
        /// </summary>
        /// <param name="candidateID"></param>
        /// <returns></returns>
        public bool DeleteCandidate(int candidateID)
        {
            try
            {
                return 1 == _candidateAccessor.DeleteCandidate(candidateID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// edits a candidates votes
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public bool EditCandidateVotes(Candidate candidate)
        {
            try
            {
                return 1 == _candidateAccessor.UpdateCandidateVotes(candidate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Updated.", ex);
            }
        }
    }
}
