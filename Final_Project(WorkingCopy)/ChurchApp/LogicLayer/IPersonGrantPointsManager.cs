using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the PersonGrantPoints table
    /// </summary>
    interface IPersonGrantPointsManager
    {
        int AddPersonGrantPoints(PersonGrantPoints personGrantPoints);
        List<PersonGrantPointsVM> RetrieveAllPersonGrantPoints();
        PersonGrantPointsVM RetrievePersonGrantPointsByPersonGantPointsID(int personGrantPointsID);
        bool EditPersonGrantPoints(PersonGrantPoints oldPersonGrantPoints, PersonGrantPoints newPersonGrantPoints);
        bool DeletePersonGrantPoints(int personGrantPointsID);
    }
}
