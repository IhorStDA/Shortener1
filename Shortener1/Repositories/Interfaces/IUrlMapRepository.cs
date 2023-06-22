using Shortener1.DTO;
using Shortener1.DTO.Urls;
using Shortener1.Entities;

namespace Shortener1.Repositories
{
    public interface IUrlMapRepository
    {
        public Task<bool> CheckIfLongUrlExist(UrlInputDto urlInputDto, string userId);

        public Task SaveNewUrlMap(UrlMap urlMap);

        public Task<UrlOutputDto> GetLongUrlByShortenedUrl(UrlInputDto urlInputDto, string userId);


        public Task<List<UrlMap>> GetAll(int pageIndex , int pageSize , string userId);
    }
}