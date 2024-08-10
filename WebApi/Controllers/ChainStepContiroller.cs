



using Application.Services;
using Core.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChainStepContiroller(ChainStepService chainStepService) : ControllerBase
{
    [HttpGet("GetChainStepsByFatherChain/{fatherChainId:int}")]
    public async Task<IActionResult> GetChainStepsByFatherChain([FromRoute] int fatherChainId)
    {
        var result = await chainStepService.GetChainStepsByFatherChain(fatherChainId);
        if (result == null) 
            return NotFound();
        return Ok(result);
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
        var result = await chainStepService.UpdateChainStep(id, chainStepRequest.Title, chainStepRequest.Description, chainStepRequest.Start, chainStepRequest.End);
        if (result.IsFailure) 
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
    
    [HttpPost("DeleteChainStep/{id:int}")] 
    public async Task<IActionResult> DeleteChainStep([FromRoute] int id, [FromBody] int fatherChainId)
    {
        await chainStepService.DeleteChainStep(id, fatherChainId);
        return Ok();
    }
    
}