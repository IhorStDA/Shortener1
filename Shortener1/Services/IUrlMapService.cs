using ConsoleApp2205.Entities;
using Shortener1.DTO;

namespace Shortener1.Services
{
    public interface IUrlMapService
    {
        public Task<bool> LongUrlExist(UrlInputDto urlInputDto, int userId);

        public Task<UrlOutputDTO> CreateNewShortenedUrl(UrlInputDto urlInputDto, int userId);
        

        public  Task <UrlOutputDTO> GetLongUrlForRedirect(UrlInputDto urlInputDto, int userId);

        public Task<List<UrlMap>> GetAll(int pageIndex ,int pageSize, int userId);

     
    }
}
