using Shortener1.DTO;
using Shortener1.DTO.Urls;
using Shortener1.Entities;

namespace Shortener1.Services
{
    public interface IUrlMapService
    {
        public Task<bool> LongUrlExist(UrlInputDto urlInputDto, string userId);

        public Task<UrlOutputDto> CreateNewShortenedUrl(UrlInputDto urlInputDto, string userId);
        

        public  Task <UrlOutputDto> GetLongUrlForRedirect(UrlInputDto urlInputDto, string userId);

        public Task<List<UrlMap>> GetAll(int pageIndex ,int pageSize, string userId);

     
    }
}
