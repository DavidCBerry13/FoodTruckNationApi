using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DavidBerry.Framework.Domain;

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
            : this(string.Empty, string.Empty, string.Empty, ObjectState.UNCHANGED)
        {

        }

        /// <summary>
        /// Constructor to be used when instantiating a Food Truck that already exists (like in your data access layer)
        /// </summary>
        /// <param name="foodTruckId">An int of the unique id for this food truck</param>
        /// <param name="name">A String of the name of this food truck</param>
        /// <param name="description">A String of the description of this food truck</param>
        /// <param name="website">A String of the website for this food truck</param>
        internal FoodTruck(int foodTruckId, string name, string description, string website)
            : this(name, description, website, ObjectState.UNCHANGED)
        {
            _foodTruckId = foodTruckId;
        }

        /// <summary>
        /// Constructor used when the application needs to create a new Food Truck that does not yet exist
        /// </summary>
        /// <param name="name">A String of the name of the new food truck</param>
        /// <param name="description">A String of the description of the food truck</param>
        /// <param name="website">A String of the website for the food truck</param>
        public FoodTruck(string name, string description, string website)
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
        protected FoodTruck(string name, string description, string website, ObjectState objectState)
            : base(objectState)
        {
            _name = name;
            _description = description;
            _website = website;
            _tags = new List<FoodTruckTag>();
            _reviews = new List<Review>();
            _schedules = new List<Schedule>();
            _socialMediaAccounts = new List<SocialMediaAccount>();
        }

        #region Member Variables

        private int _foodTruckId;
        private string _name;
        private string _description;
        private string _website;
        private Locality _locality;
        private DateTime _lastModifiedDate;
        private readonly List<FoodTruckTag> _tags;
        private readonly List<Review> _reviews;
        private readonly List<Schedule> _schedules;
        private readonly List<SocialMediaAccount> _socialMediaAccounts;
        #endregion


        #region Validation Constants

        public const string NAME_VALIDATION = @"^\w[\w ?,!\.'-]{1,39}$";

        public const string DESCRIPTION_VALIDATION = @"^\w[\w ?,!\.]{1,511}$";

        public const string WEBSITE_VALIDATION = @"^https?:\/\/([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

        #endregion


        public int FoodTruckId
        {
            get { return _foodTruckId; }
            internal set { _foodTruckId = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetObjectModified();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                SetObjectModified();
            }
        }

        public string Website
        {
            get { return _website; }
            set
            {
                _website = value;
                SetObjectModified();
            }
        }

        [Required]
        public string LocalityCode
        {
            get;
            private set;
        }


        [Required]
        public Locality Locality
        {
            get { return _locality; }
            set
            {
                _locality = value;
                SetObjectModified();
            }
        }

        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            internal set { _lastModifiedDate = value; }
        }

        public List<FoodTruckTag> Tags
        {
            get
            {
                return _tags.Where(t => t.ObjectState.IsActiveState())
                    .ToList();
            }
        }


        public void AddTag(Tag tag)
        {
            // If the tag is already on the truck, then just return
            // A different implementation may throw an exception here, but for
            // a food truck tag, it is OK just to return
            if (_tags.Any(t => t.Tag == tag))
                return;

            FoodTruckTag foodTruckTag = new FoodTruckTag(this, tag);
            _tags.Add(foodTruckTag);
            SetObjectModified();
        }



        public void RemoveTag(FoodTruckTag foodTruckTag)
        {
            foodTruckTag.SetDeleted();
            SetObjectModified();
        }


        public int ReviewCount
        {
            get { return _reviews.Count; }
        }


        public double ReviewAverage
        {
            get
            {
                if (_reviews.Count == 0)
                    return 0.0;

                return _reviews.Average(r => r.Rating);
            }
        }


        public List<Review> Reviews
        {
            get
            {
                return _reviews.ToList();
            }
        }


        public void AddReview(Review review)
        {
            _reviews.Add(review);
            SetObjectModified();
        }


        public List<Schedule> Schedules
        {
            get
            {
                return _schedules.ToList();
            }
        }


        public void AddSchedule(Schedule schedule)
        {
            _schedules.Add(schedule);
            SetObjectModified();
        }



        public List<SocialMediaAccount> SocialMediaAccounts
        {
            get
            {
                return _socialMediaAccounts
                    .Where(a => a.ObjectState.IsActiveState())
                    .ToList();
            }
        }


        public void AddSocialMediaAccount(SocialMediaAccount account)
        {
            _socialMediaAccounts.Add(account);
            SetObjectModified();
        }


        public void RemoveSocialMediaAccount(SocialMediaAccount account)
        {
            account.SetDeleted();
            SetObjectModified();
        }

    }


}
