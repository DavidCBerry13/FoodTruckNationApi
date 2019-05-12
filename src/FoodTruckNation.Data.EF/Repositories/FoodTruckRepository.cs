using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Framework;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class FoodTruckRepository : IFoodTruckRepository
    {
        public FoodTruckRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
        }


        private readonly FoodTruckContext _foodTruckContext;


        public IList<FoodTruck> GetAllFoodTrucks()
        {
            var foodTrucks = _foodTruckContext.FoodTrucks                
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .Include(f => f.Reviews)
                .Include(f => f.Schedules)
                .Include(f => f.SocialMediaAccounts)
                .ThenInclude(x => x.Platform)
                .OrderBy(x => x.FoodTruckId)
                .AsNoTracking()
                .ToList();

            return foodTrucks;
        }


        /// <summary>
        /// Get a FoodTruck by its unique id number
        /// </summary>
        /// <param name="foodTruckId">An int of the unique food truck id</param>
        /// <returns>A FoodTruck object or null if no food truck is found with the given id</returns>
        public FoodTruck GetFoodTruck(int foodTruckId)
        {
            var foodTruck = _foodTruckContext.FoodTrucks
                .Include(f => f.SocialMediaAccounts)
                .ThenInclude(x => x.Platform)
                .Include(f => f.Tags)               
                .ThenInclude(t => t.Tag)
                .Include(f => f.Reviews)
                .Include(f => f.Schedules)

                .Where(ft => ft.FoodTruckId == foodTruckId)                
                .AsNoTracking()
                .SingleOrDefault();

            return foodTruck;
        }


        /// <summary>
        /// Gets a list of FoodTruck objects that have the given tag.
        /// </summary>
        /// <param name="tag">A String of the tag to search for</param>
        /// <returns>A List of FoodTruck objects.  If no Food Trucks are found with 
        /// the given tag, an empty list is returned</returns>
        public IList<FoodTruck> GetFoodTruckByTag(string tag)
        {
            var foodTruck = _foodTruckContext.FoodTrucks
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .Include(f => f.Reviews)
                .Include(f => f.Schedules)
                .Include(f => f.SocialMediaAccounts)
                .ThenInclude(x => x.Platform)
                .Where(ft => ft.Tags.Any(ftt => ftt.Tag.Text == tag) )
                .AsNoTracking()
                .ToList();

            return foodTruck;
        }


        public void Save(FoodTruck foodTruck)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(foodTruck, EfExtensions.ConvertStateOfNode);
        }



        public void Delete(FoodTruck foodTruck)
        {
            _foodTruckContext.Remove(foodTruck);
        }

    }
}
