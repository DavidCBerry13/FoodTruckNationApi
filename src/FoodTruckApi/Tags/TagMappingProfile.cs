using AutoMapper;
using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Tags
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
