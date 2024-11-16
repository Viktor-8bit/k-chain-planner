




using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Services;
using WebApi.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Pages;

public class ChainByIdModel : PageModel
{
    // возвращаемое
    public ChainResponce? Chain { get; set; }
    public List<ChainStepResponce>? ChainSteps { get; set; }
    
    // входиные параметры
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    
    // сервисы 
    private readonly ChainStepService _chainStepService;
    private readonly ChainService _chainService;
    private readonly IMapper _mapper;
    
    public ChainByIdModel(ChainStepService chainStepService, ChainService chainService, IMapper mapper)
    {
        _chainStepService = chainStepService;
        _chainService = chainService;
        _mapper = mapper;
    }
    
    public async Task OnGetAsync()
    {
        if (Id == null) return;
        var chain = await _chainService.GetChainById((int)Id);
        if (chain != null)
        {
            Chain = _mapper.Map<ChainResponce>(chain);

            var chainSteps = await _chainStepService.GetChainStepsByFatherChain(chain.Id);
            if (chainSteps != null)
                ChainSteps = _mapper.Map<List<ChainStepResponce>>(chainSteps);
        }
    }
    
    [BindProperty]
    public ChainStepRequest ChainStepRequest { get; set; }
    
    // Метод для добавления нового ChainStep
    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Id == null)
            return Page();

        var result = await _chainStepService.AddChainStep((int)Id, ChainStepRequest.Title, ChainStepRequest.Description, ChainStepRequest.Start, ChainStepRequest.End);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error);
            return Page();
        }
        return Redirect($"/ChainById/{Id}");
    }

    [BindProperty] 
    public int ChainStepId { get; set; }
    public async Task<IActionResult> OnPostDeleteAsync()
    {
        if (Id == null)
        {
            ModelState.AddModelError(string.Empty, "пост не найден");
            return Page();
        }
        await _chainStepService.DeleteChainStep(ChainStepId, (int)Id);
        return Redirect($"/ChainById/{Id}");
    }
    
}