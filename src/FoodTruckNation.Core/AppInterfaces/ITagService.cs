using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework;
using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ITagService
    {

        public Task<Result<IEnumerable<Tag>>> GetAllTagsAsync();

        public Task<Result<IEnumerable<Tag>>> GetTagsInUseAsync();

        public Task<Result<Tag>> GetTagByIdAsync(int tagId);

        public Task<Result<Tag>> GetTagByNameAsync(string tag);

        public Task<Result<Tag>> CreateNewTagAsync(string tag);

        public Task<Result<Tag>> UpdateTagAsync(UpdateTagCommand updateTagCommand);


    }
}
