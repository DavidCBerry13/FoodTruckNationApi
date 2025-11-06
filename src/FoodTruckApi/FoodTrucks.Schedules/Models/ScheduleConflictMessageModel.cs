using System.Collections.Generic;
using DavidBerry.Framework.ApiUtil.Models;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.FoodTrucks.Schedules;

namespace FoodTruckApi.FoodTrucks.Schedules.Models;

/// <summary>
/// Reprresents the model to return when a 422 Unprocessable Entity occurs due to scheduling conflicts
/// </summary>
public class ScheduleConflictMessageModel : ApiMessageModel
{

    /// <summary>
    /// A list of the schedules that the proposed schedule conflicts with
    /// </summary>
    public IEnumerable<FoodTruckScheduleModel> ConflictingSchedules { get; set; }



}

