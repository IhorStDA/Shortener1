using ConsoleApp2205.Entities;
using ConsoleApp2205.Repositories;
using Shortener1.DTO;
using System.Runtime.CompilerServices;

namespace Shortener1.Services
{
    public class UrlMapServiceImpl : IUrlMapService
    {
        private readonly IUrlMapRepository _urlMapRepository;
        
        private readonly string host = "localhost/";
        private readonly string successUrlCreatedMessage = "New shortened url was created!";
        private readonly string urlCreatedFailedMessage = "Failed to create new shortened url! See massage: ";
        private readonly string failedToFindLongUrlFromShortened = "Long url for redirect was`t found! See massage: ";

        public UrlMapServiceImpl(IUrlMapRepository urlMapRepository)
        {
            _urlMapRepository = urlMapRepository;
        }


        // Checks if original long Urks already exists in Db
        public bool LongUrlExist(UrlInputDTO urlInputDTO) {
            try
            {
                return _urlMapRepository.ChekIfLongUrlExist(urlInputDTO);
            }
            catch(Exception ex) 
            {
                return false;
            }
        }


        // Creates new urlMap and save it in Db.
        public UrlOutputDTO CreateNewShortenedUrl (UrlInputDTO urlInputDTO) 
        {
            try
            {
                var shortenedUrl = host + ShortUrlGenerator();

                UrlMap urlMap = new(new Random().Next(), urlInputDTO.Url, shortenedUrl, DateTime.Now);

                _urlMapRepository.SaveNewUrlMap(urlMap);

                return new (urlMap.shortenedUrl, urlMap.originalUrl, successUrlCreatedMessage);
           
            }
            catch (Exception ex)
            {
                return new UrlOutputDTO(urlCreatedFailedMessage + ex);
            }
        }


        // Not in use  while
        public UrlOutputDTO UpdateExistingShortUrl(UrlInputDTO urlInputDTO) { return null; }
      
        
        //Returns original long url according to it`s shortened version.
        public UrlOutputDTO GetLongUrlForRedirect(UrlInputDTO urlInputDTO) {
            try
            {
              return  _urlMapRepository.GetLongUrlByShortenedUrl(urlInputDTO);
            }
            catch (Exception ex)
            {
                return new(failedToFindLongUrlFromShortened + ex.Message);
            }

         }

        
        // return all urlMaps from Db.
        public List<UrlMap> GetAll()
        {
            try
            {
                return _urlMapRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<UrlMap>(); 
            }
        } 


        // Generates short url 
        private string ShortUrlGenerator() 
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
            return randomNumber.ToString();
        }

        

    }
}
