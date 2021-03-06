﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface foe interacting with the Completed vote Table
    /// </summary>
    public interface ICompletedVoteAccessor
    {
        int InsertCompletedVote(CompletedVote completedVote);
        List<CompletedVoteVM> SelectAllCompletedVotes();
        CompletedVoteVM SelectCompletedVoteByCompletedVoteID(int completedVoteID);
        List<CompletedVoteVM> SelectCompletedVotesByElectionID(int electionID);
        int UdateCompletedVote(CompletedVote oldCompletedVote, CompletedVote newCompletedVote);
        int DeleteCompletedVote(int completedVoteID);
    }
}
