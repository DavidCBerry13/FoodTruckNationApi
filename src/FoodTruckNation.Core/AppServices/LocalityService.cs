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


        public Result<List<Locality>> GetLocalities()
        {
            var localities = _localityRepository.GetLocalities();
            return Result.Success<List<Locality>>(localities);
        }


        public Result<Locality> GetLocality(string code)
        {
            var locality = _localityRepository.GetLocality(code);
            return ( locality != null )
                ? Result.Success<Locality>(locality)
                : Result.Failure<Locality>($"No locality found with the locality code of {code}");
        }


        public Result<Locality> CreateLocality(CreateLocalityCommand localityInfo)
        {
            // First, we need to check and see if a locality already exists with the provided locality code
            var existingLocality = _localityRepository.GetLocality(localityInfo.Code);
            if (existingLocality != null)
            {
                return Result.Failure<Locality>(new ObjectAlreadyExistsError<Locality>($"A locality with the locality code of {localityInfo.Code} already exists", existingLocality));
            }

            Locality locality = Locality.CreateNewLocality(localityInfo.Code, localityInfo.Name);

            _localityRepository.Save(locality);
            UnitOfWork.SaveChanges();

            return Result.Success<Locality>(locality);
        }


        public Result<Locality> UpdateLocality(UpdateLocalityCommand localityInfo)
        {
            Locality locality = _localityRepository.GetLocality(localityInfo.Code);
            if (locality == null)
                return Result.Failure<Locality>($"No locality was found with the code {localityInfo.Code}");

            // Update the properties
            locality.Name = localityInfo.Name;

            _localityRepository.Save(locality);
            UnitOfWork.SaveChanges();

            return Result.Success<Locality>(locality);
        }



        public Result DeleteLocality(string code)
        {
            Locality locality = _localityRepository.GetLocality(code);

            if (locality == null)
                return Result.Failure(new ObjectNotFoundError($"Locality code {code} not found so it could not be deleted"));

            _localityRepository.Delete(locality);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }






    }
}
