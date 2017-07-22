using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Commands
{
    public class UpdateTagCommand
    {
        public int TagId { get; set; }

        public String TagText { get; set; }

    }
}
