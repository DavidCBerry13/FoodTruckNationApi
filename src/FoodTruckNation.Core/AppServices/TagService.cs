using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Framework.Data;
using Framework.Exceptions;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.DataInterfaces;
using FoodTruckNation.Core.Domain;


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


        public IList<Tag> GetAllTags()
        {
            try
            {
                var tags = _tagRepository.GetAllTags();
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
                var tags = _tagRepository.GetAllTagsInUse();
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
                var tag = _tagRepository.GetTagById(tagId);
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
                var tag = _tagRepository.GetTagByName(tagName);
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

                _tagRepository.SaveTag(tag);
                UnitOfWork.SaveChanges();

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
                Tag tag = _tagRepository.GetTagById(updateTagCommand.TagId);

                if (tag == null)
                    throw new ObjectNotFoundException($"No tag found with the id of {updateTagCommand.TagId}");

                tag.Text = updateTagCommand.TagText;

                _tagRepository.SaveTag(tag);
                UnitOfWork.SaveChanges();

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
