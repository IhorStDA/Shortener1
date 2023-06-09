namespace Shortener1.DTO
{
    public record UrlInputDTO
    {
        public UrlInputDTO(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }

}
