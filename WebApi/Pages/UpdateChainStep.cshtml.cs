




using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Pages;

public class UpdateChainStep : PageModel
{

    // входиные параметры
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    public int? FatherChainId { get; set; }
    
    // сервисы 
    private readonly ChainStepService _chainStepService;
    private readonly IMapper _mapper;
    
    public UpdateChainStep(ChainStepService chainStepService, ChainService chainService, IMapper mapper)
    {
        _chainStepService = chainStepService;
        _mapper = mapper;
    }
    
    public async Task OnGet()
    {
        if (this.Id != null)
        {
            var chainStepResponce = await _chainStepService.GetChainStepById((int)this.Id);
            FatherChainId = chainStepResponce!.FatherChain.Id;
            ChainStepRequest = _mapper.Map<ChainStepResponce>(chainStepResponce!);
        }
    }
    
    // Метод для добавления нового ChainStep
    [BindProperty]
    public ChainStepResponce ChainStepRequest { get; set; }
    public async Task<IActionResult> OnPostUpdateAsync()
    {
        if (!ModelState.IsValid)
            return new JsonResult(new { message = "форма не валидна" });
        
        if (Id == null)
            return new JsonResult(new { message = "родительский chain не найден" });

        
        var chainStepResponce = await _chainStepService.GetChainStepById((int)this.Id);
        var fatherChainId = chainStepResponce!.FatherChain.Id;

        var result = await _chainStepService.UpdateChainStep((int)Id, _mapper.Map<ChainStep>(ChainStepRequest));
        if (result.IsFailure)
            return new JsonResult(new { message = result.Error });
        
        return Redirect($"/ChainById/{fatherChainId!}");
    }
    
}