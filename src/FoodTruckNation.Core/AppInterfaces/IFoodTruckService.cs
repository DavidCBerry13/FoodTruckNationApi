using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using DavidBerry.Framework;
using DavidBerry.Framework.Functional;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppInterfaces
{

    /// <summary>
    /// Interface defining the FoodTruckService, mainly so the service can be mocked
    /// </summary>
    public interface IFoodTruckService
    {

        public Task<Result<IEnumerable<FoodTruck>>> GetAllFoodTrucksAsync();

        public Task<Result<IEnumerable<FoodTruck>>> GetFoodTrucksAsync(string? localityCode, string? tag);

        public Task<Result<FoodTruck>> GetFoodTruckAsync(int id);

        public Task<Result<FoodTruck>> CreateFoodTruckAsync(CreateFoodTruckCommand command);

        public Task<Result<FoodTruck>> UpdateFoodTruckAsync(UpdateFoodTruckCommand command);

        public Task<Result> DeleteFoodTruckAsync(int foodTruckId);

        public Task<Result<IEnumerable<Review>>> GetFoodTruckReviewsAsync(int foodTruckId);

        public Task<Result<Review>> GetFoodTruckReviewAsync(int foodTruckId, int reviewId);

        public Task<Result<Review>> CreateFoodTruckReviewAsync(CreateReviewCommand command);

        public Task<Result<FoodTruck>> AddFoodTruckTagsAsync(int foodTruckId, List<string> tags);

        public Task<Result<FoodTruck>> UpdateFoodTruckTagsAsync(int foodTruckId, List<string> tags);

        public Task<Result> DeleteFoodTruckTagAsync(int foodTruckId, string tag);

        public Task<Result<SocialMediaAccount>> AddSocialMediaAccountAsync(int foodTruckId, int socialMediaPlatformId, string accountName);

        public Task<Result<SocialMediaAccount>> UpdateSocialMediaAccountAsync(int foodTruckId, int socialMediaAccountId, string accountName);

        public Task<Result> DeleteSocialMediaAccountAsync(int foodTruckId, int socialMediaPlatformId);

    }
}
