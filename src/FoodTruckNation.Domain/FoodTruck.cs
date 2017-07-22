using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace FoodTruckNation.Domain
{

    /// <summary>
    /// Represents a Food Truck
    /// </summary>
    public class FoodTruck : BaseEntity
    {

        /// <summary>
        /// Private constructor needed by Entity Framework (and maybe Newtonsoft?)
        /// </summary>
        private FoodTruck() 
            : this(String.Empty, String.Empty, String.Empty, ObjectState.UNCHANGED)
        {

        }

        /// <summary>
        /// Constructor to be used when instantiating a Food Truck that already exists (like in your data access layer)
        /// </summary>
        /// <param name="foodTruckId">An int of the unique id for this food truck</param>
        /// <param name="name">A String of the name of this food truck</param>
        /// <param name="description">A String of the description of this food truck</param>
        /// <param name="website">A String of the website for this food truck</param>
        public FoodTruck(int foodTruckId, String name, String description, String website)
            : this(name, description, website, ObjectState.UNCHANGED)
        {
            this.foodTruckId = foodTruckId;
        }

        /// <summary>
        /// Constructor used when the application needs to create a new Food Truck that does not yet exist
        /// </summary>
        /// <param name="name">A String of the name of the new food truck</param>
        /// <param name="description">A String of the description of the food truck</param>
        /// <param name="website">A String of the website for the food truck</param>
        public FoodTruck(String name, String description, String website)
            : this(name, description, website, ObjectState.NEW)
        {
            
        }

        /// <summary>
        /// Protected constructor used by all the other constructors that actually instantiates the food truck 
        /// </summary>
        /// <param name="foodTruckId">An int of the unique id for this food truck</param>
        /// <param name="name">A String of the name of this food truck</param>
        /// <param name="description">A String of the description of this food truck</param>
        /// <param name="website">A String of the website for this food truck</param>
        /// <param name="objectState">An ObjectState value tracking if this is a new or existing (unchanged) food truck</param>
        protected FoodTruck(String name, String description, String website, ObjectState objectState)
            : base(objectState)
        {
            
            this.name = name;
            this.description = description;
            this.website = website;
            this.tags = new List<FoodTruckTag>();

            
        }

        #region Member Variables

        private int foodTruckId;
        private String name;
        private String description;
        private String website;
        private List<FoodTruckTag> tags;

        #endregion


        public int FoodTruckId
        {
            get { return this.foodTruckId; }
            private set { this.foodTruckId = value; }
            //get;
            //private set;
        }

        public String Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.SetObjectModified();
            }
        }

        public String Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                this.SetObjectModified();
            }
        }

        public String Website
        {
            get { return this.website; }
            set
            {
                this.website = value;
                this.SetObjectModified();
            }
        }


        public List<FoodTruckTag> Tags
        {
            get
            {
                return this.tags.Where(t => t.ObjectState.IsActiveState())
                    .ToList();
            }
        }


        public void AddTag(Tag tag)
        {
            // TODO:  Verify tag is not already on truck

            FoodTruckTag foodTruckTag = new FoodTruckTag(this, tag);
            this.tags.Add(foodTruckTag);
            this.SetObjectModified();   // TODO: Do we need to do this?
        }



        public void RemoveTag(FoodTruckTag foodTruckTag)
        {
            foodTruckTag.SetDeleted();
            this.SetObjectModified();   // TODO: Do we need to do this?
        }




    }


}
