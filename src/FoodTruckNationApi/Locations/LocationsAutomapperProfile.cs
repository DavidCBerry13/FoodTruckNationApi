using AutoMapper;
using FoodTruckNation.Core.Commands;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Locations
{
    public class LocationsAutomapperProfile : Profile
    {

        public LocationsAutomapperProfile()
        {
            this.AddLocationModelMaps();
        }


        private void AddLocationModelMaps()
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
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocationsController.GET_LOCATION_BY_ID,
                            RouteParams = new { id = src.LocationId }
                        }
                    )
                )
                .ForMember(
                    dest => dest.Schedules,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = Schedules.LocationSchedulesController.GET_ALL_SCHEDULES_FOR_LOCATION,
                            RouteParams = new { locationId = src.LocationId }
                        }
                    )
                );

        }

        private void AddCreateLocationModelMaps()
        {
            this.CreateMap<CreateLocationModel, CreateLocationCommand>();
        }

        private void AddUpdateLocationModelMaps()
        {
            this.CreateMap<UpdateLocationModel, UpdateLocationCommand>();

        }
    }
}
