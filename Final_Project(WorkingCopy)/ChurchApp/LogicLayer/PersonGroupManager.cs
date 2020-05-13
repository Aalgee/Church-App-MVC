using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class PersonGroupManager : IPersonGroupManager
    {
		IPersonGroupAccessor _personGroupAccessor;

		/// <summary>
		/// the no argument constructor
		/// </summary>
		public PersonGroupManager()
		{
			_personGroupAccessor = new PersonGroupAccessor();
		}

		/// <summary>
		/// Deletes a list of person groups by group
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
        public bool DeletePersonGroupsByGroupID(string groupID)
        {
			try
			{
				return _personGroupAccessor.DeletePersonGroupsByGroupID(groupID) > 0;
			}
			catch (Exception ex)
			{

				throw new ApplicationException("Person Groups not deleted", ex);
			}
        }
    }
}
