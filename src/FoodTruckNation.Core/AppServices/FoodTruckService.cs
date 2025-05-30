using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.TimeAndDate;
using DavidBerry.Framework.Util;
using DavidBerry.Framework.Exceptions;
using Microsoft.Extensions.Logging;
using DavidBerry.Framework.Functional;

namespace FoodTruckNation.Core.AppServices
{
    public class FoodTruckService : BaseService, IFoodTruckService
    {


        public FoodTruckService(ILoggerFactory loggerFactory, IUnitOfWork uow, IDateTimeProvider dateTimeProvider,
            IFoodTruckRepository foodTruckRepository, ITagRepository tagRepository, ISocialMediaPlatformRepository socialMediaPlatformRepository)
            : base(loggerFactory, uow)
        {
            _dateTimeProvider = dateTimeProvider;
            _foodTruckRepository = foodTruckRepository;
            _tagRepository = tagRepository;
            _socialMediaPlatformRepository = socialMediaPlatformRepository;
        }


        #region Member Variables

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFoodTruckRepository _foodTruckRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ISocialMediaPlatformRepository _socialMediaPlatformRepository;

        #endregion


        public Result<List<FoodTruck>> GetAllFoodTrucks()
        {
            var foodTrucks = _foodTruckRepository.GetAllFoodTrucks();
            return Result.Success<List<FoodTruck>>(foodTrucks.ToList());
        }


        public Result<List<FoodTruck>> GetFoodTrucksByTag(string tag)
        {
            var foodTrucks = _foodTruckRepository.GetFoodTruckByTag(tag);
            return Result.Success<List<FoodTruck>>(foodTrucks.ToList());
        }



        public Result<FoodTruck> GetFoodTruck(int id)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(id);
            return (foodTruck != null)
                ? Result.Success<FoodTruck>(foodTruck)
                : Result.Failure<FoodTruck>(new ObjectNotFoundError($"No food truck found with the id of ${id}"));
        }


        public Result<FoodTruck> CreateFoodTruck(CreateFoodTruckCommand foodTruckInfo)
        {
            // Creates our Food Truck object
            var foodTruck = new FoodTruck(foodTruckInfo.Name, foodTruckInfo.Description, foodTruckInfo.Website);

            // Converts tag strings into tag objects (including creating tags that don't exist)
            var tagObjects = DecodeTags(foodTruckInfo.Tags);

            // Attaches the tags to the Food Truck Object
            tagObjects.ForEach(obj => foodTruck.AddTag(obj));

            // Social Media Accounts
            foreach (var accountInfo in foodTruckInfo.SocialMediaAccounts)
            {
                var platform = _socialMediaPlatformRepository.GetSocialMediaPlatform(accountInfo.SocialMediaPlatformId);
                if (platform == null)
                    throw new InvalidDataException($"The id {accountInfo.SocialMediaPlatformId} is not a valid social media platform id");

                SocialMediaAccount account = new SocialMediaAccount(platform, foodTruck, accountInfo.AccountName);
                foodTruck.AddSocialMediaAccount(account);
            }

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<FoodTruck>(foodTruck);
        }


        internal List<Tag> DecodeTags(IEnumerable<string> inputTags)
        {
            List<Tag> tagObjects = new List<Tag>();

            var allTags = _tagRepository.GetAllTags();
            foreach (var tag in inputTags)
            {
                var tagObject = allTags.SingleOrDefault(t => t.Text == tag);
                if (tagObject != null)
                {
                    // Tag already exists
                    tagObjects.Add(tagObject);
                }
                else
                {
                    // This is a completely new tag
                    Tag newTag = new Tag(tag);
                    tagObjects.Add(newTag);
                }
            }
            return tagObjects;
        }




