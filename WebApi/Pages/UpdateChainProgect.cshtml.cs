using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services;
using WebApi.Contracts;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Pages;

public class UpdateChainProgect : PageModel
{

    
    // возвращаемое
    public ChainResponce? Chain { get; set; }

    
    // входиные параметры
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    
    // сервисы 
    private readonly ChainService _chainService;
    private readonly UserService _userService;
    private readonly IMapper _mapper;

    public UpdateChainProgect(ChainService chainService, UserService userService, IMapper mapper)
    {
        _chainService = chainService;
        _userService = userService;
        _mapper = mapper;
    }
    
    
    [BindProperty]
    public ChainRequest ChainRequest { get; set; }
    
    // Метод для Обновления ChainStep
    public async Task<IActionResult> OnPostUpdateAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        if (Id == null)
            return Page();

        var result = await _chainService.UpdatePentestObj((int)Id, ChainRequest!.PentestObj);    
        if (result.IsFailure)
            return Page();
        
        return Redirect($"/ChainById/{Id!}");
    }

    public async Task OnGetAsync()
    {
        if (Id == null) return;
        var chain = await _chainService.GetChainById((int)Id);
        if (chain != null)
        {
            Chain = _mapper.Map<ChainResponce>(chain);
        }
    }

}