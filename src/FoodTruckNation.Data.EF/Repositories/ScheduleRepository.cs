using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {

        public ScheduleRepository(FoodTruckContext context)
        {
            this.foodTruckContext = context;

            this.baseQuery = this.foodTruckContext.Schedules
                .Include(s => s.Location)
                .Include(s => s.FoodTruck);
        }


        private FoodTruckContext foodTruckContext;
        private IQueryable<Schedule> baseQuery;


        public Schedule GetSchedule(int scheduleId)
        {
            //var schedule = this.foodTruckContext.Schedules
            //    .Include(s => s.Location)
            //    .Include(s => s.FoodTruck)
            //    .Where(s => s.ScheduleId >= scheduleId)
            //    .AsNoTracking()
            //    .FirstOrDefault();

            var schedule = this.baseQuery
                .Where(s => s.ScheduleId >= scheduleId)
                .AsNoTracking()
                .FirstOrDefault();

            return schedule;
        }

        public List<Schedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            var foodTruckSchedules = this.foodTruckContext.Schedules
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
            var foodTruckSchedules = this.foodTruckContext.Schedules
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
            var foodTruckSchedules = this.foodTruckContext.Schedules
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
            throw new NotImplementedException();
        }
    }
}
