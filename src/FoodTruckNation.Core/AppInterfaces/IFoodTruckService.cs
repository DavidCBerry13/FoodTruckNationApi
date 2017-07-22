using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using FoodTruckNation.Core.Commands;
using Framework;

namespace FoodTruckNation.Core.AppInterfaces
{

    /// <summary>
    /// Interface defining the FoodTruckService, mainly so the service can be mocked
    /// </summary>
    public interface IFoodTruckService
    {

        List<FoodTruck> GetAllFoodTrucks();

        List<FoodTruck> GetFoodTrucksByTag(String tag);

        FoodTruck GetFoodTruck(int id);

        FoodTruck CreateFoodTruck(CreateFoodTruckCommand command);

        FoodTruck UpdateFoodTruck(UpdateFoodTruckCommand command);

        void DeleteFoodTruck(int foodTruckId);

        List<Review> GetFoodTruckReviews(int foodTruckId);

        Review GetFoodTruckReview(int foodTruckId, int reviewId);

        Review CreateFoodTruckReview(CreateReviewCommand command);

        FoodTruck AddFoodTruckTags(int foodTruckId, List<String> tags);

        FoodTruck UpdateFoodTruckTags(int foodTruckId, List<String> tags);

        void DeleteFoodTruckTag(int foodTruckId, String tag);

        SocialMediaAccount AddSocialMediaAccount(int foodTruckId, int socialMediaPlatformId, String accountName);

        SocialMediaAccount UpdateSocialMediaAccount(int foodTruckId, int socialMediaAccountId, String accountName);

        void DeleteSocialMediaAccount(int foodTruckId, int socialMediaPlatformId);

    }
}
