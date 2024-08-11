


using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApi.Pages;

public class Index : PageModel
{

    private readonly ChainService _chainService;
    private readonly IMapper _mapper;
    
    public Index(ChainService chainService, IMapper mapper)
    {
        _chainService = chainService;
        _mapper = mapper;
    }


    public void OnGet()
    {
        
    }
}