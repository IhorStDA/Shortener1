using System.Security.Cryptography;
using System.Text;
using Shortener1.DTO.Urls;
using Shortener1.Entities;
using Shortener1.Repositories;

namespace Shortener1.Services.Implementations
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
        public async Task<bool> LongUrlExist(UrlInputDto urlInputDto, string userId)
        {
            try
            {
                return await _urlMapRepository.CheckIfLongUrlExist(urlInputDto, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        // Creates new urlMap and save it in Db.
        public async Task<UrlOutputDto> CreateNewShortenedUrl(UrlInputDto urlInputDto, string userId)
        {
            var shortenedUrl = _configuration["Host"] + GenerateHash(urlInputDto.Url);
            UrlMap urlMap = new(new Random().Next(), urlInputDto.Url, shortenedUrl, DateTime.Now, userId);
            await _urlMapRepository.SaveNewUrlMap(urlMap);
            return new  UrlOutputDto (urlMap.ShortenedUrl!, urlMap.OriginalUrl!,
                                      ResponseMessages.SuccessUrlCreatedMessage, urlMap.UrlMapId);
        }


        //Returns original long url according to it`s shortened version.
        public async Task<UrlOutputDto> GetLongUrlForRedirect(UrlInputDto urlInputDto, string userId)
        {
            return await _urlMapRepository.GetLongUrlByShortenedUrl(urlInputDto, userId);
        }


        // return all urlMaps from Db.
        public async Task<List<UrlMap>> GetAll(int pageIndex, int pageSize, string userId)
        {
            return await _urlMapRepository.GetAll(pageIndex, pageSize, userId);
        }
        private static string  GenerateHash(string stringForHashing)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(stringForHashing));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString()[..10];
        }
        
    }
}