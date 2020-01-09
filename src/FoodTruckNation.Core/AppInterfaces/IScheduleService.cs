using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface IScheduleService
    {

        Result<Schedule> GetSchedule(int scheduleId);

        Result<Schedule> GetSchedule(int foodTruckId, int scheduleId);

        Result<List<Schedule>> GetSchedules(DateTime startDate, DateTime endDate);

        Result<List<Schedule>> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate);


        Result<List<Schedule>> GetSchedulesForLocation(int location, DateTime startDate, DateTime endDate);


        Result<Schedule> AddFoodTruckSchedule(CreateFoodTruckScheduleCommand command);


        Result<Schedule> UpdateFoodTruckSchedule(UpdateFoodTruckScheduleCommand command);


        Result DeleteFoodTruckSchedule(int foodTruckId, int scheduleId);

    }
}
