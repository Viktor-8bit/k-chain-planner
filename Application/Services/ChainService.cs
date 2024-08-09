
using Core.Common;
using Core.Interfaces;

using CSharpFunctionalExtensions;

namespace Application.Services;

public class ChainService(IChainRepository chainRepository, ITagRepository tagRepository)
{

    public async Task<IEnumerable<Chain>?> GetChains() => await chainRepository.GetChains();
    public async Task<Chain?> GetChainById(int chainId) => await chainRepository.GetChainById(chainId);
    
    public async Task<Result<Chain>> AddChain(string pentestObj, List<Tag> tags)
    {
        var chain = Chain.CreateChain(pentestObj, tags);
        if (chain.IsFailure) return Result.Failure<Chain>(chain.Error);
        return await chainRepository.AddChain(chain.Value);
    }

    public async Task<Result<Chain>> UpdatePentestObj(int id, string pentestObj)
    {
        var chain = await chainRepository.GetChainById(id);
        if (chain == null)
            return Result.Failure<Chain>("Chain не найден");
        chain.ChangePentestObj(pentestObj);
        await chainRepository.UpdateChain(chain);
        return Result.Success(chain);
    }

    public async Task<Result<Chain>> AddTag(int id, int tagId)
    {
        var chain = await chainRepository.GetChainById(id);
        if (chain == null) return Result.Failure<Chain>("Chain не найден");
        
        var tag = await tagRepository.GetTagById(tagId);
        if (tag == null) return Result.Failure<Chain>("Tag не найден");

        var result = chain.AddTag(tag);
        if (result.IsFailure) return Result.Failure<Chain>(result.Error);
        
        await chainRepository.UpdateChain(chain);
        return Result.Success(chain);
    }

    public async Task<Result<Chain>> RemoveTag(int id, int tagId)
    {
        var chain = await chainRepository.GetChainById(id);
        if (chain == null) return Result.Failure<Chain>("Chain не найден");
        
        var tag = await tagRepository.GetTagById(tagId);
        if (tag == null) return Result.Failure<Chain>("Tag не найден");

        var result = chain.RemoveTag(tag);
        if (result.IsFailure) return Result.Failure<Chain>(result.Error);
        
        await chainRepository.UpdateChain(chain);
        return Result.Success(chain);
    }
    
    public async Task DeleteChain(int chainId) => await chainRepository.DeleteChain(chainId);

}