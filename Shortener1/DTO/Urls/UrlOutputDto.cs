namespace Shortener1.DTO.Urls
{
    public class UrlOutputDto
    {
        public string? ShortenedUrl { get; set; }
        public string? OriginalUrl { get; set; }
        public string? Description { get; set; }
        
        public int UrlMapId { get; set; }




        public UrlOutputDto(string description)
        {
            Description = description;
        }

        public UrlOutputDto(string shortenedUrl, string originalUrl, string description, int urlMapId) 
        {
            this.OriginalUrl  = originalUrl;
            this.Description  = description;
            this.ShortenedUrl = shortenedUrl;
            this.UrlMapId     = urlMapId;
        }
    }
}
