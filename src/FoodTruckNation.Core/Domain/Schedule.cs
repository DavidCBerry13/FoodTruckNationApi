using DavidBerry.Framework.Domain;
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
            _foodTruck = null;
            _location = null;
            _scheduleStart = DateTime.MinValue;
            _scheduleEnd = DateTime.MinValue;
        }


        /// <summary>
        /// Constructor used by application code when a new scheduled time for a food truck is being created
        /// </summary>
        public Schedule(FoodTruck foodTruck, Location location, DateTime startTime, DateTime endTime)
            : base(ObjectState.NEW)
        {
            _foodTruckId = foodTruck.FoodTruckId;
            _foodTruck = foodTruck;
            _locationId = location.LocationId;
            _location = location;
            ScheduledStart = startTime;
            ScheduledEnd = endTime;
        }


        internal Schedule(int scheduleId, FoodTruck foodTruck, Location location, DateTime startTime, DateTime endTime)
            : base(ObjectState.UNCHANGED)
        {
            _scheduleId = scheduleId;
            _foodTruckId = foodTruck.FoodTruckId;
            _foodTruck = foodTruck;
            _locationId = location.LocationId;
            _location = location;
            ScheduledStart = startTime;
            ScheduledEnd = endTime;
        }



        private int _scheduleId;
        private int _foodTruckId;
        private FoodTruck _foodTruck;
        private int _locationId;
        private Location _location;
        private DateTime _scheduleStart;
        private DateTime _scheduleEnd;



        public int ScheduleId
        {
            get { return _scheduleId;  }
            private set { _scheduleId = value; }
        }


        public int FoodTruckId
        {
            get { return _foodTruckId; }
            private set { _foodTruckId = value; }
        }

        public FoodTruck FoodTruck
        {
            get { return _foodTruck; }
            private set { _foodTruck = value; }
        }


        public int LocationId
        {
            get { return _locationId; }
            private set { _locationId = value; }
        }

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }


        public DateTime ScheduledStart
        {
            get { return _scheduleStart; }
            set
            {
                _scheduleStart = value;
                SetObjectModified();
            }
        }


        public DateTime ScheduledEnd
        {
            get { return _scheduleEnd; }
            set
            {
                _scheduleEnd = value;
                SetObjectModified();
            }
        }


        /// <summary>
        /// Provides a way to cancel this scheduled event/appointment
        /// </summary>
        public void CancelScheduledAppointment()
        {
            SetObjectDeleted();
        }

    }
}
