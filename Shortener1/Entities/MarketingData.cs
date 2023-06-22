using System.ComponentModel.DataAnnotations;

namespace Shortener1.Entities;

public class MarketingData
{
    public MarketingData(string userAgent, string userLanguage, string ipAddress, int urlMapId)
    {
        UserAgent = userAgent;
        UserLanguage = userLanguage;
        IpAddress = ipAddress;
        UrlMapId = urlMapId;
    }

    private MarketingData()
    {
    }


    [Key] public int MarketingDataId { get; set; }

    public int UrlMapId { get; set; }
    public string? UserAgent { get; set; }
    public string? UserLanguage { get; set; }
    public string? IpAddress { get; set; }

    public override string ToString()
    {
        return $"{UserAgent},{UserLanguage},{IpAddress},{UrlMapId}";
    }
}