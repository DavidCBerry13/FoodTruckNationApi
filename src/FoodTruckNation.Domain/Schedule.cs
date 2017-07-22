using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Domain
{


    /// <summary>
    /// Represents a schedule for a food truck to be at a certain location on a certain day for a certain period of time
    /// </summary>
    public class Schedule
    {


        public int scheduleId;
        public int foodTruckId;
        public FoodTruck foodTruck;
        public Location location;

        public DateTime scheduleDate;
        public TimeSpan scheduleStart;
        public TimeSpan scheduleEnd;



        public int ScheduleId
        {
            get { return this.scheduleId;  }
            private set { this.scheduleId = value; }
        }


    }
}
