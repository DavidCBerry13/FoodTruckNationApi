using FoodTruckNation.Core.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;
using DavidBerry.Framework.ResultType;

namespace FoodTruckNation.Core.AppServices
{
    public class SocialMediaPlatformService : ISocialMediaPlatformService
    {

        public SocialMediaPlatformService(ISocialMediaPlatformRepository repository)
        {
            _socialMediaPlatformRepository = repository;
        }


        private readonly ISocialMediaPlatformRepository _socialMediaPlatformRepository;


        public Result<List<SocialMediaPlatform>> GetAllSocialMediaPlatforms()
        {
            return Result.Success(_socialMediaPlatformRepository.GetSocialMediaPlatforms());
        }


        public Result<SocialMediaPlatform> GetSocialMediaPlatform(int platformId)
        {
            var socialMediaPlatform = _socialMediaPlatformRepository.GetSocialMediaPlatform(platformId);
            return ( socialMediaPlatform != null )
                ? Result.Success<SocialMediaPlatform>(socialMediaPlatform)
                : Result.Failure<SocialMediaPlatform>(new ObjectNotFoundError($"No social media platform was found with the id {platformId}"));
        }

    }
}
