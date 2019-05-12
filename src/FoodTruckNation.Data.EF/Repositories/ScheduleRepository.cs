using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Framework;

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


        public Schedule GetSchedule(int scheduleId)
        {
            var schedule = _baseQuery
                .Where(s => s.ScheduleId >= scheduleId)
                .AsNoTracking()
                .FirstOrDefault();

            return schedule;
        }

        public List<Schedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck)
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToList();

            return foodTruckSchedules;
        }

        public List<Schedule> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = _foodTruckContext.Schedules
                .Include(s => s.Location)                
                .Include(s => s.FoodTruck)
                .Where(s => s.FoodTruckId == foodTruckId)                
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToList();

            return foodTruckSchedules;
        }

        public List<Schedule> GetSchedulesForLocation(int locationId, DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = _foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck)
                .Where(s => s.LocationId == locationId)
                .Where(s => s.ScheduledStart >= startDate)
                .Where(s => s.ScheduledEnd <= endDate)
                .AsNoTracking()
                .ToList();

            return foodTruckSchedules;
        }

        public void Save(Schedule schedule)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(schedule, EfExtensions.ConvertStateOfNode);
        }
    }
}
