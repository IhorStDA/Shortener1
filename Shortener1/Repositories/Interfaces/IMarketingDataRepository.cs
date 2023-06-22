using Shortener1.Data.Context;
using Shortener1.Entities;

namespace Shortener1.Repositories;

public interface IMarketingDataRepository
{
    public Task<MarketingData> GetByUrlId(int urlId);
    public Task SaveMarketingData(MarketingData marketingData,DataContext context);

    public Task<List<MarketingData>> GetAll(int pageIndex, int pageSize);
}