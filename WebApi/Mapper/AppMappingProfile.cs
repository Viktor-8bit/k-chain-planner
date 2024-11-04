

using System.Security.Cryptography;
using System.Text;
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
        CreateMap<ChainStep, ChainStepResponce>();
        CreateMap<ChainStepRequest, ChainStep>();
        CreateMap<ChainStepResponce, ChainStep>();
        
        // User 
        CreateMap<User, UserResponce>();
        CreateMap<UserRequest, User>()
            .ForMember(
                dest => dest.HashPassword, 
                opt => 
                    opt.MapFrom(src => HashPassword(src.NoHashPassword)));
        
        // Tag
        CreateMap<Tag, TagResponce>();
        CreateMap<TagRequest, Tag>();
    }
    
    
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}