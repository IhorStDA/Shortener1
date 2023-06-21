using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener1.Entities;
using Shortener1.Services;

namespace Shortener1.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketingController : ControllerBase
{
    private readonly IMarketingDataService _marketingDataService;

    public MarketingController(IMarketingDataService marketingDataService)
    {
        _marketingDataService = marketingDataService;
    }

   
    [Authorize]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<MarketingData>))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
    {
        var marketingData = await _marketingDataService.GetAll(pageIndex, pageSize);
        
        return Ok(marketingData);
    }
    
}