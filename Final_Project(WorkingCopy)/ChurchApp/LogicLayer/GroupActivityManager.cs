using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class GroupActivityManager : IGroupActivityManager
    {
        IGroupActivityAccessor _groupActivityAccessor;

        /// <summary>
        /// constructor for this class
        /// </summary>
        public GroupActivityManager()
        {
            _groupActivityAccessor = new GroupActivityAccessor();
        }

        /// <summary>
        /// deletes group activities bt group id
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public bool DeleteGroupActivitiesByGroupID(string groupID)
        {
            try
            {
                return _groupActivityAccessor.DeleteGroupActivitiesByGroupID(groupID) > 0;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Group Activities not deleted", ex);
            }
        }
    }
}
