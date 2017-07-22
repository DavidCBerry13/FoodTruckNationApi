using FoodTruckNation.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using FoodTruckNation.Core.Domain;
using System.Linq;
using FoodTruckNation.Core.Commands;
using Framework;
using FoodTruckNation.Core.AppInterfaces;

namespace FoodTruckNation.Core.AppServices
{
    public class TagService : BaseService, ITagService
    {

        public TagService(ILoggerFactory loggerFactory, IUnitOfWork uow, ITagRepository tagRepository)
            : base(loggerFactory, uow)
        {
            this.tagRepository = tagRepository;
        }


        private ITagRepository tagRepository;


        public IList<Tag> GetAllTags()
        {
            try
            {
                var tags = this.tagRepository.GetAllTags();
                return tags;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetAllTags()");
                throw;
            }
        }


        public IList<Tag> GetTagsInUse()
        {
            try
            {
                var tags = this.tagRepository.GetAllTagsInUse();
                return tags;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagsInUse()");
                throw;
            }
        }


        public Tag GetTagById(int tagId)
        {
            try
            {
                var tag = this.tagRepository.GetTagById(tagId);
                return tag;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagById()");
                throw;
            }
        }



        public Tag GetTagByName(string tagName)
        {
            try
            {
                var tag = this.tagRepository.GetTagByName(tagName);
                return tag;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.GetTagByName()");
                throw;
            }
        }



        public Tag CreateNewTag(string tagText)
        {
            try
            {
                Tag tag = new Tag(tagText);

                this.tagRepository.SaveTag(tag);
                this.UnitOfWork.SaveChanges();

                return tag;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.CreateNewTag()");
                throw;
            }
        }


        public Tag UpdateTag(UpdateTagCommand updateTagCommand)
        {
            try
            {
                Tag tag = this.tagRepository.GetTagById(updateTagCommand.TagId);

                if (tag == null)
                    throw new ObjectNotFoundException($"No tag found with the id of {updateTagCommand.TagId}");

                tag.Text = updateTagCommand.TagText;

                this.tagRepository.SaveTag(tag);
                this.UnitOfWork.SaveChanges();

                return tag;
            }
            catch (Exception ex)
            {
                Logger.LogError(new EventId(101), ex, "Error thrown while calling TagService.UpdateTag()");
                throw ex;
            }
        }
    }
}
