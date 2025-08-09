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

        public LocalityService(ILoggerFactory loggerFactory, IUnitOfWork uow, ILocalityRepository localityRepository)
            : base(loggerFactory, uow)
        {
            _localityRepository = localityRepository;
        }


        #region Member Variables

        private readonly ILocalityRepository _localityRepository;

        #endregion


        public async Task<Result<IEnumerable<Locality>>> GetLocalitiesAsync()
        {
            var localities = await _localityRepository.GetLocalitiesAsync();
            return Result.Success<IEnumerable<Locality>>(localities);
        }


        public async Task<Result<Locality>> GetLocalityAsync(string code)
        {
            var locality = await _localityRepository.GetLocalityAsync(code);
            return ( locality != null )
                ? Result.Success<Locality>(locality)
                : Result.Failure<Locality>($"No locality found with the locality code of {code}");
        }


        public async Task<Result<Locality>> CreateLocalityAsync(CreateLocalityCommand localityInfo)
        {
            // First, we need to check and see if a locality already exists with the provided locality code
            var existingLocality = await _localityRepository.GetLocalityAsync(localityInfo.Code);
            if (existingLocality != null)
            {
                return Result.Failure<Locality>(new ObjectAlreadyExistsError<Locality>($"A locality with the locality code of {localityInfo.Code} already exists", existingLocality));
            }

            Locality locality = Locality.CreateNewLocality(localityInfo.Code, localityInfo.Name);

            await _localityRepository.SaveAsync(locality);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success<Locality>(locality);
        }


        public async Task<Result<Locality>> UpdateLocalityAsync(UpdateLocalityCommand localityInfo)
        {
            Locality locality = await _localityRepository.GetLocalityAsync(localityInfo.Code);
            if (locality == null)
                return Result.Failure<Locality>($"No locality was found with the code {localityInfo.Code}");

            // Update the properties
            locality.Name = localityInfo.Name;

            await _localityRepository.SaveAsync(locality);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success<Locality>(locality);
        }



        public async Task<Result> DeleteLocalityAsync(string code)
        {
            Locality locality = await _localityRepository.GetLocalityAsync(code);

            if (locality == null)
                return Result.Failure(new ObjectNotFoundError($"Locality code {code} not found so it could not be deleted"));

            await _localityRepository.DeleteAsync(locality);
            await UnitOfWork.SaveChangesAsync();

            return Result.Success();
        }






    }
}
