using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface for interacting with the GroupActivities table
    /// </summary>
    public interface IGroupActivityAccessor
    {
        int DeleteGroupActivitiesByGroupID(string groupID);
    }
}
