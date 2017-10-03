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
using FoodTruckNationApi.Api.SocialMediaPlatforms.Models;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.Api.SocialMediaPlatforms
{
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



        public IActionResult Get()
        {
            var platforms = this._socialMediaPlatformService.GetAllSocialMediaPlatforms();

            var models = this.mapper.Map<List<SocialMediaPlatform>, List<SocialMediaPlatformModel>>(platforms);

            return Ok(models);
        }

    }
}