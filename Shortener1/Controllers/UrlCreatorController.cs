using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener1.DTO.Urls;
using Shortener1.Entities;
using Shortener1.Services;
using Shortener1.Services.BackgroundServices;

namespace Shortener1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlCreatorController : ControllerBase
    {
        private readonly IUrlMapService _urlMapService;
        private readonly AnalyticsBackgroundService _analyticsBackgroundService;
        private readonly IValidator<UrlInputDto> _validator;

     
        public UrlCreatorController(IUrlMapService urlMapService, 
                                    AnalyticsBackgroundService analyticsBackgroundService,
                                    IValidator<UrlInputDto> validator)
        {
            _urlMapService = urlMapService;
            _analyticsBackgroundService = analyticsBackgroundService;
            _validator = validator;
        }

        

        [Authorize]
        [HttpGet("getAll")]
        [ProducesResponseType(200, Type = typeof(List<UrlOutputDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(int pageIndex , int pageSize)
        {
            return Ok(await _urlMapService.GetAll( pageIndex , pageSize, GetUserId()));
        }


        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(UrlOutputDto))]
        public async Task<IActionResult> CreateNewShortUrl([FromBody] UrlInputDto urlInputDto)
        {
            var validationResult = await _validator.ValidateAsync(urlInputDto);
              //check if Url is valid 
            if ( !validationResult.IsValid)
            {
                return BadRequest(ResponseMessages.UrlValidatorFailedMessageInvalidStructure);
            }
                // check if long Url already exist in db
            if (await _urlMapService.LongUrlExist(urlInputDto, GetUserId()))
            {
                return Conflict(ResponseMessages.UrlCreatedFailedMessageUrlIsDuplicate);
            }

            return Ok( await _urlMapService.CreateNewShortenedUrl(urlInputDto, GetUserId()));
        }


        [Authorize]
        [HttpPost("getLong")]
        [ProducesResponseType(200, Type = typeof(UrlOutputDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLongUrl([FromBody] UrlInputDto urlInputDto)
        { 
            var urlOutput = await _urlMapService.GetLongUrlForRedirect(urlInputDto, GetUserId());
            var data = GetMarketingDataForBackground(urlOutput);
            _analyticsBackgroundService.EnqueueAnalytics(data); 
            return Ok(urlOutput);
            
        }

        
        private MarketingData GetMarketingDataForBackground(UrlOutputDto urlOutPutDto)
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            var userLanguage = HttpContext.Request.Headers["Accept-Language"].ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            return new MarketingData(userAgent, userLanguage, ipAddress!, urlOutPutDto.UrlMapId);
        }

        private string GetUserId()
        {
            return User.FindFirst("id")!.Value;
        }
    }
}