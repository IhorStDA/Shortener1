using ConsoleApp2205.Cofigs;
using ConsoleApp2205.Entities;
using Shortener1.DTO;
using System.Collections.Generic;

namespace ConsoleApp2205.Repositories
{
    public class UrlMapRepositoryImpl : IUrlMapRepository
    {
        private readonly SqlLightContext _sqlLightContext;
       

        
        public UrlMapRepositoryImpl(SqlLightContext sqlLightContext)
        {
            _sqlLightContext = sqlLightContext;
        }
        

        // Checks if new long url already exist in Db.
        public  bool ChekIfLongUrlExist(UrlInputDTO urlInputDTO) 
        {
          return _sqlLightContext.UrlMap.Any(UrlMap => UrlMap.originalUrl == urlInputDTO.Url);
        }


        // Save new UrlMap to database.
        public void SaveNewUrlMap(UrlMap urlMap) 
        {
            _sqlLightContext.UrlMap.Add(urlMap);
            _sqlLightContext.SaveChanges();

        }


        // Return original long Url from db where input short url matches with short url in UrlMap table. 
        public UrlOutputDTO GetLongUrlByShortenedUrl(UrlInputDTO urlInputDTO) 
        {
            UrlMap url =  _sqlLightContext.UrlMap.Where(u => u.shortenedUrl == urlInputDTO.Url).First();
           
            return new (url.shortenedUrl,url.originalUrl, "Url was found!");
           
             
        }

        // updates shortened url for already existing UrlMaps in db. Not in use while.
        public UrlOutputDTO RegenerateShortenedUrlForAlreadyExistingOne(UrlInputDTO urlInputDTO) 
        { 
            return null;
        }

        
        // returns all UrlMaps from db.
        public List<UrlMap> GetAll()
        {
          return _sqlLightContext.UrlMap.ToList();

        }


    }
}



