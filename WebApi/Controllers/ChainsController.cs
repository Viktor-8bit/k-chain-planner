


using Microsoft.AspNetCore.Mvc;
using Application.Services;
using AutoMapper;
using WebApi.Contracts;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChainsController(ChainService chainsService, UserService userService, IMapper mapper) : ControllerBase
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

    [HttpGet("GetChainById/{chainId:int}")]
    public async Task<IActionResult> GetChainById([FromRoute] int chainId)
    {
        var chain = await chainsService.GetChainById(chainId);
        if (chain == null) 
            return NotFound();
        return Ok(mapper.Map<ChainResponce>(chain));
    }

    [HttpPost("CreateChain")]
    public async Task<IActionResult> CreateChain([FromBody] ChainRequest chainRequest)
    {
        var user = await userService.GetUserById(chainRequest.CreatorId);
        
        if (user == null) 
            return BadRequest("Пользователь не найден");
        
        var result = await chainsService.CreateChain(chainRequest.PentestObj, user, chainRequest.Tags);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }
    
    [HttpPost("UpdatePentestObj/{chainId:int}")]
    public async Task<IActionResult> UpdatePentestObj([FromRoute] int chainId, [FromBody] string newPentestObj )
    {
        var result = await chainsService.UpdatePentestObj(chainId, newPentestObj);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPost("AddTag/{chainId:int}/{tagId:int}")]
    public async Task<IActionResult> AddTag([FromRoute] int chainId, [FromRoute] int tagId)
    {
        var result = await chainsService.AddTag(chainId, tagId);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        return Ok();
    }

    
    [HttpPost("RemoveTag/{chainId:int}/{tagId:int}")]
    public async Task<IActionResult> RemoveTag([FromRoute] int chainId, [FromRoute] int tagId)
    {
        var result = await chainsService.RemoveTag(chainId, tagId);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        return Ok();
    }
    
    [HttpPost("DeleteChain/{id:int}")]
    public async Task DeleteChain([FromRoute] int id) => await chainsService.DeleteChain(id);

    
}