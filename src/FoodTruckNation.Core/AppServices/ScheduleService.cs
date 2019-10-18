using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Framework.Data;
using Framework.Utility;
using Framework.Exceptions;
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
            _dateTimeProvider = dateTimeProvider;
            _foodTruckRepository = foodTruckRepository;
            _locationRepository = locationRepository;
            _scheduleRepository = scheduleRepository;
        }


        #region Member Variables

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFoodTruckRepository _foodTruckRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IScheduleRepository _scheduleRepository;

        #endregion


        public Schedule GetSchedule(int scheduleId)
        {
            return _scheduleRepository.GetSchedule(scheduleId);
        }


        public Schedule GetSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found");

            return _scheduleRepository.GetSchedule(scheduleId);
        }

        public List<Schedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            return _scheduleRepository.GetSchedules(startDate, endDate);
        }


        public List<Schedule> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found");

            return _scheduleRepository.GetSchedulesForFoodTruck(foodTruckId, startDate, endDate);

        }


        public List<Schedule> GetSchedulesForLocation(int locationId, DateTime startDate, DateTime endDate)
        {
            var location = _locationRepository.GetLocation(locationId);
            if (location == null)
                throw new ObjectNotFoundException($"No location with the id {locationId} found");

            return _scheduleRepository.GetSchedulesForLocation(locationId, startDate, endDate);
        }


        public Schedule AddFoodTruckSchedule(CreateFoodTruckScheduleCommand command)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(command.FoodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {command.FoodTruckId} found");


            var location = _locationRepository.GetLocation(command.LocationId);
            if (location == null)
                throw new ObjectNotFoundException($"No location with the id {command.LocationId} found");

            // Create the new schedule object and add it to the food truck
            Schedule schedule = new Schedule(foodTruck, location, command.StartTime, command.EndTime);
            foodTruck.AddSchedule(schedule);

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return schedule;
        }


        public void DeleteFoodTruckSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id {foodTruckId} found so the schedule could not be deleted");

            var schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
                throw new ObjectNotFoundException($"No schedule with the id {scheduleId} found so the schedule could not be deleted");

            schedule.CancelScheduledAppointment();

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();
        }

    }
}
