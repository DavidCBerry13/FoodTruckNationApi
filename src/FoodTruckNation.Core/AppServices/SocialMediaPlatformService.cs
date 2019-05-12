using FoodTruckNation.Core.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.DataInterfaces;

namespace FoodTruckNation.Core.AppServices
{
    public class SocialMediaPlatformService : ISocialMediaPlatformService
    {

        public SocialMediaPlatformService(ISocialMediaPlatformRepository repository)
        {
            _socialMediaPlatformRepository = repository;
        }


        private readonly ISocialMediaPlatformRepository _socialMediaPlatformRepository;


        public List<SocialMediaPlatform> GetAllSocialMediaPlatforms()
        {
            return _socialMediaPlatformRepository.GetSocialMediaPlatforms();
        }


        public SocialMediaPlatform GetSocialMediaPlatform(int platformId)
        {
            return _socialMediaPlatformRepository.GetSocialMediaPlatform(platformId);
        }

    }
}
