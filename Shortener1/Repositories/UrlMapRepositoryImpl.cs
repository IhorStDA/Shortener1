using ConsoleApp2205.Entities;
using Microsoft.EntityFrameworkCore;
using Shortener1.Cofigs;
using Shortener1.DTO;

namespace Shortener1.Repositories
{
    public class UrlMapRepositoryImpl : IUrlMapRepository
    {
        private readonly DataContext _dataContext;


        public UrlMapRepositoryImpl(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // Checks if new long url already exist in Db.
        public async Task<bool> CheckIfLongUrlExist(UrlInputDto urlInputDto, int userId)
        {
            return await _dataContext
                                .UrlMap
                                .AnyAsync(UrlMap => UrlMap.OriginalUrl == urlInputDto.Url);
        }


        // Save new UrlMap to database.
        public async Task SaveNewUrlMap(UrlMap urlMap)
        {
            await _dataContext.UrlMap.AddAsync(urlMap);
            await _dataContext.SaveChangesAsync();
        }


        // Return original long Url from db where input short url matches with short url in UrlMap table. 
        public async Task<UrlOutputDTO> GetLongUrlByShortenedUrl(UrlInputDto urlInputDto, int userId)
        {
            UrlMap? url = await _dataContext
                                            .UrlMap
                                            .Where(u => u.ShortenedUrl == urlInputDto.Url
                                                              && u.CreatorId == userId)
                                            .FirstOrDefaultAsync();

            return new UrlOutputDTO(url.ShortenedUrl, url.OriginalUrl, "Url was found!", url.UrlMapId);
        }

      
       


        // returns all UrlMaps from db.
        public async Task<List<UrlMap>> GetAll(int pageIndex , int pageSize , int userId)
        {
            return await _dataContext
                .UrlMap
                .Where(u=>u.CreatorId   == userId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}