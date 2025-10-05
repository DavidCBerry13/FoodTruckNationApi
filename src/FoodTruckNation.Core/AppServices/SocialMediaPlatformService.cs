using FoodTruckNation.Core.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using DavidBerry.Framework.Functional;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FoodTruckNation.Core.AppServices
{
    public class SocialMediaPlatformService : BaseService, ISocialMediaPlatformService
    {

        public SocialMediaPlatformService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase) :
            base(loggerFactory, foodTruckDatabase)
        {
        }



        public async Task<Result<IEnumerable<SocialMediaPlatform>>> GetAllSocialMediaPlatformsAsync()
        {
            return Result.Success(await FoodTruckDatabase.SocialMediaPlatformRepository.GetSocialMediaPlatformsAsync());
        }


        public async Task<Result<SocialMediaPlatform>> GetSocialMediaPlatformAsync(int platformId)
        {
            var socialMediaPlatform = await FoodTruckDatabase.SocialMediaPlatformRepository.GetSocialMediaPlatformAsync(platformId);
            return ( socialMediaPlatform != null )
                ? Result.Success<SocialMediaPlatform>(socialMediaPlatform)
                : Result.Failure<SocialMediaPlatform>(new ObjectNotFoundError($"No social media platform was found with the id {platformId}"));
        }

    }
}
