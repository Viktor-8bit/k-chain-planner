using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Contracts;

namespace WebApi.Pages;

public class GantDiagrammByChainId : PageModel
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
    
    public GantDiagrammByChainId(ChainStepService chainStepService, ChainService chainService, IMapper mapper)
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
}