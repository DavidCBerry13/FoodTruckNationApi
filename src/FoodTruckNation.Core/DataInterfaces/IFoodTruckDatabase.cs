using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Data;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface IFoodTruckDatabase : IUnitOfWork
    {


        public IFoodTruckRepository FoodTruckRepository { get; }

        public ILocalityRepository LocalityRepository { get; }

        public ILocationRepository LocationRepository { get; }

        public IScheduleRepository ScheduleRepository { get; }

        public ISocialMediaPlatformRepository SocialMediaPlatformRepository { get; }

        public ITagRepository TagRepository { get; }

    }
}
