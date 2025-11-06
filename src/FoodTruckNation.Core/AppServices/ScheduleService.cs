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
using FoodTruckNation.Core.Util;
using DavidBerry.Framework.Util;

namespace FoodTruckNation.Core.AppServices
{
    public class ScheduleService : BaseService, IScheduleService
    {

        public ScheduleService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase, IDateTimeProvider dateTimeProvider)
            : base(loggerFactory, foodTruckDatabase)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        private readonly IDateTimeProvider _dateTimeProvider;


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

            // Check to make sure this schedule does not overlap with any existing schedules for this food truck
            var overlappingSchedules = foodTruck.Schedules.Where(s => s.Overlaps(command.StartTime, command.EndTime));
            if ( overlappingSchedules.Any() )
                return Result.Failure<Schedule>(new SchedulingConflictError(
                    $"The scheduled time ({command.StartTime}-{command.EndTime}) conflicts with an existing scheduled time for this food truck", overlappingSchedules));

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
            // First, get the food truck with the schedule that needs updating
            var foodTruck = await FoodTruckDatabase.FoodTruckRepository.GetFoodTruckAsync(command.FoodTruckId);
            if (foodTruck == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No food truck found with id {command.FoodTruckId}"));

            // Now make sure the scheule specified exists in the food truck (that is, someone did not give us the scheule id of a schedule that belongs to another food truck)
            Schedule schedule = foodTruck.Schedules.FirstOrDefault(s => s.ScheduleId == command.ScheduleId);
            if (schedule == null)
                return Result.Failure<Schedule>(new ObjectNotFoundError($"No schedule found with id {command.ScheduleId} in Food Truck {command.FoodTruckId}"));

            // Validate the new times do not overlap with any other scheduled times for this food truck
            var overlappingSchedules = foodTruck.Schedules
                .Where(s => s.ScheduleId != command.ScheduleId)
                .Where(s => s.Overlaps(command.StartTime, command.EndTime));
            if (overlappingSchedules.Any())
                return Result.Failure<Schedule>(new SchedulingConflictError(
                    $"The scheduled time ({command.StartTime}-{command.EndTime}) conflicts with an existing scheduled time for this food truck", overlappingSchedules));

            schedule.StartTime = command.StartTime;
            schedule.EndTime = command.EndTime;

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
