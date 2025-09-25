using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DavidBerry.Framework.Data;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class FoodTruckRepository : IFoodTruckRepository
    {
        public FoodTruckRepository(FoodTruckContext context)
        {
            _foodTruckContext = context;
            _foodTrucks = _foodTruckContext.FoodTrucks
                .Include(f => f.Locality)
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .Include(f => f.Reviews)
                .Include(f => f.Schedules)
                .Include(f => f.SocialMediaAccounts)
                .ThenInclude(x => x.Platform)
                .AsNoTracking();
        }


        private readonly FoodTruckContext _foodTruckContext;
        private readonly IQueryable<FoodTruck> _foodTrucks;

        public async Task<IEnumerable<FoodTruck>> GetAllFoodTrucksAsync()
        {
            var foodTrucks = await _foodTrucks
                .ToListAsync();

            return foodTrucks;
        }


        /// <summary>
        /// Get a FoodTruck by its unique id number
        /// </summary>
        /// <param name="foodTruckId">An int of the unique food truck id</param>
        /// <returns>A FoodTruck object or null if no food truck is found with the given id</returns>
        public async Task<FoodTruck> GetFoodTruckAsync(int foodTruckId)
        {
            var foodTruck = await _foodTrucks
                .Where(ft => ft.FoodTruckId == foodTruckId)
                .SingleOrDefaultAsync();

            return foodTruck;
        }


        public async Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(Locality locality)
        {
            var foodTrucks = await _foodTrucks
                .Where(ft => ft.LocalityCode == locality.LocalityCode)
                .AsNoTracking()
                .ToListAsync();

            return foodTrucks;
        }


        /// <summary>
        /// Gets a list of FoodTruck objects that have the given tag.
        /// </summary>
        /// <param name="tag">A String of the tag to search for</param>
        /// <returns>A List of FoodTruck objects.  If no Food Trucks are found with
        /// the given tag, an empty list is returned</returns>
        public async Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(string tag)
        {
            var foodTrucks = await _foodTrucks
                .Where(ft => ft.Tags.Any(ftt => ftt.Tag.Text == tag) )
                .AsNoTracking()
                .ToListAsync();

            return foodTrucks;
        }


        public async Task<IEnumerable<FoodTruck>> GetFoodTrucksAsync(Locality locality, string tag)
        {
            var foodTrucks = await _foodTrucks
                .Where(ft => ft.LocalityCode == locality.LocalityCode)
                .Where(ft => ft.Tags.Any(ftt => ftt.Tag.Text == tag))
                .AsNoTracking()
                .ToListAsync();

            return foodTrucks;
        }


        public Task SaveAsync(FoodTruck foodTruck)
        {
            _foodTruckContext.ChangeTracker.TrackGraph(foodTruck, EfExtensions.ConvertStateOfNode);
            return Task.CompletedTask;
        }


        public Task DeleteAsync(FoodTruck foodTruck)
        {
            _foodTruckContext.Remove(foodTruck);
            return Task.CompletedTask;
        }

    }
}
