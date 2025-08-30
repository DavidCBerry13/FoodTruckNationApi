using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class SocialMediaPlatformRepository : ISocialMediaPlatformRepository
    {

        public SocialMediaPlatformRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
        }


        private readonly FoodTruckContext _foodTruckContext;


        public async Task<SocialMediaPlatform> GetSocialMediaPlatformAsync(int platformId)
        {
            var platform = await _foodTruckContext.SocialMediaPlatforms
                .FirstOrDefaultAsync(p => p.PlatformId == platformId);

            return platform;
        }

        public async Task<IEnumerable<SocialMediaPlatform>> GetSocialMediaPlatformsAsync()
        {
            var platforms = await _foodTruckContext.SocialMediaPlatforms
                .AsNoTracking()
                .ToListAsync();

            return platforms;
        }
    }
}
