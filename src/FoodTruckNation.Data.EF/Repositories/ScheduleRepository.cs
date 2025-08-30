using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DavidBerry.Framework.Data;
using System.Threading.Tasks;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {

        public ScheduleRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;

            _baseQuery = _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck);
        }


        private readonly FoodTruckContext _foodTruckContext;
        private readonly IQueryable<Schedule> _baseQuery;


        public async Task<Schedule> GetScheduleAsync(int scheduleId)
        {
            var schedule = await _baseQuery
                .Where(s => s.ScheduleId >= scheduleId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return schedule;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesAsync(DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = await _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck)
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToListAsync();

            return foodTruckSchedules;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesForFoodTruckAsync(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = await _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck)
                .Where(s => s.FoodTruckId == foodTruckId)
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToListAsync();

            return foodTruckSchedules;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesForLocationAsync(int locationId, DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = await _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck)
                .Where(s => s.LocationId == locationId)
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToListAsync();

            return foodTruckSchedules;
        }

        public Task SaveAsync(Schedule schedule)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(schedule, EfExtensions.ConvertStateOfNode);
            return Task.CompletedTask;
        }
    }
}
