using Microsoft.EntityFrameworkCore;
using Shortener1.Cofigs;
using Shortener1.Entities;

namespace Shortener1.Repositories;

public class MarketingDataRepositoryImpl : IMarketingDataRepository


{
    private readonly DataContext _dataContext;


    public MarketingDataRepositoryImpl(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task<MarketingData> GetByUrlId(int urlId)
    {
        return await _dataContext.MarketingData.Where(data => data.UrlMapId == urlId).FirstOrDefaultAsync();
    }

    public async Task SaveMarketingData(MarketingData marketingData, DataContext context)
    {
        await context.MarketingData.AddAsync(marketingData);
        await context.SaveChangesAsync();
    }

    public async Task<List<MarketingData>> GetAll(int pageIndex, int pageSize)
    {
        return await _dataContext.MarketingData
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}