using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class CreateLocationCommand
    {

        public string Name { get; set; }

        public string LocalityCode { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }


    }
}
