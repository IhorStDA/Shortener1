namespace Shortener1.DTO.Urls
{
    public record UrlInputDto
    {
        public UrlInputDto(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
        
    }

}
