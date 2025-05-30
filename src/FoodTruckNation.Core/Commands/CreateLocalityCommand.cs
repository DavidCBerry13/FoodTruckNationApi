using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.Commands
{
    public class CreateLocalityCommand
    {
        /// <summary>
        /// A unique 2-5 letter code assigned to be assigned to the locality
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The name of the locality (Typically the city or area name)
        /// </summary>
        public string Name { get; set; }

    }
}
