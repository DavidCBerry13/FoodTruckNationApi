using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface IScheduleService
    {

        public Task<Result<Schedule>> GetScheduleAsync(int scheduleId);

        public Task<Result<Schedule>> GetScheduleAsync(int foodTruckId, int scheduleId);

        public Task<Result<IEnumerable<Schedule>>> GetSchedulesAsync(DateTime startDate, DateTime endDate);

        public Task<Result<IEnumerable<Schedule>>> GetSchedulesForFoodTruckAsync(int foodTruckId, DateTime startDate, DateTime endDate);


        public Task<Result<IEnumerable<Schedule>>> GetSchedulesForLocationAsync(int location, DateTime startDate, DateTime endDate);


        public Task<Result<Schedule>> AddFoodTruckScheduleAsync(CreateFoodTruckScheduleCommand command);


        public Task<Result<Schedule>> UpdateFoodTruckScheduleAsync(UpdateFoodTruckScheduleCommand command);


        public Task<Result> DeleteFoodTruckScheduleAsync(int foodTruckId, int scheduleId);

    }
}
