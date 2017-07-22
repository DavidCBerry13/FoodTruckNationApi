using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class CreateFoodTruckCommand
    {

        public String Name { get; set; }

        public String Description { get; set; }

        public String Website { get; set; }

        public List<String> Tags { get; set; }

    }
}
