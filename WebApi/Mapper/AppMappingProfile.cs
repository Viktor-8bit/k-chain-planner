

using Core.Common;
using WebApi.Contracts;

using AutoMapper;

namespace WebApi.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // Chain
        CreateMap<Chain, ChainResponce>();
        CreateMap<ChainRequest, Chain>();
        
        // ChainStep
        CreateMap<ChainStep, ChainStepRequest>();
        CreateMap<ChainStepRequest, ChainStep>();
    }
}