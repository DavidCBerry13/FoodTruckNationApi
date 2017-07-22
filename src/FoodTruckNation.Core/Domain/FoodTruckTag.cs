using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Domain
{

    /// <summary>
    /// Represents a tag applied to a food truck
    /// </summary>
    public class FoodTruckTag : BaseEntity
    {

        internal FoodTruckTag() : base(ObjectState.UNCHANGED)
        {

        }


        protected internal FoodTruckTag(int id, FoodTruck foodTruck, Tag tag)
            : base(ObjectState.UNCHANGED)
        {
            this.foodTruckTagId = id;
            this.FoodTruck = foodTruck;
            this.FoodTruckId = this.FoodTruck.FoodTruckId;
            this.Tag = tag;
            this.TagId = this.Tag.TagId;
        }


        public FoodTruckTag(FoodTruck foodTruck, Tag tag)
            : base(ObjectState.NEW)
        {
            //this.foodTruckTagId = default(int);
            this.FoodTruck = foodTruck;
            this.FoodTruckId = this.FoodTruck.FoodTruckId;
            this.Tag = tag;
            this.TagId = this.Tag.TagId;
        }


        private int foodTruckTagId;
        //private FoodTruck foodTruck;
        //private Tag tag;



        public int FoodTruckTagId
        {
            get { return this.foodTruckTagId; }
            private set { this.foodTruckTagId = value; }
        }

        public int FoodTruckId
        {
            get;
            private set;
        }

        public virtual FoodTruck FoodTruck
        {
            get;
            private set;
        }

        public int TagId
        {
            get;
            private set;
        }

        public virtual Tag Tag
        {
            get;
            private set;
        }


        internal void SetDeleted()
        {
            this.SetObjectDeleted();
        }

    }
}
