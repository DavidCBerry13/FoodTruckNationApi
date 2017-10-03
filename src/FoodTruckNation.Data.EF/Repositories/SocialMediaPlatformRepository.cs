using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class SocialMediaPlatformRepository : ISocialMediaPlatformRepository
    {

        public SocialMediaPlatformRepository(FoodTruckContext context)
        {
            this.foodTruckContext = context;
        }


        private FoodTruckContext foodTruckContext;


        public SocialMediaPlatform GetSocialMediaPlatform(int platformId)
        {
            var platform = this.foodTruckContext.SocialMediaPlatforms
                .FirstOrDefault(p => p.PlatformId == platformId);
                
            return platform;
        }

        public List<SocialMediaPlatform> GetSocialMediaPlatforms()
        {
            var platforms = this.foodTruckContext.SocialMediaPlatforms
                .AsNoTracking()
                .ToList();

            return platforms;
        }
    }
}
