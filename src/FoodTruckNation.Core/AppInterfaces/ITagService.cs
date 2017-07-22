using FoodTruckNation.Core.Domain;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ITagService
    {

        IList<Tag> GetAllTags();

        IList<Tag> GetTagsInUse();

        Tag GetTagById(int tagId);

        Tag GetTagByName(String tag);

        Tag CreateNewTag(String tag);

        // TODO: Update Tag Method


    }
}
