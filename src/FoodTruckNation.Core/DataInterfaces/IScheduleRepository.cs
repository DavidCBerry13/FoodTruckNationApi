using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface IScheduleRepository
    {

        public Task<Schedule> GetScheduleAsync(int scheduleId);

        public Task<IEnumerable<Schedule>> GetSchedulesAsync(DateTime startDate, DateTime endDate);

        public Task<IEnumerable<Schedule>> GetSchedulesForLocationAsync(int locationId, DateTime startDate, DateTime endDate);

        public Task<IEnumerable<Schedule>> GetSchedulesForFoodTruckAsync(int foodTruckId, DateTime startDate, DateTime endDate);

        public Task SaveAsync(Schedule schedule);


    }
}
