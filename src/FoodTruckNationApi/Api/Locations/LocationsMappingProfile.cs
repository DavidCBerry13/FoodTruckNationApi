using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Api.Locations
{

    /// <summary>
    /// Mapping profile for dealing with Location related objects in the Locations feature
    /// </summary>
    public class LocationsMappingProfile : Profile
    {

        public LocationsMappingProfile()
        {
            this.AddLocationToLocationModelMap();
            this.AddCreateLocationModelToCreateLocationCommandMap();
            this.AddUpdateLocationModelToUpdateLocationCommand();
        }


        internal void AddLocationToLocationModelMap()
        {
            this.CreateMap<Location, LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );


            // For the links in the Meta object
            this.CreateMap<Location, LocationLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.ResolveUsing<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Schedules.LocationSchedulesController.GET_ALL_SCHEDULES_FOR_LOCATION,
                            RouteParams = new { locationId = src.LocationId }
                        }
                    )
                );

        }


        internal void AddCreateLocationModelToCreateLocationCommandMap()
        {
            this.CreateMap<CreateLocationModel, CreateLocationCommand>();

        }


        internal void AddUpdateLocationModelToUpdateLocationCommand()
        {
            this.CreateMap<UpdateLocationModel, UpdateLocationCommand>();
        }






    }
}
