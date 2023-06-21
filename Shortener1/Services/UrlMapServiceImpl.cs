using ConsoleApp2205.Entities;
using Shortener1.DTO;
using Shortener1.Helpers;
using Shortener1.Repositories;

namespace Shortener1.Services
{
    public class UrlMapServiceImpl : IUrlMapService
    {
        private readonly IUrlMapRepository _urlMapRepository;
        private readonly IConfiguration _configuration;


        public UrlMapServiceImpl(IUrlMapRepository urlMapRepository, IConfiguration configuration)
        {
            _urlMapRepository = urlMapRepository;
            _configuration = configuration;
        }


        // Checks if original long Urls already exists in Db
        public async Task<bool> LongUrlExist(UrlInputDto urlInputDto, int userId)
        {
            try
            {
                return await _urlMapRepository.CheckIfLongUrlExist(urlInputDto, userId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        // Creates new urlMap and save it in Db.
        public async Task<UrlOutputDTO> CreateNewShortenedUrl(UrlInputDto urlInputDto, int userId)
        {
            var shortenedUrl = _configuration["Host"] + HashGenerator.GenerateHash(urlInputDto.Url).ShortenedUrl;


            UrlMap urlMap = new(new Random().Next(), urlInputDto.Url, shortenedUrl, DateTime.Now, userId);

            await _urlMapRepository.SaveNewUrlMap(urlMap);

            return new(urlMap.ShortenedUrl, urlMap.OriginalUrl, ResponseMessages.SuccessUrlCreatedMessage,
                urlMap.UrlMapId);
        }


        //Returns original long url according to it`s shortened version.
        public async Task<UrlOutputDTO> GetLongUrlForRedirect(UrlInputDto urlInputDto, int userId)
        {
            return await _urlMapRepository.GetLongUrlByShortenedUrl(urlInputDto, userId);
        }


        // return all urlMaps from Db.
        public async Task<List<UrlMap>> GetAll(int pageIndex, int pageSize, int userId)
        {
            return await _urlMapRepository.GetAll(pageIndex, pageSize, userId);
        }
    }
}