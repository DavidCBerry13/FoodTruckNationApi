using AutoMapper;
using DavidBerry.Framework.ApiUtil;
using FoodTruckNation.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodTruckApi.Localities.Models
{

    /// <summary>
    /// Model class to represent a locality to the API Client
    /// </summary>
    public class LocalityModel
    {
        /// <summary>
        /// The unique code assigned to the locality
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The name of the locality
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Meta data object containing associated links for the locality
        /// </summary>
        public LocalityLinks Meta { get; set; }

        #region Nested Types

        /// <summary>
        /// Class to encapsulate the links (urls) for a locality
        /// </summary>
        public class LocalityLinks
        {
            /// <summary>
            /// Gets the URL that refers to this locality
            /// </summary>
            public string Self { get; set; }

        }

        #endregion
    }



#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class FoodTruckModelAutomapperProfile : Profile
    {
        public FoodTruckModelAutomapperProfile()
        {
            CreateMap<Locality, LocalityModel>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => src.LocalityCode)
                )
                .ForMember(
                    dest => dest.Meta,
                    opt => opt.MapFrom(src => src)
                );

            // For the links in the Meta object
            CreateMap<Locality, LocalityModel.LocalityLinks>()
                .ForMember(
                    dest => dest.Self,
                    opt => opt.MapFrom<UrlResolver, RouteUrlInfo>(src =>
                        new RouteUrlInfo()
                        {
                            RouteName = LocalitiesController.GET_LOCALITY_BY_CODE,
                            RouteParams = new { code = src.LocalityCode }
                        }
                    )
                );
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
