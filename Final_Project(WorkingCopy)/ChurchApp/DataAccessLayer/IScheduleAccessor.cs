using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface methods for interacting with the schedule table
    /// </summary>
    public interface IScheduleAccessor
    {
        List<PersonScheduleVM> SelectSchedulesByPersonID(int personID);
        int UpdateActivitySchedule(ActivityVM oldSchedule, ActivityVM newSchedule);
        int InsertSchedule(Schedule schedule);
        int DeactivateSchedule(int scheduleID);
        List<PersonScheduleVM> SelectUserScheduleByActivityID(int personID, string scheduleType, bool active);
        List<ActivityVM> SelectUserSchedulesByUserIDAndType(int personID, string scheduleType, bool active);
        PersonScheduleVM SelectScheduleByScheduleID(int scheduleID);
    }
}
