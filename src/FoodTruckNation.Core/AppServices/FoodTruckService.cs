using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FoodTruckNation.Core.AppServices
{
    public class FoodTruckService : BaseService, IFoodTruckService
    {
        

        public FoodTruckService(ILoggerFactory loggerFactory, IUnitOfWork uow, IDateTimeProvider dateTimeProvider,
            IFoodTruckRepository foodTruckRepository, ITagRepository tagRepository, ISocialMediaPlatformRepository socialMediaPlatformRepository)
            : base(loggerFactory, uow)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.foodTruckRepository = foodTruckRepository;
            this.tagRepository = tagRepository;
            this.socialMediaPlatformRepository = socialMediaPlatformRepository;
        }


        #region Member Variables

        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IFoodTruckRepository foodTruckRepository;
        private readonly ITagRepository tagRepository;
        private readonly ISocialMediaPlatformRepository socialMediaPlatformRepository;

        #endregion


        public List<FoodTruck> GetAllFoodTrucks()
        {
            try
            {
                var foodTrucks = this.foodTruckRepository.GetAllFoodTrucks();
                return foodTrucks.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling FoodTruckService.GetAllFoodTrucks()");
                throw;
            }
        }


        public List<FoodTruck> GetFoodTrucksByTag(String tag)
        {
            try
            {
                var foodTrucks = this.foodTruckRepository.GetFoodTruckByTag(tag);
                return foodTrucks.ToList();
            }
            catch (Exception ex)
            {                
                Logger.LogError(new EventId(102), ex, $"Error thrown while calling FoodTruckService.GetFoodTrucksByTag(), tag={tag}");
                throw;
            }
        }



        public FoodTruck GetFoodTruck(int id)
        {
            try
            {
                var foodTruck = this.foodTruckRepository.GetFoodTruck(id);
                return foodTruck;
            }
            catch (Exception ex)
            {                
                Logger.LogError(new EventId(103), ex, $"Error thrown while calling FoodTruckService.GetFoodTruck(), id={id}");
                throw;
            }
        }


        public FoodTruck CreateFoodTruck(CreateFoodTruckCommand foodTruckInfo)
        {
            try
            {
                // Creates our Food Truck object
                var foodTruck = new FoodTruck(foodTruckInfo.Name, foodTruckInfo.Description, foodTruckInfo.Website);

                // Converts tag strings into tag objects (including creating tags that don't exist)
                var tagObjects = this.DecodeTags(foodTruckInfo.Tags);

                // Attaches the tags to the Food Truck Object
                tagObjects.ForEach(obj => foodTruck.AddTag(obj));

                // Social Media Accounts
                foreach (var accountInfo in foodTruckInfo.SocialMediaAccounts)
                {
                    var platform = this.socialMediaPlatformRepository.GetSocialMediaPlatform(accountInfo.SocialMediaPlatformId);
                    if (platform == null)
                        throw new InvalidDataException($"The id {accountInfo.SocialMediaPlatformId} is not a valid social media platform id");
                    
                    SocialMediaAccount account = new SocialMediaAccount(platform, foodTruck, accountInfo.AccountName);
                    foodTruck.AddSocialMediaAccount(account);
                }

                // Persist to the database
                this.foodTruckRepository.Save(foodTruck);
                this.UnitOfWork.SaveChanges();

                return foodTruck;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(104), ex, $"Error thrown while calling FoodTruckService.CreateFoodTruck()");
                throw;
            }

        }


        internal List<Tag> DecodeTags(IEnumerable<String> inputTags)
        {
            List<Tag> tagObjects = new List<Tag>();

            var allTags = this.tagRepository.GetAllTags();
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




        public FoodTruck UpdateFoodTruck(UpdateFoodTruckCommand foodTruckInfo)
        {
            try
            {
                // Creates our Food Truck object
                var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckInfo.FoodTruckId);

                if (foodTruck == null)
                    throw new ObjectNotFoundException($"No food truck was found with the id {foodTruckInfo.FoodTruckId}");

                // Handle Properties
                foodTruck.Name = foodTruckInfo.Name;
                foodTruck.Description = foodTruckInfo.Description;
                foodTruck.Website = foodTruckInfo.Website;
                foodTruck.LastModifiedDate = foodTruckInfo.LastModifiedDate;

                // Persist the changes to the database
                this.foodTruckRepository.Save(foodTruck);
                this.UnitOfWork.SaveChanges();

                return foodTruck;
            }
            catch (DBConcurrencyException ce)
            {
                // This is the object in the database - so the object our update conflicted with
                var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckInfo.FoodTruckId);

                Logger.LogWarning(new EventId(125), ce, $"Concurrency exception thrown while calling FoodTruckService.UpdateFoodTruck() - Update Command = || {foodTruckInfo.ToJson()} || Database Object = || {foodTruck.ToJson() } ||");

                // Get the current state of the object so we can return it with the ConcurrencyException
                throw new ConcurrencyException<FoodTruck>($"The food truck could not be updated due to a concurrency exception.  This is most likely because the object has changed since the object was retrieved.  Compare your changes to the current state of the object (included) and resubmit as neccessary",
                    foodTruck);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(104), ex, $"Error thrown while calling FoodTruckService.UpdateFoodTruck()");
                throw;
            }
        }


        internal List<FoodTruckTag> FindRemovedTags(List<String> inputTags, List<FoodTruckTag> foodTruckTags)
        {
            var removedTags = foodTruckTags.Where(foodTruckTag => inputTags.Any(inputTag => inputTag == foodTruckTag.Tag.Text)).ToList();
            return removedTags;
        }



        public void DeleteFoodTruck(int foodTruckId)
        {
            FoodTruck foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
               throw new ObjectNotFoundException($"Food truck id {foodTruckId} not found so it could not be deleted");

            this.foodTruckRepository.Delete(foodTruck);
            this.UnitOfWork.SaveChanges();
        }




        public FoodTruck AddFoodTruckTags(int foodTruckId, List<String> tags)
        {
            try
            {
                // Get the Food Truck object
                var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
                if (foodTruck == null)
                    throw new ObjectNotFoundException("No food truck with the id of {foodTruckId} could be found");

                // Converts tag strings into tag objects (including creating tags that don't exist)
                var tagObjects = this.DecodeTags(tags);

                // Attaches the tags to the Food Truck Object
                tagObjects.ForEach(obj => foodTruck.AddTag(obj));

                // Persist to the database
                this.foodTruckRepository.Save(foodTruck);
                this.UnitOfWork.SaveChanges();

                return foodTruck;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(104), ex, $"Error thrown while calling FoodTruckService.AddFoodTruckTags()");
                throw;
            }
        }

        public FoodTruck UpdateFoodTruckTags(int foodTruckId, List<String> tags)
        {
            // Get the Food Truck object
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException("No food truck with the id of {foodTruckId} could be found");

            // Handle Tags on Object but not in Input list (i.e. tags to be removed)
            var removedTags = foodTruck.Tags.WhereNotExists(tags, (foodTruckTag, inputTag) => (foodTruckTag.Tag.Text == inputTag));
            removedTags.ToList().ForEach(removedTag => foodTruck.RemoveTag(removedTag));

            // Now deal with the tags that are on the object
            var newTags = tags.WhereNotExists(foodTruck.Tags, (inputTag, foodTruckTag) => (inputTag == foodTruckTag.Tag.Text));

            // Converts tag strings into tag objects (including creating tags that don't exist)
            var tagObjects = this.DecodeTags(newTags);

            // Attaches the tags to the Food Truck Object
            tagObjects.ForEach(obj => foodTruck.AddTag(obj));


            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return foodTruck;
        }

        public void DeleteFoodTruckTag(int foodTruckId, String tag)
        {
            // Get the Food Truck object
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException("No food truck with the id of {foodTruckId} could be found");

            var tagToRemove = foodTruck.Tags.FirstOrDefault(t => t.Tag.Text.Equals(tag, StringComparison.CurrentCultureIgnoreCase) );
            if ( tagToRemove == null)
                throw new ObjectNotFoundException("No tag of {tag} found on the food truck with the id of {foodTruckId}");

            foodTruck.RemoveTag(tagToRemove);

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();
        }




        public SocialMediaAccount AddSocialMediaAccount(int foodTruckId, int socialMediaPlatformId, String accountName)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException("No food truck with the id of {foodTruckId} could be found");

            var platform = this.socialMediaPlatformRepository.GetSocialMediaPlatform(socialMediaPlatformId);
            if (platform == null)
                throw new ObjectNotFoundException("No social media platform with the id {socialMediaPlatformId} could be found");

            SocialMediaAccount account = new SocialMediaAccount(platform, foodTruck, accountName);
            foodTruck.AddSocialMediaAccount(account);

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return account;
        }

        public SocialMediaAccount UpdateSocialMediaAccount(int foodTruckId, int socialMediaAccountId, String accountName)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException($"No food truck with the id of {foodTruckId} could be found");

            SocialMediaAccount account = foodTruck.SocialMediaAccounts.FirstOrDefault(a => a.SocialMediaAccountId == socialMediaAccountId);
            if (account == null)
                throw new ObjectNotFoundException($"No social media account with with the id {socialMediaAccountId} could be found on the food truck with id {foodTruckId}");

            account.AccountName = accountName;

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return account;
        }

        public void DeleteSocialMediaAccount(int foodTruckId, int socialMediaPlatformId)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);
            if (foodTruck == null)
                throw new ObjectNotFoundException("No food truck with the id of {foodTruckId} could be found");

            var platform = this.socialMediaPlatformRepository.GetSocialMediaPlatform(socialMediaPlatformId);
            if (platform == null)
                throw new ObjectNotFoundException($"No social media platform with the id {socialMediaPlatformId} could be found");

            SocialMediaAccount account = foodTruck.SocialMediaAccounts.FirstOrDefault(a => a.PlatformId == socialMediaPlatformId);
            if (account == null)
                throw new ObjectNotFoundException($"No social media account with for {platform.Name} could be found on the food truck with id {foodTruckId}");

            foodTruck.RemoveSocialMediaAccount(account);

            // Persist to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return;
        }




        public List<Review> GetFoodTruckReviews(int foodTruckId)
        {
            FoodTruck foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                throw new ObjectNotFoundException($"Food truck id {foodTruckId} was not found");

            return foodTruck.Reviews;
        }

        public Review GetFoodTruckReview(int foodTruckId, int reviewId)
        {
            FoodTruck foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                throw new ObjectNotFoundException($"Food truck id {foodTruckId} was not found");

            var review =  foodTruck.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);            
            return review;
        }


        public Review CreateFoodTruckReview(CreateReviewCommand command)
        {
            FoodTruck foodTruck = this.foodTruckRepository.GetFoodTruck(command.FoodTruckId);

            if (foodTruck == null)
                throw new ObjectNotFoundException($"Food truck id {command.FoodTruckId} was not found");

            Review review = new Review(foodTruck, this.dateTimeProvider.CurrentDateTime, command.Rating, command.Comments);
            foodTruck.AddReview(review);

            // Persist the changes to the database
            this.foodTruckRepository.Save(foodTruck);
            this.UnitOfWork.SaveChanges();

            return review;
        }


    }
}
