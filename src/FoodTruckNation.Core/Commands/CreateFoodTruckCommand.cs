using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class CreateFoodTruckCommand
    {
        public CreateFoodTruckCommand()
        {
            this.Tags = new List<string>();
            this.SocialMediaAccounts = new List<FoodTruckSocialMediaAccountData>();
        }



        public String Name { get; set; }

        public String Description { get; set; }

        public String Website { get; set; }

        public List<String> Tags { get; set; }

        public List<FoodTruckSocialMediaAccountData> SocialMediaAccounts { get; set; }
    }


    public class FoodTruckSocialMediaAccountData
    {

        public int SocialMediaPlatformId { get; set; }

        public String AccountName { get; set; }
    }


}
