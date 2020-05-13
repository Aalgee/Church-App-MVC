using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Interfaces for the Candidate manager class
    /// </summary>
    public interface ICandidateManager
    {
        int AddCandidate(Candidate candidate);
        List<CandidateVM> RetrieveCandidateByActive(bool active);
        CandidateVM RetrieveCandidateByCandidateID(int candidateID);
        List<CandidateVM> RetrieveCandidateByElectionID(int electionID);
        bool EditCanadidate(Candidate oldCandidate, Candidate newCandidate);
        bool EditCandidateVotes(Candidate candidate);
        bool DeactivateCandidate(int candidateID);
        bool DeleteCandidate(int candidateID);
    }
}
