using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Framework.ApiUtil.Controllers;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using Framework.ApiUtil.Models;
using FoodTruckNationApi.Api.SocialMediaPlatforms.Models;

namespace FoodTruckNationApi.Api.SocialMediaPlatforms
{

    /// <summary>
    /// Controller exposing API endpoints related to what social media platforms are availible
    /// for food trucks to have accounts on
    /// </summary>
    [Produces("application/json")]
    [Route("api/SocialMediaPlatforms")]
    public class SocialMediaPlatformsController : BaseController
    {


        public SocialMediaPlatformsController(ILogger<SocialMediaPlatformsController> logger, IMapper mapper, 
            ISocialMediaPlatformService socialMediaPlatformService)
            : base(logger, mapper)
        {
            this._socialMediaPlatformService = socialMediaPlatformService;
        }


        private ISocialMediaPlatformService _socialMediaPlatformService;



        /// <summary>
        /// Gets a list of all social media platforms that a food truck may have a social media account on
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<SocialMediaPlatformModel>), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get()
        {
            var platforms = this._socialMediaPlatformService.GetAllSocialMediaPlatforms();

            var models = this.mapper.Map<List<SocialMediaPlatform>, List<SocialMediaPlatformModel>>(platforms);

            return Ok(models);
        }

    }
}