        public Result<FoodTruck> UpdateFoodTruck(UpdateFoodTruckCommand foodTruckInfo)
        {
            try
            {
                // Creates our Food Truck object
                var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckInfo.FoodTruckId);

                if (foodTruck == null)
                    return Result.Failure<FoodTruck>(new ObjectNotFoundError($"No food truck found with the id of {foodTruckInfo.FoodTruckId}"));

                // Handle Properties
                foodTruck.Name = foodTruckInfo.Name;
                foodTruck.Description = foodTruckInfo.Description;
                foodTruck.Website = foodTruckInfo.Website;
                foodTruck.LastModifiedDate = foodTruckInfo.LastModifiedDate;

                // Persist the changes to the database
                _foodTruckRepository.Save(foodTruck);
                UnitOfWork.SaveChanges();

                return Result.Success<FoodTruck>(foodTruck);
            }
            catch (DBConcurrencyException)
            {
                // If there is a database conflict, then data access layer (like EF) will throw a DbConcurrencyException, so we catch it and turn
                // it into an error to be passed up the stack with the existing object
                var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckInfo.FoodTruckId);
                return Result.Failure<FoodTruck>(
                    new ConcurrencyError<FoodTruck>($"The food truck could not be updated due to a concurrency exception.  This is most likely because the object has changed since the object was retrieved.  Compare your changes to the current state of the object (included) and resubmit as neccessary",
                    foodTruck));
            }
        }


        internal List<FoodTruckTag> FindRemovedTags(List<string> inputTags, List<FoodTruckTag> foodTruckTags)
        {
            var removedTags = foodTruckTags.Where(foodTruckTag => inputTags.Any(inputTag => inputTag == foodTruckTag.Tag.Text)).ToList();
            return removedTags;
        }



        public Result DeleteFoodTruck(int foodTruckId)
        {
            FoodTruck foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                return Result.Failure(new ObjectNotFoundError($"Food truck id {foodTruckId} not found so it could not be deleted"));

            _foodTruckRepository.Delete(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }




        public Result<FoodTruck> AddFoodTruckTags(int foodTruckId, List<string> tags)
        {
            // Get the Food Truck object
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure<FoodTruck>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            // Converts tag strings into tag objects (including creating tags that don't exist)
            var tagObjects = DecodeTags(tags);

            // Attaches the tags to the Food Truck Object
            tagObjects.ForEach(obj => foodTruck.AddTag(obj));

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<FoodTruck>(foodTruck);
        }

        public Result<FoodTruck> UpdateFoodTruckTags(int foodTruckId, List<string> tags)
        {
            // Get the Food Truck object
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure<FoodTruck>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            // Handle Tags on Object but not in Input list (i.e. tags to be removed)
            var removedTags = foodTruck.Tags.WhereNotExists(tags, (foodTruckTag, inputTag) => ( foodTruckTag.Tag.Text == inputTag ));
            removedTags.ToList().ForEach(removedTag => foodTruck.RemoveTag(removedTag));

            // Now deal with the tags that are on the object
            var newTags = tags.WhereNotExists(foodTruck.Tags, (inputTag, foodTruckTag) => ( inputTag == foodTruckTag.Tag.Text ));

            // Converts tag strings into tag objects (including creating tags that don't exist)
            var tagObjects = DecodeTags(newTags);

            // Attaches the tags to the Food Truck Object
            tagObjects.ForEach(obj => foodTruck.AddTag(obj));


            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<FoodTruck>(foodTruck);
        }

        public Result DeleteFoodTruckTag(int foodTruckId, string tag)
        {
            // Get the Food Truck object
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure<FoodTruck>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            var tagToRemove = foodTruck.Tags.FirstOrDefault(t => t.Tag.Text.Equals(tag, StringComparison.CurrentCultureIgnoreCase));
            if (tagToRemove == null)
                return Result.Failure<FoodTruck>(new ObjectNotFoundError("No tag of {tag} found on the food truck with the id of {foodTruckId}"));

            foodTruck.RemoveTag(tagToRemove);

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }




        public Result<SocialMediaAccount> AddSocialMediaAccount(int foodTruckId, int socialMediaPlatformId, string accountName)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure<SocialMediaAccount>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            var platform = _socialMediaPlatformRepository.GetSocialMediaPlatform(socialMediaPlatformId);
            if (platform == null)
                return Result.Failure<SocialMediaAccount>(new InvalidDataError("No social media platform with the id {socialMediaPlatformId} could be found"));

            SocialMediaAccount account = new SocialMediaAccount(platform, foodTruck, accountName);
            foodTruck.AddSocialMediaAccount(account);

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<SocialMediaAccount>(account);
        }

        public Result<SocialMediaAccount> UpdateSocialMediaAccount(int foodTruckId, int socialMediaAccountId, string accountName)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure<SocialMediaAccount>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            SocialMediaAccount account = foodTruck.SocialMediaAccounts.FirstOrDefault(a => a.SocialMediaAccountId == socialMediaAccountId);
            if (account == null)
                return Result.Failure<SocialMediaAccount>(new ObjectNotFoundError($"No social media account with with the id {socialMediaAccountId} could be found on the food truck with id {foodTruckId}"));

            account.AccountName = accountName;

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<SocialMediaAccount>(account);
        }

        public Result DeleteSocialMediaAccount(int foodTruckId, int socialMediaPlatformId)
        {
            var foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                return Result.Failure(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            var platform = _socialMediaPlatformRepository.GetSocialMediaPlatform(socialMediaPlatformId);
            if (platform == null)
                return Result.Failure(new ObjectNotFoundError($"No social media platform with the id {socialMediaPlatformId} could be found"));

            SocialMediaAccount account = foodTruck.SocialMediaAccounts.FirstOrDefault(a => a.PlatformId == socialMediaPlatformId);
            if (account == null)
                return Result.Failure(new ObjectNotFoundError($"No social media account with for {platform.Name} could be found on the food truck with id {foodTruckId}"));

            foodTruck.RemoveSocialMediaAccount(account);

            // Persist to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success();
        }




        public Result<List<Review>> GetFoodTruckReviews(int foodTruckId)
        {
            FoodTruck foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                return Result.Failure<List<Review>>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            return Result.Success<List<Review>>(foodTruck.Reviews);
        }

        public Result<Review> GetFoodTruckReview(int foodTruckId, int reviewId)
        {
            FoodTruck foodTruck = _foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                return Result.Failure<Review>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            var review = foodTruck.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
            return Result.Success<Review>(review);
        }


        public Result<Review> CreateFoodTruckReview(CreateReviewCommand command)
        {
            FoodTruck foodTruck = _foodTruckRepository.GetFoodTruck(command.FoodTruckId);

            if (foodTruck == null)
                return Result.Failure<Review>(new ObjectNotFoundError("No food truck with the id of {foodTruckId} could be found"));

            Review review = new Review(foodTruck, _dateTimeProvider.CurrentDateTime, command.Rating, command.Comments);
            foodTruck.AddReview(review);

            // Persist the changes to the database
            _foodTruckRepository.Save(foodTruck);
            UnitOfWork.SaveChanges();

            return Result.Success<Review>(review);
        }


    }
}
