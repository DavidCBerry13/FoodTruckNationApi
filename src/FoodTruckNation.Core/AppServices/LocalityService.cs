using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.Functional;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Microsoft.Extensions.Logging;

namespace FoodTruckNation.Core.AppServices
{
    public class LocalityService : BaseService, ILocalityService
    {

        public LocalityService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase)
            : base(loggerFactory, foodTruckDatabase)
        {
        }




        public async Task<Result<IEnumerable<Locality>>> GetLocalitiesAsync()
        {
            var localities = await FoodTruckDatabase.LocalityRepository.GetLocalitiesAsync();
            return Result.Success<IEnumerable<Locality>>(localities);
        }


        public async Task<Result<Locality>> GetLocalityAsync(string code)
        {
            var locality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(code);
            return ( locality != null )
                ? Result.Success<Locality>(locality)
                : Result.Failure<Locality>($"No locality found with the locality code of {code}");
        }


        public async Task<Result<Locality>> CreateLocalityAsync(CreateLocalityCommand localityInfo)
        {
            // First, we need to check and see if a locality already exists with the provided locality code
            var existingLocality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(localityInfo.Code);
            if (existingLocality != null)
            {
                return Result.Failure<Locality>(new ObjectAlreadyExistsError<Locality>($"A locality with the locality code of {localityInfo.Code} already exists", existingLocality));
            }

            Locality locality = Locality.CreateNewLocality(localityInfo.Code, localityInfo.Name);

            await FoodTruckDatabase.LocalityRepository.SaveAsync(locality);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Locality>(locality);
        }


        public async Task<Result<Locality>> UpdateLocalityAsync(UpdateLocalityCommand localityInfo)
        {
            Locality locality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(localityInfo.Code);
            if (locality == null)
                return Result.Failure<Locality>($"No locality was found with the code {localityInfo.Code}");

            // Update the properties
            locality.Name = localityInfo.Name;

            await FoodTruckDatabase.LocalityRepository.SaveAsync(locality);
            FoodTruckDatabase.CommitChanges();

            return Result.Success<Locality>(locality);
        }



        public async Task<Result> DeleteLocalityAsync(string code)
        {
            Locality locality = await FoodTruckDatabase.LocalityRepository.GetLocalityAsync(code);

            if (locality == null)
                return Result.Failure(new ObjectNotFoundError($"Locality code {code} not found so it could not be deleted"));

            await FoodTruckDatabase.LocalityRepository.DeleteAsync(locality);
            FoodTruckDatabase.CommitChanges();

            return Result.Success();
        }






    }
}
