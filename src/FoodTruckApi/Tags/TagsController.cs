using System.Collections.Generic;
using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ApiUtil.Models;
using DavidBerry.Framework.Functional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodTruckNationApi.Tags
{

    /// <summary>
    /// Controller for API endpoints dealing with tags
    /// </summary>
    [Produces("application/json")]
    [Route("api/Tags")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class TagsController : ApiControllerBase
    {

        /// <summary>
        /// Creates a new TagsController
        /// </summary>
        /// <param name="logger">An ILoggerFactory of the factory class used to create an ILogger instance for use in this controller</param>
        /// <param name="mapper">An AutoMapper IMapper instance used to perform object mapping in the controller</param>
        /// <param name="tagService">An iTagService instance of the tag service object where the business logic for tag functions resides</param>
        public TagsController(ILogger<TagsController> logger, IMapper mapper, ITagService tagService)
            : base(logger, mapper)
        {
            _tagService = tagService;
        }


        private readonly ITagService _tagService;


        /// <summary>
        /// Gets a list of all of the tags in the system
        /// </summary>
        /// <param name="inUseOnly"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IList<Tag>> Get([FromQuery]bool inUseOnly = false)
        {
            Result<IList<Tag>> result = inUseOnly
                ? _tagService.GetTagsInUse()
                : _tagService.GetAllTags();

            return CreateResponse<IList<Tag>, List<string>>(result);
        }



    }
}