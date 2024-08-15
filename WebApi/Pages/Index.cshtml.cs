


using Application.Services;
using AutoMapper;
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


    public async Task OnGetAsync()
    {
        var chains = await _chainService.GetChains();

        if (chains != null)
            Chains = _mapper.Map<List<ChainResponce>>(chains);
    }
    
}