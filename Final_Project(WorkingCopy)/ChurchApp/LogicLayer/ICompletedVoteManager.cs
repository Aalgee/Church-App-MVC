using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Completed Vote table
    /// </summary>
    public interface ICompletedVoteManager
    {
        int AddCompletedVote(CompletedVote completedVote);
        List<CompletedVoteVM> RetrieveAllCompletedVotes();
        CompletedVoteVM RetrieveCompletedVoteByCompletedVoteID(int completedVoteID);
        List<CompletedVoteVM> RetrieveCompletedVotesByElectionID(int electionID);
        bool EditCompletedVote(CompletedVote oldCompletedVote, CompletedVote newCompletedVote);
        bool DeleteCompletedVote(int completedVoteID);
    }
}
