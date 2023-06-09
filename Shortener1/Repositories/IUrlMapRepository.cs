using ConsoleApp2205.Entities;
using Shortener1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2205.Repositories
{
    public interface IUrlMapRepository
    {
        public bool ChekIfLongUrlExist(UrlInputDTO urlInputDTO);

        public void SaveNewUrlMap(UrlMap urlMap);

        public UrlOutputDTO GetLongUrlByShortenedUrl(UrlInputDTO urlInputDTO);

        public UrlOutputDTO RegenerateShortenedUrlForAlreadyExistingOne(UrlInputDTO urlInputDTO);


        public List<UrlMap> GetAll();

    }
}   
