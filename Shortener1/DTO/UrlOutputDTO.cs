namespace Shortener1.DTO
{
    public class UrlOutputDTO
    {
        public string ShortenedUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string Description { get; set; }
        
        public int UrlMapId { get; set; }




        public UrlOutputDTO(string description)
        {
            Description = description;
        }

        public UrlOutputDTO(string shortenedUrl, string originalUrl, string description, int urlMapId) 
        {
            this.OriginalUrl  = originalUrl;
            this.Description  = description;
            this.ShortenedUrl = shortenedUrl;
            this.UrlMapId     = urlMapId;
        }
    }
}
