using FoodTruckNation.AppServices.Framework;
using FoodTruckNation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.AppServices.Interfaces
{
    public interface ITagService
    {

        EntityResult<IList<Tag>> GetAllTags();

        EntityResult<IList<Tag>> GetTagsInUse();

        EntityResult<Tag> GetTagById(int tagId);

        EntityResult<Tag> GetTagByName(String tag);

        EntityResult<Tag> CreateNewTag(String tag);

        // TODO: Update Tag Method


    }
}
