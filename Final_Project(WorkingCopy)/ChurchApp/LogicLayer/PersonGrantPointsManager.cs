using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace LogicLayer
{
    class PersonGrantPointsManager : IPersonGrantPointsManager
    {
        IPersonGrantPointsAccessor _personGrantPointsAccessor;

        /// <summary>
        /// full constructor
        /// </summary>
        /// <param name="personGrantPointsAccessor"></param>
        public PersonGrantPointsManager(IPersonGrantPointsAccessor personGrantPointsAccessor)
        {
            _personGrantPointsAccessor = personGrantPointsAccessor;
        }

        /// <summary>
        /// no argument constructor
        /// </summary>
        public PersonGrantPointsManager()
        {
            _personGrantPointsAccessor = new PersonGrantPointsAccessor();
        }

        /// <summary>
        /// adds a persons grant points
        /// </summary>
        /// <param name="personGrantPoints"></param>
        /// <returns></returns>
        public int AddPersonGrantPoints(PersonGrantPoints personGrantPoints)
        {
            int result;
            try
            {
                result = _personGrantPointsAccessor.InsertPersonGrantPoints(personGrantPoints);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Person Grant Points not Added", ex);
            }
            return result;
        }

        /// <summary>
        /// Deletes a persons grant points
        /// </summary>
        /// <param name="personGrantPointsID"></param>
        /// <returns></returns>
        public bool DeletePersonGrantPoints(int personGrantPointsID)
        {
            bool result;
            try
            {
                result = 1 == _personGrantPointsAccessor.DeletePersonGrantPoints(personGrantPointsID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Person Grant Points not Deleted", ex);
            }
            return result;
        }

        /// <summary>
        /// Edits a person grant points
        /// </summary>
        /// <param name="oldPersonGrantPoints"></param>
        /// <param name="newPersonGrantPoints"></param>
        /// <returns></returns>
        public bool EditPersonGrantPoints(PersonGrantPoints oldPersonGrantPoints, PersonGrantPoints newPersonGrantPoints)
        {
            bool result;
            try
            {
                result = 1 == _personGrantPointsAccessor.UpdatePersonGrantPoints(oldPersonGrantPoints, newPersonGrantPoints);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Person Grant Points not Edited", ex);
            }
            return result;
        }

        /// <summary>
        /// retrieves a list of all person grant points
        /// </summary>
        /// <returns></returns>
        public List<PersonGrantPointsVM> RetrieveAllPersonGrantPoints()
        {
            try
            {
                return _personGrantPointsAccessor.SelectAllPersonGrantPoints();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }

        /// <summary>
        /// retrieves a person grant points by grant point id
        /// </summary>
        /// <param name="personGrantPointsID"></param>
        /// <returns></returns>
        public PersonGrantPointsVM RetrievePersonGrantPointsByPersonGantPointsID(int personGrantPointsID)
        {
            try
            {
                return _personGrantPointsAccessor.SelectPersonGrantPointsByPersonGrantPointsID(personGrantPointsID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not Found.", ex);
            }
        }
    }
}
