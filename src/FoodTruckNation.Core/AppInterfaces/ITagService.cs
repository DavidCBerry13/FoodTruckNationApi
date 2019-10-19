using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework;
using Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ITagService
    {

        Result<IList<Tag>> GetAllTags();

        Result<IList<Tag>> GetTagsInUse();

        Result<Tag> GetTagById(int tagId);

        Result<Tag> GetTagByName(string tag);

        Result<Tag> CreateNewTag(string tag);

        Result<Tag> UpdateTag(UpdateTagCommand updateTagCommand);


    }
}
