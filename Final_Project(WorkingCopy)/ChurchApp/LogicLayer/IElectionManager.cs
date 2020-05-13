using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Election table
    /// </summary>
    public interface IElectionManager
    {
        int AddElection(Election election);
        List<ElectionVM> RetrieveElectionsByActive(bool active);
        ElectionVM RetrieveElectionByElectionID(int electionID);
        bool EditElection(Election oldElection, Election newElection);
        bool DeactivateElection(int electionID);
    }
}
