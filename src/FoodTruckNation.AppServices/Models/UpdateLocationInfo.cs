using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.AppServices.Models
{
    public class UpdateLocationInfo
    {

        public int LocationId { get; set; }

        public String Name { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

    }
}
