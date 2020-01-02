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
            AddTagToStringMapping();
        }


        public void AddTagToStringMapping()
        {

            CreateMap<Tag, string>()
                .ConvertUsing(tag => tag.Text );
        }

    }
}
