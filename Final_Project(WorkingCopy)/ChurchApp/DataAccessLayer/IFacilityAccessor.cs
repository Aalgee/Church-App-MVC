using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface foe interacting with the Facility Table
    /// </summary>
    public interface IFacilityAccessor
    {
        List<Facility> SelectAllFacilitiesByActive(bool active);
        int InsertFacility(Facility facility);
        int UpdateFacility(Facility oldFacility, Facility newFacility);
    }
}
