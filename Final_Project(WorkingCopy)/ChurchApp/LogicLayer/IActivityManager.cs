using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Activity table
    /// </summary>
    public interface IActivityManager
    {
        List<ActivityVM> RetrieveActivitiesByActivitySchedule(bool activitySchedule = true);
        List<ActivityVM> RetrieveActivitiesByPersonID(int personID, bool activitySchedule = true);
        ActivityVM RetrieveActivityByID(int activityID);
        bool DeletePersonActivity(int personID, int activityID);
        bool AddPersonActivity(int personID, int activityID);
        List<string> RetrieveAllActivityTypes();
        int AddActivity(ActivityVM activity);
        int AddActivitySchedule(int activityID, DateTime start, DateTime end);
        bool EditActivity(ActivityVM oldActivity, ActivityVM newActivity);
        bool DeleteGroupActivity(int activityID, string GroupID);
        List<GroupActivityVM> RetrieveAllGroupActivities();
        bool AddGroupActivity(int activityID, string groupID);
        List<ActivityVM> RetrieveActivitiesByScheduleType(int personID, string scheduleType);
        List<ActivityVM> RetrieveAllActivitySchedulesByActive(bool active = true);
        bool ActivateActivity(int activityID);
        bool DeactivateActivity(int activityID);
    }
}
