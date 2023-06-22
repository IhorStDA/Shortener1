using System.ComponentModel.DataAnnotations;

namespace Shortener1.Entities
{
    public class UrlMap
    {
        public UrlMap()
        {
        }
        public UrlMap(int urlMapId, string originalUrl, string shortenedUrl, DateTime created, string creator)
        {
            this.UrlMapId = urlMapId;
            this.OriginalUrl = originalUrl;
            this.ShortenedUrl = shortenedUrl;
            this.Created = created;
            this.CreatorId = creator;
        }

        [Key] public int UrlMapId { get; set; }
        public string? OriginalUrl { get; set; }
        public string? ShortenedUrl { get; set; }
        public DateTime? Created { get; set; }

        public string? CreatorId { get; set; }


        public override string ToString()
        {
            return $"id = {UrlMapId}, originalUrl = {OriginalUrl}, " +
                   $"shortenedUrl = {ShortenedUrl}," +
                   $" created = {Created}, creator {CreatorId}";
        }
    }
}