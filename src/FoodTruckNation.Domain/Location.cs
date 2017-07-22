using Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodTruckNation.Domain
{
    /// <summary>
    /// Represents a well known location where a food trucks gather
    /// </summary>
    public class Location : BaseEntity
    {

        /// <summary>
        /// Parameterless constructor for Entity Framework to use
        /// </summary>
        internal Location() : base(ObjectState.UNCHANGED)
        {

        }


        internal Location(int id, String name, String streetAddress, String city,
            String state, String zipCode) : base(ObjectState.UNCHANGED)
        {
            this.locationId = id;
            this.locationName = name;
            this.streetAddress = streetAddress;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
        }


        public Location(String name, String streetAddress, String city,
            String state, String zipCode) : base(ObjectState.NEW)
        {
            this.locationName = name;
            this.streetAddress = streetAddress;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
        }


        private int locationId;
        private String locationName;
        private String streetAddress;
        private String city;
        private String state;
        private String zipCode;




        public int LocationId
        {
            get { return this.locationId; }
            internal set { this.locationId = value; }            
        }



        [Required]
        public String Name
        {
            get { return this.locationName; }
            set
            {
                this.locationName = value;
                this.SetObjectModified();
            }
        }


        [Required]
        public String StreetAddress
        {
            get { return this.streetAddress; }
            set
            {
                this.streetAddress = value;
                this.SetObjectModified();
            }
        }

        [Required]
        public String City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                this.SetObjectModified();
            }
        }



        [Required]
        public String State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                this.SetObjectModified();
            }
        }

        [Required]
        public String ZipCode
        {
            get { return this.zipCode; }
            set
            {
                this.zipCode = value;
                this.SetObjectModified();
            }
        }
    }
}
