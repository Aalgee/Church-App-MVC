using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Person Group table
    /// </summary>
    public interface IPersonGroupManager
    {
        bool DeletePersonGroupsByGroupID(string groupID);
    }
}
