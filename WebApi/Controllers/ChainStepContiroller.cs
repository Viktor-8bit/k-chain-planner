


using Microsoft.AspNetCore.Mvc;
using Application.Services;
using WebApi.Contracts;
using Core.Common;
using AutoMapper;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChainStepContiroller(ChainStepService chainStepService, IMapper mapper) : ControllerBase
{
    [HttpGet("GetChainStepsByFatherChain/{fatherChainId:int}")]
    public async Task<IActionResult> GetChainStepsByFatherChain([FromRoute] int fatherChainId)
    {
        var result = await chainStepService.GetChainStepsByFatherChain(fatherChainId);
        if (result == null) 
            return NotFound();
        return Ok(mapper.Map<List<ChainStepResponce>>(result));
    }
    
    [HttpPost("AddChainStepByFatherId/{fatherChainId:int}")]
    public async Task<IActionResult> AddChainStep([FromBody] ChainStepRequest chainStepRequest, [FromRoute] int fatherChainId)
    {
        var result = await chainStepService.AddChainStep(fatherChainId, chainStepRequest.Title, chainStepRequest.Description, chainStepRequest.Start, chainStepRequest.End);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
    
    [HttpPost("UpdateChainStepById/{id:int}")]
    public async Task<IActionResult> UpdateChainStep([FromBody] ChainStepRequest chainStepRequest, [FromRoute] int id)
    {
        var result = await chainStepService.UpdateChainStep(id, mapper.Map<ChainStep>(chainStepRequest));
        if (result.IsFailure) 
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
    
    [HttpPost("DeleteChainStep/{chainStepId:int}/{fatherChainId:int}")] 
    public async Task<IActionResult> DeleteChainStep([FromRoute] int chainStepId, [FromRoute] int fatherChainId)
    {
        await chainStepService.DeleteChainStep(chainStepId, fatherChainId);
        return Ok();
    }
    
}