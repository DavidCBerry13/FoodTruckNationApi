using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.Data;
using System.Threading.Tasks;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class TagRepository : ITagRepository
    {
        public TagRepository(FoodTruckContext context)
        {
            _dataContext = context;
        }

        private readonly FoodTruckContext _dataContext;


        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
           return await _dataContext.Tags
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<IEnumerable<Tag>> GetAllTagsInUseAsync()
        {
            return await _dataContext.FoodTrucks
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .SelectMany(ft => ft.Tags.Select(ftt => ftt.Tag))
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Tag> GetTagByIdAsync(int id)
        {
            return await _dataContext.Tags
                .Where(t => t.TagId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            return await _dataContext.Tags
                .Where(t => t.Text == name)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task SaveTagAsync(Tag tag)
        {
            _dataContext.ChangeTracker.TrackGraph(tag, EfExtensions.ConvertStateOfNode);
            return Task.CompletedTask;
        }
    }
}
