using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodTruckNation.Core.AppServices
{
    public class ScheduleService : BaseService, IScheduleService
    {


        public ScheduleService(ILoggerFactory loggerFactory, IUnitOfWork uow, IDateTimeProvider dateTimeProvider,
            IFoodTruckRepository foodTruckRepository, ILocationRepository locationRepository, IScheduleRepository scheduleRepository)
            : base(loggerFactory, uow)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.foodTruckRepository = foodTruckRepository;
            this.locationRepository = locationRepository;
            this.scheduleRepository = scheduleRepository;
        }


        #region Member Variables

        private IDateTimeProvider dateTimeProvider;
        private IFoodTruckRepository foodTruckRepository;
        private ILocationRepository locationRepository;
        private IScheduleRepository scheduleRepository;

        #endregion


        public Schedule GetSchedule(int scheduleId)
        {
            return this.scheduleRepository.GetSchedule(scheduleId);
        }


        public Schedule GetSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found");

            return this.scheduleRepository.GetSchedule(scheduleId);
        }

        public List<Schedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            return this.scheduleRepository.GetSchedules(startDate, endDate);
        }


        public List<Schedule> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found");

            return this.scheduleRepository.GetSchedulesForFoodTruck(foodTruckId, startDate, endDate);

        }


        public List<Schedule> GetSchedulesForLocation(int locationId, DateTime startDate, DateTime endDate)
        {
            var location = this.locationRepository.GetLocation(locationId);
            if (location == null)
                throw new ObjectNotFoundException($"No location with the id {locationId} found");

            return this.scheduleRepository.GetSchedulesForLocation(locationId, startDate, endDate);
        }


        public Schedule AddFoodTruckSchedule(CreateFoodTruckScheduleCommand command)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(command.FoodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {command.FoodTruckId} found");


            var location = this.locationRepository.GetLocation(command.LocationId);
            if (location == null)
                throw new ObjectNotFoundException($"No location with the id {command.LocationId} found");

            // Create the new schedule object and add it to the food truck
            Schedule schedule = new Schedule(foodTruck, location, command.StartTime, command.EndTime);
            foodTruck.AddSchedule(schedule);

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return schedule;
        }


        public void DeleteFoodTruckSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found so the schedule could not be deleted");

            var schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
                throw new ObjectNotFoundException($"No schedule with the id {scheduleId} found so the schedule could not be deleted");

            schedule.CancelScheduledAppointment();

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();
        }

    }
}
