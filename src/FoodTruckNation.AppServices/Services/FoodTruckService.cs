using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.AppServices.Interfaces;
using FoodTruckNation.AppServices.Models;
using FoodTruckNation.Domain;
using FoodTruckNation.Domain.Repositories;
using Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodTruckNation.AppServices
{
    public class FoodTruckService : BaseService, IFoodTruckService
    {
        

        public FoodTruckService(ILoggerFactory loggerFactory, IUnitOfWork uow, 
            IFoodTruckRepository foodTruckRepository, ITagRepository tagRepository)
            : base(loggerFactory, uow)
        {
            this.foodTruckRepository = foodTruckRepository;
            this.tagRepository = tagRepository;
        }


        #region Member Variables

        private IFoodTruckRepository foodTruckRepository;
        private ITagRepository tagRepository;

        #endregion


        public EntityResult<List<FoodTruck>> GetAllFoodTrucks()
        {
            try
            {
                var foodTrucks = this.foodTruckRepository.GetAllFoodTrucks();
                return EntityResult<List<FoodTruck>>.Success(foodTrucks.ToList());
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling FoodTruckService.GetAllFoodTrucks()");
                return EntityResult<List<FoodTruck>>.Error("An error occured processing your request");
            }
        }


        public EntityResult<List<FoodTruck>> GetFoodTrucksByTag(String tag)
        {
            try
            {
                var foodTrucks = this.foodTruckRepository.GetFoodTruckByTag(tag);
                return EntityResult<List<FoodTruck>>.Success(foodTrucks.ToList());
            }
            catch (Exception ex)
            {                
                Logger.LogError(new EventId(102), ex, $"Error thrown while calling FoodTruckService.GetFoodTrucksByTag(), tag={tag}");
                return EntityResult<List<FoodTruck>>.Error("An error occured processing your request");
            }
        }



        public EntityResult<FoodTruck> GetFoodTruck(int id)
        {
            try
            {
                var foodTruck = this.foodTruckRepository.GetFoodTruck(id);
                return EntityResult<FoodTruck>.Success(foodTruck);
            }
            catch (Exception ex)
            {                
                Logger.LogError(new EventId(103), ex, $"Error thrown while calling FoodTruckService.GetFoodTruck(), id={id}");
                return EntityResult<FoodTruck>.Error("An error occured processing your request");
            }
        }


        public EntityResult<FoodTruck> CreateFoodTruck(CreateFoodTruckInfo foodTruckInfo)
        {
            try
            {
                // Creates our Food Truck object
                var foodTruck = new FoodTruck(foodTruckInfo.Name, foodTruckInfo.Description, foodTruckInfo.Website);

                // Converts tag strings into tag objects (including creating tags that don't exist)
                var tagObjects = this.DecodeTags(foodTruckInfo.Tags);

                // Attaches the tags to the Food Truck Object
                tagObjects.ForEach(obj => foodTruck.AddTag(obj));

                // Persist to the database
                this.foodTruckRepository.Save(foodTruck);
                this.UnitOfWork.SaveChanges();

                return EntityResult<FoodTruck>.Success(foodTruck);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(104), ex, $"Error thrown while calling FoodTruckService.CreateFoodTruck()");
                return EntityResult<FoodTruck>.Error("An error occured processing your request");
            }

        }


        internal List<Tag> DecodeTags(IEnumerable<String> inputTags)
        {
            List<Tag> tagObjects = new List<Tag>();

            var allTags = this.tagRepository.GetAllTags();
            foreach (var tag in inputTags)
            {
                var tagObject = allTags.Where(t => t.Text == tag).SingleOrDefault();
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




        public EntityResult<FoodTruck> UpdateFoodTruck(UpdateFoodTruckInfo foodTruckInfo)
        {
            try
            {
                // Creates our Food Truck object
                var foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckInfo.FoodTruckId);

                if (foodTruck == null)
                    return EntityResult<FoodTruck>.Failure($"No food truck was found with the id {foodTruckInfo.FoodTruckId}");

                // Handle Properties
                foodTruck.Name = foodTruckInfo.Name;
                foodTruck.Description = foodTruckInfo.Description;
                foodTruck.Website = foodTruckInfo.Website;

                // Deal with the tags
                var inputTags = foodTruckInfo.Tags;    // For convenience

                // Handle Tags on Object but not in Input list (i.e. tags to be removed)
                var removedTags = foodTruck.Tags.WhereNotExists(inputTags, (foodTruckTag, inputTag) => (foodTruckTag.Tag.Text == inputTag));
                removedTags.ToList().ForEach(removedTag => foodTruck.RemoveTag(removedTag));

                // Now deal with the tags that are on the object
                var newTags = inputTags.WhereNotExists(foodTruck.Tags, (inputTag, foodTruckTag) => (inputTag == foodTruckTag.Tag.Text));

                // Converts tag strings into tag objects (including creating tags that don't exist)
                var tagObjects = this.DecodeTags(newTags);

                // Attaches the tags to the Food Truck Object
                tagObjects.ForEach(obj => foodTruck.AddTag(obj));

                // Persist the changes to the database
                this.foodTruckRepository.Save(foodTruck);
                this.UnitOfWork.SaveChanges();

                return EntityResult<FoodTruck>.Success(foodTruck);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(104), ex, $"Error thrown while calling FoodTruckService.UpdateFoodTruck()");
                return EntityResult<FoodTruck>.Error("An error occured processing your request");
            }
        }


        internal List<FoodTruckTag> FindRemovedTags(List<String> inputTags, List<FoodTruckTag> foodTruckTags)
        {
            var removedTags = foodTruckTags.Where(foodTruckTag => inputTags.Any(inputTag => inputTag == foodTruckTag.Tag.Text)).ToList();
            return removedTags;
        }



        public Result DeleteFoodTruck(int foodTruckId)
        {
            FoodTruck foodTruck = this.foodTruckRepository.GetFoodTruck(foodTruckId);

            if (foodTruck == null)
                return Result.Failure($"Food truck id {foodTruckId} not found so it could not be deleted");

            this.foodTruckRepository.Delete(foodTruck);
            this.UnitOfWork.SaveChanges();

            return Result.Success();
        }


    }
}
