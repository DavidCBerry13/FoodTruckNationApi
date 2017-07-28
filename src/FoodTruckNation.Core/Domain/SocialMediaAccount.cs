using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Domain
{
    public class SocialMediaAccount : BaseEntity
    {

        public SocialMediaAccount() : base(ObjectState.UNCHANGED)
        {

        }


        public SocialMediaAccount(SocialMediaPlatform platform, FoodTruck truck, String accountName) 
            : base(ObjectState.NEW)
        {
            _foodTruck = truck;
            _foodTruckId = truck.FoodTruckId;
            _socialMediaPlatform = platform;
            _platformId = platform.PlatformId;
            _accountName = accountName;
        }


        #region Validation Constants

        public const String ACCOUNT_NAME_VALIDATION = @"^[\w@][\w\.-]{1,39}$";


        #endregion

        private int _socialMediaAccountId;
        private int _foodTruckId;
        private FoodTruck _foodTruck;
        private int _platformId;
        private SocialMediaPlatform _socialMediaPlatform;
        private String _accountName;


        public int SocialMediaAccountId
        {
            get { return _socialMediaAccountId;  }
            private set { _socialMediaAccountId = value;  }
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


        public int PlatformId
        {
            get { return _platformId; }
            private set { _platformId = value; }
        }


        public SocialMediaPlatform Platform
        {
            get { return _socialMediaPlatform; }
            private set { _socialMediaPlatform = value; }
        }


        public String AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                this.SetObjectModified();
            }
        }


        internal void SetDeleted()
        {
            this.SetObjectDeleted();
        }
    }
}
