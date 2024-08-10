using Core.Common;
using CSharpFunctionalExtensions;

namespace Core.Interfaces;

public interface IChainRepository
{
    Task<IEnumerable<Chain>?> GetChains();
    Task<Chain?> GetChainById(int chainId);
    Task<Chain> CreateChain(Chain chain);
    Task DeleteChain(int chainId);
    Task<Result<Chain?>> UpdatePentestObj(int chainId, string pentestObj);
    Task<Result<Chain?>> AddTag(int chainId, int tagId);
    Task<Result<Chain?>> RemoveTag(int chainId, int tagId);
}