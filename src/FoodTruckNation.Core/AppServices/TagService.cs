using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DavidBerry.Framework.Data;
using DavidBerry.Framework.Exceptions;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Functional;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppServices
{
    public class TagService : BaseService, ITagService
    {

        public TagService(ILoggerFactory loggerFactory, IFoodTruckDatabase foodTruckDatabase)
            : base(loggerFactory, foodTruckDatabase)
        {
        }



        public async Task<Result<IEnumerable<Tag>>> GetAllTagsAsync()
        {
            try
            {
                var tags = await FoodTruckDatabase.TagRepository.GetAllTagsAsync();
                return Result.Success<IEnumerable<Tag>>(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetAllTags()");
                throw;
            }
        }


        public async Task<Result<IEnumerable<Tag>>> GetTagsInUseAsync()
        {
            try
            {
                var tags = await FoodTruckDatabase.TagRepository.GetAllTagsInUseAsync();
                return Result.Success<IEnumerable<Tag>>(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagsInUse()");
                throw;
            }
        }


        public async Task<Result<Tag>> GetTagByIdAsync(int tagId)
        {
            try
            {
                var tag = await FoodTruckDatabase.TagRepository.GetTagByIdAsync(tagId);
                return (tag != null)
                    ? Result.Success<Tag>(tag)
                    : Result.Failure<Tag>($"No tag found with the tag id of {tagId}");
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagById()");
                throw;
            }
        }



        public async Task<Result<Tag>> GetTagByNameAsync(string tagName)
        {
            try
            {
                var tag = await FoodTruckDatabase.TagRepository.GetTagByNameAsync(tagName);
                return ( tag != null )
                    ? Result.Success<Tag>(tag)
                    : Result.Failure<Tag>($"No tag found with the name of {tagName}");
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagByName()");
                throw;
            }
        }



        public async Task<Result<Tag>> CreateNewTagAsync(string tagText)
        {
            try
            {
                Tag tag = new Tag(tagText);

                await FoodTruckDatabase.TagRepository.SaveTagAsync(tag);
                FoodTruckDatabase.CommitChanges();

                return Result.Success<Tag>(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.CreateNewTag()");
                FoodTruckDatabase.RollbackChanges();
                throw;
            }
        }


        public async Task<Result<Tag>> UpdateTagAsync(UpdateTagCommand updateTagCommand)
        {
            try
            {
                Tag tag = await FoodTruckDatabase.TagRepository.GetTagByIdAsync(updateTagCommand.TagId);

                if (tag == null)
                    return Result.Failure<Tag>(new ObjectNotFoundError($"No tag was found with the id of {updateTagCommand.TagId}"));

                tag.Text = updateTagCommand.TagText;

                await FoodTruckDatabase.TagRepository.SaveTagAsync(tag);
                FoodTruckDatabase.CommitChanges();

                return Result.Success<Tag>(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.UpdateTag()");
                FoodTruckDatabase.RollbackChanges();
                throw;
            }
        }
    }
}
