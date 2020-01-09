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

namespace FoodTruckNation.Core.AppServices
{
    public class TagService : BaseService, ITagService
    {

        public TagService(ILoggerFactory loggerFactory, IUnitOfWork uow, ITagRepository tagRepository)
            : base(loggerFactory, uow)
        {
            _tagRepository = tagRepository;
        }


        private readonly ITagRepository _tagRepository;


        public Result<IList<Tag>> GetAllTags()
        {
            try
            {
                var tags = _tagRepository.GetAllTags();
                return Result.Success<IList<Tag>>(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetAllTags()");
                throw;
            }
        }


        public Result<IList<Tag>> GetTagsInUse()
        {
            try
            {
                var tags = _tagRepository.GetAllTagsInUse();
                return Result.Success<IList<Tag>>(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagsInUse()");
                throw;
            }
        }


        public Result<Tag> GetTagById(int tagId)
        {
            try
            {
                var tag = _tagRepository.GetTagById(tagId);
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



        public Result<Tag> GetTagByName(string tagName)
        {
            try
            {
                var tag = _tagRepository.GetTagByName(tagName);
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



        public Result<Tag> CreateNewTag(string tagText)
        {
            try
            {
                Tag tag = new Tag(tagText);

                _tagRepository.SaveTag(tag);
                UnitOfWork.SaveChanges();

                return Result.Success<Tag>(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.CreateNewTag()");
                throw;
            }
        }


        public Result<Tag> UpdateTag(UpdateTagCommand updateTagCommand)
        {
            try
            {
                Tag tag = _tagRepository.GetTagById(updateTagCommand.TagId);

                if (tag == null)
                    return Result.Failure<Tag>(new ObjectNotFoundError($"No tag was found with the id of {updateTagCommand.TagId}"));

                tag.Text = updateTagCommand.TagText;

                _tagRepository.SaveTag(tag);
                UnitOfWork.SaveChanges();

                return Result.Success<Tag>(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.UpdateTag()");
                throw ex;
            }
        }
    }
}
