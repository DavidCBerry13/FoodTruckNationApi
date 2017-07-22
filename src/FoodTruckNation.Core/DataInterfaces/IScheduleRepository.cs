using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface IScheduleRepository
    {

        Schedule GetSchedule(int scheduleId);

        List<Schedule> GetSchedules(DateTime startDate, DateTime endDate);

        List<Schedule> GetSchedulesForLocation(int locationId, DateTime startDate, DateTime endDate);

        List<Schedule> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate);

        void Save(Schedule schedule);


    }
}
