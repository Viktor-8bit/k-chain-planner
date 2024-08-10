


using Core.Common;
using Core.Interfaces;
using CSharpFunctionalExtensions;

namespace Application.Services;

public class ChainService(IChainRepository chainRepository, ITagRepository tagRepository)
{

    public async Task<IEnumerable<Chain>?> GetChains() => 
        await chainRepository.GetChains();
    
    public async Task<Chain?> GetChainById(int chainId) => 
        await chainRepository.GetChainById(chainId);
    
    public async Task<Result<Chain>> CreateChain(string pentestObj, List<Tag> tags)
    {
        var chain = Chain.CreateChain(pentestObj);
        if (chain.IsFailure) return Result.Failure<Chain>(chain.Error);

        foreach (var t in tags)
        {
            var result = chain.Value.AddTag(t);
        }
        
        return await chainRepository.CreateChain(chain.Value);
    }

    public async Task UpdatePentestObj(int id, string pentestObj) =>
        await chainRepository.UpdatePentestObj(id, pentestObj);


    public async Task AddTag(int id, int tagId) =>
        await chainRepository.AddTag(id, tagId);

    public async Task RemoveTag(int id, int tagId) =>
        await chainRepository.RemoveTag(id, tagId);
    
    public async Task DeleteChain(int chainId) => 
        await chainRepository.DeleteChain(chainId);

}