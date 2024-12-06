using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services;
using WebApi.Contracts;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Pages;

public class CreateChainProgect : PageModel
{

    
    // сервисы 
    private readonly ChainService _chainService;
    private readonly UserService _userService;
    private readonly IMapper _mapper;

    public CreateChainProgect(ChainService chainService, UserService userService, IMapper mapper)
    {
        _chainService = chainService;
        _userService = userService;
        _mapper = mapper;
    }
    
    
    [BindProperty]
    public ChainRequest ChainRequest { get; set; }
    
    // Метод для добавления нового ChainStep
    public async Task<IActionResult> OnPostAddAsync()
    {
        var user = await _userService.GetUserById(ChainRequest.CreatorId);
        var result = await _chainService.CreateChain(ChainRequest.PentestObj, user!, new List<Tag>());
        
        if (result.IsFailure)
            return new JsonResult(new { message = result.Error });
        
        return Redirect("/");
    }

    public async Task OnGetAsync() { }

}