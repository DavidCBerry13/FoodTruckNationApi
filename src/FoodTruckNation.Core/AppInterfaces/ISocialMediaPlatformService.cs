using FoodTruckNation.Core.Domain;
using Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ISocialMediaPlatformService
    {

        Result<SocialMediaPlatform> GetSocialMediaPlatform(int platformId);

        Result<List<SocialMediaPlatform>> GetAllSocialMediaPlatforms();


    }
}
