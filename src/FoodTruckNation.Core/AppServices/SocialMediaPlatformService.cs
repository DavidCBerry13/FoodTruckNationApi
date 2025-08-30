using FoodTruckNation.Core.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using DavidBerry.Framework.Functional;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppServices
{
    public class SocialMediaPlatformService : ISocialMediaPlatformService
    {

        public SocialMediaPlatformService(ISocialMediaPlatformRepository repository)
        {
            _socialMediaPlatformRepository = repository;
        }


        private readonly ISocialMediaPlatformRepository _socialMediaPlatformRepository;


        public async Task<Result<IEnumerable<SocialMediaPlatform>>> GetAllSocialMediaPlatformsAsync()
        {
            return Result.Success(await _socialMediaPlatformRepository.GetSocialMediaPlatformsAsync());
        }


        public async Task<Result<SocialMediaPlatform>> GetSocialMediaPlatformAsync(int platformId)
        {
            var socialMediaPlatform = await _socialMediaPlatformRepository.GetSocialMediaPlatformAsync(platformId);
            return ( socialMediaPlatform != null )
                ? Result.Success<SocialMediaPlatform>(socialMediaPlatform)
                : Result.Failure<SocialMediaPlatform>(new ObjectNotFoundError($"No social media platform was found with the id {platformId}"));
        }

    }
}
