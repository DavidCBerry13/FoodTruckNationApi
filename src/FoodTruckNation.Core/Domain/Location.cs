using DavidBerry.Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FoodTruckNation.Core.Domain
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


        internal Location(int id, string name, Locality locality, string streetAddress, string city,
            string state, string zipCode, decimal latitude, decimal longitude) : base(ObjectState.UNCHANGED)
        {
            _locationId = id;
            _locationName = name;
            _locality = locality;
            _streetAddress = streetAddress;
            _city = city;
            _state = state;
            _zipCode = zipCode;
            _latitude = latitude;
            _longitude = longitude;
        }


        public Location(string name, Locality locality, string streetAddress, string city,
            string state, string zipCode) : base(ObjectState.NEW)
        {
            _locationName = name;
            _locality = locality;
            _streetAddress = streetAddress;
            _city = city;
            _state = state;
            _zipCode = zipCode;
            _latitude = 0.0m;
            _longitude = 0.0m;
        }


        private int _locationId;
        private string _locationName;
        private Locality _locality;
        private string _streetAddress;
        private string _city;
        private string _state;
        private string _zipCode;
        private decimal _latitude;
        private decimal _longitude;



        #region Validation Constants

        public const string NAME_VALIDATION = @"^\w[\w ?,!\.'\-\(\)]{1,49}$";

        public const string ADDRESS_VALIDATION = @"^\w[\w ?,!'\.]{1,49}$";

        public const string CITY_VALIDATION = @"^\w[\w ?,!\.]{1,29}$";

        public const string STATE_VALIDATION = @"^((A[LKSZR])|(C[AOT])|(D[EC])|(F[ML])|(G[AU])|(HI)|(I[DLNA])|(K[SY])|(LA)|(M[EHDAINSOT])|(N[EVHJMYCD])|(MP)|(O[HKR])|(P[WAR])|(RI)|(S[CD])|(T[NX])|(UT)|(V[TIA])|(W[AVIY]))$";

        public const string ZIP_CODE_VALIDATION = @"^\d{5}$";

        #endregion


        public int LocationId
        {
            get { return _locationId; }
            internal set { _locationId = value; }
        }



        [Required]
        public string Name
        {
            get { return _locationName; }
            set
            {
                _locationName = value;
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
            get {  return _locality; }
            set
            {
                _locality = value;
                SetObjectModified();
            }
        }


        [Required]
        public string StreetAddress
        {
            get { return _streetAddress; }
            set
            {
                _streetAddress = value;
                SetObjectModified();
            }
        }

        [Required]
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                SetObjectModified();
            }
        }



        [Required]
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                SetObjectModified();
            }
        }

        [Required]
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                SetObjectModified();
            }
        }


        public decimal Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                SetObjectModified();
            }
        }


        public decimal Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                SetObjectModified();
            }
        }
    }
}
