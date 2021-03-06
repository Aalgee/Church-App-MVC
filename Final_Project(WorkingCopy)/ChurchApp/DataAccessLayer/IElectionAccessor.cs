﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface foe interacting with the Election Table
    /// </summary>
    public interface IElectionAccessor
    {
        int InsertElection(Election election);
        List<ElectionVM> SelectElectionsByActive(bool active);
        ElectionVM SelectElectionByElectionID(int electionID);
        int UpdateElection(Election oldElection, Election newElection);
        int DeactivateElection(int electionID);
    }
}
