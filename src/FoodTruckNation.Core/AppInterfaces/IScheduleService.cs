using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface IScheduleService
    {

        Schedule GetSchedule(int scheduleId);

        Schedule GetSchedule(int foodTruckId, int scheduleId);

        List<Schedule> GetSchedules(DateTime startDate, DateTime endDate);

        List<Schedule> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate);


        List<Schedule> GetSchedulesForLocation(int location, DateTime startDate, DateTime endDate);


        Schedule AddFoodTruckSchedule(CreateFoodTruckScheduleCommand command);


        void DeleteFoodTruckSchedule(int foodTruckId, int scheduleId);

    }
}
