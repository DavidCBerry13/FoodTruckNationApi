using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ISocialMediaPlatformRepository
    {

        public Task<IEnumerable<SocialMediaPlatform>> GetSocialMediaPlatformsAsync();

        public Task<SocialMediaPlatform> GetSocialMediaPlatformAsync(int platformId);

    }
}
