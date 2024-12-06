


using Application.Services;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Contracts;

namespace WebApi.Pages;

public class Index : PageModel
{

  
    
    public List<ChainResponce>? Chains = new List<ChainResponce>();
    
    private readonly ChainService _chainService;
    private readonly IMapper _mapper;
    
    public Index(ChainService chainService, IMapper mapper)
    {
        _chainService = chainService;
        _mapper = mapper;
    }

    [BindProperty(SupportsGet = true)] 
    public string? Search { get; set; } 
    
    public async Task OnGetAsync()
    {
        IEnumerable<Chain>? chains = null;
        if (!string.IsNullOrEmpty(Search))
            chains = await _chainService.SearchChains(Search);
        else 
            chains = await _chainService.GetChains();

        if (chains != null)
            Chains = _mapper.Map<List<ChainResponce>>(chains);
    }
    
}