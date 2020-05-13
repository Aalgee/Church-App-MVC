using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the GroupActivity table
    /// </summary>
    public interface IGroupActivityManager
    {
        bool DeleteGroupActivitiesByGroupID(string groupID);
    }
}
