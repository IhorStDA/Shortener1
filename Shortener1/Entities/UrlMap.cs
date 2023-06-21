using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2205.Entities
{
    public class UrlMap
    {
        public UrlMap()
        {
        }
        public UrlMap(int urlMapId, string originalUrl, string shortenedUrl, DateTime created, int creator)
        {
            this.UrlMapId = urlMapId;
            this.OriginalUrl = originalUrl;
            this.ShortenedUrl = shortenedUrl;
            this.Created = created;
            this.CreatorId = creator;
        }

        [Key] public int UrlMapId { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int UsegCounter { get; set; }
        public DateTime Created { get; set; }

        public int CreatorId { get; set; }


        public override string ToString()
        {
            return $"id = {UrlMapId}, originalUrl = {OriginalUrl}, " +
                   $"shortenedUrl = {ShortenedUrl}, usegCounter = {UsegCounter}," +
                   $" created = {Created}, creator {CreatorId}";
        }
    }
}