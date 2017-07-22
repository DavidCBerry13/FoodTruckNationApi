using FoodTruckNation.AppServices.Interfaces;
using FoodTruckNation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.Domain;
using System.Linq;
using FoodTruckNation.AppServices.Models;
using Framework;

namespace FoodTruckNation.AppServices
{
    public class TagService : BaseService, ITagService
    {

        public TagService(ILoggerFactory loggerFactory, IUnitOfWork uow, ITagRepository tagRepository)
            : base(loggerFactory, uow)
        {
            this.tagRepository = tagRepository;
        }


        private ITagRepository tagRepository;


        public EntityResult<IList<Tag>> GetAllTags()
        {
            try
            {
                var tags = this.tagRepository.GetAllTags();
                return EntityResult<IList<Tag>>.Success(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetAllTags()");
                return EntityResult<IList<Tag>>.Error("An error occured processing your request");
            }
        }


        public EntityResult<IList<Tag>> GetTagsInUse()
        {
            try
            {
                var tags = this.tagRepository.GetAllTagsInUse();
                return EntityResult<IList<Tag>>.Success(tags);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagsInUse()");
                return EntityResult<IList<Tag>>.Error("An error occured processing your request");
            }
        }


        public EntityResult<Tag> GetTagById(int tagId)
        {
            try
            {
                var tag = this.tagRepository.GetTagById(tagId);
                return EntityResult<Tag>.Success(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagById()");
                return EntityResult<Tag>.Error("An error occured processing your request");
            }
        }



        public EntityResult<Tag> GetTagByName(string tagName)
        {
            try
            {
                var tag = this.tagRepository.GetTagByName(tagName);
                return EntityResult<Tag>.Success(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagByName()");
                return EntityResult<Tag>.Error("An error occured processing your request");
            }
        }



        public EntityResult<Tag> CreateNewTag(string tagText)
        {
            try
            {
                Tag tag = new Tag(tagText);

                this.tagRepository.SaveTag(tag);
                this.UnitOfWork.SaveChanges();


                return EntityResult<Tag>.Success(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.CreateNewTag()");
                return EntityResult<Tag>.Error("An error occured processing your request");
            }
        }


        public EntityResult<Tag> UpdateTag(UpdateTagInfo tagInfo)
        {
            try
            {
                Tag tag = this.tagRepository.GetTagById(tagInfo.TagId);

                if (tag == null)
                    return EntityResult<Tag>.Failure($"No tag was found to update with the id of {tagInfo.TagId}");

                tag.Text = tagInfo.TagText;

                this.tagRepository.SaveTag(tag);
                this.UnitOfWork.SaveChanges();

                return EntityResult<Tag>.Success(tag);
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.UpdateTag()");
                return EntityResult<Tag>.Error("An error occured processing your request");
            }
        }
    }
}
