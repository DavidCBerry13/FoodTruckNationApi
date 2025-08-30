using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ITagRepository
    {

        public Task<IEnumerable<Tag>> GetAllTagsAsync();

        public Task<IEnumerable<Tag>> GetAllTagsInUseAsync();

        public Task<Tag> GetTagByIdAsync(int id);

        public Task<Tag> GetTagByNameAsync(string name);

        public Task SaveTagAsync(Tag tag);



    }
}
