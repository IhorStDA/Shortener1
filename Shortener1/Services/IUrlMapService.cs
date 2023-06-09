using ConsoleApp2205.Entities;
using Shortener1.DTO;

namespace Shortener1.Services
{
    public interface IUrlMapService
    {
        public bool LongUrlExist(UrlInputDTO urlInputDTO);

        public UrlOutputDTO CreateNewShortenedUrl(UrlInputDTO urlInputDTO);

        public UrlOutputDTO UpdateExistingShortUrl(UrlInputDTO urlInputDTO);

        public UrlOutputDTO GetLongUrlForRedirect(UrlInputDTO urlInputDTO);

        public List<UrlMap> GetAll();
    }
}
