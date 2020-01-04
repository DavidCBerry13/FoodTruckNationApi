using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil;

namespace FoodTruckNationApi.Locations
{

    /// <summary>
    /// Model class to represent a location when data is sent back to the client
    /// </summary>
    /// <remarks>
    /// Locations are places where food trucks gather
    /// </remarks>
    public class LocationModel
    {

        /// <summary>
        /// Gets the unique id of this location
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The name of this location, usually the name of a park or business that is associated with the location
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// The street address of this location
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// The city this location is in
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state of this location
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The zip code of this location
        /// </summary>
        public string ZipCode { get; set; }


        /// <summary>
        /// Meta data object containing associated links for the location
        /// </summary>
        public LocationLinks Meta { get; set; }


        /// <summary>
        /// Class to encapsulate the links (urls) for a location
        /// </summary>
        public class LocationLinks
        {
            /// <summary>
            /// Gets the URL that refers to this location
            /// </summary>
            public string Self { get; set; }

            /// <summary>
            /// URL link containing the upcoming schedules of food trucks at this location
            /// </summary>
            public string Schedules { get; set; }
        }

    }






#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class LocationModelAutomapperProfile : Profile
    {
        public LocationModelAutomapperProfile()
        {
            CreateMap<Location, LocationModel>()
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );

                // For the links in the Meta object
                CreateMap<Location, LocationModel.LocationLinks>()
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
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
