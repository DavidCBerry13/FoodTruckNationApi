using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Functional;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ILocalityService
    {

        Result<List<Locality>> GetLocalities();


        Result<Locality> GetLocality(string code);


        Result<Locality> CreateLocality(CreateLocalityCommand localityInfo);


        Result<Locality> UpdateLocality(UpdateLocalityCommand localityInfo);


        Result DeleteLocality(string code);


    }
}
