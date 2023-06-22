using Shortener1.Data.Context;
using Shortener1.Entities;
using Shortener1.Repositories;

namespace Shortener1.Services.Implementations;

public class MarketingDataServiceImpl : IMarketingDataService
{
    private readonly IMarketingDataRepository _marketingDataRepository;
    private readonly IServiceProvider _serviceProvider;

    public MarketingDataServiceImpl(IMarketingDataRepository marketingDataRepository, IServiceProvider serviceProvider)
    {
        _marketingDataRepository = marketingDataRepository;
        _serviceProvider = serviceProvider;
    }


    public async Task Add(MarketingData marketingData)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var repository = scope.ServiceProvider.GetRequiredService<IMarketingDataRepository>();
            await repository.SaveMarketingData(marketingData, context);
        }
    }

    public async Task<List<MarketingData>> GetAll(int pageIndex, int pageSize)
    {
        return await _marketingDataRepository.GetAll(pageIndex, pageSize);
    }
}