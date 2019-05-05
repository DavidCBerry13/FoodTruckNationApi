using FoodTruckNation.Core.DataInterfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FoodTruckNation.Core.Domain;
using Framework;

namespace FoodTruckNation.Data.EF.Repositories
{
    public class TagRepository : ITagRepository
    {
        public TagRepository(FoodTruckContext context)
        {
            _dataContext = context;
        }

        private readonly FoodTruckContext _dataContext;


        public IList<Tag> GetAllTags()
        {
           return _dataContext.Tags.AsNoTracking().ToList();
        }


        public IList<Tag> GetAllTagsInUse()
        {
            return _dataContext.FoodTrucks
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .SelectMany(ft => ft.Tags.Select(ftt => ftt.Tag))
                .Distinct()
                .AsNoTracking()
                .ToList();                                   
        }



        public Tag GetTagById(int id)
        {
            return _dataContext.Tags
                .Where(t => t.TagId == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Tag GetTagByName(string name)
        {
            return _dataContext.Tags
                .Where(t => t.Text == name)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void SaveTag(Tag tag)
        {
            _dataContext.ChangeTracker.TrackGraph(tag, EfExtensions.ConvertStateOfNode);
        }
    }
}
