


using Microsoft.AspNetCore.Mvc;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpPost("CreateChain")]
    public async Task<IActionResult> CreateChain([FromBody] ChainRequest chainRequest)
    {
        var result = await chainsService.CreateChain(chainRequest.PentestObj, chainRequest.Tags);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
    
    [HttpPost("UpdatePentestObj/{id:int}")]
    public async Task<IActionResult> UpdatePentestObj([FromRoute] int id, [FromBody] string newPentestObj )
    {
        await chainsService.UpdatePentestObj(id, newPentestObj);

        return Ok();
    }

    [HttpPost("AddTag/{id:int}/{tagId:int}")]
    public async Task<IActionResult> AddTag([FromRoute] int id, [FromRoute] int tagId)
    {
        await chainsService.AddTag(id, tagId);
        return Ok();
    }

    
    [HttpPost("RemoveTag/{id:int}/{tagId:int}")]
    public async Task<IActionResult> RemoveTag([FromRoute] int id, [FromRoute] int tagId)
    {
        await chainsService.RemoveTag(id, tagId);
        return Ok();
    }
    
    [HttpPost("DeleteChain/{id:int}")]
    public async Task DeleteChain([FromRoute] int id) => await chainsService.DeleteChain(id);

    
}