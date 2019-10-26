using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class UpdateFoodTruckScheduleCommand
    {
        public int ScheduleId { get; set; }

        public int FoodTruckId { get; set; }

        public int LocationId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
