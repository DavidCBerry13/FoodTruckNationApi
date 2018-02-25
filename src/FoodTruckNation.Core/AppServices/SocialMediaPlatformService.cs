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
            this._socialMediaPlatformRepository = repository;
        }


        private ISocialMediaPlatformRepository _socialMediaPlatformRepository;


        public List<SocialMediaPlatform> GetAllSocialMediaPlatforms()
        {
            return this._socialMediaPlatformRepository.GetSocialMediaPlatforms();
        }


        public SocialMediaPlatform GetSocialMediaPlatform(int platformId)
        {
            return this._socialMediaPlatformRepository.GetSocialMediaPlatform(platformId);
        }

    }
}
