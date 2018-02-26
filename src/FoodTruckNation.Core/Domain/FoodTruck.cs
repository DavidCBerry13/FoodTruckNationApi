using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNation.Core.Domain
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
            this.reviews = new List<Review>();
            this.schedules = new List<Schedule>();
            this.socialMediaAccounts = new List<SocialMediaAccount>();
        }

        #region Member Variables

        private int foodTruckId;
        private String name;
        private String description;
        private String website;
        private DateTime lastModifiedDate;
        private List<FoodTruckTag> tags;
        private List<Review> reviews;
        private List<Schedule> schedules;
        private List<SocialMediaAccount> socialMediaAccounts;
        #endregion


        #region Validation Constants

        public const String NAME_VALIDATION = @"^\w[\w ?,!\.'-]{1,39}$";

        public const String DESCRIPTION_VALIDATION = @"^\w[\w ?,!\.]{1,511}$";

        public const String WEBSITE_VALIDATION = @"^https?:\/\/([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

        #endregion


        public int FoodTruckId
        {
            get { return this.foodTruckId; }
            private set { this.foodTruckId = value; }
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


        public DateTime LastModifiedDate
        {
            get { return this.lastModifiedDate;  }
            internal set { this.lastModifiedDate = value; }
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
            // If the tag is already on the truck, then just return
            // A different implementation may throw an exception here, but for
            // a food truck tag, it is OK just to return
            if (this.tags.Any(t => t.Tag == tag))
                return;            

            FoodTruckTag foodTruckTag = new FoodTruckTag(this, tag);
            this.tags.Add(foodTruckTag);
            this.SetObjectModified();
        }



        public void RemoveTag(FoodTruckTag foodTruckTag)
        {
            foodTruckTag.SetDeleted();
            this.SetObjectModified();
        }


        public int ReviewCount
        {
            get { return this.reviews.Count;  }
        }


        public double ReviewAverage
        {
            get
            {
                if (this.reviews.Count == 0)
                    return 0.0;

                return this.reviews.Average(r => r.Rating);
            }
        }
        

        public List<Review> Reviews
        {
            get
            {
                return this.reviews.ToList();
            }
        }


        public void AddReview(Review review)
        {
            this.reviews.Add(review);
            this.SetObjectModified();   
        }


        public List<Schedule> Schedules
        {
            get
            {
                return this.schedules.ToList();
            }
        }


        public void AddSchedule(Schedule schedule)
        {
            this.schedules.Add(schedule);
            this.SetObjectModified();   
        }



        public List<SocialMediaAccount> SocialMediaAccounts
        {
            get
            {
                return this.socialMediaAccounts
                    .Where(a => a.ObjectState.IsActiveState())
                    .ToList();
            }
        }


        public void AddSocialMediaAccount(SocialMediaAccount account)
        {
            this.socialMediaAccounts.Add(account);
            this.SetObjectModified();
        }


        public void RemoveSocialMediaAccount(SocialMediaAccount account)
        {
            account.SetDeleted();
            this.SetObjectModified();
        }

    }


}
