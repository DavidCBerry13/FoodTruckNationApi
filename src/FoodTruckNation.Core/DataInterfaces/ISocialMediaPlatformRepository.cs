using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ISocialMediaPlatformRepository
    {

        List<SocialMediaPlatform> GetSocialMediaPlatforms();

        SocialMediaPlatform GetSocialMediaPlatform(int platformId);

    }
}
