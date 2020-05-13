using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Facility table
    /// </summary>
    public interface IFacilityManager
    {
        List<Facility> RetrieveAllFacilitiesByActive(bool active = true);
        bool AddFacility(Facility facility);
        bool EditFacility(Facility oldFacility, Facility newFacility);
    }
}
