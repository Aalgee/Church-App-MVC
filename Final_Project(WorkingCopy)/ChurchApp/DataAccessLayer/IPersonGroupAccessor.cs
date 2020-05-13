using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// interface for interacting with the PersonGroup Table
    /// </summary>
    public interface IPersonGroupAccessor
    {
        int DeletePersonGroupsByGroupID(string groupID);
    }
}
