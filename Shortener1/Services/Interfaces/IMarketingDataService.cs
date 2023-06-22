using Shortener1.Entities;

namespace Shortener1.Services;

public interface IMarketingDataService
{
    Task Add(MarketingData marketingData);

    Task <List<MarketingData>> GetAll(int pageIndex, int pageSize);

}