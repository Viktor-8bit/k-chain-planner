




using Application.Services;
using AutoMapper;
using Core.Common;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController(TagService tagService, IMapper mapper) : ControllerBase
{
    private Random _random = new Random();
 
    [HttpGet("GetTags")]
    public async Task<IActionResult> GetTags()
    {
        var tags = await tagService.GetTags();
        if (tags == null) 
            return NotFound(); 
        var result = mapper.Map<List<TagResponce>>(tags);
        return Ok(result);
    }
    
    static T GetRandomEnumValue<T>(Random random)
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }
    
    [HttpPost("CreateTag")]
    public async Task<IActionResult> CreateTag([FromBody] TagRequest tagRequest)
    {
        var tag = mapper.Map<Tag>(tagRequest);
        tag.TagColor = GetRandomEnumValue<TagColor>(this._random);
        await tagService.CreateTag(tag);
        return Ok(tag);
    }
    
    [HttpPost("DeleteTag/{id:int}")]
    public async Task<IActionResult> DeleteTag([FromRoute] int id)
    {
        await tagService.DeleteTag(id);
        return Ok();
    }
    
    
}