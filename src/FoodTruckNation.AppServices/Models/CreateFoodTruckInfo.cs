using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.AppServices.Models
{
    public class CreateFoodTruckInfo
    {

        public String Name { get; set; }

        public String Description { get; set; }

        public String Website { get; set; }

        public List<String> Tags { get; set; }

    }
}
