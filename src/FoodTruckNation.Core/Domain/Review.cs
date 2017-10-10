using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Domain
{


    public class Review : BaseEntity
    {


        public Review()
            :base(ObjectState.UNCHANGED)
        {
            _foodTruck = null;
            _reviewDate = DateTime.Today;
            _rating = 1;
            _details = String.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        public Review(FoodTruck foodTruck, DateTime reviewDate, int rating, String details) 
            : base(ObjectState.NEW)
        {
            _foodTruck = foodTruck;
            _reviewDate = reviewDate;
            _rating = rating;
            _details = details;
        }


        protected Review(int reviewId, FoodTruck foodTruck, DateTime reviewDate, int rating, String details)
            : base(ObjectState.UNCHANGED)
        {
            _reviewId = reviewId;
            _foodTruck = foodTruck;
            _reviewDate = reviewDate;
            _rating = rating;
            _details = details;
        }


        #region Member Variables

        private int _reviewId;
        private int _foodTruckId;
        private FoodTruck _foodTruck;
        private DateTime _reviewDate;
        private int _rating;
        private String _details;

        #endregion


        #region Validation Constants

        public const String COMMENTS_VALIDATION = @"^\w[\w ?,-'""!\.]{1,1023}$";

        #endregion


        public int ReviewId
        {
            get { return _reviewId; }
            private set { _reviewId = value; }
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

        public DateTime ReviewDate
        {
            get { return _reviewDate; }
            private set { _reviewDate = value.Date; }
        }

        public int Rating
        {
            get { return _rating; }
            private set { _rating = value; }
        }


        public String Details
        {
            get { return _details; }
            private set { _details = value; }
        }

    }
}
