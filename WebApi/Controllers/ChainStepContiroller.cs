



using Application.Services;
using Core.Common;
using Microsoft.AspNetCore.Mvc;

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
    
    
    
}