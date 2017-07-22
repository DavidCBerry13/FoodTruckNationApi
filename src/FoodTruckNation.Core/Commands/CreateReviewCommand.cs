using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class CreateReviewCommand
    {

        public int FoodTruckId { get; set; }

        public int Rating { get; set; }

        public String Comments { get; set; }

    }
}
