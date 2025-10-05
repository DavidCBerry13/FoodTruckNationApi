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
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppServices
{
    public class ScheduleService : BaseService, IScheduleService
    {

        public ScheduleService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase, IDateTimeProvider dateTimeProvider)
            : base(loggerFactory, foodTruckDatabase)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        private IDateTimeProvider _dateTimeProvider;


        public async Task<Result<Schedule>> GetScheduleAsync(int scheduleId)
        {
            var schedule = await FoodTruckDatabase.ScheduleRepository.GetScheduleAsync(scheduleId);
            return ( schedule != null )
                ? Result.Success<Schedule>(schedule)
                : Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {scheduleId}"));
        }


        public async Task<Result<Schedule>> GetScheduleAsync(int foodTruckId, int scheduleId)
        {
            var foodTruck =  await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(foodTruckId);
            if (foodTruck == null)
                Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {foodTruckId}"));

            var schedule = await FoodTruckDatabase.ScheduleRepository.GetScheduleAsync(scheduleId);
            return ( schedule != null )
                ? Result.Success<Schedule>(schedule)
                : Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {scheduleId}"));
        }

        public async Task<Result<IEnumerable<Schedule>>> GetSchedulesAsync(DateTime startDate, DateTime endDate)
        {
            var schedules = await FoodTruckDatabase.ScheduleRepository.GetSchedulesAsync(startDate, endDate);
            return Result.Success<IEnumerable<Schedule>>(schedules);
        }


        public async Task<Result<IEnumerable<Schedule>>> GetSchedulesForFoodTruckAsync(int foodTruckId, DateTime startDate, DateTime endDate)
        {
            var foodTruck = await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(foodTruckId);
            if (foodTruck == null)
                Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {foodTruckId}"));

            var schedules = await FoodTruckDatabase.ScheduleRepository.GetSchedulesForFoodTruckAsync(foodTruckId, startDate, endDate);
            return Result.Success<IEnumerable<Schedule>>(schedules);
        }


        public async Task<Result<IEnumerable<Schedule>>> GetSchedulesForLocationAsync(int locationId, DateTime startDate, DateTime endDate)
        {
            var location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(locationId);
            if (location == null)
                return Result.Failure<IEnumerable<Schedule>>(new ObjectNotFoundError($"No location with the id {locationId} found"));

            var schedules = await FoodTruckDatabase.ScheduleRepository.GetSchedulesForLocationAsync(locationId, startDate, endDate);
            return Result.Success<IEnumerable<Schedule>>(schedules);
        }


        public async Task<Result<Schedule>> AddFoodTruckScheduleAsync(CreateFoodTruckScheduleCommand command)
        {
            var foodTruck = await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(command.FoodTruckId);
            if (foodTruck == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {command.FoodTruckId}"));

            var location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(command.LocationId);
            if (location == null)
                return Result.Failure<Schedule>(new InvalidDataError($"No location with the id {command.LocationId} found"));

            // Create the new schedule object and add it to the food truck
            Schedule schedule = new Schedule(foodTruck, location, command.StartTime, command.EndTime);
            foodTruck.AddSchedule(schedule);

            // Persist to the database
            await FoodTruckDatabase.FoodTruckRepository.SaveAsync(foodTruck);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Schedule>(schedule);
        }



        public async Task<Result<Schedule>> UpdateFoodTruckScheduleAsync(UpdateFoodTruckScheduleCommand command)
        {
            var foodTruck = await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(command.FoodTruckId);
            if (foodTruck == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {command.FoodTruckId}"));

            var location = await FoodTruckDatabase.LocationRepository.GetLocationAsync(command.LocationId);
            if (location == null)
                return Result.Failure<Schedule>(new InvalidDataError($"No location with the id {command.LocationId} found"));

            Schedule schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == command.ScheduleId);
            if (schedule == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {command.ScheduleId}"));

            schedule.Location = location;
            schedule.ScheduledStart = command.StartTime;
            schedule.ScheduledEnd = command.EndTime;

            // Persist to the database
            await FoodTruckDatabase.FoodTruckRepository.SaveAsync(foodTruck);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Schedule>(schedule);
        }


        public async Task<Result> DeleteFoodTruckScheduleAsync(int foodTruckId, int scheduleId)
        {
            var foodTruck = await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(foodTruckId);
            if (foodTruck == null)
                return Result.Failure(new ObjectNotFoundError($"No food truck with the id {foodTruckId} found so the schedule could not be deleted"));

            var schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
                return Result.Failure(new ObjectNotFoundError($"No schedule with the id {scheduleId} found so the schedule could not be deleted"));

            schedule.CancelScheduledAppointment();

            // Persist to the database
            await FoodTruckDatabase.FoodTruckRepository.SaveAsync(foodTruck);
            FoodTruckDatabase.CommitChanges();

            return Result.Success();
        }

    }
}
