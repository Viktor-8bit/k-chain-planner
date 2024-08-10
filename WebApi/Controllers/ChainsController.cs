


using Microsoft.AspNetCore.Mvc;
using Application.Services;
using AutoMapper;
using WebApi.Contracts;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChainsController(ChainService chainsService, IMapper mapper) : ControllerBase
{

    // public TimeZoneInfo KraskTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Krasnoyarsk Standard Time");

    
    [HttpGet("GetChains")]
    public async Task<IActionResult> GetChains()
    {
        var chains = await chainsService.GetChains();
        if (chains == null) 
            return NotFound();
        var result = mapper.Map<List<ChainResponce>>(chains);
        return Ok(result);
    }

    [HttpGet("GetChainById/{id:int}")]
    public async Task<IActionResult> GetChainById([FromRoute] int id)
    {
        var chain = await chainsService.GetChainById(id);
        if (chain == null) 
            return NotFound();
        
        return Ok(chain);
    }

    [HttpPost("AddChain")]
    public async Task<IActionResult> AddChain([FromBody] ChainRequest chainRequest)
    {
        var result = await chainsService.AddChain(chainRequest.PentestObj, chainRequest.Tags);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }

}