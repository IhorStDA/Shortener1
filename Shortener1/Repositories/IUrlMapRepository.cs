using ConsoleApp2205.Entities;
using Shortener1.DTO;

namespace Shortener1.Repositories
{
    public interface IUrlMapRepository
    {
        public Task<bool> CheckIfLongUrlExist(UrlInputDto urlInputDto, int userId);

        public Task SaveNewUrlMap(UrlMap urlMap);

        public Task<UrlOutputDTO> GetLongUrlByShortenedUrl(UrlInputDto urlInputDto, int userId);


        public Task<List<UrlMap>> GetAll(int pageIndex , int pageSize , int userId);
    }
}