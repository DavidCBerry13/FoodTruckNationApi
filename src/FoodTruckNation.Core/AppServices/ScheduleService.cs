using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.TimeAndDate;
using DavidBerry.Framework.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DavidBerry.Framework.Functional;

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


        public Result<Schedule> GetSchedule(int scheduleId)
        {
            var schedule = _scheduleRepository.GetSchedule(scheduleId);
            return ( schedule != null )
                ? Result.Success<Schedule>(schedule)
                : Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {scheduleId}"));
        }


        public Result<Schedule> GetSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {foodTruckId}"));

            var schedule = _scheduleRepository.GetSchedule(scheduleId);
            return ( schedule != null )
                ? Result.Success<Schedule>(schedule)
                : Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {scheduleId}"));
        }

        public Result<List<Schedule>> GetSchedules(DateTime startDate, DateTime endDate)
        {
            var schedules = _scheduleRepository.GetSchedules(startDate, endDate);
            return Result.Success<List<Schedule>>(schedules);
        }


        public Result<List<Schedule>> GetSchedulesForFoodTruck(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {foodTruckId}"));

            var schedules = _scheduleRepository.GetSchedulesForFoodTruck(foodTruckId, startDate, endDate);
            return Result.Success<List<Schedule>>(schedules);
        }


        public Result<List<Schedule>> GetSchedulesForLocation(int locationId, DateTime startDate, DateTime endDate)
        {
            var location = _locationRepository.GetLocation(locationId);
            if (location == null)
                return Result.Failure<List<Schedule>>(new ObjectNotFoundError($"No location with the id {locationId} found"));

            var schedules = _scheduleRepository.GetSchedulesForLocation(locationId, startDate, endDate);
            return Result.Success<List<Schedule>>(schedules);
        }


        public Result<Schedule> AddFoodTruckSchedule(CreateFoodTruckScheduleCommand command)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(command.FoodTruckId);
            if (foodTruck == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {command.FoodTruckId}"));

            var location = _locationRepository.GetLocation(command.LocationId);
            if (location == null)
                return Result.Failure<Schedule>(new InvalidDataError($"No location with the id {command.LocationId} found"));

            // Create the new schedule object and add it to the food truck
            Schedule schedule = new Schedule(foodTruck, location, command.StartTime, command.EndTime);
            foodTruck.AddSchedule(schedule);

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<Schedule>(schedule);
        }



        public Result<Schedule> UpdateFoodTruckSchedule(UpdateFoodTruckScheduleCommand command)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(command.FoodTruckId);
            if (foodTruck == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {command.FoodTruckId}"));

            var location = _locationRepository.GetLocation(command.LocationId);
            if (location == null)
                return Result.Failure<Schedule>(new InvalidDataError($"No location with the id {command.LocationId} found"));

            Schedule schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == command.ScheduleId);
            if (schedule == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {command.ScheduleId}"));

            schedule.Location = location;
            schedule.ScheduledStart = command.StartTime;
            schedule.ScheduledEnd = command.EndTime;

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<Schedule>(schedule);
        }


        public Result DeleteFoodTruckSchedule(int foodTruckId, int scheduleId)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure(new ObjectNotFoundError($"No food truck with the id {foodTruckId} found so the schedule could not be deleted"));

            var schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
                return Result.Failure(new ObjectNotFoundError($"No schedule with the id {scheduleId} found so the schedule could not be deleted"));

            schedule.CancelScheduledAppointment();

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }

    }
}
