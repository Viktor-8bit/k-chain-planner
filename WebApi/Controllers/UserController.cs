


using Application.Services;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserService userService, IMapper mapper) : ControllerBase
{

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.GetUsers();
        if (users == null) 
            return NotFound();
        var result = mapper.Map<List<UserResponce>>(users);
        return Ok(result);
    }
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
    {
        var user = mapper.Map<User>(userRequest);
        var result = await userService.CreateUser(user);
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
}