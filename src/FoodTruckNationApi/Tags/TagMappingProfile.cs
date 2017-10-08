using AutoMapper;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Tags
{
    public class TagMappingProfile : Profile
    {

        public TagMappingProfile()
        {
            this.AddTagToStringMapping();
        }


        public void AddTagToStringMapping()
        {

            this.CreateMap<Tag, String>()
                .ProjectUsing(tag => tag.Text );
        }

    }
}
