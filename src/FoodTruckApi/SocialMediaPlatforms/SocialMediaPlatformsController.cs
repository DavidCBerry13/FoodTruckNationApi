using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil.Models;
using Asp.Versioning;

namespace FoodTruckNationApi.SocialMediaPlatforms
{

    /// <summary>
    /// Controller exposing API endpoints related to what social media platforms are available
    /// for food trucks to have accounts on
    /// </summary>
    [Produces("application/json")]
    [Route("api/SocialMediaPlatforms")]
    [ApiVersion("1.1")]
    public class SocialMediaPlatformsController : ApiControllerBase
    {


        public SocialMediaPlatformsController(ILogger<SocialMediaPlatformsController> logger, IMapper mapper,
            ISocialMediaPlatformService socialMediaPlatformService)
            : base(logger, mapper)
        {
            _socialMediaPlatformService = socialMediaPlatformService;
        }


        private readonly ISocialMediaPlatformService _socialMediaPlatformService;


        #region Route Names

        /// <summary>
        /// Route Name Constant for route that will get all social media platforms
        /// </summary>
        internal const string GET_ALL_SOCIAL_MEDIA_PLATFORMS = "GetSocialMediaPlatforms";

        #endregion


        /// <summary>
        /// Gets a list of all social media platforms that a food truck may have a social media account on
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success.  A list of social media platforms will be returned</response>
        [ProducesResponseType(typeof(List<SocialMediaPlatformModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        [HttpGet(Name = GET_ALL_SOCIAL_MEDIA_PLATFORMS)]
        public async Task<IActionResult> Get()
        {
            var result = await _socialMediaPlatformService.GetAllSocialMediaPlatformsAsync();
            return CreateResponse<IEnumerable<SocialMediaPlatform>, IEnumerable<SocialMediaPlatformModel>>(result);
        }

    }
}