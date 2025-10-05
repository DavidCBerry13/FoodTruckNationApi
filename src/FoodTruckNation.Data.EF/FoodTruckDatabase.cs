using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Data;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Data.EF.Repositories;

namespace FoodTruckNation.Data.EF
{
    /// <summary>
    /// Concrete implementation of the FoodTruckDatabase using Entitiy Framework.  This class returns teh repositories
    /// for the FoodTruck database as well as implements the Unit of Work pattern.
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    internal class FoodTruckDatabase : BaseEfDatabase<FoodTruckContext>, IFoodTruckDatabase
    {
        public FoodTruckDatabase(FoodTruckContext dbContext) : base(dbContext)
        {
            FoodTruckRepository = new FoodTruckRepository(dbContext);
            LocalityRepository = new LocalityRepository(dbContext);
            LocationRepository = new LocationRepository(dbContext);
            ScheduleRepository = new ScheduleRepository(dbContext);
            SocialMediaPlatformRepository = new SocialMediaPlatformRepository(dbContext);
            TagRepository = new TagRepository(dbContext);
        }

        public IFoodTruckRepository FoodTruckRepository { get; init; }

        public ILocalityRepository LocalityRepository { get; init; }

        public ILocationRepository LocationRepository { get; init; }

        public IScheduleRepository ScheduleRepository { get; init; }

        public ISocialMediaPlatformRepository SocialMediaPlatformRepository { get; init; }

        public ITagRepository TagRepository { get; init; }
    }
}
