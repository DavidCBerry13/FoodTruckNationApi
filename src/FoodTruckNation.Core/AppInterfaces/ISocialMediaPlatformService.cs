using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ISocialMediaPlatformService
    {

        public Task<Result<SocialMediaPlatform>> GetSocialMediaPlatformAsync(int platformId);

        public Task<Result<IEnumerable<SocialMediaPlatform>>> GetAllSocialMediaPlatformsAsync();


    }
}
