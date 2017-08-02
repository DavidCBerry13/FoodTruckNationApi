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
            this.dataContext = context;
        }

        private FoodTruckContext dataContext;


        public IList<Tag> GetAllTags()
        {
           return this.dataContext.Tags.AsNoTracking().ToList();
        }


        public IList<Tag> GetAllTagsInUse()
        {
            return this.dataContext.FoodTrucks
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .SelectMany(ft => ft.Tags.Select(ftt => ftt.Tag))
                .Distinct()
                .AsNoTracking()
                .ToList();                                   
        }



        public Tag GetTagById(int id)
        {
            return this.dataContext.Tags
                .Where(t => t.TagId == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Tag GetTagByName(string name)
        {
            return this.dataContext.Tags
                .Where(t => t.Text == name)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void SaveTag(Tag tag)
        {
            this.dataContext.ChangeTracker.TrackGraph(tag, EfExtensions.ConvertStateOfNode);
        }
    }
}
