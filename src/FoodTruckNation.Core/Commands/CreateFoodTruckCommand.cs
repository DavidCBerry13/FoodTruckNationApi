using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class CreateFoodTruckCommand
    {
        public CreateFoodTruckCommand()
        {
            Tags = new List<string>();
            SocialMediaAccounts = new List<FoodTruckSocialMediaAccountData>();
        }



        public string Name { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public List<string> Tags { get; set; }

        public List<FoodTruckSocialMediaAccountData> SocialMediaAccounts { get; set; }
    }


    public class FoodTruckSocialMediaAccountData
    {

        public int SocialMediaPlatformId { get; set; }

        public string AccountName { get; set; }
    }


}
