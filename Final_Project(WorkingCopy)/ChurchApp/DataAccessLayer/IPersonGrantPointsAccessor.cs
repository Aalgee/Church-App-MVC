using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// interface for interacting with person grant points table
    /// </summary>
    public interface IPersonGrantPointsAccessor
    {
        int InsertPersonGrantPoints(PersonGrantPoints personGrantPoints);
        List<PersonGrantPointsVM> SelectAllPersonGrantPoints();
        PersonGrantPointsVM SelectPersonGrantPointsByPersonGrantPointsID(int personGrantPointsID);
        int UpdatePersonGrantPoints(PersonGrantPoints oldPersonGrantPoints, PersonGrantPoints newPersonGrantPoints);
        int DeletePersonGrantPoints(int personGrantPointsID);

    }
}
