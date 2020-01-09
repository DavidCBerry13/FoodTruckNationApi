using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
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
