using ConsoleApp2205.Entities;
using ConsoleApp2205.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shortener1.DTO;
using Shortener1.Services;

namespace Shortener1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlCreaterController : ControllerBase
    {

        
        private readonly IUrlMapService _urlMapService;

        private readonly string urlCreatedFailedMessageInvalidStructure = "Failed to create new shortened url! Url is Invalid!";
        private readonly string urlCreatedFailedMessageUrlIsDublicate = "Failed to create new shortened url! It is already exists!";
        private readonly string failedToFindLongUrlFromShortened = "Long url for redirect was`t found! See massage: ";
        private readonly string urlCreatedFailedMessage = "Failed to create new shortened url! See massage: ";


        public UrlCreaterController( IUrlMapService urlMapService)
        {
           _urlMapService = urlMapService;
        }



        [HttpGet("getAll")]
        [ProducesResponseType(200, Type = typeof(List<UrlOutputDTO>))]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_urlMapService.GetAll());
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex);
            }
        }



        [HttpPost("create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(UrlOutputDTO))]
        public IActionResult CreateNewShortUrl([FromBody] UrlInputDTO urlInputDTO)
        {
            //1. check if Url is valid 
            if (!IsValidUrl(urlInputDTO.Url))
            {
                return BadRequest(urlCreatedFailedMessageInvalidStructure);

            }

            // check if long Url already exist in db
            if (_urlMapService.LongUrlExist(urlInputDTO))
            {
                return Conflict(urlCreatedFailedMessageUrlIsDublicate);
            }
            
            try
            {
                return Ok(_urlMapService.CreateNewShortenedUrl(urlInputDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, urlCreatedFailedMessage + ex.Message);
            }
        
        }



        [HttpPost("getLong")]
        [ProducesResponseType(200, Type = typeof(UrlOutputDTO))]
        [ProducesResponseType(404)]
        
        public IActionResult GetlongUrl([FromBody] UrlInputDTO urlInputDTO)
        {
            try
            {
                return Ok(_urlMapService.GetLongUrlForRedirect(urlInputDTO));
            }
            catch (Exception ex)
            {
                return NotFound(failedToFindLongUrlFromShortened + ex.Message);
            }
            
        }


        private bool IsValidUrl(string url)
        {
            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                {
                    
                    return result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps;
                }

                return false;
            }
            catch (Exception ex) 
            {
                return false; 
            }
        }
    }
}