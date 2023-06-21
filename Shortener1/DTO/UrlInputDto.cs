namespace Shortener1.DTO
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
