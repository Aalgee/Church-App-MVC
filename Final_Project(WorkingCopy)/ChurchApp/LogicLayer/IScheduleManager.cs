using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the schedule table
    /// </summary>
    public interface IScheduleManager
    {
        List<PersonScheduleVM> RetrieveSchedule(int personID);
        bool EditActivitySchedule(ActivityVM oldSchedule, ActivityVM newSchedule);
        bool AddSchedule(Schedule schedule);
        bool DeactivateSchedule(int scheduleID);
        List<PersonScheduleVM> RetrieveUserScheduleByActivityID(int activityID, string activityType);
        List<ActivityVM> RetrieveAllUserSchedulesByUserIDAndType(int personID, string scheduleType);
        PersonScheduleVM RetrieveScheduleByID(int scheduleID);
    }
}
