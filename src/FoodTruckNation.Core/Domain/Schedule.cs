using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Domain
{


    /// <summary>
    /// Represents a schedule for a food truck to be at a certain location on a certain day for a certain period of time
    /// </summary>
    public class Schedule : BaseEntity
    {


        public Schedule()
            :base(ObjectState.UNCHANGED)
        {
            this.foodTruck = null;
            this.location = null;
            this.scheduleStart = DateTime.MinValue;
            this.scheduleEnd = DateTime.MinValue;
        }


        /// <summary>
        /// Constructor used by application code when a new scheduled time for a food truck is being created
        /// </summary>
        public Schedule(FoodTruck foodTruck, Location location, DateTime startTime, DateTime endTime) 
            : base(ObjectState.NEW)
        {
            this.foodTruckId = foodTruck.FoodTruckId;
            this.foodTruck = foodTruck;
            this.locationId = location.LocationId;
            this.location = location;
            this.ScheduledStart = startTime;
            this.ScheduledEnd = endTime;
        }


        internal Schedule(int scheduleId, FoodTruck foodTruck, Location location, DateTime startTime, DateTime endTime)
            : base(ObjectState.UNCHANGED)
        {
            this.scheduleId = scheduleId;
            this.foodTruckId = foodTruck.FoodTruckId;
            this.foodTruck = foodTruck;
            this.locationId = location.LocationId;
            this.location = location;
            this.ScheduledStart = startTime;
            this.ScheduledEnd = endTime;
        }



        private int scheduleId;
        private int foodTruckId;
        private FoodTruck foodTruck;
        private int locationId;
        private Location location;
        private DateTime scheduleStart;
        private DateTime scheduleEnd;



        public int ScheduleId
        {
            get { return this.scheduleId;  }
            private set { this.scheduleId = value; }
        }


        public int FoodTruckId
        {
            get { return this.foodTruckId; }
            private set { this.foodTruckId = value; }
        }

        public FoodTruck FoodTruck
        {
            get { return this.foodTruck; }
            private set { this.foodTruck = value; }
        }


        public int LocationId
        {
            get { return this.locationId; }
            private set { this.locationId = value; }
        }

        public Location Location
        {
            get { return this.location; }
            private set { this.location = value; }
        }


        public DateTime ScheduledStart
        {
            get { return this.scheduleStart; }
            set
            {
                this.scheduleStart = value;
                this.SetObjectModified();
            }
        }


        public DateTime ScheduledEnd
        {
            get { return this.scheduleEnd; }
            set
            {
                this.scheduleEnd = value;
                this.SetObjectModified();
            }
        }


        /// <summary>
        /// Provides a way to cancel this scheduled event/appointment
        /// </summary>
        public void CancelScheduledAppointment()
        {
            this.SetObjectDeleted();
        }

    }
}
