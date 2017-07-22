using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Domain.Repositories
{
    public interface ITagRepository
    {

        IList<Tag> GetAllTags();

        IList<Tag> GetAllTagsInUse();

        Tag GetTagById(int id);

        Tag GetTagByName(String name);

        void SaveTag(Tag tag);



    }
}
