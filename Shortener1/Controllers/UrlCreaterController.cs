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

        private readonly ILogger<UrlCreaterController> _logger;
        private readonly IUrlMapService _urlMapService;
        private readonly string urlCreatedFailedMessageInvalidStructure = "Failed to create new shortened url! Url is Invalid!";
        private readonly string urlCreatedFailedMessageUrlIsDublicate = "Failed to create new shortened url! It is already exists!";
     
        
        public UrlCreaterController(ILogger<UrlCreaterController> logger, IUrlMapService urlMapService)
        {
            _logger = logger;
            _urlMapService = urlMapService;
        }



        [HttpGet("getAll")]
        public List<UrlMap> GetAll()
        {
            return _urlMapService.GetAll();
        }


        [HttpPost("create")]
        public UrlOutputDTO createNewShortUrl([FromBody] UrlInputDTO urlInputDTO)
        {
            //1. check if Url is valid 
            if (!IsValidUrl(urlInputDTO.Url))
            {
                return new(urlCreatedFailedMessageInvalidStructure);

            }

            // check if long Url already exist in db
            if (_urlMapService.LongUrlExist(urlInputDTO))
            {
                return new(urlCreatedFailedMessageUrlIsDublicate);
            }
                return  _urlMapService.CreateNewShortenedUrl(urlInputDTO);
         }


        [HttpPost("getLong")]
        public UrlOutputDTO getlongUrl([FromBody] UrlInputDTO urlInputDTO)
        {
           return _urlMapService.GetLongUrlForRedirect(urlInputDTO);
        }


        private bool IsValidUrl(string url)
        {
            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                {
                    // Проверяем, что протокол HTTP или HTTPS
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