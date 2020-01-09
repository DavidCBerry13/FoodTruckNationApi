using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using DavidBerry.Framework;
using DavidBerry.Framework.Functional;

namespace FoodTruckNation.Core.AppInterfaces
{

    /// <summary>
    /// Interface defining the FoodTruckService, mainly so the service can be mocked
    /// </summary>
    public interface IFoodTruckService
    {

        Result<List<FoodTruck>> GetAllFoodTrucks();

        Result<List<FoodTruck>> GetFoodTrucksByTag(string tag);

        Result<FoodTruck> GetFoodTruck(int id);

        Result<FoodTruck> CreateFoodTruck(CreateFoodTruckCommand command);

        Result<FoodTruck> UpdateFoodTruck(UpdateFoodTruckCommand command);

        Result DeleteFoodTruck(int foodTruckId);

        Result<List<Review>> GetFoodTruckReviews(int foodTruckId);

        Result<Review> GetFoodTruckReview(int foodTruckId, int reviewId);

        Result<Review> CreateFoodTruckReview(CreateReviewCommand command);

        Result<FoodTruck> AddFoodTruckTags(int foodTruckId, List<string> tags);

        Result<FoodTruck> UpdateFoodTruckTags(int foodTruckId, List<string> tags);

        Result DeleteFoodTruckTag(int foodTruckId, string tag);

        Result<SocialMediaAccount> AddSocialMediaAccount(int foodTruckId, int socialMediaPlatformId, string accountName);

        Result<SocialMediaAccount> UpdateSocialMediaAccount(int foodTruckId, int socialMediaAccountId, string accountName);

        Result DeleteSocialMediaAccount(int foodTruckId, int socialMediaPlatformId);

    }
}
